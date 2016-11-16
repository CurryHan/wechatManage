using SensingCloud.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SensingCloud
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("showqrcode.ashx");

            routes.MapMvcAttributeRoutes();

            #region v0
            routes.MapRoute(
                name: "weixin1",
                url: "api/v0/WeixinApi/QrCode4Login/{clientUniueId}",
                defaults: new { controller = "WeixinApi", action = "QrCode4Login", clientUniueId = "" }
            );
            routes.MapRoute(
                name: "weixin2",
                url: "api/v0/WeixinApi/FindScanQrCodeUser/{qrCodeId}",
                defaults: new { controller = "WeixinApi", action = "FindScanQrCodeUser", qrCodeId = "" }
            );
            routes.MapRoute(
                name: "weixin3",
                url: "api/v0/WeixinApi/PostDataByUser/{userId}/{qrCodeId}/{score}",
                defaults: new { controller = "WeixinApi", action = "PostDataByUser", userId = "", qrCodeId = "", gameImage = "", playerImage = "", score = "" }
            );
            routes.MapRoute(
                name: "weixin4",
                url: "api/v0/WeixinApi/PostData/{clientUniueId}/{score}",
                defaults: new { controller = "WeixinApi", action = "PostData", clientUniueId = "", gameImage = "", playerImage = "", score = "" }
            );
            routes.MapRoute(
                name: "weixin5",
                url: "api/WeixinApi/error/{errorid}",
                defaults: new { controller = "WeixinApi", action = "error", errorid = "" }
            );

            routes.MapRoute(
                name: "weixin6",
                url: "api/v0/WeixinApi/GetUsersByActivitiy/{clientUniueId}/{maxUsersCnt}",
                defaults: new { controller = "WeixinApi", action = "GetUsersByActivitiy", clientUniueId = "", maxUsersCnt = "" }
            );

            routes.MapRoute(
                name: "weixin7",
                url: "api/v0/WeixinApi/GetRankUsersByActivity/{clientUniueId}/{rankColumn}/{maxRankUsersCnt}",
                defaults: new { controller = "WeixinApi", action = "GetRankUsersByActivity", clientUniueId = "", rankColumn = "", maxRankUsersCnt = "" }
            );

            routes.MapRoute(
                name: "weixin8",
                url: "api/v0/WeixinApi/GetActivityInfo/{clientUniueId}",
                defaults: new { controller = "WeixinApi", action = "GetActivityInfo", clientUniueId = "" }
            );

            routes.MapRoute(
                name: "weixin9",
                url: "api/v0/WeixinApi/GetAwardsByActivity/{clientUniueId}",
                defaults: new { controller = "WeixinApi", action = "GetAwardsByActivity",clientUniueId = "" }
            );


            routes.MapRoute(
                name: "weixin10",
                url: "api/v0/WeixinApi/GetAwardsByActivity/{clientUniueId}",
                defaults: new { controller = "WeixinApi", action = "GetAwardsByActivity", gameId = "", appId = "", clientUniueId = "", activityId = "" }
            );

            routes.MapRoute(
                name: "weixin11",
                url: "api/v0/WeixinApi/WinAwardByActionId/{clientUniueId}/{actionId}/{awardId}",
                defaults: new { controller = "WeixinApi", action = "WinAwardByActionId", clientUniueId = "", actionId = "", awardId = "" }
            );

            routes.MapRoute(
                name: "weixin12",
                url: "api/v0/WeixinApi/GetWinAwardUsersByActivityId/{clientUniueId}/",
                defaults: new { controller = "WeixinApi", action = "GetWinAwardUsersByActivityId",clientUniueId = ""}
            );

            routes.MapRoute(
                name: "weixin13",
                url: "api/v0/WeixinApi/WinAwardByRandom/{clientUniueId}/{awardId}",
                defaults: new { controller = "WeixinApi", action = "WinAwardByRandom", clientUniueId = "",  awardId = "" }
            );

            routes.MapRoute(
                name: "weixin14",
                url: "api/v0/WeixinApi/WinAwardByUser/{clientUniueId}/{awardId}/{userId}",
                defaults: new { controller = "WeixinApi", action = "WinAwardByUser", clientUniueId = "", awardId = "", userId = "" }
            );

            routes.MapRoute(
                name: "weixin15",
                url: "api/v0/WeixinApi/GetAwardWhiteUserListByActivityId/{clientUniueId}/{activityId}",
                defaults: new { controller = "WeixinApi", action = "GetAwardWhiteUserListByActivityId", clientUniueId = ""}
            );

            routes.MapRoute(
                name: "weixin16",
                url: "api/v0/WeixinApi/GetScanQrCodeUsers/{qrCodeId}",
                defaults: new { controller = "WeixinApi", action = "GetScanQrCodeUsers", qrCodeId = "" }
            );

            routes.MapRoute(
                name: "weixin17",
                url: "api/v0/WeixinApi/GetUsersByActivitiyAndGame/",
                defaults: new { controller = "WeixinApi", action = "GetUsersByActivitiyAndGame" }
            );

            routes.MapRoute(
            name: "weixin18",
            url: "api/v0/WeixinApi/GetCanWinUsers/{awardId}/",
            defaults: new { controller = "WeixinApi", action = "GetCanWinUsers", awardId = "" }
            );
            routes.MapRoute(
            name: "weixin19",
            url: "api/v0/WeixinApi/GetChatMessage/{maxCount}",
            defaults: new { controller = "WeixinApi", action = "GetChatMessage", maxCount = "" }
            );
            #endregion

            #region v1
            routes.MapRoute(
                name: "activity1",
                url: "api/v1/WeixinApi/QrCode4Login/{clientUniueId}/{roomNo}",
                defaults: new { controller = "ActivityApi", action = "QrCode4Login", clientUniueId = "", roomNo="" }
            );
            routes.MapRoute(
                name: "activity2",
                url: "api/v1/WeixinApi/FindScanQrCodeUser/{qrCodeId}",
                defaults: new { controller = "ActivityApi", action = "FindScanQrCodeUser", qrCodeId = "" }
            );
            routes.MapRoute(
                name: "activity3",
                url: "api/v1/WeixinApi/PostDataByUser/{actionId}/{score}",
                defaults: new { controller = "ActivityApi", action = "PostDataByUser", actionId = "", score = "" }
            );
            routes.MapRoute(
                name: "activity4",
                url: "api/v1/WeixinApi/PostData/{clientUniueId}/{score}/{roomNo}",
                defaults: new { controller = "ActivityApi", action = "PostData",score = "", clientUniueId="", roomNo =""}
            );
            routes.MapRoute(
                name: "activity5",
                url: "api/WeixinApi/error/{errorid}",
                defaults: new { controller = "ActivityApi", action = "error", errorid = "" }
            );

            routes.MapRoute(
                name: "activity6",
                url: "api/v1/WeixinApi/GetUsersByActivitiy/{clientUniueId}/{maxUsersCnt}",
                defaults: new { controller = "ActivityApi", action = "GetUsersByActivitiy", clientUniueId = "", maxUsersCnt = "" }
            );

            routes.MapRoute(
                name: "activity7",
                url: "api/v1/WeixinApi/GetRankUsersByActivity/{clientUniueId}/{rankColumn}/{maxRankUsersCnt}",
                defaults: new { controller = "ActivityApi", action = "GetRankUsersByActivity", clientUniueId = "", rankColumn = "", maxRankUsersCnt = "" }
            );

            routes.MapRoute(
                name: "activity8",
                url: "api/v1/WeixinApi/GetActivityInfo/{clientUniueId}",
                defaults: new { controller = "ActivityApi", action = "GetActivityInfo", clientUniueId = ""}
            );

            routes.MapRoute(
                name: "activity9",
                url: "api/v1/WeixinApi/GetAwardsByActivity/{clientUniueId}",
                defaults: new { controller = "ActivityApi", action = "GetAwardsByActivity", clientUniueId = "" }
            );


            routes.MapRoute(
                name: "activity10",
                url: "api/v1/WeixinApi/GetAwardsByActivity/{clientUniueId}",
                defaults: new { controller = "ActivityApi", action = "GetAwardsByActivity", clientUniueId = "" }
            );

            routes.MapRoute(
                name: "activity11",
                url: "api/v1/WeixinApi/WinAwardByActionId/{clientUniueId}/{actionId}/{awardId}",
                defaults: new { controller = "ActivityApi", action = "WinAwardByActionId", clientUniueId = "", actionId = "", awardId = "" }
            );

            routes.MapRoute(
                name: "activity12",
                url: "api/v1/WeixinApi/GetWinAwardUsersByActivityId/{clientUniueId}",
                defaults: new { controller = "ActivityApi", action = "GetWinAwardUsersByActivityId", clientUniueId = ""}
            );

            routes.MapRoute(
                name: "activity13",
                url: "api/v1/WeixinApi/WinAwardByRandom/{clientUniueId}/{awardId}",
                defaults: new { controller = "ActivityApi", action = "WinAwardByRandom", clientUniueId = "",  awardId = "" }
            );

            routes.MapRoute(
                name: "activity14",
                url: "api/v1/WeixinApi/WinAwardByUser/{clientUniueId}/{awardId}/{userId}",
                defaults: new { controller = "ActivityApi", action = "WinAwardByUser", clientUniueId = "", awardId = "", userId = "" }
            );

            routes.MapRoute(
                name: "activity15",
                url: "api/v1/WeixinApi/GetAwardWhiteUserListByActivityId/{clientUniueId}",
                defaults: new { controller = "ActivityApi", action = "GetAwardWhiteUserListByActivityId", clientUniueId = "" }
            );

            routes.MapRoute(
                name: "activity16",
                url: "api/v1/WeixinApi/GetScanQrCodeUsers/{qrCodeId}",
                defaults: new { controller = "ActivityApi", action = "GetScanQrCodeUsers", qrCodeId = "" }
            );

            routes.MapRoute(
                name: "activity17",
                url: "api/v1/WeixinApi/GetUsersByActivitiyAndGame/",
                defaults: new { controller = "ActivityApi", action = "GetUsersByActivitiyAndGame"}
            );

            routes.MapRoute(
            name: "activity18",
            url: "api/v1/WeixinApi/GetCanWinUsers/{awardId}/",
            defaults: new { controller = "ActivityApi", action = "GetCanWinUsers", awardId = "" }
            );
            routes.MapRoute(
            name: "activity19",
            url: "api/v1/WeixinApi/GetChatMessage/{maxCount}",
            defaults: new { controller = "ActivityApi", action = "GetChatMessage", maxCount = "" }
            );
            routes.MapRoute(
                name: "activity20",
                url: "api/v1/WeixinApi/UpdateActivityGame/{isGameStarted}/{gameOverTime}",
                defaults: new { controller = "ActivityApi", action = "UpdateActivityGame", isGameStarted = "", gameOverTime =""}
            );


            routes.MapRoute(
                name: "activity21",
                url: "api/v1/WeixinApi/GetActivityGameInfo",
                defaults: new { controller = "ActivityApi", action = "GetActivityGameInfo"}
            );
            routes.MapRoute(
                name: "activity22",
                url: "api/v1/WeixinApi/FindWeixinUser/{openId}",
                defaults: new { controller = "WeixinApi", action = "FindWeixinUser", openId = "" }
            );


            routes.MapRoute(
                name: "activity23",
                url: "api/v1/WeixinApi/GetGameInfo",
                defaults: new { controller = "ActivityApi", action = "GetGameInfo"}
            );




            #endregion

            //routes.Add(new Route("showqrcode.ashx", new QrcodeRouteHandler()));

            //Route qrcodRoute = new Route("showqrcode.ashx", new QrcodeRouteHandler());//利用IRouteHandler实现请求拦截与转发  
            //qrcodRoute.RouteExistingFiles = false;//设置检查文件是否存在  
            //routes.Add(qrcodRoute);//加入路由规则  


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Game", id = UrlParameter.Optional }
            );

            

        }
    }
}
