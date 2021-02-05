using System.Threading.Tasks;
using TicketsBasket.Shared.Dtos;
using TicketsBasket.Shared.Responses;

namespace TicketsBasket.Services
{
  public class UserProfilesService : IUserProfilesService
  {
    public Task<OperationResponse<UserProfileDto>> GetUserProfileByUserIdAsync()
    {
      throw new System.NotImplementedException();
    }
  }
}