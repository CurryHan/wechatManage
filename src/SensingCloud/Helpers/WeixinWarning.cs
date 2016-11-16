using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SensingCloud.Helpers
{
    public  class WeixinWarning
    {
        //public static Dictionary<int, string> WeixinWarningMessage = new Dictionary<int, string>();

        public const string SurpassGameMaxUserMsg = "啊呀，好像您扫的慢了，请稍后等待下一轮吧";
        public const string SurpassActivityMaxUserMsg = "啊呀，已超过了最大游戏人数，请等待下一轮";
        public const string ActivityGameStopped = "啊呀，游戏还没有开始，请稍后等待下一下";
        public const string ActivityGameStarted= "啊呀，游戏已经开始啦，请稍后等待下一下";
        public const string QrCodeNotSupported = "二维码不是感知互动第三方生成的";
        public const string QrCodeNotExisted = "二维码不是感知互动第三方生成的";

        //static WeixinWarning()
        //{
        //    WeixinWarningMessage.Add((int)WeixinWarningEnum.GameSupportUser, );
        //    WeixinWarningMessage.Add((int)WeixinWarningEnum.ActivitySuppoertUser, "六一快乐！小朋友大朋友都在排队哦，耐心等一下，还有下一轮！");
        //    WeixinWarningMessage.Add((int)WeixinWarningEnum.ActivityGameStopped, "六一快乐！小朋友大朋友都在排队哦，耐心等一下，还有下一轮！");
        //}
    }

    //public enum WeixinWarningEnum
    //{
    //    GameSupportUser = -1,
    //    ActivitySuppoertUser = -2,
    //    ActivityGameStopped = -3
    //}
}