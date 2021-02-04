using Microsoft.EntityFrameworkCore;
using TicketsBasket.Models.Domain;

namespace TicketsBasket.Models.Data
{
  public class TicketsBasketDbContext : DbContext
  {
    public TicketsBasketDbContext(DbContextOptions<TicketsBasketDbContext> options)
        : base(options)
    {

    }

    public DbSet<Event> Events { get; set; }
    public DbSet<EventImage> EventImages { get; set; }
    public DbSet<EventTag> EventTags { get; set; }
    public DbSet<JobApplication> JobApplications { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<WishListEvent> WishListEvents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<UserProfile>()
        .HasMany(x => x.Events)
        .WithOne(x => x.UserProfile)
        .OnDelete(DeleteBehavior.NoAction);

      modelBuilder.Entity<UserProfile>()
        .HasMany(x => x.WishListEvents)
        .WithOne(x => x.UserProfile)
        .OnDelete(DeleteBehavior.NoAction);

      modelBuilder.Entity<UserProfile>()
        .HasMany(x => x.Likes)
        .WithOne(x => x.UserProfile)
        .OnDelete(DeleteBehavior.NoAction);

      modelBuilder.Entity<UserProfile>()
        .HasMany(x => x.Tickets)
        .WithOne(x => x.UserProfile)
        .OnDelete(DeleteBehavior.NoAction);


      modelBuilder.Entity<UserProfile>()
        .HasMany(x => x.SentApplications)
        .WithOne(x => x.AppliedUser)
        .OnDelete(DeleteBehavior.NoAction);

      modelBuilder.Entity<UserProfile>()
        .HasMany(x => x.ReceivedApplications)
        .WithOne(x => x.Organizer)
        .OnDelete(DeleteBehavior.NoAction);

      modelBuilder.Entity<Event>()
        .HasMany(x => x.EventTags)
        .WithOne(x => x.Event)
        .OnDelete(DeleteBehavior.NoAction);

      modelBuilder.Entity<Event>()
        .HasMany(x => x.EventImages)
        .WithOne(x => x.Event)
        .OnDelete(DeleteBehavior.NoAction);

      modelBuilder.Entity<Event>()
        .HasMany(x => x.Likes)
        .WithOne(x => x.Event)
        .OnDelete(DeleteBehavior.NoAction);

      modelBuilder.Entity<Event>()
        .HasMany(x => x.Tickets)
        .WithOne(x => x.Event)
        .OnDelete(DeleteBehavior.NoAction);

      modelBuilder.Entity<Event>()
        .HasMany(x => x.WishListEvents)
        .WithOne(x => x.Event)
        .OnDelete(DeleteBehavior.NoAction);

      base.OnModelCreating(modelBuilder);
    }



  }
}