using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sensing.Data.Infrastructure;
using Sensing.Entities.SystemSettings;

namespace Sensing.Data.Repository
{
    public interface IPlatformNotificationRepository : IRepository<PlatformNotification>
    {
        PlatformNotification GetPlatformNotification();
    }

    public class PlatformNotificationRepository : RepositoryBase<PlatformNotification>, IPlatformNotificationRepository
    {
        public PlatformNotificationRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public PlatformNotification GetPlatformNotification()
        {
           return dbset.Where(p => p.Deleted == false && p.IsUsing == true).FirstOrDefault();
        }
    }
}
