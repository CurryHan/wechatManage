using Sensing.Data;
using Senparc.Weixin.MP.Entities;
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
using Senparc.Weixin.MP.CommonAPIs;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Web.Security;

namespace SensingCloud.Controllers
{
    public class ReceiveController : SensingBaseController
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

        /// <summary>
        /// 绑定公众号的回掉函数
        /// </summary>
        /// <param name="auth_code"></param>
        /// <param name="expires_in"></param>
        /// <returns></returns>
        public ActionResult CallBack(string auth_code, string expires_in)
        {
            return null;
            //try
            //{
            //    logger.Debug(Request.RawUrl);
            //    var openPlat = _weixinMPSvc.GetOpenPlatformByAppId(ConstConfig.ComponentAppID);
            //    if (openPlat == null)
            //    {
            //        logger.Debug("WeixinOpenPlatforms Cannot find any account, please add it firstly.");
            //        return Content("感知互动平台异常");
            //    }
            //    string sAppID = openPlat.ComponentAppID;// ConfigurationManager.AppSettings["ComponentAppID"];
            //    logger.Debug("sAppID:" + sAppID);
            //    logger.Debug("auth_code:" + auth_code);

            //    QueryAuthResult result = null;
            //    try
            //    {
            //        result = ComponentContainer.GetQueryAuthResult(sAppID, auth_code);
            //    }
            //    catch (Exception ex)
            //    {
            //        logger.Debug($"Cannot get the QueryAuthResult with sAppID = {sAppID},auth_code={auth_code}", ex);
            //        return Content("感知互动平台异常");
            //    }

            //    string authorizerAppID = result.authorization_info.authorizer_appid;
            //    string refleshToken = result.authorization_info.authorizer_refresh_token;
            //    logger.Debug($"authorizerAppID:{authorizerAppID},refleshToken={refleshToken}");
            //    logger.Debug(result.authorization_info);
            //    GetAuthorizerInfoResult authorizerInfoResult = AuthorizerContainer.GetAuthorizerInfoResult(openPlat.ComponentAppID, authorizerAppID);
            //    var existingPublicAcc = db.WeixinPublicAccountInfos.Where(p => p.WeixinAppID == authorizerAppID && p.Deleted == false).ToList();
            //    WeixinPublicAccountInfo authorizer = null;
            //    if (existingPublicAcc.Count > 0)
            //    {
            //        authorizer = existingPublicAcc[0];
            //        logger.Debug("existingPublicAcc is not  null");
            //    }
            //    else
            //    {
            //        logger.Debug("existingPublicAcc is null");
            //    }

            //    var user = db.Users.First(u => u.Email == User.Identity.Name || u.PhoneNumber == User.Identity.Name || u.UserName == User.Identity.Name);
            //    int groupID = 0;
            //    if (user != null)
            //    {
            //        groupID = user.GroupId.Value;
            //    }
            //    if (authorizer == null)
            //    {
            //        WeixinPublicAccountInfo obj = new WeixinPublicAccountInfo();
            //        obj.Alias = authorizerInfoResult.authorizer_info.alias;
            //        obj.Business_info = authorizerInfoResult.authorizer_info.business_info;
            //        obj.Head_img = authorizerInfoResult.authorizer_info.head_img;
            //        obj.NickName = authorizerInfoResult.authorizer_info.nick_name;
            //        obj.Qrcode_url = authorizerInfoResult.authorizer_info.qrcode_url;
            //        obj.Service_type_info = authorizerInfoResult.authorizer_info.service_type_info.id;
            //        obj.Verify_type_info = authorizerInfoResult.authorizer_info.verify_type_info.id;
            //        obj.WeixinAppID = authorizerAppID;
            //        obj.Authorizer_access_token = result.authorization_info.authorizer_access_token;
            //        obj.Authorizer_refresh_token = result.authorization_info.authorizer_refresh_token;
            //        obj.Expires_in = result.authorization_info.expires_in;
            //        obj.Func_infos = new List<WeixinFuncscopeCategoryItem>();
            //        foreach (var item in result.authorization_info.func_info)
            //        {
            //            obj.Func_infos.Add(new WeixinFuncscopeCategoryItem { WeixinPublicAccountInfoID = obj.Id, funcscope_category = (WeixinFuncscopeCategory)((int)item.funcscope_category.id), LastUpdated = DateTime.Now, Created = DateTime.Now });
            //        }
            //        obj.LastUpdated = DateTime.Now;
            //        obj.Created = DateTime.Now;
            //        obj.Creator = User.Identity.Name;
            //        obj.Updater = User.Identity.Name;
            //        obj.GroupID = groupID;
            //        obj.Name = user.Group.DisplayName;
            //        obj.AuthorizationTime = DateTime.Now;
            //        obj.ExpiredTime = DateTime.Now.AddYears(1);
            //        obj.Status = WeixinAuthStatus.Authrozied;
            //        db.Entry(obj).State = System.Data.Entity.EntityState.Added;
            //    }
            //    else
            //    {
            //        logger.Debug("update existingPublicAcc authorizer.Id is " + authorizer.Id.ToString());
            //        var funcs = db.WeixinFuncscopeCategoryItems.Where(p => p.WeixinPublicAccountInfoID == 1).ToList();
            //        if (funcs.Count > 0)
            //        {
            //            foreach (var func in funcs)
            //            {
            //                db.Entry(func).State = System.Data.Entity.EntityState.Deleted;
            //            }
            //            db.SaveChanges();
            //        }

            //        authorizer.Authorizer_access_token = result.authorization_info.authorizer_access_token;
            //        authorizer.Authorizer_refresh_token = result.authorization_info.authorizer_refresh_token;
            //        logger.Debug($"result.authorization_info.authorizer_refresh_token is {result.authorization_info.authorizer_refresh_token}");
            //        authorizer.Expires_in = result.authorization_info.expires_in;
            //        authorizer.Func_infos = new List<WeixinFuncscopeCategoryItem>();
            //        authorizer.LastUpdated = DateTime.Now;
            //        authorizer.Updater = User.Identity.Name;
            //        authorizer.AuthorizationTime = DateTime.Now;
            //        authorizer.Status = WeixinAuthStatus.Authrozied;
            //        foreach (var item in result.authorization_info.func_info)
            //        {
            //            authorizer.Func_infos.Add(new WeixinFuncscopeCategoryItem { WeixinPublicAccountInfoID = authorizer.Id, funcscope_category = (WeixinFuncscopeCategory)((int)item.funcscope_category.id), LastUpdated = DateTime.Now, Created = DateTime.Now });
            //        }
            //        db.Entry(authorizer).State = System.Data.Entity.EntityState.Modified;
            //    }
            //    db.SaveChanges();
            //    return RedirectToAction("Index", "mp");
            //    //return Content("success");
            //}
            //catch (Exception ex)
            //{
            //    logger.Debug("CallBack system error:", ex);
            //    return Content("failed");
            //}
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
            if (tmpStr==signature)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 授权事件接收
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SysMessage()
        {
            string signature = HttpContext.Request.QueryString["signature"];
            string timestamp = HttpContext.Request.QueryString["timestamp"];
            string nonce = HttpContext.Request.QueryString["nonce"];
            string echostr= HttpContext.Request.QueryString["echostr"];
            if (Validate(signature, timestamp, nonce)) {
                return Content(echostr);
            }
            return Content("fail");
        }


        /// <summary>
        /// 代替公众号接受事件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="postModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EventMsg(string id, Senparc.Weixin.MP.Entities.Request.PostModel postModel)//(string APPID, string signature, string timestamp, string nonce, string encrypt_type, string msg_signature)
        {
            return null;
            //try
            //{
            //    //TODO:need to cache.
            //    var openPlatfrom = _weixinMPSvc.GetOpenPlatformByAppId(ConstConfig.ComponentAppID);
            //    string sToken = openPlatfrom.ComponentToken;
            //    string sAppID = openPlatfrom.ComponentAppID;
            //    string sEncodingAESKey = openPlatfrom.ComponentKey;

            //    postModel.Token = sToken;
            //    postModel.EncodingAESKey = sEncodingAESKey; //根据自己后台的设置保持一致
            //    postModel.AppId = sAppID; //根据自己后台的设置保持一致


            //    string sMsg = "";  //解析之后的明文
            //    int ret = 0;
            //    var postDataDocument = Senparc.Weixin.XmlUtility.XmlUtility.Convert(Request.InputStream);

            //    WXBizMsgCrypt wxcpt = new WXBizMsgCrypt(sToken, sEncodingAESKey, sAppID);
            //    ret = wxcpt.DecryptMsg(postModel.Msg_Signature, postModel.Timestamp, postModel.Nonce, postDataDocument.ToString(), ref sMsg);
            //    if (sMsg.ToLower().Contains("subscribe"))
            //    {
            //        logger.Debug("解析之后的加关注的明文: " + sMsg);
            //    }

            //    SensingMessageHandler messageHandler = new SensingMessageHandler(Request.InputStream, postModel, id, 10);

            //    //微信全网发布代码 开始
            //    if (id == "wx570bc396a51b8ff8")
            //    {
            //        logger.Debug("接受信息的公众号_appid is:" + id);
            //        logger.Debug($"messageHandler.RequestMessage type is {messageHandler.RequestMessage.GetType().ToString()}");
            //        logger.Debug("解析之后的明文: " + sMsg);
            //        //messageHandler.WeixinContext.GetMessageContext(messageHandler.RequestMessage).StorageData = id;
            //        messageHandler.Execute();//执行微信处理过程
            //        if (messageHandler.ResponseDocument != null)
            //        {
            //            logger.Debug("ResponseDocument: " + messageHandler.ResponseDocument.ToString());
            //            return Content(messageHandler.FinalResponseDocument.ToString());
            //        }
            //        else
            //        {
            //            logger.Debug("ResponseDocument is Empty ");
            //            return Content("");
            //        }
            //    }
            //    //微信全网发布代码 结束

            //    Session[ConstConfig.SessionKeyOpenId] = messageHandler.RequestMessage.FromUserName;

            //    if (messageHandler.RequestMessage.MsgType == Senparc.Weixin.MP.RequestMsgType.Event)
            //    {
            //        if (messageHandler.RequestMessage is RequestMessageEvent_Scan
            //            || messageHandler.RequestMessage is RequestMessageEvent_Subscribe
            //            || messageHandler.RequestMessage is RequestMessageEvent_Unsubscribe)
            //        //|| messageHandler.RequestMessage is RequestMessageEvent_TemplateSendJobFinish)
            //        {
            //            logger.Debug("接受信息的公众号_appid is:" + id);
            //            logger.Debug($"messageHandler.RequestMessage type is {messageHandler.RequestMessage.GetType().ToString()}");
            //            logger.Debug("解析之后的明文: " + sMsg);

            //            if (messageHandler.RequestMessage is RequestMessageEvent_Scan)
            //            {
            //                var scanRequestMessage = messageHandler.RequestMessage as RequestMessageEvent_Scan;
            //                var qrcode = scanRequestMessage.EventKey;

            //                var isValidQrcode = WeChatService.IsFormatValidQrcode(qrcode);
            //                if (!isValidQrcode)
            //                {
            //                    return Content("");
            //                }
            //                logger.Debug("messageHandler.Execute()");
            //                messageHandler.Execute();//执行微信处理过程
            //                if (messageHandler.ResponseDocument != null)
            //                {
            //                    logger.Debug("ResponseDocument: " + messageHandler.ResponseDocument.ToString());
            //                    return Content(messageHandler.FinalResponseDocument.ToString());
            //                }
            //                else
            //                {
            //                    logger.Debug("ResponseDocument is Empty ");
            //                    return Content("");
            //                }
            //            }

            //            if (messageHandler.RequestMessage is RequestMessageEvent_Subscribe)
            //            {
            //                messageHandler.Execute();//执行微信处理过程
            //                if (messageHandler.ResponseDocument != null)
            //                {
            //                    logger.Debug("ResponseDocument: " + messageHandler.ResponseDocument.ToString());
            //                    return Content(messageHandler.FinalResponseDocument.ToString());
            //                }
            //                else
            //                {
            //                    logger.Debug("ResponseDocument is Empty ");
            //                    return Content("");
            //                }
            //            }

            //            if (messageHandler.RequestMessage is RequestMessageEvent_Unsubscribe)
            //            {
            //                messageHandler.Execute();//执行微信处理过程

            //                if (messageHandler.ResponseDocument != null)
            //                {
            //                    logger.Debug("ResponseDocument: " + messageHandler.ResponseDocument.ToString());
            //                    return Content(messageHandler.FinalResponseDocument.ToString());
            //                }
            //                else
            //                {
            //                    logger.Debug("ResponseDocument is Empty ");
            //                    return Content("");
            //                }
            //            }
            //        }

            //        if (messageHandler.RequestMessage is RequestMessageEvent_TemplateSendJobFinish)
            //        {
            //            var templateSendJob = messageHandler.RequestMessage as RequestMessageEvent_TemplateSendJobFinish;
            //            var awardUser = db.WeixinUserAwards.FirstOrDefault(p => p.Msgid == templateSendJob.MsgID);
            //            if (awardUser != null)
            //            {
            //                if (templateSendJob.Status == "success")
            //                {
            //                    awardUser.IsNotified = true;
            //                }
            //                else
            //                {
            //                    awardUser.IsNotified = false;
            //                    awardUser.Description = templateSendJob.Status;
            //                }
            //                db.Entry(awardUser).State = System.Data.Entity.EntityState.Modified;
            //                db.SaveChanges();
            //            }

            //        }

            //    }
            //    else
            //    {
            //        return Content("");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    logger.Error("Eventmsg system error", ex);
            //}
            //return Content("success");
        }


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


}