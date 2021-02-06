using System;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using TicketsBasket.Infrastructure.Options;
using TicketsBasket.Models.Domain;
using TicketsBasket.Models.Mapper;
using TicketsBasket.Repositories;
using TicketsBasket.Shared.Dtos;
using TicketsBasket.Shared.Requests;
using TicketsBasket.Shared.Responses;

namespace TicketsBasket.Services
{
  public class UserProfilesService : BaseService, IUserProfilesService
  {
    private readonly IdentityOptions _identity;
    private readonly IUnitOfWork _uow;

    public UserProfilesService(IdentityOptions identity, IUnitOfWork unitOfWork)
    {
      _identity = identity;
      _uow = unitOfWork;
    }

    public async Task<OperationResponse<UserProfileDto>> CreateUserProfileAsync(CreateUserProfileRequest createUserProfile)
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
        ProfilePicture = profilePictureUrl,
        Email = email
      };

      await _uow.UserProfiles.CreateAsync(userProfile);
      await _uow.SaveChangesAsync();

      return Success("Userprofile created successfully", userProfile.ToUserProfileDto());
    }

    public async Task<OperationResponse<UserProfileDto>> GetUserProfileByUserIdAsync()
    {
      var userProfile = await _uow.UserProfiles.GetByUserIdAsync(_identity.UserId);

      if (userProfile == null) return Error<UserProfileDto>("Profile not found", null);
      return Success<UserProfileDto>("Profile retrieved successfully", userProfile.ToUserProfileDto());
    }
  }
}