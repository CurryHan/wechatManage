using Sensing.Data.Infrastructure;
using Sensing.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensingSite.Data.Repository
{
    public interface IUserActivityRepository : IRepository<UserLog>
    {

    }
    public class UserActivityRepository : RepositoryBase<UserLog>, IUserActivityRepository
    {
        public UserActivityRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
