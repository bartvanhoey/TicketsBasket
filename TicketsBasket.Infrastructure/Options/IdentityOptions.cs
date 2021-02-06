using System.Security.Claims;

namespace TicketsBasket.Infrastructure.Options
{
    public class IdentityOptions
    {
        public string UserId => User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        public ClaimsPrincipal User { get; set; }
        

    }
}