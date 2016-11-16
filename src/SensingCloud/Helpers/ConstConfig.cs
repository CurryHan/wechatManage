using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SensingCloud.Helpers
{
    public class ConstConfig
    {
        public const string ACTION = "action";
        public const string ACTIVITY = "activity";
        public const string USERAWARD = "useraward";
        public const string TEMPTATE = "template";
        public const string Flow = "flow";
        public const string GAME = "game";
        public const string PLATHOST = "http://wx.troncell.com";

        public const string USERINFOSCOPE = "snsapi_userinfo";
        public const string BASESCOPE = "snsapi_base";
        public const string Weixin_Award_Template_Short_ID = "OPENTM204632492";

        #region session key
        public const string SessionKeyOpenId = "openid";

        public const string SessionKey_CurrentLoginGroup = "CurrentLoginGroup";

        public const string SessionKey_CurrentLoginGroupID = "CurrentLoginGroupID";

        #endregion
        public const string CacheKey_CurrentLoginGroupID = "CacheKey_CurrentLoginGroupID";
        public const string CacheKey_CurrentLoginGroupType = "CacheKey_CurrentLoginGroupType";
        #region catch key

        #endregion

        public const string DefaultAccountUserHeaderImg = "/Content/ace/avatars/avatar.png";

        private const string Upload_Base = "/upload";

        public const string ComponentAppID = "wxd8b7c1de18511fff";
        /// <summary>
        /// {0} groupId
        /// {1} activityId
        /// {2} activityGameId
        /// </summary>
        private const string Customer_ActivityGame_Path = Upload_Base + "/Customer/{0}/Activities/{1}/ActivityGames/{2}/";

        public static string GetCustomerActivityGamePath(int groupId, int activityId,int activityGameId)
        {
            return string.Format(Customer_ActivityGame_Path, groupId, activityId,activityGameId);
        }

        /// <summary>
        /// {0} groupId
        /// {1} activityId
        /// {2} activityGameId
        /// </summary>
        private const string Customer_ActivityGame_Path_UserData = Upload_Base + "/Customer/{0}/Activities/{1}/ActivityGames/{2}/UserData/";

        /// <summary>
        /// {0} groupId
        /// {1} activityId
        /// {2} activityGameId
        /// </summary>
        private const string Customer_ActivityGame_Path_Data = Upload_Base + "/Customer/{0}/Activities/{1}/ActivityGames/{2}/Data/";

        public static string GetCustomerActivityGameUserDataPath(int groupId, int activityId, int activityGameId)
        {
            return string.Format(Customer_ActivityGame_Path_UserData, groupId, activityId, activityGameId);
        }

        public static string GetCustomerActivityGameDataPath(int groupId, int activityId, int activityGameId)
        {
            return string.Format(Customer_ActivityGame_Path_Data, groupId, activityId, activityGameId);
        }

        /// <summary>
        /// {0} groupid
        /// {1} Activityid
        /// </summary>
        private const string Customer_Activity_Path = Upload_Base + "/Customer/{0}/Activity/{1}/Data/";

        public static string GetCustomerActivityDataPath(int groupId, int activityId)
        {
            return string.Format(Customer_Activity_Path, groupId, activityId);
        }

        /// <summary>
        /// {0} groupid
        /// {1} Activityid
        /// </summary>
        private const string Customer_Ads_Path = Upload_Base + "/Customer/{0}/Ads/";
        public static string GetCustomerAdsPath(int groupId)
        {
            return string.Format(Customer_Ads_Path, groupId);
        }

        /// <summary>
        /// {0} groupid
        /// {1} Activityid
        /// {2} Awardid
        /// </summary>
        private const string Customer_Activity_Award_Path = Upload_Base + "/Customer/{0}/Activity/{1}/Award/{2}/";


        public static string GetCustomerActivityAwardPath(int groupId,int activityId,int awardId)
        {
            return string.Format(Customer_Ads_Path, groupId,activityId,awardId);
        }

        /// <summary>
        /// {0} groupid
        /// {1} Activityid
        /// {2} activitygameid
        /// </summary>
        private const string Customer_Activity_Game_Path = Upload_Base + "/Customer/{0}/Activity/{1}/Game/{2}/";
        


        /// <summary>
        /// {0} groupid
        /// {1} userid
        /// </summary>
        private const string Customer_UserData_Path = Upload_Base + "/Customer/{0}/User/{1}/Data/";

        public static string GetCustomerUserDataPath(int groupId, string userId)
        {
            return string.Format(Customer_UserData_Path, groupId, userId);
        }

        /// <summary>
        /// {0} groupid
        /// </summary>
        private const string Customer_Group_DataPath = Upload_Base + "/Customer/{0}/Data/";

        public static string GetCustomerGroupDataPath(int groupId)
        {
            return string.Format(Customer_Group_DataPath, groupId);
        }

        /// <summary>
        /// 平台图片路径，与用户无关 .html模板
        /// {0} htmltemplateid
        /// </summary>
        private const string Common_HtmlTemplate_Path = Upload_Base + "/Common/HtmlTemplate/{0}/";

        public static string GetHtmlTemplatePath(int htmlTemplateId)
        {
            return string.Format(Common_HtmlTemplate_Path, htmlTemplateId);
        }

        /// <summary>
        /// 平台图片路径，与用户无关 游戏图片
        /// </summary>
        private const string Common_Game_Path = Upload_Base + "/Common/Game/{0}/";

        public static string GetGamePath(int gameId)
        {
            return string.Format(Common_Game_Path, gameId);
        }

        /// <summary>
        /// 平台图片路径，与用户无关 通用设置
        /// </summary>
        public const string Common_Setting_Path = Upload_Base + "/Common/Setting/";

        /// <summary>
        /// 默认分享时的小图标
        /// </summary>
        public const string Common_Share_Image = "/Content/img/shareDefault.png";

        /// <summary>
        /// 默认图文消息里的图片
        /// </summary>
        public const string Common_News_Image = "/Content/img/newsDefault.png";

        /// <summary>
        /// 默认中奖图文消息里的图片
        /// </summary>
        public const string Common_Award_News_Image = "/Content/img/awardNewsDefault.jpg";

        public const string GameSuppotUserWarningMessage = "啊呀，好像您扫的慢了，请稍后等待下一轮吧";

        public const string actionParam = "?openid={0}&appid={1}&actionId={2}&isShared={3}";
        public const string acticityParam = "?openid={0}&appid={1}&activityId={2}";

        public static string PlatformHost
        {
            get
            {
                var port = HttpContext.Current.Request.Url.Port;
                var url = string.Empty;
                if (port != 80)
                {
                    url = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + ":" + port;
                }
                else
                {
                    url = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host;
                }
                return url;
            }
        }

        public static string UserActionRouterUrlBase = PlatformHost + "/Action/Transfer";

        public const int GameTag = 4;

        public const int SuperPlatGroupID = 1;
    }
}