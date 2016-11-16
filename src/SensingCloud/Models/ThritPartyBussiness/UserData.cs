using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SensingCloud.Models.ThritPartyBussiness
{
    public class UserData
    {
        public int Id { get; set; }
        public string Openid { get; set; }
        public string Nickname { get; set; }
        public int Sex { get; set; }
        public object Language { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string HeadImgUrl { get; set; }
        public string Unionid { get; set; }
        public string Remark { get; set; }
        public object WeixinGroupid { get; set; }
        public string QrCodeId { get; set; }
        public object Score { get; set; }
        public object PostUrl { get; set; }
        public object GameImage { get; set; }
        public object PlayerImage { get; set; }
        public int PlayerAge { get; set; }
        public int ShareCount { get; set; }
        public int ViewCount { get; set; }
        public int LikeCount { get; set; }
    }
}