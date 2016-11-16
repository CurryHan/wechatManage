using LogService;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sensing.Thirtparty.Exstension.Model;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Sensing.Thirtparty.Exstension
{
    public class DefaultBussiness : IThirtpartyBussiness
    {
        protected static readonly IBizLogger logger = ServerLogFactory.GetLogger(typeof(DefaultBussiness));

        public string UserName { get; set; }


        public string Password { get; set; }



        private static CamelCasePropertyNamesContractResolver s_defaultResolver = new CamelCasePropertyNamesContractResolver();

        private const string JsonHeader = "application/json";

        //public string WeixinAppID { get { return ""; } set; }
        private static JsonSerializerSettings s_settings = new JsonSerializerSettings()
        {
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = s_defaultResolver
        };

        //private  HttpClient s_httpClient = new HttpClient(new HttpClientHandler { Credentials = new NetworkCredential(UserName, Password) });


        public async Task PostUserData(string userDataString, string postUrl)
        {
            ThirdPartyRequest userData = JsonConvert.DeserializeObject<ThirdPartyRequest>(userDataString, s_settings);
            var obj = new { openId = userData.openId, appId = userData.appId, integral = userData.score, createTime = DateTime.Now.ToString("yyyyMMddHHmmss") };
            await SendRequestAsync<object, object>(HttpMethod.Post, postUrl, obj);
        }

        public async Task<string> PostUserData(ThirdPartyRequest userData, string postUrl)
        {
            logger.Debug("DefaultBussiness:PostUserData is start");
            //ThirdPartyRequest userData = JsonConvert.DeserializeObject<ThirdPartyRequest>(userDataString, s_settings);
            //var obj = new ThirdPartyRequest {  Openid = userData.Openid, appId = userData.Appid, integral = userData.Score, createTime = DateTime.Now.ToString("yyyyMMddHHmmss") };
            logger.Debug($"userData.activityName is {userData.activityName}");
            logger.Debug($"userData.appId is {userData.appId}");
            logger.Debug($"userData.createdTime is {userData.createdTime}");
            logger.Debug($"userData.gameName is {userData.gameName}");
            logger.Debug($"userData.score is {userData.score}");
            logger.Debug($"userData.isAfterGame is {userData.isAfterGame}");
            logger.Debug($"userData.openId is {userData.openId}");
            logger.Debug($"userData.signStr is {userData.signStr}");

            var result = await SendRequestAsync<ThirdPartyRequest>(HttpMethod.Post, postUrl, userData);
            return result;
        }

        private async Task<TResponse> SendRequestAsync<TRequest, TResponse>(HttpMethod httpMethod, string requestUrl, TRequest requestBody)
        {
            logger.Debug($"DefaultBussiness:SendRequestAsync is start,request url is {requestUrl}");
            HttpClient s_httpClient = null;

            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
            {
                s_httpClient = new HttpClient(new HttpClientHandler
                {
                    Credentials = new NetworkCredential(UserName, Password)
                });
            }
            else
            {
                s_httpClient = new HttpClient(new HttpClientHandler
                {
                    Credentials = new NetworkCredential()
                });
            }
            var request = new HttpRequestMessage(httpMethod, requestUrl);


            request.RequestUri = new Uri(requestUrl);
            if (requestBody != null)
            {
                if (requestBody is Stream)
                {
                    request.Content = new StreamContent(requestBody as Stream);
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                }
                else if (requestBody is string)
                {
                    request.Content = new StringContent(requestBody as string);
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                }
                else
                {
                    request.Content = new StringContent(JsonConvert.SerializeObject(requestBody, s_settings), Encoding.UTF8, JsonHeader);
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                }
            }


            HttpResponseMessage response = await s_httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string responseContent = null;
                if (response.Content != null)
                {
                    responseContent = await response.Content.ReadAsStringAsync();
                }

                if (!string.IsNullOrWhiteSpace(responseContent))
                {

                    return JsonConvert.DeserializeObject<TResponse>(responseContent, s_settings);
                }

                return default(TResponse);
            }
            else
            {
                if (response.Content != null && response.Content.Headers.ContentType.MediaType.Contains(JsonHeader))
                {
                    var errorObjectString = await response.Content.ReadAsStringAsync();
                    ClientError errorCollection = JsonConvert.DeserializeObject<ClientError>(errorObjectString);
                    if (errorCollection != null)
                    {
                        throw new ClientException(errorCollection, response.StatusCode);
                    }
                }

                response.EnsureSuccessStatusCode();
            }

            return default(TResponse);
        }

        private async Task<string> SendRequestAsync<TRequest>(HttpMethod httpMethod, string requestUrl, TRequest requestBody)
        {
            logger.Debug($"DefaultBussiness:SendRequestAsync is start,request url is {requestUrl}");
            HttpClient s_httpClient = null;

            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
            {
                s_httpClient = new HttpClient(new HttpClientHandler
                {
                    Credentials = new NetworkCredential(UserName, Password)
                });
            }
            else
            {
                s_httpClient = new HttpClient(new HttpClientHandler
                {
                    Credentials = new NetworkCredential()
                });
            }
            var request = new HttpRequestMessage(httpMethod, requestUrl);


            request.RequestUri = new Uri(requestUrl);
            if (requestBody != null)
            {
                if (requestBody is Stream)
                {
                    request.Content = new StreamContent(requestBody as Stream);
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                }
                else if (requestBody is string)
                {
                    request.Content = new StringContent(requestBody as string);
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                }
                else
                {
                    request.Content = new StringContent(JsonConvert.SerializeObject(requestBody, s_settings), Encoding.UTF8, JsonHeader);
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                }
            }


            HttpResponseMessage response = await s_httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string responseContent = null;
                if (response.Content != null)
                {
                    responseContent = await response.Content.ReadAsStringAsync();
                }
                return responseContent;
            }
            else
            {
                if (response.Content != null && response.Content.Headers.ContentType.MediaType.Contains(JsonHeader))
                {
                    var errorObjectString = await response.Content.ReadAsStringAsync();
                    ClientError errorCollection = JsonConvert.DeserializeObject<ClientError>(errorObjectString);
                    if (errorCollection != null)
                    {
                        throw new ClientException(errorCollection, response.StatusCode);
                    }
                }

                response.EnsureSuccessStatusCode();
            }

            return default(string);
        }


    }

    //public class UserData
    //{
    //    public int Id { get; set; }
    //    public string Appid { get; set; }
    //    public string Openid { get; set; }
    //    public string Nickname { get; set; }
    //    public int Sex { get; set; }
    //    public object Language { get; set; }
    //    public string City { get; set; }
    //    public string Country { get; set; }
    //    public string Province { get; set; }
    //    public string HeadImgUrl { get; set; }
    //    public string Unionid { get; set; }
    //    public string Remark { get; set; }
    //    public object WeixinGroupid { get; set; }
    //    public string QrCodeId { get; set; }
    //    public object Score { get; set; }
    //    public object PostUrl { get; set; }
    //    public object GameImage { get; set; }
    //    public object PlayerImage { get; set; }
    //    public int PlayerAge { get; set; }
    //    public int ShareCount { get; set; }
    //    public int ViewCount { get; set; }
    //    public int LikeCount { get; set; }
    //}

}
