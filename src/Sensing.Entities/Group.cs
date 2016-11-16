using Sensing.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using rs = Resources.Resource;

namespace Sensing.Entities
{

    public enum GroupEnum
    {
        [Display(Name = "超级组")]
        SuperLevel = 1
    }

    public class GroupTypeString
    {
        public const string SuperLevel = "SuperLevel";

    }

    /// <summary>
    /// 分组,作为框架中的一个非常重要功能,涉及其它的方方面面。
    /// </summary>
    public class Group : EntityBase
    {
        [Required]
        [Display(Name = nameof(rs.Name), ResourceType = typeof(rs))]
        public string DisplayName { get; set; }


        /// <summary>
        /// 组的类型
        /// </summary>
        [Display(Name = nameof(rs.GroupType), ResourceType = typeof(rs))]
        public GroupEnum GroupType { get; set; }

        public int? ParentGroupId { get; set; }
        [Display(Name = nameof(rs.ParentGroup), ResourceType = typeof(rs))]
        public virtual Group ParentGroup { get; set; }

        /// <summary>
        /// all the chidren after the current group.
        /// </summary>
        [Display(Name = nameof(rs.Children), ResourceType = typeof(rs))]
        public virtual List<Group> Children { get; set;}

        /// <summary>
        /// 电商平台
        /// </summary>
        [Display(Name = nameof(rs.EcommercePlatform), ResourceType = typeof(rs))]
        public string EcommercePlatform{get;set;}

        /// <summary>
        /// 公司logo
        /// </summary>
        [Display(Name = nameof(rs.Logo), ResourceType = typeof(rs))]
        public string LogoUrl { get; set; }

        /// <summary>
        /// 网址
        /// </summary>
        [Display(Name = nameof(rs.AddressUrl), ResourceType = typeof(rs))]
        public string AddressUrl {get;set; }

        /// <summary>
        /// 介绍
        /// </summary>
        [Display(Name = nameof(rs.Introduction), ResourceType = typeof(rs))]
        public string Introduction { get; set; }

        /// <summary>
        /// 声明
        /// </summary>
        [Display(Name = nameof(rs.Declaration), ResourceType = typeof(rs))]
        public string Declaration { get; set; }

        /// <summary>
        /// 二维码
        /// </summary>
        [Display(Name = nameof(rs.Qrcode_url), ResourceType = typeof(rs))]
        public string QRCodeUrl { get; set; }


        //todo:hanqi, need to refactory. add a resource table.

        /// <summary>
        /// 图片1
        /// </summary>
        [Display(Name = nameof(rs.Image01Url), ResourceType = typeof(rs))]
        public string Image01Url { get; set; }
        /// <summary>
        /// 图片2
        /// </summary>
        [Display(Name = nameof(rs.Image02Url), ResourceType = typeof(rs))]
        public string Image02Url { get; set; }
        /// <summary>
        /// 图片3
        /// </summary>
        [Display(Name = nameof(rs.Image03Url), ResourceType = typeof(rs))]
        public string Image03Url { get; set; }
        /// <summary>
        /// 图片4
        /// </summary>
        [Display(Name = nameof(rs.Image04Url), ResourceType = typeof(rs))]
        public string Image04Url { get; set; }
    }
}
