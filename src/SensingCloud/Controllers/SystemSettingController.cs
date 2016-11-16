using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sensing.Data;
using Sensing.Entities.SystemSettings;
using Sensing.Entities.Users;
using SensingCloud.Helpers;
using SensingCloud.Authorization;
using Sensing.Entities;

namespace SensingCloud.Controllers
{
    [Authorize]
    [GroupAuthorize(Roles = RoleString.Admin, Groups = GroupTypeString.SuperLevel)]
    public class SystemSettingController : LanguageController
    {
        //private SensingSiteDbContext db = new SensingSiteDbContext();
        // GET: SystemSetting
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 平台通知设置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditPlatformNotification()
        {
            //var model = db.PlatformNotifications.Where(p => p.Deleted == false && p.IsUsing == true).FirstOrDefault();
            return PartialView("_EditPlatNotifications", null);
        }

        [HttpPost]
        public ActionResult EditPlatformNotification(PlatformNotification platformNotification)
        {
            if (ModelState.IsValid)
            {
                platformNotification.Deleted = false;
                platformNotification.IsUsing = true;
                if (platformNotification.Id == 0)
                {
                    platformNotification.Created = DateTime.Now;
                    platformNotification.Creator = User.Identity.Name;
                    //db.PlatformNotifications.Add(platformNotification);
                }
                else
                {
                    platformNotification.LastUpdated = DateTime.Now;
                    platformNotification.Updater = User.Identity.Name;
                    //db.Entry(platformNotification).State = EntityState.Modified;
                }
                //db.SaveChanges();
                ViewBag.Success = true;
            }
            return PartialView("_EditPlatNotifications", platformNotification);
        }



        //添加客户端软件更新信息包
        [HttpGet]
        public ActionResult AddTerminalSoftPackage()
        {
            return PartialView("_AddTerminalSoftPackage");
        }

        private string UpLoadFile(HttpPostedFileBase avatar, string relativepath, string file)
        {
            if (avatar.ContentLength > 0)
            {
                var _fileExtension = Path.GetExtension(avatar.FileName);
                var fileName = file + DateTime.Now.ToString("yyyyMMddHHmmssfff") + _fileExtension;
                var _servpath = Server.MapPath(relativepath);
                if (!Directory.Exists(_servpath))
                    Directory.CreateDirectory(_servpath);
                var path = Path.Combine(_servpath, fileName);
                avatar.SaveAs(path);
                var result = Path.Combine(relativepath.Substring(1), fileName);
                return result;
            }
            return null;
        }
    }
}