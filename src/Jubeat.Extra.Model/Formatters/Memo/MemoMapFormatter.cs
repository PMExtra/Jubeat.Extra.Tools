using System;
using System.IO;
using Common.Logging;
using Common.Logging.Simple;
using Jubeat.Extra.Logging;
using Jubeat.Extra.Models.Maps.Memo;

namespace Jubeat.Extra.Models.Formatters.Memo
{
    internal static class MemoMapFormatter
    {
        private static ILog GetLogger(TextWriter logWriter)
        {
            if (logWriter == Console.Out)
            {
                return new ConsoleOutLogger(nameof(MemoMap), LogLevel.All, true, false, true, null, !Console.IsOutputRedirected);
            }
            if (logWriter != null)
            {
                return new StreamLogger(logWriter, nameof(MemoMap), LogLevel.All, true, false, true, null);
            }
            return new NoOpLogger();
        }

        public static bool CheckMap(MemoMap map, bool repair, TextWriter logWriter)
        {
            var logger = GetLogger(logWriter);

            throw new NotImplementedException();
        }

        public static MemoMap ParseMap(TextReader reader)
        {
            throw new NotImplementedException();
        }

        public static string MapToString(MemoMap map)
        {
            throw new NotImplementedException();
        }
    }
}
