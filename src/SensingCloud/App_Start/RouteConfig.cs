using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SensingCloud
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                 name: "product1",
                 url: "api/ProductApi/GetDeviceForAuth",
                 defaults: new { controller = "ProductApi", action = "GetDeviceForAuth" }
                 );
            routes.MapRoute(
                 name: "product2",
                 url: "api/ProductApi/GetProducts",
                 defaults: new { controller = "ProductApi", action = "GetProducts" }
                 );




            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}
