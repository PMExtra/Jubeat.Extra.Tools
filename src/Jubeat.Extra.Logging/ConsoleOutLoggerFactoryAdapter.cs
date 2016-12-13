using System;
using Common.Logging;
using Common.Logging.Configuration;
using Common.Logging.Simple;

namespace Jubeat.Extra.Logging
{
    /// <summary>
    ///     Factory for creating <see cref="ILog" /> instances that write data to <see cref="Console.Out" />.
    /// </summary>
    /// <seealso cref="AbstractSimpleLoggerFactoryAdapter" />
    /// <seealso cref="LogManager.Adapter" />
    public class ConsoleOutLoggerFactoryAdapter : AbstractSimpleLoggerFactoryAdapter
    {
        private readonly bool _useColor;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConsoleOutLoggerFactoryAdapter" /> class using default
        ///     settings.
        /// </summary>
        public ConsoleOutLoggerFactoryAdapter()
            : base(null)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConsoleOutLoggerFactoryAdapter" /> class.
        /// </summary>
        /// <remarks>
        ///     Looks for level, showDateTime, showLogName, dateTimeFormat items from
        ///     <paramref name="properties" /> for use when the GetLogger methods are called.
        /// </remarks>
        /// <param name="properties">
        ///     The name value collection, typically specified by the user in
        ///     a configuration section named common/logging.
        /// </param>
        public ConsoleOutLoggerFactoryAdapter(NameValueCollection properties)
            : base(properties)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AbstractSimpleLoggerFactoryAdapter" /> class with
        ///     default settings for the loggers created by this factory.
        /// </summary>
        public ConsoleOutLoggerFactoryAdapter(LogLevel level, bool showDateTime, bool showLogName, bool showLevel, string dateTimeFormat)
            : base(level, showDateTime, showLogName, showLevel, dateTimeFormat)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AbstractSimpleLoggerFactoryAdapter" /> class with
        ///     default settings for the loggers created by this factory.
        /// </summary>
        public ConsoleOutLoggerFactoryAdapter(LogLevel level, bool showDateTime, bool showLogName, bool showLevel, string dateTimeFormat, bool useColor)
            : this(level, showDateTime, showLogName, showLevel, dateTimeFormat)
        {
            _useColor = useColor;
        }

        /// <summary>
        ///     Creates a new <see cref="ConsoleOutLogger" /> instance.
        /// </summary>
        protected override ILog CreateLogger(string name, LogLevel level, bool showLevel, bool showDateTime, bool showLogName, string dateTimeFormat)
        {
            return new ConsoleOutLogger(name, level, showLevel, showDateTime, showLogName, dateTimeFormat, _useColor);
        }
    }
}
