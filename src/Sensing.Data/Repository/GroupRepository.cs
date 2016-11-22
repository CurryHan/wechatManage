using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Globalization;
using Sensing.Entities.Versions;
using Sensing.Entities;
using Sensing.Data.Infrastructure;
using System.Data.Entity;

namespace Sensing.Data.Repository
{

    public interface IGroupRepository : IRepository<Group>
    {
        Group GetInclude(Group group);
        IList<Group> GetAllGroupsUnderIdAsync(int id);
    }
    public class GroupRepository : RepositoryBase<Group>, IGroupRepository
    {

        public GroupRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public IList<Group> GetAllGroupsUnderIdAsync(int id)
        {
            var details = dbset.Where(info => !info.Deleted)
            .OrderBy(info => info.Id).ToList();
            return details;
        }

       
        public Group GetInclude(Group group)
        {
            return dbset.Where(g => g.Deleted == false && g.Id == group.Id).Include(g => g.Children).Include(g => g.ParentGroup).FirstOrDefault();
        }
    }
}
