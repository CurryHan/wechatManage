using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensing.Entities.SystemSettings
{
    public class CommonSetting:EntityBase
    {
        [Required]
        public string PadPassword { get; set; }

        /// <summary>
        /// pad超时时间间隔 秒单位
        /// </summary>
        public int PadTimeOut { get; set; }

        /// <summary>
        /// 自动同步时刻
        /// </summary>
        public DateTime AutoSync { get; set; }

        /// <summary>
        /// ppt无人操作自动翻页时间间隔 秒单位
        /// </summary>
        public int PptTimeOut { get; set; }

    }
}
