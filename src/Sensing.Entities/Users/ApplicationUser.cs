using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using rs = Resources.Resource;
namespace Sensing.Entities.Users
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            IsActived = true;
        }
        [Display(Name = nameof(rs.FirstName), ResourceType = typeof(rs))]
        public string FirstName { get; set; }
        [Display(Name = nameof(rs.LastName), ResourceType = typeof(rs))]
        public string LastName { get; set; }
        /// <summary>
        /// The Company Name. 公司名称已经在分组里面列出，此处重复.
        /// </summary>
        [Display(Name = nameof(rs.CompanyName), ResourceType = typeof(rs))]
        public string CompanyName { get; set; }

        /// <summary>
        /// Female or male.
        /// </summary>
        [Display(Name = nameof(rs.Gender), ResourceType = typeof(rs))]
        public string Gender { get; set; }

        /// <summary>
        /// the avatar image of user.
        /// </summary>
        [Display(Name = nameof(rs.AvatarUrl), ResourceType = typeof(rs))]
        public string AvatarUrl { get; set; }
        [Display(Name = nameof(rs.LastLoginTime), ResourceType = typeof(rs))]
        public DateTime? LastLoginTime { get; set; }
        [Display(Name = nameof(rs.IsActived), ResourceType = typeof(rs))]
        public bool IsActived { get; set; }
        [Display(Name = nameof(rs.UserAddresses), ResourceType = typeof(rs))]
        public List<UserAddress> UserAddresses { get; set; }

        /// <summary>
        /// 所在的组,目前所在的组,可表示为零售店的地理位置,如总部---省份--市---区---店
        /// </summary>
        public int? GroupId { get; set; }
        [Display(Name = nameof(rs.Group), ResourceType = typeof(rs))]
        public virtual Group Group { get; set; }
        [Display(Name = nameof(rs.AuditStatus), ResourceType = typeof(rs))]
        public AuditStatus AuditStatus { get; set; }
        [Display(Name = nameof(rs.GenerateUserIdentityAsync), ResourceType = typeof(rs))]
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        [Display(Name = nameof(rs.DisplayName), ResourceType = typeof(rs))]
        public string DisplayName
        {
            get { return string.Format("{0} {1}",FirstName, LastName); }
        }

    }
}
