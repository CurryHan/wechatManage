using Sensing.Data;
using Sensing.Entities;
using SensingCloud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;

namespace SensingCloud.Controllers
{
    public class WeChatController : LanguageController
    {
        private SensingSiteDbContext db = new SensingSiteDbContext();
        // GET: Menu
        public ActionResult Index(string query = "", int pageIndex = 1)
        {
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = 20;
            ViewBag.Title = "菜单管理";
            var list = db.Menus.Where(m => m.Deleted == false).OrderBy(m => m.Id).ToPagedList(pageIndex, 20);
            return View(list);
        }

        public ActionResult Edit(int id)
        {
            Menu info = db.Menus.Find(id);
            if (info != null)
            {
                ViewBag.MediaList = db.Medias.Where(m => m.Deleted == false && m.Type == info.Type)
    .Select(t => new SelectListItem { Text = t.Title, Value = t.Id.ToString() });
                return PartialView("_EditDialog", info);
            }
            return Json("fail");
        }

        [HttpPost]
        public void Edit(Menu menu)
        {
            db.Entry(menu).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }


        public ActionResult Introduction()
        {
            IntroductionViewModel model = new IntroductionViewModel();
            var list1 = db.Menus.Find(1);
            model.Url1 = list1 == null ? "" : list1.Media == null ? "" : list1.Media.Url;

            var list2 = db.Menus.Find(2);
            model.Url2 = list2 == null ? "" : list2.Media == null ? "" : list2.Media.Url;

            var list3 = db.Menus.Find(3);
            model.Url3 = list3 == null ? "" : list3.Media == null ? "" : list3.Media.Url;

            var list4 = db.Menus.Find(4);
            model.Url4 = list4 == null ? "" : list4.Media == null ? "" : list4.Media.Url;

            var list5 = db.Menus.Find(5);
            model.Url5 = list5 == null ? "" : list5.Media == null ? "" : list5.Media.Url;

            return View(model);
        }

        public ActionResult About()
        {
            IntroductionViewModel model = new IntroductionViewModel();
            var list1 = db.Menus.Find(6);
            model.Url1 = list1 == null ? "" : list1.Media == null ? "" : list1.Media.Url;
            return View(model);
        }

        public ActionResult Ticket()
        {
            var list = db.Menus.Where(m => m.Id > 6&&m.Deleted==false).ToList();
            return View(list);
        }

        public ActionResult Contact()
        {
            return View();
        }




    }
}