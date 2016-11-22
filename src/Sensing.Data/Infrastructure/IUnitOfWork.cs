
using System.Threading.Tasks;

namespace Sensing.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        SensingSiteDbContext DataContext { get; }
        void Commit();

        void CommitAsync();
    }
}
