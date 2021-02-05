using System.Threading.Tasks;
using TicketsBasket.Shared.Dtos;
using TicketsBasket.Shared.Responses;

namespace TicketsBasket.Services
{
  public interface IUserProfilesService
    {
         Task<OperationResponse<UserProfileDto>> GetUserProfileByUserIdAsync();
    }
}