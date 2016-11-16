using LogService;
using Sensing.Data;
using Senparc.Weixin.Open.ComponentAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SensingCloud.Helpers;

namespace SensingCloud
{
    public class WeixinConfig
    {
        protected static readonly IBizLogger logger = ServerLogFactory.GetLogger(typeof(WeixinConfig));
        public static Func<string, string> GetComponentVerifyTicketFunc;
        public static Func<string, string> GetAuthorizerRefreshTokenFunc;
        public static Action<string, RefreshAuthorizerTokenResult> AuthorizerTokenRefreshedFunc;

        //public static void RegisterComponentContainer()
        //{
        //    SensingSiteDbContext db = new SensingSiteDbContext();
        //    var WeixinOpenPlatform = db.WeixinOpenPlatforms.FirstOrDefault(p => p.ComponentAppID == ConstConfig.ComponentAppID && p.Deleted == false);
        //    string componentTicket = WeixinOpenPlatform.Component_verify_ticket;

        //    Func<string, string> getComponentVerifyTicketFunc = componentAppId =>
        //    {
        //        SensingSiteDbContext db1 = new SensingSiteDbContext();
        //        var WeixinOpenPlatform1 = db1.WeixinOpenPlatforms.FirstOrDefault(p => p.ComponentAppID == ConstConfig.ComponentAppID && p.Deleted == false);
        //        string componentTicket1 = WeixinOpenPlatform1.Component_verify_ticket;
        //        return componentTicket1;
        //    };

        //    Func<string, string> getAuthorizerRefreshTokenFunc = AuthorizerAppId =>
        //    {
        //        SensingSiteDbContext db1 = new SensingSiteDbContext();
        //        var WeixinPublicAccountInfo = db1.WeixinPublicAccountInfos.First(p => p.WeixinAppID == AuthorizerAppId);
        //        string Authorizer_refresh_token = WeixinPublicAccountInfo.Authorizer_refresh_token;
        //        return Authorizer_refresh_token;
        //    };

        //    Action<string, RefreshAuthorizerTokenResult> authorizerTokenRefreshedFunc = (AuthorizerAppId, RefreshAuthorizerTokenResult) =>
        //    {
        //        SensingSiteDbContext db1 = new SensingSiteDbContext();
        //        var WeixinPublicAccountInfo = db1.WeixinPublicAccountInfos.FirstOrDefault(p => p.WeixinAppID == AuthorizerAppId);
        //        if (WeixinPublicAccountInfo == null) return;
        //        WeixinPublicAccountInfo.Authorizer_access_token = RefreshAuthorizerTokenResult.authorizer_access_token;
        //        WeixinPublicAccountInfo.Authorizer_refresh_token = RefreshAuthorizerTokenResult.authorizer_refresh_token;
        //        WeixinPublicAccountInfo.Expires_in = RefreshAuthorizerTokenResult.expires_in;
        //        WeixinPublicAccountInfo.LastUpdated = DateTime.Now;
        //        db1.Entry(WeixinPublicAccountInfo).State = System.Data.Entity.EntityState.Modified;
        //        db1.SaveChanges();
        //    };

        //    ComponentContainer.Register(
        //         WeixinOpenPlatform.ComponentAppID,
        //         WeixinOpenPlatform.ComponentAppSecret,
        //         getComponentVerifyTicketFunc, getAuthorizerRefreshTokenFunc, authorizerTokenRefreshedFunc);

        //    GetComponentVerifyTicketFunc = getComponentVerifyTicketFunc;
        //    GetAuthorizerRefreshTokenFunc = getAuthorizerRefreshTokenFunc;
        //    AuthorizerTokenRefreshedFunc = authorizerTokenRefreshedFunc;
        //    System.Timers.Timer wxTimer = new System.Timers.Timer(3600000);
        //    wxTimer.Elapsed += WxTimer_Elapsed;
        //    wxTimer.Enabled = true;
        //    WxTimer_Elapsed(null, null);
        //    wxTimer.AutoReset = true;
        //    wxTimer.Start();
        //}

        //private static void WxTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        //{
        //    try
        //    {
        //        logger.Debug("WxTimer_Elapsed start");
        //        SensingSiteDbContext db = new SensingSiteDbContext();
        //        var op = db.WeixinOpenPlatforms.FirstOrDefault(p => p.ComponentAppID == ConstConfig.ComponentAppID && p.Deleted == false);
        //        string ComponentVerifyTicket = ComponentContainer.TryGetComponentVerifyTicket(op.ComponentAppID);
        //        logger.Debug("ComponentVerifyTicket is: " + ComponentVerifyTicket);
        //        string Component_access_token = ComponentContainer.GetComponentAccessToken(op.ComponentAppID, ComponentVerifyTicket, true);
        //        op.Component_access_token = Component_access_token;
        //        op.Component_verify_ticket = ComponentVerifyTicket;
        //        op.LastUpdated = DateTime.Now;
        //        db.Entry(op).State = System.Data.Entity.EntityState.Modified;
        //        db.SaveChanges();
        //        logger.Debug("update Component_access_token successed");
        //        foreach (var mp in db.WeixinPublicAccountInfos.ToList())
        //        {
        //            try
        //            {
        //                string refreshCode = GetAuthorizerRefreshTokenFunc(mp.WeixinAppID);
        //                logger.Debug(string.Format("start to refresh authorizer token and componet access token is {0},ComponentAppID is {1},authorizer AppID is {2},authorizer refreshcode is {3}", op.Component_access_token, op.ComponentAppID, mp.WeixinAppID, refreshCode));
        //                RefreshAuthorizerTokenResult result = AuthorizerContainer.RefreshAuthorizerToken(op.Component_access_token, op.ComponentAppID, mp.WeixinAppID, refreshCode);
        //                AuthorizerTokenRefreshedFunc(mp.WeixinAppID, result);

        //                logger.Debug("refresh authorizer token  successed");
        //            }
        //            catch (Exception ex1)
        //            {
        //                logger.Error(ex1);
        //            }

        //        }

        //        UpdateJsApiTicket();
        //        logger.Debug("WxTimer_Elapsed end");
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Debug("WxTimer_Elapsed system error:" + ex.Message + ex.StackTrace);
        //    }

        //}

        //private static void UpdateJsApiTicket()
        //{
        //    logger.Debug("WxTimer UpdateJsApiTicket start");
        //    SensingSiteDbContext db = new SensingSiteDbContext();
        //    foreach (var mp in db.WeixinPublicAccountInfos.ToList())
        //    {
        //        try
        //        {
        //            string ticket = Senparc.Weixin.Open.ComponentAPIs.ComponentApi.GetJsApiTicket(mp.Authorizer_access_token).ticket;
        //            mp.Jsapi_ticket = ticket;
        //            mp.LastUpdated = DateTime.Now;
        //            db.Entry(mp).State = System.Data.Entity.EntityState.Modified;
        //            db.SaveChanges();
        //        }
        //        catch (Exception ex)
        //        {
        //            logger.Debug("WxTimer  UpdateJsApiTicket system error:" + ex.Message + ex.StackTrace);
        //        }
        //    }

        //}

    }
}