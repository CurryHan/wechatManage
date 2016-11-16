using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SensingCloud.Models.DeviceContracts
{
    public class DeviceSetting
    {
        public DateTime AutoSync { get; set; }

        public int PptTimeOut { get; set; }
    }
}