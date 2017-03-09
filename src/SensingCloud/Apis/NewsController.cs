using Resources;
using Senparc.Weixin.MP.TenPayLib;
using Sensing.Data;
using Sensing.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SensingCloud.Apis
{
    public class NewsController : Controller
    {
        private SensingSiteDbContext db = new SensingSiteDbContext();

        [Route("api/news")]
        [HttpGet]
        public ActionResult GetAllNews(int pageSize=10,string lasttime="")
        {
            string callback=HttpContext.Request.QueryString["callback"];
            var date = DateTime.Now;
            if (lasttime != "") date = Convert.ToDateTime(lasttime);
            List<Media> list = db.Medias.Where(m => m.Deleted == false&&m.Type==EnumType.news&&m.LastUpdated<date).OrderByDescending(m=>m.LastUpdated).Take(pageSize).ToList();
            List<MediaViewModel> result = new List<MediaViewModel>();
            foreach (var item in list)
            {
                var thumbMedia = db.Medias.Where(m => m.MediaKey == item.Thumb_MediaId && m.Deleted == false).FirstOrDefault();
                string picUrl = string.Empty;
                if (thumbMedia != null) picUrl = thumbMedia.Url;
                var newMedia = new MediaViewModel()
                {
                    Title = item.Title,
                    Url = item.Url,
                    ThumbMediaUrl = picUrl,
                    Digest = string.IsNullOrEmpty(item.Description)?"":item.Description.Length>100?item.Description.Substring(0,99):item.Description,
                    Updatetime = item.LastUpdated.HasValue ? string.Format("{0:yyyy-MM-dd HH:mm:ss}", item.LastUpdated.Value) : string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now)
                };
                result.Add(newMedia);
            }
            System.Web.Script.Serialization.JavaScriptSerializer json = new JavaScriptSerializer();
            
            return Content(callback + "(" + json.Serialize(result) + ")");
            //return Json(new { status = Resource.API_OK, errMessage = "", data = result }, JsonRequestBehavior.AllowGet);
        }

    }

    public class MediaViewModel
    {
        public string Title { get; set; }
        public string Updatetime { get; set; }
        public string ThumbMediaUrl { get; set; }
        public string Digest { get; set; }
        public string Url { get; set; }
    }

    public class WebApiResult
    {
        public string status { get; set; }
        public string message { get; set; }
        public string code { get; set; }
        public object data { get; set; }
    }
}