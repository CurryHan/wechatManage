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
    /// 平台
    /// </summary>
    [Table("S_PlatformNotification")]
    public class PlatformNotification : EntityBase
    {
        /// <summary>
        /// 发送邮件的服务器地址
        /// </summary>
        [Required]
        [Display(Name = "ServerAddress", ResourceType = typeof(Resources.Resources))]
        public string ServerAddress { get; set; }
        /// <summary>
        /// 发送邮件的端口号
        /// </summary>
        [Display(Name = "ServerPort", ResourceType = typeof(Resources.Resources))]
        public string ServerPort { get; set; }
        /// <summary>
        /// 邮件服务用户名
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "EmailName", ResourceType = typeof(Resources.Resources))]
        public string EmailName { get; set; }
        /// <summary>
        /// 邮件服务密码
        /// </summary>
        [Required]
        [Display(Name = "EmailPassword", ResourceType = typeof(Resources.Resources))]
        public string EmailPassword { get; set; }

        /// <summary>
        /// 短信服务商url
        /// </summary>
     
        [Display(Name = "SmsUrl", ResourceType = typeof(Resources.Resources))]
        public string SmsUrl { get; set; }
        /// <summary>
        /// 短信发送用户名
        /// </summary>
      
        [Display(Name = "SmsUID", ResourceType = typeof(Resources.Resources))]
        public string SmsUID { get; set; }
        /// <summary>
        /// 短信发送用户密码
        /// </summary>
       
        [Display(Name = "SmsPassword", ResourceType = typeof(Resources.Resources))]
        public string SmsPassword { get; set; }
        /// <summary>
        /// 短信的签名
        /// </summary>
       
        [Display(Name = "MessageSignatrue", ResourceType = typeof(Resources.Resources))]
        public string MessageSignatrue { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "Remarks", ResourceType = typeof(Resources.Resources))]
        public string Remarks { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsUsing { get; set; }
    }
}
