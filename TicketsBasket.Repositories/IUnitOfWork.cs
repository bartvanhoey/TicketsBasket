using System.Threading.Tasks;

namespace TicketsBasket.Repositories
{
    public interface IUnitOfWork
    {
         IUserProfilesRepository UserProfiles { get; }

         Task SaveChangesAsync();
    }
}