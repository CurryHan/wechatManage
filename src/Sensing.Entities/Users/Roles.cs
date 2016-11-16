using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensing.Entities.Users
{
    public class RoleString
    {
        /// <summary>
        /// this role is for platform administrator.
        /// </summary>
        //public const string PlatformAdmin = "PlatformAdmin";

        //Administartor for managing basic data in cloud management console.
        public const string Admin = "Admin";
    }

    public class RoleSet
    {
        public const string UserManage = "UserManage";
        public const string GroupManage = "GroupManage";
        public const string DeviceManage = "DeviceManage";
        public const string ProductManage = "ProductManage";
        public const string DeviceView = "DeviceView";
    }
}
