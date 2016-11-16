using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SensingCloud.Models.DeviceContracts
{
    public class DeviceInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IP { get; set; }
        public string Mac { get; set; }
        public int AreaDeviceId { get; set; }
        public DeviceSetting Setting { get; set; }
        public IEnumerable<DeviceShowModel> ShowModels { get; set; }
        public IEnumerable<DeviceResource> Resources { get; set; }
        


    }
}