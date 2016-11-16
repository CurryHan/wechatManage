using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sensing.Entities.Users;
using Sensing.Data;
using Sensing.Entities;
using Webdiyer.WebControls.Mvc;
using SensingCloud.Services;

namespace SensingCloud.Controllers
{
    public class HomeController : LanguageController
    {
        //private readonly IGameService _gameService;

        //public HomeController(IGameService gameService)
        //{
        //    _gameService = gameService;
        //}
        //public ActionResult Index()
        //{

        //    return View();
        //}

        //public ActionResult Game()
        //{
        //    return View();
        //}

        //public ActionResult GameCenter(string qstr, int pageIndex = 1)
        //{
        //    if (!Request.IsAjaxRequest())
        //    {
        //        return View();
        //    }

        //    int pageSize = 20;
        //    ViewBag.pageIndex = pageIndex;
        //    ViewBag.pageSize = pageSize;
        //    if (string.IsNullOrEmpty(qstr)) qstr = "";
        //    ViewBag.qstr = qstr;
        //    IEnumerable<Game> gameslist = _gameService.GetAll();

        //    if (!string.IsNullOrEmpty(qstr))
        //    {
        //        gameslist = gameslist.Where(p => p.Name.Contains(qstr));
        //    }

        //    return PartialView("_GameList", gameslist.ToList().ToPagedList(pageIndex, pageSize));

        //}

        //[AllowAnonymous]
        //public ActionResult About()
        //{
        //    ViewBag.Message = Resources.Resources.About;

        //    ViewBag.Message = "Your app description page.";


        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}

        //[Authorize(Roles = RoleString.Admin)]
        //public ActionResult Chat()
        //{
        //    return View();
        //}

        //[Authorize(Roles = RoleString.Admin)]
        //public ActionResult Pad()
        //{
        //    return View();
        //}

        //[Authorize(Roles = RoleString.Admin)]
        //public ActionResult PadConsole()
        //{
        //    return View();
        //}

        //[Authorize(Roles = RoleString.Admin)]
        //public ActionResult PCLogin()
        //{
        //    return View();
        //}

        //public ActionResult Dashboard()
        //{
        //    return View();
        //}
    }
}