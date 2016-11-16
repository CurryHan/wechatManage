//-----------------------------------------------------------------------
// <copyright file="IBehaviorTargetLogger.cs" company="troncell">
//     Copyright © troncell. All rights reserved.
// </copyright>
// <author>William Wu</author>
// <email>wulixu@troncell.com</email>
// <date>2012-10-15</date>
// <summary>no summary</summary>
//-----------------------------------------------------------------------

namespace LogService.BT
{
    public interface IBehaviorTargetLogger
    {

        /// <summary>
        /// 记录开启事件的行为日志
        /// </summary>
        /// <param name="pageName">当前页面名称，可空</param>
        /// <param name="controlName">当前控件名称，可空</param>
        /// <param name="eventType"></param>
        /// <param name="eventStr">400字符长度</param>
        void RecordOpenEvent(string pageName, string controlName, string eventType, string eventStr);

        /// <summary>
        /// 记录开启事件的行为日志，只写类型，和数据
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="eventStr">400字符长度</param>
        void RecordOpenEvent(string eventType, string eventStr);

        /// <summary>
        /// 记录用户的交互事件.
        /// </summary>
        /// <param name="fromPage">当前的页面</param>
        /// <param name="controlName">当前的控件名字</param>
        /// <param name="eventAction">用户交互的事件名称</param>
        /// <param name="eventParams">交互事件的参数</param>
        void RecordEvent(string fromPage, string controlName, string eventAction, string eventParams);
        /// <summary>
        /// 记录关闭事件的行为日志
        /// </summary>
        /// <param name="pageName">当前页面名称，可空</param>
        /// <param name="controlName">当前控件名称，可空</param>
        /// <param name="eventType"></param>
        /// <param name="eventStr">400字符长度</param>
        void RecordCloseEvent(string pageName, string controlName, string eventType, string eventStr);

        /// <summary>
        /// 记录关闭事件的行为日志，只写类型，和数据
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="eventStr">400字符长度</param>
        void RecordCloseEvent(string eventType, string eventStr);
    }
}