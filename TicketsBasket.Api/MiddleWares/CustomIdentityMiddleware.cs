using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TicketsBasket.Services;

namespace TicketsBasket.Api.MiddleWares
{
  public class CustomIdentityMiddleware
  {
    private readonly RequestDelegate _requestDelegate;

    public CustomIdentityMiddleware(RequestDelegate requestDelegate)
    {
      _requestDelegate = requestDelegate;
    }

    public async Task InvokeAsync(HttpContext context, IUserProfilesService userProfilesService)
    {
        if (context.User.Identity.IsAuthenticated)
        {
          var userProfile = await userProfilesService.GetByUserIdAsync();
          if (userProfile.Record != null)
          {
              var role = userProfile.Record.IsOrganizer ? "organizer" : "user";
              context.User.AddIdentity(new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.Role, role ) 
              }));
          }
        }
        await _requestDelegate(context);
    }

  }
}