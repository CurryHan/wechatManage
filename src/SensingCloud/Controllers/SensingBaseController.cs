using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using SensingCloud.Helpers;
using LogService;
using Sensing.Data;
using Sensing.Entities;
using SensingCloud.Services;
using SensingCloud.Models;
using Aliyun.OSS;
using Aliyun.OSS.Common;

namespace SensingCloud.Controllers
{

    public class SensingBaseController : Controller
    {
        protected static readonly IBizLogger logger = ServerLogFactory.GetLogger(typeof(SensingBaseController));


        protected string CurrentHost;

        protected string OAuthCallBackUrl;

        protected SensingSiteDbContext dbContext = new SensingSiteDbContext();

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

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            CurrentHost = Request.Url.Scheme + "://" + Request.Url.Host;
            OAuthCallBackUrl = CurrentHost + "/weixin/Auth20CallBack";
            //logger.Debug(Request.RawUrl);
            //logger.Debug("开始执行:"+ filterContext.ActionDescriptor.ControllerDescriptor.ControllerName+" "+ filterContext.ActionDescriptor.ActionName);
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var actionName = filterContext.ActionDescriptor.ActionName;
            var canControllerAnonymouse = filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), false).Count() < 1 ? false : true;
            var canActionAnonymouse = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), false).Count() < 1 ? false : true;
            //MakeSureSessionHasValue();
            base.OnActionExecuting(filterContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            //logger.Debug("Action执行结束:" + filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + " " + filterContext.ActionDescriptor.ActionName);
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
               
#if USE_OOS
                var filePath =  (directory + fileName).TrimStart(new char[] {'/' });
                logger.Debug($"SaveUploadedFile name is: {directory + fileName}");
                var absolutePath = OSSHelper.UploadFile(fileBase.InputStream, filePath);
                Thread.Sleep(10);
                return absolutePath;

#endif
#if Use_Local
                 var mappedDirectory = Server.MapPath(directory);
                var path = Path.Combine(mappedDirectory, fileName);
                if (!Directory.Exists(mappedDirectory))
                {
                    Directory.CreateDirectory(mappedDirectory);
                }
                fileBase.SaveAs(path);
                logger.Debug($"SaveUploadedFile name is: {directory + fileName}");
                Thread.Sleep(10);
                return directory + fileName;
#endif
            }
            return "";
        }



        


        protected ActionResult RedirectToError(string message)
        {
            ViewBag.Message = message;
            return View("Error");
        }


        private void MakeSureSessionHasValue()
        {
            System.Diagnostics.Debug.WriteLine(CurrentGroup.Id);
        }
    }
}