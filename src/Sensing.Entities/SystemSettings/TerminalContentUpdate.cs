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
    /// 终端内容更新
    /// </summary>
    [Table("S_TerminalContentUpdate")]
    public class TerminalContentUpdate : EntityBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [Display(Name = "PName", ResourceType = typeof(Resources.Resources))]
        public string Name { get; set; }
        /// <summary>
        /// 是否自动更新
        /// </summary>
        [Display(Name = "IsAutoUpdate", ResourceType = typeof(Resources.Resources))]
        public bool IsAutoUpdate { get; set; }
        /// <summary>
        /// 自动更新频率
        /// </summary>
        [Display(Name = "UpdateFrequency", ResourceType = typeof(Resources.Resources))]
        public string UpdateFrequency { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [Display(Name = "UpdateTime", ResourceType = typeof(Resources.Resources))]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "Remarks", ResourceType = typeof(Resources.Resources))]
        public string Remarks { get; set; }
    }
}
