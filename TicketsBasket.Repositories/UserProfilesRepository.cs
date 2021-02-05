using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketsBasket.Models.Data;
using TicketsBasket.Models.Domain;

namespace TicketsBasket.Repositories
{
  public class UserProfilesRepository : IUserProfilesRepository
  {
    private readonly TicketsBasketDbContext _db;

    public UserProfilesRepository(TicketsBasketDbContext db)
    {
      _db = db;
    }

    public async Task CreateAsync(UserProfile userProfile)
    {
      await _db.UserProfiles.AddAsync(userProfile);
    }

    public async Task<UserProfile> GetByIdAsync(string id)
    {
      return await _db.UserProfiles.FindAsync(id);
    }

    public async Task<UserProfile> GetByUserIdAsync(string id)
    {
      return await _db.UserProfiles.SingleOrDefaultAsync(up => up.UserId == id);
    }

    public void Remove(UserProfile userProfile)
    {
      _db.UserProfiles.Remove(userProfile);
    }

    public void Update(UserProfile userProfile)
    {
      _db.Entry(userProfile).State = EntityState.Modified;
    }
  }
}