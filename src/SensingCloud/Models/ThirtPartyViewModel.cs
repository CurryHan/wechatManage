using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SensingCloud.Models
{
    public class ThirtPartyViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "接口名称")]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [Display(ResourceType = typeof(Resources.Resources), Name = "Description")]
        [MaxLength(300)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "推送Url")]
        [MaxLength(300)]
        public string PostUrl { get; set; }


        [Display(Name = "用户名")]
        [MaxLength(300)]
        public string UserName { get; set; }

     
        [Display(Name = "密码")]
        [MaxLength(300)]
        public string Password { get; set; }

        [Display(Name = "MD5Key")]
        [MaxLength(300)]
        public string MD5Key { get; set; }

        [Required]
        [Display(Name = "Group名")]
        public int GroupID { get; set; }

        
    }
}