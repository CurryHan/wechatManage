
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensing.Thirtparty.Exstension.Model
{
    public class ThirdPartyResponse
    {
        public string respCode { get; set; }
        public string errorMsg { get; set; }
        public CrmMember data { get; set; }

    }

    public class CrmMember
    {
        public string phone { get; set; }
        public string newPoints { get; set; }
        public string name { get; set; }
        public string registeUrl { get; set; }
        public string isMember { get; set; }
        public string myUrl { get; set; }


    }
}
