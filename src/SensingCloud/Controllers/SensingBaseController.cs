using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using SensingCloud.Helpers;
using Sensing.Entities;
using Sensing.Data;
using LogService;
using Sensing.Entities.Users;
using System.Data.Entity;
using SensingCloud.Apis;
using Top.Api.Request;
using Top.Api;
using Top.Api.Response;

namespace SensingCloud.Controllers
{
    public class SensingBaseController : Controller
    {
        protected SensingSiteDbContext dbContext = new SensingSiteDbContext();
        protected static readonly IBizLogger logger = ServerLogFactory.GetLogger(typeof(SensingBaseController));

        public const string AppKey = "23492108";
        public const string Secret = "15eb4df38df0b913328730418b57b101";
        public const string taobao_Auth_Query = "{0}/Product/AuthorizeCallBack";
        public const string taobao_AccessToken_Query = "{0}/Product/AccessTokenCallBack";
        public const string platformHost = "139.224.15.28:252";
        public const string ProductQrCodeUrl = "http://h5.m.taobao.com/alizhanggui/buyer-goto.html?linkType=item&storeId={0}&sellerId={1}&itemId={2}&appType=zhinengping&displayType=zhinengping";

        public const string server_Url = "http://gw.api.taobao.com/router/rest";
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = null;

            // Attempt to read the culture cookie from Request
            HttpCookie cultureCookie = Request.Cookies["_culture"];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ?
                        Request.UserLanguages[0] :  // obtain it from HTTP header AcceptLanguages
                        null;
            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe

            // Modify current thread's cultures           
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            return base.BeginExecuteCore(callback, state);
        }

        /// <summary>
        /// 保存上传文件
        /// </summary>
        /// <param name="name">name 表单字段</param>
        /// <param name="directory">示例/upload/avatars/</param>
        /// <returns></returns>


        protected string SaveUploadedFile(HttpPostedFileBase fileBase, string directory, string prefix = "")
        {
            if (fileBase != null && fileBase.ContentLength > 0)
            {
                Random r = new Random();
                var extension = fileBase.FileName.Substring(fileBase.FileName.LastIndexOf('.'));
                var fileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + r.Next(0, 9999).ToString() + extension;
                var mappedDirectory = Server.MapPath(directory);
                var path = Path.Combine(mappedDirectory, fileName);
                if (!Directory.Exists(mappedDirectory))
                {
                    Directory.CreateDirectory(mappedDirectory);
                }
                fileBase.SaveAs(path);
                Thread.Sleep(10);
                return directory + fileName;
            }
            return "";
        }

        /// <summary>
        /// 保存上传文件
        /// </summary>
        /// <param name="name">name 表单字段</param>
        /// <param name="directory">示例/upload/avatars/</param>
        /// <returns></returns>
        //        protected string SaveUploadedFile(HttpPostedFileBase fileBase, string directory, string prefix = "")
        //        {
        //            if (fileBase != null && fileBase.ContentLength > 0)
        //            {
        //                Random r = new Random();
        //                var extension = fileBase.FileName.Substring(fileBase.FileName.LastIndexOf('.'));
        //                var fileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + r.Next(0, 9999).ToString() + extension;

        //#if USE_OOS
        //                var filePath =  (directory + fileName).TrimStart(new char[] {'/' });
        //                logger.Debug($"SaveUploadedFile name is: {directory + fileName}");
        //                var absolutePath = OSSHelper.UploadFile(fileBase.InputStream, filePath);
        //                Thread.Sleep(10);
        //                return absolutePath;

        //#endif
        //#if Use_Local
        //                 var mappedDirectory = Server.MapPath(directory);
        //                var path = Path.Combine(mappedDirectory, fileName);
        //                if (!Directory.Exists(mappedDirectory))
        //                {
        //                    Directory.CreateDirectory(mappedDirectory);
        //                }
        //                fileBase.SaveAs(path);
        //                logger.Debug($"SaveUploadedFile name is: {directory + fileName}");
        //                Thread.Sleep(10);
        //                return directory + fileName;
        //#endif
        //            }
        //            return "";
        //        }

        protected int CurrentGroupID
        {
            get
            {
                if (Session[ConstConfig.SessionKey_CurrentLoginGroupID] == null)
                {
                    Session[ConstConfig.SessionKey_CurrentLoginGroupID] = CurrentGroup.Id;
                }
                int s = 0;
                if (int.TryParse(Session[ConstConfig.SessionKey_CurrentLoginGroupID].ToString(), out s))
                    return s;
                else return 0;
            }
        }

        protected Group CurrentGroup
        {
            get
            {
                if (Session[ConstConfig.SessionKey_CurrentLoginGroup] == null)
                {
                    var userName = User.Identity.Name;
                    var user = dbContext.Users.Where(u => u.UserName == userName).FirstOrDefault();
                    Session[ConstConfig.SessionKey_CurrentLoginGroup] = user.Group;
                }
                return Session[ConstConfig.SessionKey_CurrentLoginGroup] as Group;
            }
        }

        protected ApplicationUser GetLoggedUserInfo()
        {
            var userName = User.Identity.Name;
            var _temp = dbContext.Users.Where(u => u.UserName == userName).Include(u => u.Group).FirstOrDefault();
            if (_temp != null)
                return _temp;
            return null;
        }

        #region 商品API
        public string GetOnSaleProducts(string sessionkey)
        {
            ITopClient client = new DefaultTopClient(server_Url, AppKey, Secret, "json");
            ItemsOnsaleGetRequest req = new ItemsOnsaleGetRequest();
            req.Fields = "num_iid";
            ItemsOnsaleGetResponse rsp = client.Execute(req, sessionkey);
            return rsp.Body;
        }

        public string GetSellerProductList(string num_ids, string sessionkey)
        {
            ITopClient client = new DefaultTopClient(server_Url, AppKey, Secret, "json");
            ItemsSellerListGetRequest req = new ItemsSellerListGetRequest();
            req.Fields = "num_iid,title,nick,approve_status,num,sku,price,pic_url,item_imgs,prop_imgs,cid,seller_cids";
            req.NumIids = num_ids;
            ItemsSellerListGetResponse rsp = client.Execute(req, sessionkey);
            return rsp.Body;
        }
        #endregion
        #region 店铺API
        public string GetSellerCatsList(string nick,string sessionkey)
        {
            ITopClient client = new DefaultTopClient(server_Url, AppKey, Secret, "json");
            SellercatsListGetRequest req = new SellercatsListGetRequest();
            req.Nick = nick;
            SellercatsListGetResponse rsp = client.Execute(req, sessionkey);
            return rsp.Body;
        }
        #endregion
    }
}