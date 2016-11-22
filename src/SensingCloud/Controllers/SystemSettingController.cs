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

namespace SensingCloud.Controllers
{
    [Authorize(Roles = (RoleString.Admin))]
    public class SystemSettingController : LanguageController
    {
        private SensingSiteDbContext db = new SensingSiteDbContext();
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
            var model = db.PlatformNotifications.Where(p => p.Deleted == false && p.IsUsing == true).FirstOrDefault();
            return PartialView("_EditPlatNotifications", model);
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
                db.PlatformNotifications.Add(platformNotification);
            }
            else
            {
                platformNotification.LastUpdated = DateTime.Now;
                platformNotification.Updater = User.Identity.Name;
                db.Entry(platformNotification).State = EntityState.Modified;
            }
            db.SaveChanges();
                ViewBag.Success = true;
        }
            return PartialView("_EditPlatNotifications", platformNotification);
        }

        //审批流程
        [HttpGet]
        public ActionResult EditApproveProcess()
        {
            var roles = new List<string>();
            string r1 = RoleString.Auditor.ToString();
            roles.Add(r1);
            ViewBag.ApproverRole = roles;
            var list = db.ApproveProcesss.Where(a => a.Deleted == false).ToList();
            return PartialView("_EditApproveProcess", list);
        }

        [HttpPost]
        public ActionResult EditApproveProcess([Bind(Prefix = "item")]ApproveProcess approveProcess)
        {
            var _temp = db.ApproveProcesss.Find(approveProcess.Id);
            if (_temp != null)
            {
                var _temproles = Request.Params["ApproveRolesSelect"];
                _temp.NeedApprove = approveProcess.NeedApprove;
                _temp.ApproverRole = _temproles;
                _temp.LastUpdated = DateTime.Now;
                _temp.Updater = User.Identity.Name;
                db.Entry(_temp).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Success = true;
            }

            var roles = new List<string>();
            string r1 = RoleString.Auditor.ToString();
            roles.Add(r1);
            ViewBag.ApproverRole = roles;
            var list = db.ApproveProcesss.Where(a => a.Deleted == false).ToList();
            return PartialView("_EditApproveProcess", list);
        }

        [HttpGet]
        //终端内容更新
        public ActionResult EditUpdateCleintContent()
        {
            var model = db.TerminalContentUpdates.Where(t => t.Deleted == false).FirstOrDefault();
            if (model == null)
            {
                model = new TerminalContentUpdate();
                model.Name = "autocreate";
                model.Created = DateTime.Now;
                model.Creator = User.Identity.Name;
                model.Deleted = false;
                model.Description = "use people auto create!";
                model.IsAutoUpdate = true;
                db.TerminalContentUpdates.Add(model);
                db.SaveChanges();
            }
            return PartialView("_EditUpdateCleintContent", model);
        }
        [HttpPost]
        public ActionResult EditUpdateCleintContent(TerminalContentUpdate tc)
        {
            var temp = db.TerminalContentUpdates.Find(tc.Id);
            if (temp != null)
            {
                var _tempUpdateFrequency = Request.Params["updatefrequencySlct"];
                temp.IsAutoUpdate = tc.IsAutoUpdate;
                temp.UpdateFrequency = _tempUpdateFrequency;
                temp.UpdateTime = tc.UpdateTime;
                temp.LastUpdated = DateTime.Now;
                temp.Updater = User.Identity.Name;
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Success = true;
            }
            return PartialView("_EditUpdateCleintContent", tc);
        }

        //查询客户端软件更新
        public ActionResult EditUpdateCleintSoft()
        {
            var list = db.TerminalSoftUpdates.Where(t => t.Deleted == false).ToList();
            return PartialView("_EditUpdateCleintSoft", list);
        }

        //添加客户端软件更新信息包
        [HttpGet]
        public ActionResult AddTerminalSoftPackage()
        {
            return PartialView("_AddTerminalSoftPackage");
        }

        [HttpPost]
        public ActionResult AddTerminalSoftPackage(TerminalSoftUpdate tsu)
        {
            if (ModelState.IsValid)
            {
                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase avatar = Request.Files[file];
                    switch (file)
                    {
                        case "softpackage":
                            tsu.SoftPath = UpLoadFile(avatar, "~/TerminalSoft/Package", tsu.Version);
                            break;
                    }
                }
                var _time = Request.Params["SoftUpdatedTime"];
                if (_time != null) tsu.UpdateTimes = DateTime.Parse(_time);
                tsu.Created = DateTime.Now;
                tsu.Creator = User.Identity.Name;

                db.TerminalSoftUpdates.Add(tsu);
                db.SaveChanges();
                ViewBag.Success = true;
                var list = db.TerminalSoftUpdates.Where(t => t.Deleted == false).ToList();
                return PartialView("_EditUpdateCleintSoft", list);
            }
            return PartialView("_AddTerminalSoftPackage", tsu);
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