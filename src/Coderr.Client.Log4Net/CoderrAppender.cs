using log4net.Appender;
using log4net.Core;

namespace codeRR.Client.log4net
{
    /// <summary>
    ///     Our appender for logging.
    /// </summary>
    /// <remarks>
    ///     <para>Will upload all log entries that contains exceptions to codeRR.</para>
    /// </remarks>
    public class CoderrAppender : AppenderSkeleton
    {
        /// <summary>
        /// Uploads all log entries that contains an exception to codeRR.
        /// </summary>
        /// <param name="loggingEvent">The logging event.</param>
        protected override void Append(LoggingEvent loggingEvent)
        {
            if (loggingEvent.ExceptionObject == null)
                return;

            Err.Report(loggingEvent.ExceptionObject, new LogEntryDetails
            {
                LogLevel = loggingEvent.Level.ToString(),
                Message = loggingEvent.RenderedMessage,
                ThreadName = loggingEvent.ThreadName,
                Timestamp = loggingEvent.TimeStamp
            });
        }
    }
}