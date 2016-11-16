using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SensingCloud.Models
{
    public class WeChatJSViewModel
    {
        public string Signature { get; set; }
        public string AppId { get; set; }

        public string Timestamp { get; set; }

        public string NonceStr { get; set; }
        
        public string CurrentOpenId { get; set; }
        public string ComponentAppID { get; set; }

        public string Scope { get; set; }
        
    }
}