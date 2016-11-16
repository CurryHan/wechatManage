using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sensing.Weixin
{
    public class ConstEntity
    {
        public static string ComponentAppID = "wx4c29339ddb6f82cf";
        public static string ComponentAppSecret = "85154fcc53bc82c247881e9ba2f902af";
        public static string ComponentToken = "troncelltoken";
        public static string ComponentKey = "troncellkeytroncellkeytroncellkeytroncellke";
        public static string Component_access_token_url = "https://api.weixin.qq.com/cgi-bin/component/api_component_token";
        public static string Component_preauth_code_url = "https://api.weixin.qq.com/cgi-bin/component/api_create_preauthcode?component_access_token={0}";
        public static string Component_authorizer_access_token_url = "https://api.weixin.qq.com/cgi-bin/component/api_query_auth?component_access_token={0}";

        public static string Access_token_url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";
        public static string Access_token_expire_url = "https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}";



        //public static string 
    }
}
