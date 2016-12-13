using System;
using System.IO;
using System.Text;
using Common.Logging;
using Common.Logging.Simple;

namespace Jubeat.Extra.Logging
{
    public class StreamLogger : AbstractSimpleLogger
    {
        private readonly TextWriter _writer;

        /// <summary>
        ///     Creates and initializes a logger that writes messages to <see cref="Console.Out" />.
        /// </summary>
        /// <param name="logName">The name, usually type name of the calling class, of the logger.</param>
        /// <param name="logLevel">
        ///     The current logging threshold. Messages recieved that are beneath this threshold will not be
        ///     logged.
        /// </param>
        /// <param name="writer"></param>
        /// <param name="showLevel">Include the current log level in the log message.</param>
        /// <param name="showDateTime">Include the current time in the log message.</param>
        /// <param name="showLogName">Include the instance name in the log message.</param>
        /// <param name="dateTimeFormat">The date and time format to use in the log message.</param>
        public StreamLogger(TextWriter writer, string logName, LogLevel logLevel, bool showLevel, bool showDateTime, bool showLogName, string dateTimeFormat)
            : base(logName, logLevel, showLevel, showDateTime, showLogName, dateTimeFormat)
        {
            _writer = writer;
        }

        /// <summary>
        ///     Do the actual logging by constructing the log message using a <see cref="StringBuilder" /> then
        ///     sending the output to <see cref="Console.Out" />.
        /// </summary>
        /// <param name="level">The <see cref="LogLevel" /> of the message.</param>
        /// <param name="message">The log message.</param>
        /// <param name="e">An optional <see cref="Exception" /> associated with the message.</param>
        protected override void WriteInternal(LogLevel level, object message, Exception e)
        {
            // Use a StringBuilder for better performance
            var sb = new StringBuilder();
            FormatOutput(sb, level, message, e);

            _writer.WriteLine(sb.ToString());
        }
    }
}
