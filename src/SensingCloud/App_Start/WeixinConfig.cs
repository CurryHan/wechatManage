using LogService;
using Sensing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SensingCloud.Helpers;
using Senparc.Weixin.MP.Containers;
using System.Text;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using Microsoft.AspNet.Identity;
using System.IO.Compression;

namespace SensingCloud
{
    public class WeixinConfig
    {
        protected static readonly IBizLogger logger = ServerLogFactory.GetLogger(typeof(WeixinConfig));
        public static Func<string, string> GetComponentVerifyTicketFunc;
        public static Func<string, string> GetAuthorizerRefreshTokenFunc;
        //public static Action<string, RefreshAuthorizerTokenResult> AuthorizerTokenRefreshedFunc;

        private const string AppID = "wx3a01457937e273a1";
        private const string AppSecret = "daaa2763975d353b74da5e38bb59eae4";

        public static void RegisterComponentContainer()
        {
            System.Timers.Timer wxTimer = new System.Timers.Timer(3600000);
            wxTimer.Elapsed += WxTimer_Elapsed;
            wxTimer.Enabled = true;
            WxTimer_Elapsed(null, null);
            wxTimer.AutoReset = true;
            wxTimer.Start();
        }

        private static void WxTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                logger.Debug("WxTimer_Elapsed start");
                SensingSiteDbContext db = new SensingSiteDbContext();
                var op = db.Groups.FirstOrDefault();
                string url = $"https://api.weixin.qq.com/cgi-bin/token";
                Dictionary<string, string> pout = new Dictionary<string, string>();
                pout.Add("grant_type", "client_credential");
                pout.Add("appid", AppID);
                pout.Add("secret", AppSecret);
                string result = WebUtilHelper.DoGet(url, pout, null);
                logger.Debug(result);
                AccessTokenInfo info = Newtonsoft.Json.JsonConvert.DeserializeObject<AccessTokenInfo>(result);
                op.access_token = info.access_token;
                op.expires_in = info.expires_in;
                db.Entry(op).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                logger.Debug("WxTimer_Elapsed end");
            }
            catch (Exception ex)
            {
                logger.Debug("WxTimer_Elapsed system error:" + ex.Message + ex.StackTrace);
            }

        }

        private static void UpdateJsApiTicket()
        {
            //logger.Debug("WxTimer UpdateJsApiTicket start");
            SensingSiteDbContext db = new SensingSiteDbContext();
            //foreach (var mp in db.WeixinPublicAccountInfos.ToList())
            //{
            //    try
            //    {
            //        string ticket = Senparc.Weixin.Open.ComponentAPIs.ComponentApi.GetJsApiTicket(mp.Authorizer_access_token).ticket;
            //        mp.Jsapi_ticket = ticket;
            //        mp.LastUpdated = DateTime.Now;
            //        db.Entry(mp).State = System.Data.Entity.EntityState.Modified;
            //        db.SaveChanges();
            //    }
            //    catch (Exception ex)
            //    {
            //        logger.Debug("WxTimer  UpdateJsApiTicket system error:" + ex.Message + ex.StackTrace);
            //    }
            //}

        }

    }

    public class AccessTokenInfo
    {
        public string access_token { get; set; }

        public int expires_in { get; set; }
    }
}