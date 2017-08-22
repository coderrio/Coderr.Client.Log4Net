using System;

namespace OneTrueError.Client.log4net
{
    /// <summary>
    ///     Context View Model attached to all reported exceptions
    /// </summary>
    public class LogEntryDetails
    {
        /// <summary>
        ///     Logged message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Used log4net log level
        /// </summary>
        public string LogLevel { get; set; }

        /// <summary>
        ///     Thread that logged the error
        /// </summary>
        public string ThreadName { get; set; }

        /// <summary>
        ///     log4net time stamp
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}