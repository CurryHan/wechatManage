using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sensing.Data.Infrastructure;
using Sensing.Entities.SystemSettings;

namespace Sensing.Data.Repository
{
    public interface ISystemSettingRepository : IRepository<ApproveProcess>
    {
        bool NeedApprove(string name);
    }
    public class SystemSettingRepository : RepositoryBase<ApproveProcess>, ISystemSettingRepository
    {
        public SystemSettingRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public bool NeedApprove(string name)
        {
            var setting = dbset.Where(a => a.Name == name).FirstOrDefault();
            if (setting != null)
                return setting.NeedApprove;
            else
                return true;
        }
    }
}
