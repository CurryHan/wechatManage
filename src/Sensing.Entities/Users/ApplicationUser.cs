using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Sensing.Entities.Users
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            IsActived = true;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        /// <summary>
        /// The Company Name.
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Female or male.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// the avatar image of user.
        /// </summary>
        public string AvatarUrl { get; set; }
        public DateTime? LastLoginTime { get; set; }

        public bool IsActived { get; set; }

        public List<UserAddress> UserAddresses { get; set; }

        /// <summary>
        /// 所在的组,目前所在的组,可表示为零售店的地理位置,如总部---省份--市---区---店
        /// </summary>
        public int? GroupId { get; set; }
        public virtual Group Group { get; set; }

        public AuditStatus AuditStatus { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string DisplayName
        {
            get { return string.Format("{0} {1}",FirstName, LastName); }
        }

    }
}
