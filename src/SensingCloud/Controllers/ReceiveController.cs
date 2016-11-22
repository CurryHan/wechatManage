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