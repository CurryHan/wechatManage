//-----------------------------------------------------------------------
// <copyright file="IBizLogger.cs" company="troncell">
//     Copyright © troncell. All rights reserved.
// </copyright>
// <author>William Wu</author>
// <email>wulixu@troncell.com</email>
// <date>2012-10-15</date>
// <summary>no summary</summary>
//-----------------------------------------------------------------------
using System;

namespace LogService
{
    public interface IBizLogger
    {
        void Debug(object o);
        void Debug(object o, Exception e);
        void DebugParams(string msgStr, params string[] paramArr);
        void Warning(object o);
        void Warning(object o, Exception e);
        void Error(object o);
        void ErrorParams(string errorStr, params string[] paramArr);
        void Error(object o, Exception e);
    }
}