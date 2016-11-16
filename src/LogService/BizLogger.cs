//-----------------------------------------------------------------------
// <copyright file="BizLogger.cs" company="troncell">
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
    public class BizLogger : IBizLogger, IDisposable
    {
        private ILog logger;
        public void Debug(object msg)
        {
            if (ServerLogFactory.DebugMode)
            {
                logger.Debug(msg);
            }
        }
        public void Debug(object msg,Exception e)
        {
            if (ServerLogFactory.DebugMode)
            {
                logger.Debug(msg, e);
            }
            
        }
        public void Warning(object msg)
        {
            logger.Warn(msg);
        }
        public void Warning(object msg, Exception e)
        {
            logger.Warn(msg,e);
        }
        public void Error(object msg)
        {
            logger.Error(msg);
        }
        public void Error(object msg, Exception e)
        {
            logger.Error(msg,e);
        }
        public BizLogger(ILog logger)
        {
            this.logger = logger;
        }

        #region IBizLogger Members


        public void ErrorParams(string errorStr, params string[] paramArr)
        {
            string errorMsg = string.Format(errorStr, paramArr);
            Error(errorMsg);
        }

        #endregion

        #region IBizLogger Members


        public void DebugParams(string msgStr, params string[] paramArr)
        {
            string errorMsg = string.Format(msgStr, paramArr);
            Debug(errorMsg);
        }

        #endregion

        //public void Log(string message, Category category, Priority priority)
        //{
        //    if (category == Category.Debug)
        //        logger.Debug(message);
        //    else if (category == Category.Exception)
        //        logger.Error(message);
        //    else if (category == Category.Info)
        //        logger.Info(message);
        //    else if (category == Category.Warn)
        //        logger.Warn(message);
        //}

        /// <summary>
        /// Disposes the associated <see cref="TextWriter"/>.
        /// </summary>
        /// <param name="disposing">When <see langword="true"/>, disposes the associated <see cref="TextWriter"/>.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //TODO:release the unmanaged resources
                //if (writer != null)
                //{
                //    writer.Dispose();
                //}
            }
        }

        ///<summary>
        ///Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        ///</summary>
        /// <remarks>Calls <see cref="Dispose(bool)"/></remarks>.
        ///<filterpriority>2</filterpriority>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}