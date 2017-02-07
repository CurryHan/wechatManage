using Newtonsoft.Json.Linq;
using Sensing.Data;
using Sensing.Entities;
using SensingCloud.Helpers;
using SensingCloud.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Webdiyer.WebControls.Mvc;

namespace SensingCloud.Controllers
{
    public class MediaController : LanguageController
    {
        private SensingSiteDbContext db = new SensingSiteDbContext();
        private readonly IGroupService _groupSvc;
        public MediaController(IGroupService groupSvc)
        {
            _groupSvc = groupSvc;
        }
        // GET: Media
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetList(string type, string query = "", int pageIndex = 1)
        {
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = 20;
            if (type == "news")
            {
                var list = db.Medias.Where(m => m.Deleted == false && m.Type == EnumType.news && m.Title.Contains(query)).OrderBy(m => m.Id).ToPagedList(pageIndex, 20);
                return PartialView("_newsMediadList", list);
            }
            else
            {
                var list = db.Medias.Where(m => m.Deleted == false && m.Type == EnumType.image && m.Title.Contains(query)).OrderBy(m => m.Id).ToPagedList(pageIndex, 20);
                return PartialView("_imageMediaList", list);
            }
        }

        public ActionResult CreateMenu()
        {

            var group = _groupSvc.GetAll().FirstOrDefault();

            var url = $"https://api.weixin.qq.com/cgi-bin/menu/create?access_token={group.access_token}";
            Dictionary<string, string> pout = new Dictionary<string, string>();
            string json = @"{
	""button"":[
	 {
		  ""name"":""创思感知"",	
		  ""sub_button"":[
		  {
			""type"":""view"",
			""name"":""解决方案"",
			""url"":""http://www.troncell.com/znls.html""
		  },
		  {
			""type"":""view"",
			""name"":""客户案例"",
			""url"":""http://www.troncell.com/client.html""
		  },
		  {
			""type"":""view"",
			""name"":""关于我们"",
			""url"":""http://www.troncell.com/about.html""
		  }
		  ]
	  },
	  {
		  ""name"":""软件产品"",	
		  ""sub_button"":[
		  {
			""type"":""view"",
			""name"":""营销活动游戏"",
			""url"":""http://www.troncell.com/hdyx.html""
		  },
		  {
			""type"":""view"",
			""name"":""互动商品型录"",
			""url"":""http://www.troncell.com/hdsp.html""
		  },
		  {
			""type"":""view"",
			""name"":""智能搭配下单"",
			""url"":""http://www.troncell.com/zndp.html""
		  },
		  {
			""type"":""view"",
			""name"":""数字商品导视"",
			""url"":""http://www.troncell.com/szsp.html""
		  },
		  {
			""type"":""view"",
			""name"":""行为分析模块"",
			""url"":""http://www.troncell.com/xwfx.html""
		  }
		  ]
	  }, 
	  {
		  ""name"":""互动中心"",	
		  ""sub_button"":[
		  {
			""type"":""click"",
			""name"":""新闻资讯"",
			""key"":""V1001_TODAY_NEWS""
		  },
		  {
			""type"":""view"",
			""name"":""打飞机游戏"",
			""url"":""http://game.troncell.com/playing/game?activityGameId=17""
		  },
		  {
			""type"":""view"",
			""name"":""爱消除游戏"",
			""url"":""http://game.troncell.com/playing/game?activityGameId=23""
		  }
		  ]
	  }]
}";
            string result = PostHttpResponse.PostHttpResponseJson(url, json);
            logger.Debug(result);
            return Json(true);
        }

        public ActionResult DeleteMenu()
        {

            var group = _groupSvc.GetAll().FirstOrDefault();

            var url = $"https://api.weixin.qq.com/cgi-bin/menu/delete?access_token={group.access_token}";
            string result = PostHttpResponse.PostHttpResponseJson(url);
            logger.Debug(result);
            return Json(true);
        }

        public ActionResult GetMenu()
        {
            var group = _groupSvc.GetAll().FirstOrDefault();
            var url = $"https://api.weixin.qq.com/cgi-bin/get_current_selfmenu_info?access_token={group.access_token}";
            string result = PostHttpResponse.PostHttpResponseJson(url);
            logger.Debug(result);
            return Json(true);
        }



        public ActionResult SyscMedias()
        {
            int total = 0;
            int index = 0;
            int current = 0;
            int offset = 0;
            int count = 20;
            var group = _groupSvc.GetAll().FirstOrDefault();
            bool isFirst = true;
            while (isFirst || total > current)
            {
                if (!isFirst)
                {
                    int diff = total - current;
                    if (diff / 20 > 0)
                    {
                        count = 20;

                    }
                    else
                    {
                        count = diff;
                    }
                    offset = index * 20;
                }
                var url = $"https://api.weixin.qq.com/cgi-bin/material/batchget_material?access_token={group.access_token}";
                Dictionary<string, string> pout = new Dictionary<string, string>();
                pout.Add("type", "news");
                pout.Add("offset", offset.ToString());
                pout.Add("count", count.ToString());
                string json = (new JavaScriptSerializer()).Serialize(pout);
                string result = PostHttpResponse.PostHttpResponseJson(url, json);
                JObject jsonobj = JObject.Parse(result);
                JArray arraylist = JArray.Parse(jsonobj["item"].ToString());
                foreach (var item in arraylist)
                {
                    string mediaId = item["media_id"].ToString();
                    var media = db.Medias.FirstOrDefault(m => m.MediaKey == mediaId && m.Deleted == false);
                    if (media == null)
                    {
                        Media info = new Media();
                        info.MediaKey = mediaId;
                        info.Title = item["content"]["news_item"][0]["title"].ToString();
                        info.Url = item["content"]["news_item"][0]["url"].ToString();
                        info.Thumb_MediaId = item["content"]["news_item"][0]["thumb_media_id"].ToString();
                        info.Type = EnumType.news;
                        db.Entry(info).State = System.Data.Entity.EntityState.Added;
                        db.SaveChanges();
                    }
                    else
                    {
                        media.Title = item["content"]["news_item"][0]["title"].ToString();
                        media.Url = item["content"]["news_item"][0]["url"].ToString();
                        media.Thumb_MediaId = item["content"]["news_item"][0]["thumb_media_id"].ToString();
                        db.Entry(media).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                logger.Debug(result);
                index++;
                total = int.Parse(jsonobj["total_count"].ToString());
                current = index * 20;
                if (isFirst) isFirst = false;
            }
            return null;
        }

        public ActionResult SyscPic()
        {
            int total = 0;
            int index = 0;
            int current = 0;
            int offset = 0;
            int count = 20;
            var group = _groupSvc.GetAll().FirstOrDefault();
            bool isFirst = true;
            while (isFirst || total > current)
            {
                if (!isFirst)
                {
                    int diff = total - current;
                    if (diff / 20 > 0)
                    {
                        count = 20;
                    }
                    else
                    {
                        count = diff;
                    }
                    offset = index * 20;
                }
                var url = $"https://api.weixin.qq.com/cgi-bin/material/batchget_material?access_token={group.access_token}";
                Dictionary<string, string> pout = new Dictionary<string, string>();
                pout.Add("type", "image");
                pout.Add("offset", offset.ToString());
                pout.Add("count", count.ToString());
                string json = (new JavaScriptSerializer()).Serialize(pout);
                string result = PostHttpResponse.PostHttpResponseJson(url, json);
                JObject jsonobj = JObject.Parse(result);
                JArray arraylist = JArray.Parse(jsonobj["item"].ToString());
                foreach (var item in arraylist)
                {
                    string mediaId = item["media_id"].ToString();
                    var media = db.Medias.FirstOrDefault(m => m.MediaKey == mediaId && m.Deleted == false);
                    if (media == null)
                    {
                        Media info = new Media();
                        info.MediaKey = mediaId;
                        info.Title = item["name"].ToString();
                        info.Url = item["url"].ToString();
                        info.Type = EnumType.image;
                        db.Entry(info).State = System.Data.Entity.EntityState.Added;
                        db.SaveChanges();
                    }
                    else
                    {
                        media.Title = item["name"].ToString();
                        media.Url = item["url"].ToString();
                        db.Entry(media).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                logger.Debug(result);
                index++;
                total = int.Parse(jsonobj["total_count"].ToString());
                current = index * 20;
                if (isFirst) isFirst = false;
            }
            return null;
        }
    }

}