using System;
using log4net;
using log4net.Core;
using log4net.Repository.Hierarchy;
using Coderr.Client.Config;
using Coderr.Client.log4net;

// ReSharper disable once CheckNamespace

namespace Coderr.Client
{
    /// <summary>
    ///     Adds the codeRR logger to log4net.
    /// </summary>
    public static class ConfigExtensions
    {
        /// <summary>
        ///     Adds the codeRR logger to log4net.
        /// </summary>
        /// <param name="config">config</param>
        /// <exception cref="NotSupportedException">
        ///     This configuration/version of Log4Net do not allow dynamic adding of appenders.
        ///     Configure this adapter using code instead. See our online documentation for an example.
        /// </exception>
        public static void CatchLog4NetExceptions(this CoderrConfiguration config)
        {
            if (config == null) throw new ArgumentNullException("config");

            var root = ((Hierarchy) LogManager.GetRepository()).Root;
            var attachable = root as IAppenderAttachable;
            if (attachable == null)
                throw new NotSupportedException(
                    "This configuration/version of Log4Net do not allow dynamic adding of appenders. Configure this adapter using code instead. See our online documentation for an example.");

            var appender = new CoderrAppender();
            appender.ActivateOptions();
            attachable.AddAppender(appender);
        }
    }
}