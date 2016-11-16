using Sensing.Data;
using Senparc.Weixin.Open.ComponentAPIs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SensingCloud.Controllers;
using LogService;
using System.Web.Security;
using System.Web.Script.Serialization;
using SensingCloud.Authentication;

namespace SensingCloud
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected static readonly IBizLogger logger = ServerLogFactory.GetLogger(typeof(MvcApplication));
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/bin/LogService.dll.config")));

            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MappingConfig.RegisterMaps();
            //Database.SetInitializer(new SensingSiteSampleData());
            //WeixinConfig.RegisterComponentContainer();
        }

        //protected void Application_Error(object sender, EventArgs e)
        //{
        //    Exception ex = HttpContext.Current.Server.GetLastError();
        //    if(ex is Senparc.Weixin.Base.Exceptions.ErrorJsonResultException)
        //    {
        //        ((Senparc.Weixin.Base.Exceptions.ErrorJsonResultException)ex).
        //    }
        //}

        protected void Application_Error(Object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Server.ClearError();

            var routeData = new RouteData();
            routeData.Values.Add("controller", "ErrorPage");
            routeData.Values.Add("action", "Error");
            routeData.Values.Add("exception", exception);

            if (exception.GetType() == typeof(HttpException))
            {
                routeData.Values.Add("statusCode", ((HttpException)exception).GetHttpCode());
            }
            else
            {
                routeData.Values.Add("statusCode", 500);
            }
            logger.Error("Application_Error", exception);

            Response.TrySkipIisCustomErrors = true;
            IController controller = new ErrorPageController();
            controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
            Response.End();
        }

        //protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        //{
        //    HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
        //    if (authCookie != null)
        //    {
        //        FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
        //        JavaScriptSerializer serializer = new JavaScriptSerializer();
        //        if (authTicket.UserData == "OAuth") return;
        //        SensingPrincipalSerializedModel serializeModel =
        //          serializer.Deserialize<SensingPrincipalSerializedModel>(authTicket.UserData);
        //        SensingPrincipal newUser = new SensingPrincipal(authTicket.Name);
        //        newUser.GroupID = serializeModel.GroupID;
        //        newUser.GroupType = serializeModel.GroupType;
        //        HttpContext.Current.User = newUser;
        //    }
        //}
    }
}
