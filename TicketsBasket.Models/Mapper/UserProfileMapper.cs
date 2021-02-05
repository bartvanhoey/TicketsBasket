using TicketsBasket.Models.Domain;
using TicketsBasket.Shared.Dtos;

namespace TicketsBasket.Models.Mapper
{
  public static class UserProfileMapper
  {
    public static UserProfileDto ToUserProfileDto(this UserProfile userProfile)
    {
      return new UserProfileDto
      {
        UserId = userProfile.UserId,
        FirstName = userProfile.FirstName,
        LastName = userProfile.LastName,
        ProfilePicture = userProfile.ProfilePicture,
        Email = userProfile.Email,
        Country = userProfile.Country,
        City = userProfile.City,
        IsOrganizer = userProfile.IsOrganizer,
        CreatedSince = "1m"
      };
    }
  }
}