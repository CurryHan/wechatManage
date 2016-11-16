using LogService;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using System.IO;
using System;

[assembly: OwinStartupAttribute(typeof(SensingCloud.Startup))]
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "bin/LogService.dll.config", Watch = true)]
namespace SensingCloud
{
    public partial class Startup
    {
        private static readonly IBizLogger logger = ServerLogFactory.GetLogger(typeof(Startup));
        public void Configuration(IAppBuilder app)
        {
            logger.Debug("Startup is coming.");
            AutoConfiguration(app);
            //app.Map("/EnableDetailedErrors", map =>
            //{
            //    var hubConfiguration = new HubConfiguration
            //    {
            //        EnableDetailedErrors = true
            //    };
            //    map.MapSignalR(hubConfiguration);
            //});
            app.MapSignalR();
#if DEBUG
            GlobalHost.Configuration.DisconnectTimeout = TimeSpan.FromSeconds(6);
#endif
        }
    }
}
