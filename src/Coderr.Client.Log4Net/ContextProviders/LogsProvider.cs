using System;
using System.Collections.Generic;
using System.Linq;
using Coderr.Client.ContextCollections;
using Coderr.Client.Contracts;
using Coderr.Client.Reporters;

namespace Coderr.Client.Log4Net.ContextProviders
{
    /// <summary>
    ///     This provider do not add an collection but attaches the 50 latest log entries to each report.
    /// </summary>
    public class LogsProvider : IContextCollectionProvider
    {
        private static readonly LinkedList<LogEntryDto> Logs = new LinkedList<LogEntryDto>();

        /// <inheritdoc />
        public ContextCollectionDTO Collect(IErrorReporterContext context)
        {
            if (!(context is IContextWithLogEntries logCtx))
            {
                return null;
            }

            lock (Logs)
            {
                if (!Logs.Any())
                {
                    return null;
                }

                var myLogs = Logs.ToArray();
                logCtx.LogEntries = myLogs;
                return null;
            }
        }

        /// <inheritdoc />
        public string Name { get; } = "Logs";

        /// <summary>
        ///     Add a new log entry to the internal collection.
        /// </summary>
        /// <param name="dto">entry</param>
        public static void Add(LogEntryDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            lock (Logs)
            {
                Logs.AddLast(dto);
                while (Logs.Count > 50) Logs.RemoveLast();
            }
        }
    }
}