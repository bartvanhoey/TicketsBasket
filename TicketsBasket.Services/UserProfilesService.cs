using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TicketsBasket.Infrastructure.Options;
using TicketsBasket.Models.Domain;
using TicketsBasket.Models.Mapper;
using TicketsBasket.Repositories;
using TicketsBasket.Services.Storage;
using TicketsBasket.Shared.Dtos;
using TicketsBasket.Shared.Requests;
using TicketsBasket.Shared.Responses;

namespace TicketsBasket.Services
{
  public class UserProfilesService : BaseService, IUserProfilesService
  {
    private readonly IdentityOptions _identity;
    private readonly IUnitOfWork _uow;
    private readonly IStorageService _storageService;

    public UserProfilesService(IdentityOptions identity, IUnitOfWork unitOfWork, IStorageService storageService)
    {
      _identity = identity;
      _uow = unitOfWork;
      _storageService = storageService;
    }

    public async Task<OperationResponse<UserProfileDto>> CreateAsync(CreateUserProfileRequest createUserProfile)
    {
      var user = _identity.User;
      var city = user.FindFirst("city").Value;
      var country = user.FindFirst("country").Value;
      var firstName = user.FindFirst(ClaimTypes.GivenName).Value;
      var lastName = user.FindFirst(ClaimTypes.Surname).Value;
      var email = user.FindFirst("emails").Value;
      var displayName = user.FindFirst("name").Value;

      string profilePictureUrl = "unknown";


      var userProfile = new UserProfile
      {
        Country = country,
        City = city,
        CreatedOn = DateTime.UtcNow,
        FirstName = firstName,
        LastName = lastName,
        Id = Guid.NewGuid().ToString(),
        UserId = _identity.UserId,
        IsOrganizer = createUserProfile.IsOrganizer,
        ProfilePictureUrl = profilePictureUrl,
        Email = email
      };

      await _uow.UserProfiles.CreateAsync(userProfile);
      await _uow.SaveChangesAsync();

      return Success("Userprofile created successfully", userProfile.ToUserProfileDto());
    }

    public async Task<OperationResponse<UserProfileDto>> GetByUserIdAsync()
    {
      var userProfile = await _uow.UserProfiles.GetByUserIdAsync(_identity.UserId);

      if (userProfile == null) return Error<UserProfileDto>("Profile not found", null);
      return Success<UserProfileDto>("Profile retrieved successfully", userProfile.ToUserProfileDto());
    }

    public async Task<OperationResponse<UserProfileDto>> UpdateProfilePictureAsync(IFormFile file)
    {
      var userProfile = await _uow.UserProfiles.GetByUserIdAsync(_identity.UserId);
      if (userProfile == null) return Error<UserProfileDto>("Profile not found", null);
      var profilePictureUrl = userProfile.ProfilePictureUrl;

      try
      {
          profilePictureUrl = await _storageService.SaveBlobAsync("users", file, BlobType.Image);
          if (userProfile.ProfilePictureUrl != "unknown"  )
          {
              await _storageService.DeleteIfExistsAsync("users", userProfile.ProfilePictureUrl);
          }
          if (string.IsNullOrWhiteSpace(profilePictureUrl)) return Error<UserProfileDto>("Image is required", userProfile.ToUserProfileDto());
      }
      catch (Exception)
      {
        return Error("Invalid image file", userProfile.ToUserProfileDto());
      }

      userProfile.ProfilePictureUrl = profilePictureUrl;

      await _uow.SaveChangesAsync();
      return Success("Image saved successfully", userProfile.ToUserProfileDto());
    }
  }
}