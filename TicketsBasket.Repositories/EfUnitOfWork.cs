using System.Threading.Tasks;
using TicketsBasket.Models.Data;

namespace TicketsBasket.Repositories
{
  public class EfUnitOfWork : IUnitOfWork
  {
    private readonly TicketsBasketDbContext _db;
    private IUserProfilesRepository _userProfiles;
    
    public EfUnitOfWork(TicketsBasketDbContext db)
    {
      _db = db;
    }


    public IUserProfilesRepository UserProfiles
    {
      get
      {
        _userProfiles ??= new UserProfilesRepository(_db);
        return _userProfiles;
      }
    }

    public async Task SaveChangesAsync()
    {
      await _db.SaveChangesAsync();
    }
  }
}