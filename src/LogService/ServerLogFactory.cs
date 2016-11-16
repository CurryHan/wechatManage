//-----------------------------------------------------------------------
// <copyright file="ServerLogFactory.cs" company="troncell">
//     Copyright © troncell. All rights reserved.
// </copyright>
// <author>William Wu</author>
// <email>wulixu@troncell.com</email>
// <date>2012-10-15</date>
// <summary>no summary</summary>
//-----------------------------------------------------------------------
using System;
using log4net;

namespace LogService
{

    public class ServerLogFactory
    {
        public static bool DebugMode=true;
        
        static ServerLogFactory()
        {
            //log4net.Config.XmlConfigurator.Configure();
        }
        public static IBizLogger GetLogger(Type t)
        {
            return new BizLogger(LogManager.GetLogger(t));
        }
        public static IBizLogger GetLogger(string s)
        {
            return new BizLogger(LogManager.GetLogger(s));
        }
    }
}