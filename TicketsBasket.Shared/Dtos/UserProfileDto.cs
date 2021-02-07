namespace TicketsBasket.Shared.Dtos
{
  public class UserProfileDto
  {
    public string UserId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
    
    public string ProfilePictureUrl { get; set; }

    public string FullName => $"{FirstName} {LastName}";

    public string Email { get; set; }

    public string Country { get; set; }

    public string City { get; set; }

    public bool IsOrganizer { get; set; }

    public string CreatedSince { get; set; }

  }
}