using System;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;

namespace SensingCloud.Modules
{
    public class BasicAuthentication : IHttpModule, IDisposable
    {
        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += AuthenticateRequests;
            context.EndRequest += TriggerCredentials;
        }

        private static void TriggerCredentials(object sender, EventArgs e)
        {
            HttpResponse resp = HttpContext.Current.Response;
            if (resp.StatusCode == 401)
            {
                resp.Headers.Add("WWW-Authenticate", @"Basic realm='sensingsite'");
            }
        }
        private static void AuthenticateRequests(object sender, EventArgs e)
        {
            string authHeader =
              HttpContext.Current.Request.Headers["Authorization"];
            if (authHeader != null)
            {
                AuthenticationHeaderValue authHeaderVal =
                    AuthenticationHeaderValue.Parse(authHeader);
                if (authHeaderVal.Parameter != null)
                {
                    byte[] unencoded = Convert.FromBase64String(
                      authHeaderVal.Parameter);
                    string userpw =
                      Encoding.GetEncoding("iso-8859-1").GetString(unencoded);
                    string[] creds = userpw.Split(':');
                    
                    if (creds[0] == "wulixu@troncell.com" && creds[1] == "1qaz@WSX")
                    {
                        GenericIdentity gi = new GenericIdentity(creds[0]);
                        Thread.CurrentPrincipal = new GenericPrincipal(gi, null);
                        HttpContext.Current.User = Thread.CurrentPrincipal;
                    }
                }
            }
        }

  

        public void Dispose()
        {
        }


    }
}