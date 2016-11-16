using Sensing.Entities;
using Sensing.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rs = Resources.Resource;
namespace Sensing.Entities.Entity
{
    public class UserLog: EntityBase
    {
        [Display(Name = nameof(rs.ApplicationUserId), ResourceType = typeof(rs))]
        public string ApplicationUserId { get; set; }
        [Display(Name = nameof(rs.Content), ResourceType = typeof(rs))]
        public string Content { get; set; }
        [Display(Name = nameof(rs.CreatedTime), ResourceType = typeof(rs))]
        public DateTime CreatedTime { get; set; }
        [Display(Name = nameof(rs.ApplicationUser), ResourceType = typeof(rs))]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
