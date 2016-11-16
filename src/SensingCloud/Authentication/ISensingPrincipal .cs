using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using Sensing.Entities;

namespace SensingCloud.Authentication
{
    public interface ISensingPrincipal : System.Security.Principal.IPrincipal
    {
        Sensing.Entities.GroupEnum GroupType { get; set; }

        int GroupID { get; set; }
    }

    public class SensingPrincipal : ISensingPrincipal
    {
        public int GroupID
        {
            get; set;
        }

        public GroupEnum GroupType
        {
            get; set;
        }

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            return Identity != null && Identity.IsAuthenticated &&
               !string.IsNullOrWhiteSpace(role) && Roles.IsUserInRole(Identity.Name, role);
        }

        public SensingPrincipal(string username)
        {
            this.Identity = new GenericIdentity(username);
        }
    }

    public class SensingPrincipalSerializedModel
    {

        public int GroupID { get; set; }

        public GroupEnum GroupType { get; set; }
    }
}