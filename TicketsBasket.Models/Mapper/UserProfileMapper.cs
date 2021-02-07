using TicketsBasket.Models.Domain;
using TicketsBasket.Shared.Dtos;
using TicketsBasket.Infrastructure.Utilities;
using System;

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
        ProfilePictureUrl = userProfile.ProfilePictureUrl,
        Email = userProfile.Email,
        Country = userProfile.Country,
        City = userProfile.City,
        IsOrganizer = userProfile.IsOrganizer,
        CreatedSince = DateTimeUtilities.GetPassedTime(DateTime.UtcNow, userProfile.CreatedOn)
      };
    }
  }
}