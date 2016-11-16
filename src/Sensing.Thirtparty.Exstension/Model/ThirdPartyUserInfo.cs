using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensing.Thirtparty.Exstension.Model
{
    public class ThirdPartyUserInfo
    {
        /// <summary>
        /// 是否是会员
        /// </summary>
        public bool IsMember { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 我的主页
        /// </summary>
        public string MyUrl { get; set; }
        /// <summary>
        /// 新增积分
        /// </summary>
        public string NewPoints { get; set; }
    }
}
