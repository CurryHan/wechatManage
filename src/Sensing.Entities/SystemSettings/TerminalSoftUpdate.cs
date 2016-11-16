using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensing.Entities.SystemSettings
{
    /// <summary>
    /// 终端软件更新
    /// </summary>
    [Table("S_TerminalSoftUpdate")]
    public class TerminalSoftUpdate : EntityBase
    {
        /// <summary>
        /// 软件名称
        /// </summary>
        [Required]
        [Display(Name = "PName", ResourceType = typeof(Resources.Resources))]
        public string Name { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        [Display(Name = "SoftVersion", ResourceType = typeof(Resources.Resources))]
        public string Version { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [Display(Name = "UpdateTime", ResourceType = typeof(Resources.Resources))]
        public DateTime? UpdateTimes { get; set; }
        /// <summary>
        /// 更新包地址
        /// </summary>
        [Display(Name = "SoftPath", ResourceType = typeof(Resources.Resources))]
        public string SoftPath { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "Remarks", ResourceType = typeof(Resources.Resources))]
        public string Remarks { get; set; }
    }
}
