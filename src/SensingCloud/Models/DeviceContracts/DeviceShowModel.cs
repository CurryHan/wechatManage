using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SensingCloud.Models.DeviceContracts
{
    public class DeviceShowModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDefaultModel { get; set; }
        public IEnumerable<DeviceShowArea> ShowAreas { get; set; }
    }
}