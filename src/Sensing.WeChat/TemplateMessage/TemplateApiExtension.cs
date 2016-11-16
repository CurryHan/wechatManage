using Senparc.Weixin;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using Sensing.WeChat.TemplateMessage.TemplateMessageJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensing.WeChat
{
    public static class TemplateApiExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="api"></param>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="template_id_short">
        ///// {{first.DATA}}
        ///活动：{{keyword1.DATA}}
        ///奖品：{{keyword2.DATA}}
        ///{{remark.DATA}}
        /// </param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static AddTemplateResult GetTemplateid(string accessTokenOrAppId, string template_id_short , int timeOut = Config.TIME_OUT)
        {
            Config.IsDebug = true;
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/cgi-bin/template/api_add_template?access_token={0}";
                var msgData = new 
                {
                    template_id_short = template_id_short
                };
                return CommonJsonSend.Send<AddTemplateResult>(accessToken, urlFormat, msgData, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
    }
    

}
