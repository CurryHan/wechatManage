using Sensing.Data.Infrastructure;
using Sensing.Data.Repository;
using Sensing.Entities.Entity;
using Sensing.Entities.Users;
using SensingSite.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensingCloud.Services
{
    public interface IUserService
    {
        ApplicationUser GetUser(string userId);
        IEnumerable<ApplicationUser> GetUsers();
        IEnumerable<ApplicationUser> GetUsers(string username);
        ApplicationUser GetUserProfile(string userid);
        ApplicationUser GetUsersByEmail(string email);
        IEnumerable<ApplicationUser> GetUserByUserId(IEnumerable<string> userid);
        IEnumerable<ApplicationUser> SearchUser(string searchString);

        void UpdateUser(ApplicationUser user);
        void SaveUser();
        void EditUser(string id, string firstname, string lastname, string email);

        void SaveImageURL(string userId, string imageUrl);

        void AddUserActivity(string userId, string log);
        IEnumerable<UserLog> GetAllUserActivity();
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserActivityRepository userActivityRepository;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork,  IUserActivityRepository userActivityRepository)
        {
            this.userRepository = userRepository;

            this.unitOfWork = unitOfWork;
            this.userActivityRepository = userActivityRepository;
        }
        public ApplicationUser GetUserProfile(string userid)
        {
            var userprofile = userRepository.Get(u => u.Id == userid);
            return userprofile;
        }


        #region IUserService Members

        public ApplicationUser GetUser(string userId)
        {
            return userRepository.Get(u => u.Id == userId);
        }

        public IEnumerable<ApplicationUser> GetUsers()
        {
            var users = userRepository.GetAll();
            return users;
        }
        public ApplicationUser GetUsersByEmail(string email)
        {
            var users = userRepository.Get(u => u.Email.Contains(email));
            return users;

        }

        public IEnumerable<ApplicationUser> GetInvitedUsers(string username, int groupId)
        {
            return null;
        }

        public void SaveImageURL(string userId, string imageUrl)
        {
            var user = GetUser(userId);
            user.AvatarUrl = imageUrl;
            UpdateUser(user);
        }
        public void EditUser(string id, string firstname, string lastname, string email)
        {
            var user = GetUser(id);
            user.Email = email;
            
            UpdateUser(user);
        }
        public void UpdateUser(ApplicationUser user)
        {
            userRepository.Update(user);
            SaveUser();
        }


        public void SaveUser()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<ApplicationUser> GetUserByUserId(IEnumerable<string> userid)
        {
            List<ApplicationUser> users = new List<ApplicationUser> { };
            foreach (string item in userid)
            {
                var Users = userRepository.GetById(item);
                users.Add(Users);

            }
            return users;
        }

        #endregion


        public IEnumerable<ApplicationUser> GetUsers(string username)
        {
            return userRepository.GetMany(p => p.UserName == username);
        }

        public IEnumerable<ApplicationUser> SearchUser(string searchString)
        {
            throw new System.NotImplementedException();
        }


        public void AddUserActivity(string userId, string log)
        {
            userActivityRepository.Add(new UserLog { ApplicationUserId = userId, Content = log, CreatedTime = DateTime.Now });
        }

        public IEnumerable<UserLog> GetAllUserActivity()
        {
            return userActivityRepository.GetAll();
        }


    }
}
