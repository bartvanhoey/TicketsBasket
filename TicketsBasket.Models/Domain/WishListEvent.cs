using System;

namespace TicketsBasket.Models.Domain
{
  public class WishListEvent : Record
  {
    public DateTime CreatedOn { get; set; }

    public WishListEvent()
    {
      CreatedOn = DateTime.UtcNow;
    }

    public virtual Event Event { get; set; }
    public string EventId { get; set; }

    public virtual UserProfile UserProfile { get; set; }
    public virtual string UserProfileId { get; set; }
  }
}