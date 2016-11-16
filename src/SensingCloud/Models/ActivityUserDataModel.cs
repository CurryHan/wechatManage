using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SensingCloud.Models
{
    public class ActivityUserDataModel
    {
        public int Id { get; set; }
        public int ActivityID { get; set; }

        //public virtual Activity Activity { get; set; }

        /// <summary>
        /// 基本微信用户数据
        /// </summary>
        public int WeixinUserInfoID { get; set; }

        //public virtual WeixinUserInfo WeixinUserInfo { get; set; }

        /// <summary>
        /// 用户真是姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用户电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 联系地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 用户身份证号码
        /// </summary>
        public string IdentityID { get; set; }

        /// <summary>
        /// 用户公司名
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 是否签到
        /// </summary>
        public bool IsSigned { get; set; }

        /// <summary>
        /// 身份是否被认证
        /// </summary>
        public bool IsValidated { get; set; }

        public int FlowType { get; set; }

    }
}