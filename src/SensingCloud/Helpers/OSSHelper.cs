using Aliyun.OSS;
using Aliyun.OSS.Common;
using LogService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace SensingCloud.Helpers
{
    public class OSSHelper
    {

        protected static readonly IBizLogger logger = ServerLogFactory.GetLogger(typeof(OSSHelper));

        static string accessKeyId = ConfigurationManager.AppSettings["OOS_AccessKeyId"];
        static string accessKeySecret = ConfigurationManager.AppSettings["OOS_AccessKeySecret"];
        static string endpoint = ConfigurationManager.AppSettings["OOS_Endpoint"];
        static string bucketName = ConfigurationManager.AppSettings["OOS_DefaultBucketName"];
        static OssClient client = new OssClient(endpoint, accessKeyId, accessKeySecret);

        //uri template: http://gamecloud.oss-cn-shanghai.aliyuncs.com/upload/Customer/3/Activity/2/Data/2016091917330711168761.png
        public static string UploadFile(Stream fileStream,string filePath)
        {
            try
            {
                var result = client.PutObject(bucketName, filePath, fileStream);
            }
            catch (OssException ex)
            {
                logger.Error("upload file to OOS Failed with error info :", ex);
            }
            catch (Exception ex)
            {
                logger.Error("upload to OSS Failed with error info :", ex);
            }
            return $"http://{bucketName}.{endpoint}/{filePath}";
        }
    }
}