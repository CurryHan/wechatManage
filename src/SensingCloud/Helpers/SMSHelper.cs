using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using SensingCloud.Services;

namespace SensingCloud.Helpers
{
    public class SMSHelper
    {
        private string SendUrl;
        private string UID;
        private string Password;
        private string Signature;
        private string SMSFormat = @"uid={0}&pwd={1}&mobile={2}&content={3}Signature";
        private readonly ISystemSettingService _systemSetting;

//        static SMSHelper()
//        {
//            SendUrl = ConfigurationManager.AppSettings["SMS_API_URL"];
//            UID = ConfigurationManager.AppSettings["SMS_UID"];

//            var pass = ConfigurationManager.AppSettings["SMS_PWD"];
//#pragma warning disable 618
//            //密码进行MD5加密,详细参照sms.cn的官方说明.
//            Password = FormsAuthentication.HashPasswordForStoringInConfigFile(pass + UID, "MD5");
//#pragma warning restore 618

//            Signature = ConfigurationManager.AppSettings["SMS_MESSAGESIGNATURE"];
//            SMSFormat = @"uid={0}&pwd={1}&mobile={2}&content={3}" + Signature;
//        }

        public SMSHelper(ISystemSettingService systemSettingService)
        {
            this._systemSetting = systemSettingService;
           
        }

        public async  Task<bool> SendSMS(string mobiles, string message)
        {
            var temp = _systemSetting.GetPlatformNotification();
            if (temp != null)
            {
                SendUrl = temp.SmsUrl;
                UID = temp.SmsUID;
                Password = FormsAuthentication.HashPasswordForStoringInConfigFile(temp.SmsPassword + UID, "MD5");
                Signature = "【" + temp.MessageSignatrue + "】";
                SMSFormat = @"uid={0}&pwd={1}&mobile={2}&content={3}" + Signature;
            }
            var sbTemp = new StringBuilder();
            using (var httpClient = new HttpClient())
            {
                //POST 传值
                string postBody = string.Format(SMSFormat, UID, Password, mobiles, message);
                sbTemp.Append(postBody);
                //we use the UTF8 encoding for the specificition, 想起参照官方说明
                byte[] bTemp = System.Text.Encoding.UTF8.GetBytes(sbTemp.ToString());

                ByteArrayContent content = new ByteArrayContent(bTemp);
                httpClient.Timeout = TimeSpan.FromMilliseconds(5000);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                HttpResponseMessage response = await httpClient.PostAsync(SendUrl, content);
                // Check that response was successful or throw exception 
                response.EnsureSuccessStatusCode();
                // Read response asynchronously as JsonValue and write out top facts for each country 
                string responseString =await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(responseString) && responseString.Contains("stat=100"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
               
            }
        }



    }
}