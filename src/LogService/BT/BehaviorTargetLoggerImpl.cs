//-----------------------------------------------------------------------
// <copyright file="BehaviorTargetLoggerImpl.cs" company="troncell">
//     Copyright © troncell. All rights reserved.
// </copyright>
// <author>William Wu</author>
// <email>wulixu@troncell.com</email>
// <date>2012-10-15</date>
// <summary>no summary</summary>
//-----------------------------------------------------------------------

using log4net.Core;

namespace LogService.BT
{
    class BehaviorTargetLoggerImpl : LogImpl, IBehaviorTargetLogger
    {

        public BehaviorTargetLoggerImpl(ILogger logger)
            : base(logger)
        {
        }
        #region  Private Members

        private void WriteLog(string pageName, string controlName, string eventAction, string eventStr,bool isOpenEvent)
        {
            if (string.IsNullOrEmpty(pageName)) pageName = string.Empty;
            if (string.IsNullOrEmpty(controlName)) controlName = string.Empty;

            string msg;
            if (ServerLogFactory.DebugMode)
            {
                msg = string.Format("TriggerType = [{0}] ,PageName =[{1}] , ControlName=[{2}] BTEventType=[{3}] BTEvent=[{4}]"
                    , isOpenEvent ? "Open" : "Close", pageName, controlName, eventAction, eventStr);
            }
            else
            {
                msg = string.Empty;
            }
            LoggingEvent loggingEvent = CreateLoggingEvent(msg);
            loggingEvent.Properties["PageName"] = pageName;
            loggingEvent.Properties["ControlName"] = controlName;
            loggingEvent.Properties["BTEventAction"] = eventAction;
            loggingEvent.Properties["BTEventQuery"] = eventStr;

            //TODO:
            Logger.Log(loggingEvent);
        }

        private LoggingEvent CreateLoggingEvent(string message)
        {
            return new LoggingEvent(typeof(BehaviorTargetLoggerImpl), Logger.Repository, Logger.Name, Level.Notice, message, null);
        } 



        #endregion

        #region IBehaviorTargetLogger Members

        public void RecordOpenEvent(string pageName, string controlName, string eventType, string eventStr)
        {
            WriteLog(pageName, controlName, eventType, eventStr, true);
        }

        public void RecordOpenEvent(string eventType, string eventStr)
        {
            WriteLog(string.Empty, string.Empty, eventType, eventStr, true);
        }

        public void RecordCloseEvent(string pageName, string controlName, string eventType, string eventStr)
        {
            WriteLog(pageName, controlName, eventType, eventStr, false);
        }

        public void RecordCloseEvent(string eventType, string eventStr)
        {
            WriteLog(string.Empty, string.Empty, eventType, eventStr, false);
        }

        #endregion


        public void RecordEvent(string fromPage, string controlName, string eventAction, string eventParams)
        {
            WriteLog(fromPage, controlName, eventAction, eventParams, false);
        }
    }
}