using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensing.Entities.Users
{
    public class RoleString
    {
        //Administartor for managing basic data in cloud management console.
        public const string Admin = "Administrator";

        //Resource editor.
        public const string Editor = "Editor";

        //Auditor for managing online.
        public const string Auditor = "Auditor";

        //Manager for viewing data reports.
        public const string Manager = "Manager";

        //Member for CRM System.
        public const string Member = "Member";

        //
        public const string User = "User";
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
