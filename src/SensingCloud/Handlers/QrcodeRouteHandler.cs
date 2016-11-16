using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace SensingCloud.Handlers
{
    public class QrcodeRouteHandler// : IRouteHandler
    {
        public System.Web.IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            //return new QrcodeHandler(requestContext);
            return null;
        }
    }
}