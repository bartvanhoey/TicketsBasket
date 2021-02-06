using Microsoft.AspNetCore.Http;

namespace TicketsBasket.Shared.Requests
{
    public class CreateUserProfileRequest
    {
        public bool IsOrganizer { get; set; }
        public IFormFile ProfilePicture { get; set; }
    }
}