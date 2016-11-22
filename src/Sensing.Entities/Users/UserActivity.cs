using Sensing.Entities;
using Sensing.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensing.Entities.Entity
{
    public class UserActivity: EntityBase
    {
        public string ApplicationUserId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedTime { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
