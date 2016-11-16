using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SensingCloud.Models.DeviceContracts
{
    public class DeviceProfile
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<int> ResourceIds { get; set; }
    }
}