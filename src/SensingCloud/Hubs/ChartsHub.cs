using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SensingCloud.Hubs
{
    public class ChartsHub : Hub
    {
        public void SayHI()
        {
            Clients.All.log("aaaaa");
        }
    }
}