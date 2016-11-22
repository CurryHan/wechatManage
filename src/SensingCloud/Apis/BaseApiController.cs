using Sensing.Entities;
using SensingCloud.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SensingCloud.Apis
{
    public class BaseApiController: LanguageController
    {
        //protected static readonly IBizLogger logger = ServerLogFactory.GetLogger(typeof(WeixinBaseApiController));
        protected new Group CurrentGroup = null;
        //protected SensingSiteDbContext dbContext = new SensingSiteDbContext();
        protected string subscriptionKey;
        protected string mac;
        protected string CurrentHost;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                subscriptionKey = Request["subscriptionKey"];
                mac = Request["mac"];

                logger.Debug("BaseApiController OnActionExecuting start");
                logger.Debug($"subscriptionKey is {subscriptionKey},mac is {mac}");

                CurrentHost = Request.Url.Scheme + "://" + Request.Url.Host;

                base.OnActionExecuting(filterContext);
                if (filterContext.ActionDescriptor.ActionName != "error")
                {
                    int errorid = CheckParams();
                    if (errorid > 0)
                    {
                        string ReturnUrl = "/api/productapi/error/" + errorid.ToString(); //特定页面
                        filterContext.Result = new RedirectResult(ReturnUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("BaseApiController OnActionExecuting system error:", ex);
            }

        }

        private int CheckParams()
        {
            int errorid = 0;
            if (string.IsNullOrEmpty(subscriptionKey) || string.IsNullOrEmpty(mac))
            {
                return (int)ProductApiError.InvalidBaseParams;
            }

            return errorid;
        }
    }

    public enum ProductApiError
    {
        [Display(Name = "invalid base params request")]
        InvalidBaseParams = 1,
        [Display(Name = "appid or activity expired")]
        Expired,
        [Display(Name = "appid locked")]
        Locked,
        [Display(Name = "appid not existing")]
        Deleted,
        [Display(Name = "WeixinAppId,ActivityId has no relations.")]
        UnAuthrozied
    }

}