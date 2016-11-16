
using LogService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sensing.Weixin.SDK
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class wxAPI
    {
        protected static readonly IBizLogger logger = ServerLogFactory.GetLogger(typeof(wxAPI));
        /// <summary>
        /// 获取第三方平台access_token
        /// </summary>
        /// <param name="ComponentVerifyTicket">在公众号第三方平台创建审核通过后，微信服务器会向其“授权事件接收URL”每隔10分钟定时推送component_verify_ticket</param>
        /// <returns></returns>
        public static string GetComponentToken(string ComponentVerifyTicket)
        {
            string postData = "component_appid="+ConstEntity.ComponentAppID + "&component_appsecret=" + ConstEntity.ComponentAppSecret + "&component_verify_ticket="+ ComponentVerifyTicket;
            logger.Debug("GetComponentToken postData:" + postData);
            string strJson = HttpRequestUtil.RequestUrl(ConstEntity.Component_access_token_url, postData);
            logger.Debug("GetComponentToken result Json:" + strJson);
            return Tools.GetJsonValue(strJson, "component_access_token");
        }

        /// <summary>
        /// 获取预授权码
        /// </summary>
        /// <param name="component_access_token">第三方平台access_token</param>
        /// <returns></returns>
        public static string GetPreauthcode(string component_access_token)
        {
            string postData = "component_appid=" + ConstEntity.ComponentAppID;
            logger.Debug("GetPreauthcode postData:" + postData);
            string strJson = HttpRequestUtil.RequestUrl(string.Format(ConstEntity.Component_preauth_code_url,component_access_token),postData);
            logger.Debug("GetPreauthcode result json:" + postData);
            return Tools.GetJsonValue(strJson, "pre_auth_code");
        }

        #region 获取Token
        /// <summary>
        /// 获取Token
        /// </summary>
        public static string GetToken(string appid, string secret)
        {
            string strJson = HttpRequestUtil.RequestUrl(string.Format(ConstEntity.Access_token_url, appid, secret));
            return Tools.GetJsonValue(strJson, "access_token");
        }
        #endregion

        #region 验证Token是否过期
        /// <summary>
        /// 验证Token是否过期
        /// </summary>
        public static bool TokenExpired(string access_token)
        {
            string jsonStr = HttpRequestUtil.RequestUrl(string.Format(ConstEntity.Access_token_expire_url, access_token));
            if (Tools.GetJsonValue(jsonStr, "errcode") == "42001")
            {
                return true;
            }
            return false;
        }
        #endregion

    }
}
