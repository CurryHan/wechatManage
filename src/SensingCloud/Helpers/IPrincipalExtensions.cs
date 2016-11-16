using SensingCloud.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace SensingCloud.Helpers
{
    public static class IPrincipalExtensions
    {
        public static int GetGroupID(this IPrincipal user)
        {
            if (HttpContext.Current.Cache[user.Identity.Name+ConstConfig.CacheKey_CurrentLoginGroupID]==null)
            {
                var userService = DependencyResolver.Current.GetService<IUserService>();
                var signUser = userService.GetUsers(user.Identity.Name).FirstOrDefault();
                HttpContext.Current.Cache.Insert(user.Identity.Name + ConstConfig.CacheKey_CurrentLoginGroupID, signUser.GroupId.Value);
                return signUser.GroupId.Value;
            }
            else
            {
                return Convert.ToInt32(HttpContext.Current.Cache[user.Identity.Name + ConstConfig.CacheKey_CurrentLoginGroupID]);
            }

        }

        public static Sensing.Entities.GroupEnum GetGroupType(this IPrincipal user)
        {
            if (HttpContext.Current.Cache[user.Identity.Name + ConstConfig.CacheKey_CurrentLoginGroupType] == null)
            {
                var userService = DependencyResolver.Current.GetService<IUserService>();
                var signUser = userService.GetUsers(user.Identity.Name).FirstOrDefault();
                HttpContext.Current.Cache.Insert(user.Identity.Name + ConstConfig.CacheKey_CurrentLoginGroupType, signUser.Group.GroupType);
                return signUser.Group.GroupType;
            }
            else
            {
                return (Sensing.Entities.GroupEnum)HttpContext.Current.Cache[user.Identity.Name + ConstConfig.CacheKey_CurrentLoginGroupType];
            }
        }
    }
}