using Sensing.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using mvc = System.Web.Mvc;

namespace SensingCloud.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "username ")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "password")]
        public string Password { get; set; }

        [Display(Name = "保持登录")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0}至少{2}长.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码与确认密码不同.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class AdminResetPasswordViewModel
    {
        [Display(Name = "用户名")]
        [Required]
        public string UserName { get; set; }
        [Display(Name = "公司名")]
        [Required]
        public string Company { get; set; }
        [Display(Name = "密码重置")]
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class UserViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "公司名")]
        public string Company { get; set; }
        [Required]
        [Display(Name = "角色")]
        public string Role { get; set; }
        [Required]
        [Phone]
        [Display(Name = "电话")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "邮箱")]
        public string Email { get; set; }

        [Display(Name = "性别")]
        public string Gender { get; set; }

        [Display(Name = "是否禁用")]
        public bool IsActived { get; set; }

        public Group Group { get; set; }
    }

    public class CreateUserViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }
        //[Required]
        [Display(Name = "公司名")]
        public string Company { get; set; }

        [Required]
        [Phone]
        [Display(Name = "电话")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "邮箱")]
        public string Email { get; set; }

        [Display(Name = "性别")]
        public string Gender { get; set; }

        [Display(Name = "是否禁用")]
        public bool IsActived { get; set; }

        [Display(Name ="新建组")]
        public string NewGroupName { get; set; }
        public List<GroupName> GroupNames { get; set; }

        [Required]
        [Display(Name ="组名")]
        public int SelectedGroupId { get; set; }
        public IEnumerable<mvc.SelectListItem> GroupItems
        {
            get
            {
                return Enumerable.Repeat(new mvc.SelectListItem
                        {
                            Value = "",
                            Text = "选择组"
                        }, count: 1)
                        .Concat(GroupNames.Select(f => new mvc.SelectListItem
                        {
                            Value = f.Id.ToString(),
                            Text = f.Name
                        }));
            }
        }

        public List<string> RoleNames { get; set; }
       
        [Required]
        [Display(Name = "角色")]
        public string SelectRoleName { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> RoleItems
        {
            get
            {
                return Enumerable.Repeat(new mvc.SelectListItem { Text = "选择角色", Value="",Selected=true},count:1)
                        .Concat(RoleNames.Select(r => new mvc.SelectListItem
                        {
                            Text = r,
                            Value = r
                        }));
            }
        }
    }

    public class GroupName
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
