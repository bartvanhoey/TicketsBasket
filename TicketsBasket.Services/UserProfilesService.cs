using System.Threading.Tasks;
using TicketsBasket.Infrastructure.Options;
using TicketsBasket.Models.Mapper;
using TicketsBasket.Repositories;
using TicketsBasket.Shared.Dtos;
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

    public async Task<OperationResponse<UserProfileDto>> GetUserProfileByUserIdAsync()
    {
      var userProfile = await _uow.UserProfiles.GetByUserIdAsync(_identity.UserId);

      if (userProfile == null) return Error<UserProfileDto>("Profile not found", null);
      return Success<UserProfileDto>("Profile retrieved successfully", userProfile.ToUserProfileDto());      
    }
  }
}