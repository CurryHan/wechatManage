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
    /// 平台审批流程
    /// </summary>
    [Table("S_ApproveProcess")]
    public class ApproveProcess : EntityBase
    {
        /// <summary>
        /// 需要审批项目名称
        /// </summary>
        [Required]
        [Display(Name = "PName", ResourceType = typeof(Resources.Resources))]
        public string Name { get; set; }
        /// <summary>
        /// 是否需要审批
        /// </summary>
        [Display(Name = "NeedApprove", ResourceType = typeof(Resources.Resources))]
        public bool NeedApprove { get; set; }
        /// <summary>
        /// 审批者角色
        /// </summary>
        [Display(Name = "ApproverRole", ResourceType = typeof(Resources.Resources))]
        public string ApproverRole { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "Remarks", ResourceType = typeof(Resources.Resources))]
        public string Remarks { get; set; }
    }

    public class ApproveNames
    {
        public static string Device = "设备上线审批";
        public static string Thing = "Thing上架审批";
    }
}
