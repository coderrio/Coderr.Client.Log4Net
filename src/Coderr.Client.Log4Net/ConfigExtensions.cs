using System;
using System.Linq;
using System.Reflection;
using log4net;
using log4net.Core;
using log4net.Repository.Hierarchy;
using Coderr.Client.Config;
using Coderr.Client.log4net;
using Coderr.Client.Log4Net.ContextProviders;
using log4net.Layout;
using log4net.Repository;

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
            if (config == null) throw new ArgumentNullException(nameof(config));

            CatchLog4NetExceptions(config, Assembly.GetCallingAssembly());
        }

        /// <summary>
        ///     Adds the codeRR logger to log4net.
        /// </summary>
        /// <param name="config">config</param>
        /// <param name="assembly">Assembly that log4net was configured in (typically your entry assembly)</param>
        /// <exception cref="NotSupportedException">
        ///     This configuration/version of Log4Net do not allow dynamic adding of appenders.
        ///     Configure this adapter using code instead. See our online documentation for an example.
        /// </exception>
        public static void CatchLog4NetExceptions(this CoderrConfiguration config, Assembly assembly)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));

            Err.Configuration.ContextProviders.Add(LogsProvider.Instance);

            var repository = LogManager.GetRepository(assembly) as Hierarchy;
            if (repository?.Configured != true)
                throw new InvalidOperationException("log4net has not yet been configured. It must be configured before activating Coderr, or Coderr wont be able to receive log entries.");

            foreach (var rootAppender in repository.Root.Appenders)
            {
                if (rootAppender is CoderrAppender)
                {
                    return;
                }
            }

            if (repository.Root.Appenders.Count == 0)
            {
                throw new InvalidOperationException("Did not detect any log4net appenders.");
            }

            if (!(repository.Root is IAppenderAttachable attachable))
                throw new NotSupportedException(
                    "This configuration/version of Log4Net do not allow dynamic adding of appenders. Configure this adapter using code instead. See our online documentation for an example.");

            var appender = new CoderrAppender();
            appender.ActivateOptions();
            attachable.AddAppender(appender);
        }

    }
}