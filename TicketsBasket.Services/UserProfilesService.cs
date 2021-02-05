using System.Threading.Tasks;
using TicketsBasket.Infrastructure.Options;
using TicketsBasket.Shared.Dtos;
using TicketsBasket.Shared.Responses;

namespace TicketsBasket.Services
{
  public class UserProfilesService : IUserProfilesService
  {
    private readonly IdentityOptions _identityOptions;

    public UserProfilesService(IdentityOptions identityOptions)
    {
      _identityOptions = identityOptions;
    }

    public Task<OperationResponse<UserProfileDto>> GetUserProfileByUserIdAsync()
    {
      throw new System.NotImplementedException();
    }
  }
}