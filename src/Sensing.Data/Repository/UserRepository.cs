using Sensing.Data.Infrastructure;
using Sensing.Entities.Users;
using System;
using System.Linq.Expressions;
namespace Sensing.Data.Repository
{
    public class UserRepository: RepositoryBase<ApplicationUser>, IUserRepository
        {
        public UserRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {
            }        
        }
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        
    }
}