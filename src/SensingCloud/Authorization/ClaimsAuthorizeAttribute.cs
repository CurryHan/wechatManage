using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SensingCloud.Authorization
{
    public class ClaimsAuthorizeAttribute : AuthorizeAttribute
    {
        private string[] _claims;
        private string _claimsString;
        public string Claims
        {
            get { return _claimsString; }
            set
            {
                if(value != _claimsString)
                {
                    _claimsString = value;
                    _claims = _claimsString.Split(',');
                }
            }
        }
        public ClaimsAuthorizeAttribute()
        {
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = HttpContext.Current.User as ClaimsPrincipal;
            if (user.HasClaim(claim => _claims.Contains(claim.Type)))
            {
                base.OnAuthorization(filterContext);
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}
