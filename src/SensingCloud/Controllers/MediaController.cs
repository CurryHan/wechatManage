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
        public ActionResult Index(string query = "", int pageIndex = 1)
        {
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = 20;
            var list = db.Medias.Where(m => m.Deleted == false).OrderBy(m => m.Id).ToPagedList(pageIndex, 20);
            return View(list);
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
            while (isFirst||total>current)
            {
                if (!isFirst)
                {
                    int diff = total - current;
                    if (diff / 20 > 0)
                    {
                        count = 20;
                        
                    }
                    else {
                        count = diff;
                    }
                    offset = index * 20;
                }
                var url = $"https://api.weixin.qq.com/cgi-bin/material/batchget_material?access_token={group.access_token}";
                Dictionary<string, string> pout = new Dictionary<string, string>();
                pout.Add("access_token", group.access_token);
                pout.Add("type", "news");
                pout.Add("offset", offset.ToString());
                pout.Add("count", count.ToString());
                string json = (new JavaScriptSerializer()).Serialize(pout);
                string result = PostHttpResponse.PostHttpResponseJson(url, json);
                JObject jsonobj = JObject.Parse(result);
                JArray arraylist = JArray.Parse(jsonobj["item"].ToString());
                foreach (var item in arraylist)
                {
                    string mediaId= item["media_id"].ToString();
                    var media=db.Medias.FirstOrDefault(m => m.MediaKey == mediaId && m.Deleted == false);
                    if (media == null)
                    {
                        Media info = new Media();
                        info.MediaKey = item["media_id"].ToString();
                        info.Title = item["content"]["news_item"][0]["title"].ToString();
                        info.Url = item["content"]["news_item"][0]["url"].ToString();
                        db.Entry(info).State = System.Data.Entity.EntityState.Added;
                        db.SaveChanges();
                    }
                    else {
                        media.Title= item["content"]["news_item"][0]["title"].ToString();
                        media.Url = item["content"]["news_item"][0]["url"].ToString();
                        db.Entry(media).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                logger.Debug(result);
                index++;
                total = int.Parse(jsonobj["total_count"].ToString());
                current = index * 20;
                if(isFirst) isFirst = false;
            }

            
           
            return null;
        }

        public ActionResult GetMedia(string mediaId)
        {
            var group = _groupSvc.GetAll().FirstOrDefault();
            var url = $"https://api.weixin.qq.com/cgi-bin/material/get_material?access_token={group.access_token}";
            Dictionary<string, string> pout = new Dictionary<string, string>();
            pout.Add("media_id", mediaId);
            string json = (new JavaScriptSerializer()).Serialize(pout);
            string result = PostHttpResponse.PostHttpResponseJson(url, json);
            logger.Debug(result);
            //JObject jsonobj = JObject.Parse(result);
            //JArray arraylist = JArray.Parse(jsonobj["item"].ToString());
            //foreach (var item in arraylist)
            //{
            //    Media info = new Media();
            //    info.MediaKey = item["media_id"].ToString();
            //    info.Url = item["content"]["news_item"][0]["title"].ToString();
            //    db.Entry(info).State = System.Data.Entity.EntityState.Added;
            //    db.SaveChanges();
            //}
            return null;
        }
    }

    public class MediaListInfo
    {
        public int total_count { get; set; }
        public int item_count { get; set; }

        public List<MediaInfo> media { get; set; }
    }

    public class MediaInfo {
        public string media_id { get; set; }

        public string content { get; set; }

        public string update_time { get; set; }

    }
}