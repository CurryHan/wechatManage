using Sensing.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Globalization;
using Sensing.Entities.Versions;

namespace Sensing.Data.Repository
{

    public interface ISoftwareUpdateRepository : IRepository<SoftwareClientDetails>
    {
        IList<SoftwareClientDetails> GetUpperVersionByIdAsync(int id);
        Task<IList<SoftwareClientDetails>> GetAllAsync();
    }
    public class SoftwareUpdateRepository : RepositoryBase<SoftwareClientDetails>, ISoftwareUpdateRepository
    {

        public SoftwareUpdateRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public async Task<IList<SoftwareClientDetails>> GetAllAsync()
        {
            var details = dbset.Where(info => !info.Deleted)
            .OrderBy(info => info.Id)
            .ToList();
            return details;
        }

        public override SoftwareClientDetails GetById(string id)
        {
            int result;
            if (int.TryParse(id,out result))
            {
                return GetById(result);
            }
            return null;
           
        }

        public SoftwareClientDetails GetById(int id)
        {
            return dbset.FirstOrDefault(client => client.Id == id && !client.Deleted);
        }

        public IList<SoftwareClientDetails> GetUpperVersionByIdAsync(int id)
        {
            var upperClients = dbset.Where(client => !client.Deleted && client.Id > id)
                .OrderBy(client => client.Id).ToList();
            return upperClients;
        }
    }
}
