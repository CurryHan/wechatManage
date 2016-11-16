//-----------------------------------------------------------------------
// <copyright file="BehaviorTargetLoggerManager.cs" company="troncell">
//     Copyright © troncell. All rights reserved.
// </copyright>
// <author>William Wu</author>
// <email>wulixu@troncell.com</email>
// <date>2012-10-15</date>
// <summary>no summary</summary>
//-----------------------------------------------------------------------
using System;
using log4net;
using log4net.Core;
using System.Reflection;

namespace LogService.BT
{
    public class BehaviorTargetLoggerManager
    {
        #region Static Member Variables

        /// <summary>
        /// The wrapper map to use to hold the <see cref="BehaviorTargetLoggerImpl"/> objects
        /// </summary>
        private static readonly WrapperMap s_wrapperMap = new WrapperMap(new WrapperCreationHandler(WrapperCreationHandler));

        #endregion

        /// <summary>
        /// Method to create the <see cref="ILoggerWrapper"/> objects used by
        /// this manager.
        /// </summary>
        /// <param name="logger">The logger to wrap</param>
        /// <returns>The wrapper for the logger specified</returns>
        private static ILoggerWrapper WrapperCreationHandler(ILogger logger)
        {
            return new BehaviorTargetLoggerImpl(logger);
        }
        /// <summary>
        /// Lookup the wrapper object for the logger specified
        /// </summary>
        /// <param name="logger">the logger to get the wrapper for</param>
        /// <returns>the wrapper for the logger specified</returns>
        private static IBehaviorTargetLogger WrapLogger(ILogger logger)
        {
            return (IBehaviorTargetLogger)s_wrapperMap.GetWrapper(logger);
        }
        /// <summary>
        /// Shorthand for <see cref="LogManager.GetLogger(string)"/>.
        /// </summary>
        /// <remarks>
        /// Get the logger for the fully qualified name of the type specified.
        /// </remarks>
        /// <param name="type">The full name of <paramref name="type"/> will 
        /// be used as the name of the logger to retrieve.</param>
        /// <returns>the logger with the name specified</returns>
        public static IBehaviorTargetLogger GetLogger(Type type)
        {
            return GetLogger(Assembly.GetCallingAssembly(), type.FullName);
        }
         /// <summary>
        /// Retrieve or create a named logger.
        /// </summary>
        /// <remarks>
        /// <para>Retrieve a logger named as the <paramref name="name"/>
        /// parameter. If the named logger already exists, then the
        /// existing instance will be returned. Otherwise, a new instance is
        /// created.</para>
        /// 
        /// <para>By default, loggers do not have a set level but inherit
        /// it from the hierarchy. This is one of the central features of
        /// log4net.</para>
        /// </remarks>
        /// <param name="assembly">the assembly to use to lookup the domain</param>
        /// <param name="name">The name of the logger to retrieve.</param>
        /// <returns>the logger with the name specified</returns>
        public static IBehaviorTargetLogger GetLogger(Assembly assembly, string name)
        {
            return WrapLogger(LoggerManager.GetLogger(assembly, name));
        }

    }
}