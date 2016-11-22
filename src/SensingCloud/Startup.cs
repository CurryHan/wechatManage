using LogService;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using SensingCloud.Hubs;
using System.IO;

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


            app.MapSignalR();


            app.Map("/EnableDetailedErrors", map =>
            {
                var hubConfiguration = new HubConfiguration
                {
                    EnableDetailedErrors = true
                };
                map.MapSignalR(hubConfiguration);
            });

        }
    }
}
