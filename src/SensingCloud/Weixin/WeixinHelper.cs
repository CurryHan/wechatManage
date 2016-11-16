namespace SensingCloud.Weixin
{
    //public class WeixinHelper
    //{
    //    private static readonly IBizLogger logger = ServerLogFactory.GetLogger(typeof(WeixinHelper));

    //    public static bool SendTemplateMessage(string template_id_short, WeixinUserAward userAward1)
    //    {
    //        bool returnBool = false;
    //        Activity activity = null;
    //        try
    //        {
    //            using (SensingSiteDbContext db = new SensingSiteDbContext())
    //            {
    //                var userAward = db.WeixinUserAwards.Where(user => user.Id == userAward1.Id).FirstOrDefault();
    //                var currentUser = userAward.WeixinUserInfo;
    //                activity = userAward.Activity;
    //                var currentAward = userAward.Award;
    //                var mp = currentUser.WeixinPublicAccountInfo;

    //                bool isNeedSave = false;
    //                string template_id = mp.AwardTemplateId;
    //                if (string.IsNullOrEmpty(template_id))
    //                {
    //                    isNeedSave = true;
    //                    AddTemplateResult addTemplateResult = TemplateApiExtension.GetTemplateid(mp.Authorizer_access_token, template_id_short);
    //                    template_id = addTemplateResult.template_id;
    //                    mp.AwardTemplateId = template_id;
    //                }

    //                logger.Debug("SendTemplateMessage template_id " + template_id);


    //                var url = activity.AwardMessage.HtmlTemplate.ActionUrl;
    //                //var url = ConstConfig.PLATHOST + "/weixin/WechatPage";
    //                //if (template.ActionUrl.StartsWith(ConstConfig.PLATHOST))
    //                //{
    //                //    url = template.ActionUrl;
    //                //}
    //                //url += $"?openid={currentUser.Openid}&appid={currentUser.WeixinAppID}&weixinuseractionid={action.Id}";
    //                url += $"?openid={currentUser.Openid}&appid={currentUser.WeixinAppID}&weixinuserawardid={userAward.Id}";

    //                url = GetActivityAwardUrl(userAward);
    //                logger.Debug("SendTemplateMessage url:" + url);
    //                var data = new
    //                {
    //                    first = new TemplateDataItem($"恭喜您参与的{activity.Name}中奖了"),
    //                    keyword1 = new TemplateDataItem(activity.Name),
    //                    keyword2 = new TemplateDataItem(currentAward.AwardProduct),
    //                    remark = new TemplateDataItem("感谢您的参与")
    //                };


    //                var result = TemplateApi.SendTemplateMessage(mp.Authorizer_access_token, currentUser.Openid, template_id, "#FF0000", url, data);

    //                logger.Debug($"SendTemplateMessage result errorcode:{result.errcode.ToString()}");
    //                if (result.errcode == Senparc.Weixin.ReturnCode.请求成功)
    //                {
    //                    logger.Debug($"SendTemplateMessage result msgid:{result.msgid.ToString()}");
    //                    //db.Entry(userAward).State = System.Data.Entity.EntityState.Detached;
    //                    //db.Entry(mp).State = System.Data.Entity.EntityState.Detached;
    //                    userAward.Msgid = (long)result.msgid;
    //                    db.Entry(userAward).State = System.Data.Entity.EntityState.Modified;
    //                    if (isNeedSave)
    //                    {
    //                        db.Entry(mp).State = System.Data.Entity.EntityState.Modified;
    //                    }
    //                    db.SaveChanges();
    //                    returnBool = true;
    //                }
    //                else
    //                {
    //                    logger.Debug("SendTemplateMessage NG:" + result.errcode + " " + result.errmsg);
    //                    returnBool = false;
    //                }
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            logger.Error("SendTemplateMessage,Excpetion=", ex);
    //            returnBool = false;
    //        }

    //        return returnBool;
    //    }

    //    public static bool SendAwardNews(Activity activity, WeixinUserAward userAward, string CurrentHost)
    //    {
    //        try
    //        {
    //            using (SensingSiteDbContext db = new SensingSiteDbContext())
    //            {
    //                var url = activity.AwardMessage.HtmlTemplate.ActionUrl;
    //                url += $"?openid={userAward.WeixinUserInfo.Openid}&appid={activity.WeixinAppID}&weixinuserawardid={userAward.Id}";
    //                url = GetActivityAwardUrl(userAward);
    //                Senparc.Weixin.Open.OAuthScope[] arrOAuthScope = new Senparc.Weixin.Open.OAuthScope[1];
    //                arrOAuthScope[0] = Senparc.Weixin.Open.OAuthScope.snsapi_base;
    //                var open = db.WeixinOpenPlatforms.FirstOrDefault();
    //                //string url = Senparc.Weixin.Open.OAuthAPIs.OAuthApi.GetAuthorizeUrl(activity.WeixinAppID, open.ComponentAppID, CurrentHost + "/weixin/Auth20CallBack", ConstConfig.ACTION + userAction.Id.ToString() + ";" + ConstConfig.TEMPTATE + afterTempID, arrOAuthScope);
    //                List<Article> articles = new List<Article>();
    //                Article article1 = new Article();
    //                article1.Title = "中奖结果通知";
    //                article1.PicUrl = CurrentHost + ConstConfig.Common_Award_News_Image;
    //                article1.Description = $"恭喜您参与的{activity.Name}中奖了，奖品{userAward.Award.AwardProduct}";


    //                article1.Url = url;
    //                articles.Add(article1);
    //                CustomApi.SendNews(userAward.WeixinUserInfo.WeixinPublicAccountInfo.Authorizer_access_token, userAward.WeixinUserInfo.Openid, articles);

    //                logger.Debug($"SendAwardNews picurl is {article1.PicUrl}");
    //                return true;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            logger.Error("SendAwardNews,Excpetion=", ex);
    //            return false;
    //        }


    //    }

    //    public static string GetActivityUrl(Activity currentActivity)
    //    {
    //        string finalUrl = string.Empty;
    //        using (SensingSiteDbContext db = new SensingSiteDbContext())
    //        {
    //            try
    //            {
    //                logger.Debug($"currentActivity.WeixinAppID:{currentActivity.WeixinAppID}");
    //                Senparc.Weixin.Open.OAuthScope[] arrOAuthScope = new Senparc.Weixin.Open.OAuthScope[1];
    //                arrOAuthScope[0] = Senparc.Weixin.Open.OAuthScope.snsapi_base;
    //                var mp = db.WeixinPublicAccountInfos.FirstOrDefault(p => p.WeixinAppID == currentActivity.WeixinAppID);
    //                var open = db.WeixinOpenPlatforms.FirstOrDefault();

    //                if (currentActivity.ActivityHtmlTemplateID == null) return "";
    //                var template = db.HtmlTemplates.Find(currentActivity.ActivityHtmlTemplateID);

    //                logger.Debug($"currentActivity.ActivityHtmlTemplate:{template.ActionUrl}");
    //                //如果说线上游戏的活动，则当作action来share
    //                if (template.ActionUrl.ToLower().Contains("online"))
    //                {
    //                    finalUrl = Senparc.Weixin.Open.OAuthAPIs.OAuthApi.GetAuthorizeUrl(mp.WeixinAppID, open.ComponentAppID, HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + "/weixin/Auth20CallBack", ConstConfig.ACTION + "0;" + ConstConfig.ACTIVITY + currentActivity.Id.ToString() + ";" + ConstConfig.TEMPTATE + currentActivity.ActivityHtmlTemplateID + ";" + ConstConfig.GAME + "21", arrOAuthScope);
    //                }
    //                else
    //                {
    //                    finalUrl = Senparc.Weixin.Open.OAuthAPIs.OAuthApi.GetAuthorizeUrl(mp.WeixinAppID, open.ComponentAppID, HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + "/weixin/Auth20CallBack", ConstConfig.ACTIVITY + currentActivity.Id.ToString() + ";" + ConstConfig.TEMPTATE + currentActivity.ActivityHtmlTemplateID, arrOAuthScope);
    //                }

    //            }
    //            catch (Exception ex)
    //            {
    //                logger.Error("GetActivityUrl", ex);
    //            }
    //        }
    //        return finalUrl;
    //    }

    //    /// <summary>
    //    /// 获得活动中奖的url
    //    /// </summary>
    //    /// <param name="currentActivity"></param>
    //    /// <returns></returns>
    //    public static string GetActivityAwardUrl(WeixinUserAward userAward)
    //    {
    //        string finalUrl = string.Empty;
    //        using (SensingSiteDbContext db = new SensingSiteDbContext())
    //        {
    //            try
    //            {
    //                Senparc.Weixin.Open.OAuthScope[] arrOAuthScope = new Senparc.Weixin.Open.OAuthScope[1];
    //                arrOAuthScope[0] = Senparc.Weixin.Open.OAuthScope.snsapi_base;
    //                var mp = db.WeixinPublicAccountInfos.FirstOrDefault(p => p.WeixinAppID == userAward.Activity.WeixinAppID);
    //                var open = db.WeixinOpenPlatforms.FirstOrDefault();
    //                finalUrl = Senparc.Weixin.Open.OAuthAPIs.OAuthApi.GetAuthorizeUrl(mp.WeixinAppID, open.ComponentAppID, HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + "/weixin/Auth20CallBack", ConstConfig.USERAWARD + userAward.Id.ToString() + ";" + ConstConfig.TEMPTATE + userAward.Activity.AwardMessage.HtmlTemplateID.Value.ToString(), arrOAuthScope);
    //            }
    //            catch (Exception ex)
    //            {
    //                logger.Error("GetActivityAwardUrl", ex);
    //            }
    //        }
    //        return finalUrl;
    //    }
    //}
}