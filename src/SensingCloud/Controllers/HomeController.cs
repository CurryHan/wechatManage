using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sensing.Entities.Users;

namespace SensingCloud.Controllers
{
    [Authorize]
    public class HomeController : LanguageController
    {
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = Resources.Resources.About;

            var configuration = new Sensing.Data.Migrations.Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
            ViewBag.Message = "Your app description page.";


            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize(Roles = RoleString.Admin)]
        public ActionResult Chat()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            return View();
        }
    }
}