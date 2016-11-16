using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensing.Thirtparty.Exstension.Model
{
    public class ThirdPartyRequest
    {
        public string appId { get; set; }
        public string openId { get; set; }
        public string nickname { get; set; }
        public string headerImage { get; set; }

        /// <summary>
        /// yyyyMMddHHmmss 创建时间
        /// </summary>
        public string createdTime { get; set; }
        /// <summary>
        /// 游戏成绩
        /// </summary>
        public int score { get; set; }
        /// <summary>
        /// 是否在游戏结束后调用
        /// </summary>
        public bool isAfterGame { get; set; }

        /// <summary>
        /// 游戏名称
        /// </summary>
        public string gameName { get; set; }
        /// <summary>
        /// 活动名称
        /// </summary>
        public string activityName { get; set; }

        /// <summary>
        /// 参数签名
        /// </summary>
        public string signStr { get; set; }

        /// <summary>
        /// md5key
        /// </summary>
        //public string key { get; set; }
    }
}
