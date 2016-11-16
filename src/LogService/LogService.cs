//-----------------------------------------------------------------------
// <copyright file="LogService.cs" company="troncell">
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
    public interface ILogService
    {
        IBizLogger GetLogger(Type t);
        IBizLogger GetLogger(string t);
    }
    
    
    public class LogService : ILogService
    {
        public IBizLogger GetLogger(Type t)
        {
            return new BizLogger(LogManager.GetLogger(t));
        }
        public IBizLogger GetLogger(string t)
        {
            return new BizLogger(LogManager.GetLogger(t));
        }
    }
}