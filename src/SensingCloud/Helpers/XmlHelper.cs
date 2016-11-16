using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace SensingCloud.Helpers
{
    public class XmlHelper
    {
        public static string GetXmlValue(string xmlContent,string key)
        {
            string result = string.Empty;
            if(!string.IsNullOrEmpty(xmlContent))
            {
                XmlDocument dom = new XmlDocument();
                dom.LoadXml(xmlContent);
                XmlElement root = null;
                root = dom.DocumentElement;
                XmlNodeList listNodes = null;
                listNodes = root.SelectNodes("/xml/"+ key);
                if (listNodes.Count > 0)
                    result = listNodes[0].InnerText;
            }
            return result;
        }
    }
}