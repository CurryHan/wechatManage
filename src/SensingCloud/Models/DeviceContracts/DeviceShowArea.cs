using System.Collections.Generic;
namespace SensingCloud.Models.DeviceContracts
{
    public class DeviceShowArea
    {
        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 默认该区域是音频
        /// </summary>
        public bool DefaultIsAudio { get; set; }

        /// <summary>
        /// 比例 如16:9
        /// </summary>
        public string Proportion { get; set; }

        public IEnumerable<DeviceProfile> Profiles { get; set; }
    }
}