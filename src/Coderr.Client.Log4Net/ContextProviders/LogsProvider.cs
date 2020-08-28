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
        /// <summary>
        /// Instance used to add log4net lines.
        /// </summary>
        public static readonly LogsProvider Instance = new LogsProvider();

        private readonly LinkedList<LogEntryDto> Logs = new LinkedList<LogEntryDto>();

        /// <inheritdoc />
        public ContextCollectionDTO Collect(IErrorReporterContext context)
        {
            if (!(context is IContextWithLogEntries logCtx))
            {
                return null;
            }

            RemoveOldEntries();
            lock (Logs)
            {
                if (!Logs.Any())
                {
                    return null;
                }

                if (logCtx.LogEntries?.Length > 0)
                {
                    var entries = logCtx.LogEntries.ToList();
                    entries.AddRange(Logs);
                    logCtx.LogEntries = entries
                        .OrderBy(x => x.TimestampUtc)
                        .ToArray();
                }
                else
                {
                    logCtx.LogEntries = Logs.ToArray();
                }

                return null;
            }
        }

        /// <inheritdoc />
        public string Name { get; } = "Logs";

        /// <summary>
        ///     Add a new log entry to the internal collection.
        /// </summary>
        /// <param name="dto">entry</param>
        public void Add(LogEntryDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            lock (Logs)
            {
                Logs.AddLast(dto);
                while (Logs.Count > 50)
                {
                    Logs.RemoveLast();
                }
            }
        }

        private void RemoveOldEntries()
        {
            lock (Logs)
            {
                var threshold = DateTime.UtcNow.AddMinutes(-5);
                while (Logs.First != null && Logs.First.Value.TimestampUtc < threshold)
                {
                    Logs.RemoveFirst();
                }
            }
        }
    }
}