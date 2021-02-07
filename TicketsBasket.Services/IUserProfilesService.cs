using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TicketsBasket.Shared.Dtos;
using TicketsBasket.Shared.Requests;
using TicketsBasket.Shared.Responses;

namespace TicketsBasket.Services
{
  public interface IUserProfilesService
  {
    Task<OperationResponse<UserProfileDto>> GetByUserIdAsync();
    Task<OperationResponse<UserProfileDto>> CreateAsync(CreateUserProfileRequest createUserProfile);
    Task<OperationResponse<UserProfileDto>> UpdateProfilePictureAsync(IFormFile file);
    
  }
}