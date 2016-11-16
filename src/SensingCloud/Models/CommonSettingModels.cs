using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SensingCloud.Models
{
    public class ChangePadPasswordViewModel
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "旧密码")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangeCommonSettingViewModel
    {
        public int Id { get; set; }

        [Required]
        [Range(1,10,ErrorMessage ="请输入1到10以内的数")]
        public int PadTimeOut { get; set; }

        [Required]
        [DataType(DataType.Time,ErrorMessage ="输入正确的时间格式")]
        public DateTime AutoSync { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "请输入1到100以内的数")]
        public int PptTimeOut { get; set; }
    }
}