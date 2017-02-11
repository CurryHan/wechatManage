using Sensing.Data;
using SensingCloud.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml.Linq;
using SensingCloud.Authorization;
using Sensing.Entities.Users;
using Sensing.Entities;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Web.Security;
using System.Xml;

namespace SensingCloud.Controllers
{
    public class ReceiveController : LanguageController
    {
        //private static readonly IBizLogger logger = ServerLogFactory.GetLogger(typeof(this));
        private SensingSiteDbContext db = new SensingSiteDbContext();

        public ReceiveController()
        {
        }


        // GET: Receive
        public ActionResult Index()
        {
            return View();
        }


        private bool Validate(string signature, string timestamp, string nonce)
        {
            //1. 将token、timestamp、nonce三个参数进行字典序排序  
            string token = "troncelltoken";
            string[] arrTmp = { token, timestamp, nonce };
            Array.Sort(arrTmp);
            //2.将三个参数字符串拼接成一个字符串进行sha1加密  
            string tmpStr = string.Join("", arrTmp);
            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            tmpStr = tmpStr.ToLower();
            //3. 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信  
            logger.Debug(tmpStr == signature);
            if (tmpStr == signature)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 授权事件接收
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        public ActionResult SysMessage()
        {
            string signature = HttpContext.Request.QueryString["signature"];
            string timestamp = HttpContext.Request.QueryString["timestamp"];
            string nonce = HttpContext.Request.QueryString["nonce"];
            string echostr = HttpContext.Request.QueryString["echostr"];
            if (HttpContext.Request.HttpMethod == "GET")
            {
                if (Validate(signature, timestamp, nonce))
                {
                    return Content(echostr);
                }
                return Content("fail");
            }
            else
            {
                string postString = string.Empty;
                using (Stream stream = HttpContext.Request.InputStream)
                {
                    Byte[] postBytes = new Byte[stream.Length];
                    stream.Read(postBytes, 0, (Int32)stream.Length);
                    postString = Encoding.UTF8.GetString(postBytes);
                    Handle(postString);
                }
                return Content("success");
            }


        }

        private void Handle(string postStr)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(postStr);
            string responseContent = EventHandle(doc);
            HttpContext.Response.ContentEncoding = Encoding.UTF8;
            HttpContext.Response.Write(responseContent);
        }

        private string EventHandle(XmlDocument xmldoc)
        {
            string responseContent = "";
            XmlNode Event = xmldoc.SelectSingleNode("/xml/Event");
            XmlNode EventKey = xmldoc.SelectSingleNode("/xml/EventKey");
            XmlNode ToUserName = xmldoc.SelectSingleNode("/xml/ToUserName");
            XmlNode FromUserName = xmldoc.SelectSingleNode("/xml/FromUserName");
            if (Event != null)
            {
                //菜单单击事件
                if (Event.InnerText.Equals("CLICK"))
                {
                    var list=db.Menus.Where(m => m.Deleted == false && m.MediaId != null).ToList();
                    int count = list.Count();
                    StringBuilder sb = new StringBuilder();
                    foreach (var item in list)
                    {
                        string picUrl = string.Empty;
                        var media = db.Medias.Where(m => m.Deleted == false && m.MediaKey == item.Media.Thumb_MediaId).FirstOrDefault();
                        if (media != null)
                        {
                            picUrl = media.Url;
                        }
                        string newsItem = string.Format(ReplyType.Message_News_Item, item.Media.Title, item.Media.Title, picUrl, item.Media.Url);
                        sb.Append(newsItem);
                    }
                    if (EventKey.InnerText.Equals("V1001_TODAY_NEWS"))//click_three
                    {
                        responseContent = string.Format(ReplyType.Message_News_Main,
                            FromUserName.InnerText,
                            ToUserName.InnerText,
                            DateTime.Now.Ticks,
                            count.ToString(),
                             sb.ToString());
                    }
                }
            }
            return responseContent;
        }



        //[HttpPost]
        //public ActionResult SysMessage()
        //{
        //    logger.Debug("111");
        //    return Content("ok");
        //}




        /// <summary>
        /// get post data
        /// </summary>
        /// <returns></returns>
        private string GetPost()
        {
            try
            {
                System.IO.Stream s = Request.InputStream;
                int count = 0;
                byte[] buffer = new byte[s.Length];
                StringBuilder builder = new StringBuilder();
                while ((count = s.Read(buffer, 0, buffer.Length)) > 0)
                {
                    builder.Append(Request.ContentEncoding.GetString(buffer, 0, count));
                }
                s.Flush();
                s.Close();
                s.Dispose();
                return builder.ToString();
            }
            catch (Exception ex)
            {
                logger.Debug("GetPost system error" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }


        //[GroupAuthorize(Roles = RoleString.Admin, Groups = GroupTypeString.SuperLevel)]
        //private ActionResult CreateMenu()
        //{
        //    try
        //    {
        //        var authorizers = db.WeixinPublicAccountInfos.Where(p => p.Deleted == false).ToList();
        //        foreach (var item in authorizers)
        //        {
        //            FileStream fs1 = new FileStream(System.AppDomain.CurrentDomain.BaseDirectory + @"WeixinResource\" + item.WeixinAppID + @"\menu.txt", FileMode.Open);
        //            StreamReader sr = new StreamReader(fs1, Encoding.GetEncoding("GBK"));
        //            string menu = sr.ReadToEnd();
        //            sr.Close();
        //            fs1.Close();
        //            GetMenuResult result = CommonApi.GetMenuFromJson(menu);
        //            CommonApi.CreateMenu(item.Authorizer_access_token, result.menu);

        //            //bool menuResult = CommonApi.CreateMenu(item.Authorizer_access_token, menu);
        //            //if (!menuResult)
        //            //{ return Content("failed"); }

        //        }
        //        return Content("success");
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Debug("system error CreateMenu:" + ex.Message + ex.StackTrace);
        //        return Content("system error");
        //    }

        //}

        //[GroupAuthorize(Roles = RoleString.Admin, Groups = GroupTypeString.SuperLevel)]
        //private ActionResult DeleteMenu()
        //{
        //    try
        //    {
        //        var authorizers = db.WeixinPublicAccountInfos.Where(p => p.Deleted == false).ToList();
        //        foreach (var item in authorizers)
        //        {
        //            CommonApi.DeleteMenu(item.Authorizer_access_token);
        //        }

        //        return Content("success");
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Debug("system error DeleteMenu:" + ex.Message + ex.StackTrace);
        //        return Content("failed");
        //    }
        //}
    }

    public static class ReplyType
    {
        public static string Message_News_Main
        {
            get
            {
                return @"<xml>
                             <ToUserName><![CDATA[{0}]]></ToUserName>
                             <FromUserName><![CDATA[{1}]]></FromUserName>
                             <CreateTime>{2}</CreateTime>
                             <MsgType><![CDATA[news]]></MsgType>
                             <ArticleCount>{3}</ArticleCount>
                             <Articles>
                             {4}
                             </Articles>
                             </xml> ";
            }
        }


        public static string Message_News_Item
        {
            get
            {
                return @"<item>
                             <Title><![CDATA[{0}]]></Title> 
                             <Description><![CDATA[{1}]]></Description>
                             <PicUrl><![CDATA[{2}]]></PicUrl>
                             <Url><![CDATA[{3}]]></Url>
                             </item>";
            }
        }
    }
    public class messageHelp
    {


        public string ReturnMessage(string postStr)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(postStr);
            return EventHandle(doc);
        }

        public string EventHandle(XmlDocument xmldoc)
        {
            string responseContent = "";
            XmlNode Event = xmldoc.SelectSingleNode("/xml/Event");
            XmlNode EventKey = xmldoc.SelectSingleNode("/xml/EventKey");
            XmlNode ToUserName = xmldoc.SelectSingleNode("/xml/ToUserName");
            XmlNode FromUserName = xmldoc.SelectSingleNode("/xml/FromUserName");
            if (Event != null)
            {
                //菜单单击事件
                if (Event.InnerText.Equals("CLICK"))
                {

                    if (EventKey.InnerText.Equals("V1001_TODAY_NEWS"))//click_three
                    {
                        responseContent = string.Format(ReplyType.Message_News_Main,
                            FromUserName.InnerText,
                            ToUserName.InnerText,
                            DateTime.Now.Ticks,
                            "5", 
                             string.Format(ReplyType.Message_News_Item, "拒绝平庸！创思感知｜祝贺 3W•创客无锡年会圆满成功", "拒绝平庸！创思感知｜祝贺 3W•创客无锡年会圆满成功",
                             "http://mmbiz.qpic.cn/mmbiz_jpg/dibdpzTKZf234cpTrChLiacnnlvFy0Sc0zjVDrWpMd9D8659CFFiaPA25bKDR3xOq7gJNJl5mPyoLU6zUQyX5Q74g/0?wx_fmt=jpeg",
                             "http://mp.weixin.qq.com/s?__biz=MjM5NTU4Nzc5NA==&mid=502286638&idx=1&sn=4690ce0e8f474ba209f4d6532fe365c6&chksm=3ef2da93098553852605c3f28971b704980065226c7855ab2cd4795655ab5e5e22f451fb9ac5#rd") + string.Format(ReplyType.Message_News_Item, "拒绝平庸！创思感知｜祝贺 3W•创客无锡年会圆满成功", "拒绝平庸！创思感知｜祝贺 3W•创客无锡年会圆满成功",
                             "http://mmbiz.qpic.cn/mmbiz_jpg/dibdpzTKZf234cpTrChLiacnnlvFy0Sc0zjVDrWpMd9D8659CFFiaPA25bKDR3xOq7gJNJl5mPyoLU6zUQyX5Q74g/0?wx_fmt=jpeg",
                             "http://mp.weixin.qq.com/s?__biz=MjM5NTU4Nzc5NA==&mid=502286638&idx=1&sn=4690ce0e8f474ba209f4d6532fe365c6&chksm=3ef2da93098553852605c3f28971b704980065226c7855ab2cd4795655ab5e5e22f451fb9ac5#rd"));
                    }
                }
            }
            return responseContent;
        }

    }

    //public class BaseMsg
    //{
    //    /// <summary>
    //    /// 发送者标识
    //    /// </summary>
    //    public string FromUser { get; set; }
    //    /// <summary>
    //    /// 消息表示。普通消息时，为msgid，事件消息时，为事件的创建时间
    //    /// </summary>
    //    public string MsgFlag { get; set; }
    //    /// <summary>
    //    /// 添加到队列的时间
    //    /// </summary>
    //    public DateTime CreateTime { get; set; }
    //}

    //public class MessageFactory
    //{
    //    private static List<BaseMsg> _queue;
    //    public static BaseMessage CreateMessage(string xml)
    //    {
    //        if (_queue == null)
    //        {
    //            _queue = new List<BaseMsg>();
    //        }
    //        else if (_queue.Count >= 50)
    //        {
    //            _queue = _queue.Where(q => { return q.CreateTime.AddSeconds(20) > DateTime.Now; }).ToList();//保留20秒内未响应的消息
    //        }
    //        XElement xdoc = XElement.Parse(xml);
    //        var msgtype = xdoc.Element("MsgType").Value.ToUpper();
    //        var FromUserName = xdoc.Element("FromUserName").Value;
    //        var MsgId = xdoc.Element("MsgId").Value;
    //        var CreateTime = xdoc.Element("CreateTime").Value;
    //        MsgType type = (MsgType)Enum.Parse(typeof(MsgType), msgtype);
    //        if (type != MsgType.EVENT)
    //        {
    //            if (_queue.FirstOrDefault(m => { return m.MsgFlag == MsgId; }) == null)
    //            {
    //                _queue.Add(new BaseMsg
    //                {
    //                    CreateTime = DateTime.Now,
    //                    FromUser = FromUserName,
    //                    MsgFlag = MsgId
    //                });
    //            }
    //            else
    //            {
    //                return null;
    //            }

    //        }
    //        else
    //        {
    //            if (_queue.FirstOrDefault(m => { return m.MsgFlag == CreateTime; }) == null)
    //            {
    //                _queue.Add(new BaseMsg
    //                {
    //                    CreateTime = DateTime.Now,
    //                    FromUser = FromUserName,
    //                    MsgFlag = CreateTime
    //                });
    //            }
    //            else
    //            {
    //                return null;
    //            }
    //        }
    //        switch (type)
    //        {
    //            case MsgType.TEXT: return Utils.ConvertObj<TextMessage>(xml);
    //            case MsgType.IMAGE: return Utils.ConvertObj<ImgMessage>(xml);
    //            case MsgType.VIDEO: return Utils.ConvertObj<VideoMessage>(xml);
    //            case MsgType.VOICE: return Utils.ConvertObj<VoiceMessage>(xml);
    //            case MsgType.LINK:
    //                return Utils.ConvertObj<LinkMessage>(xml);
    //            case MsgType.LOCATION:
    //                return Utils.ConvertObj<LocationMessage>(xml);
    //            case MsgType.EVENT://事件类型
    //                {
    //                    var eventtype = (Event)Enum.Parse(typeof(Event), xdoc.Element("Event").Value.ToUpper());
    //                    switch (eventtype)
    //                    {
    //                        case Event.CLICK:
    //                            return Utils.ConvertObj<NormalMenuEventMessage>(xml);
    //                        case Event.VIEW: return Utils.ConvertObj<NormalMenuEventMessage>(xml);
    //                        case Event.LOCATION: return Utils.ConvertObj<LocationEventMessage>(xml);
    //                        case Event.LOCATION_SELECT: return Utils.ConvertObj<LocationMenuEventMessage>(xml);
    //                        case Event.SCAN: return Utils.ConvertObj<ScanEventMessage>(xml);
    //                        case Event.SUBSCRIBE: return Utils.ConvertObj<SubEventMessage>(xml);
    //                        case Event.UNSUBSCRIBE: return Utils.ConvertObj<SubEventMessage>(xml);
    //                        case Event.SCANCODE_WAITMSG: return Utils.ConvertObj<ScanMenuEventMessage>(xml);
    //                        default:
    //                            return Utils.ConvertObj<EventMessage>(xml);
    //                    }
    //                }
    //                break;
    //            default:
    //                return Utils.ConvertObj<BaseMessage>(xml);
    //        }
    //    }
    //}
    ///// <summary>
    ///// 事件类型枚举
    ///// </summary>
    //public enum Event
    //{
    //    /// <summary>
    //    /// 非事件类型
    //    /// </summary>
    //    NOEVENT,
    //    /// <summary>
    //    /// 订阅
    //    /// </summary>
    //    SUBSCRIBE,
    //    /// <summary>
    //    /// 取消订阅
    //    /// </summary>
    //    UNSUBSCRIBE,
    //    /// <summary>
    //    /// 扫描带参数的二维码
    //    /// </summary>
    //    SCAN,
    //    /// <summary>
    //    /// 地理位置
    //    /// </summary>
    //    LOCATION,
    //    /// <summary>
    //    /// 单击按钮
    //    /// </summary>
    //    CLICK,
    //    /// <summary>
    //    /// 链接按钮
    //    /// </summary>
    //    VIEW,
    //    /// <summary>
    //    /// 扫码推事件
    //    /// </summary>
    //    SCANCODE_PUSH,
    //    /// <summary>
    //    /// 扫码推事件且弹出“消息接收中”提示框
    //    /// </summary>
    //    SCANCODE_WAITMSG,
    //    /// <summary>
    //    /// 弹出系统拍照发图
    //    /// </summary>
    //    PIC_SYSPHOTO,
    //    /// <summary>
    //    /// 弹出拍照或者相册发图
    //    /// </summary>
    //    PIC_PHOTO_OR_ALBUM,
    //    /// <summary>
    //    /// 弹出微信相册发图器
    //    /// </summary>
    //    PIC_WEIXIN,
    //    /// <summary>
    //    /// 弹出地理位置选择器
    //    /// </summary>
    //    LOCATION_SELECT,
    //    /// <summary>
    //    /// 模板消息推送
    //    /// </summary>
    //    TEMPLATESENDJOBFINISH
    //}


}