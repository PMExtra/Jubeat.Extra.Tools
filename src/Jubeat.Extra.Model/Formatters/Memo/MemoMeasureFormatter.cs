using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Common.Logging;
using Common.Logging.Simple;
using Jubeat.Extra.Logging;
using Jubeat.Extra.Models.Maps.Memo;
using static Jubeat.Extra.Models.Maps.Memo.MemoDefinitions;

namespace Jubeat.Extra.Models.Formatters.Memo
{
    internal static class MemoMeasureFormatter
    {
        private static ILog GetLogger(TextWriter logWriter)
        {
            if (logWriter == Console.Out)
            {
                return new ConsoleOutLogger(nameof(MemoMeasure), LogLevel.All, true, false, true, null, !Console.IsOutputRedirected);
            }
            if (logWriter != null)
            {
                return new StreamLogger(logWriter, nameof(MemoMeasure), LogLevel.All, true, false, true, null);
            }
            return new NoOpLogger();
        }

        public static bool CheckMeasure(MemoMeasure measure, bool repair, TextWriter logWriter)
        {
            var logger = GetLogger(logWriter);

            throw new NotImplementedException();
        }

        public static MemoMeasure ParseMeasure(IReadOnlyList<string> lines)
        {
            return ParseMeasure(lines.Select(l => MemoLineRegex.Match(l)).ToList());
        }

        public static MemoMeasure ParseMeasure(IReadOnlyList<Match> matches)
        {
            Debug.Assert((matches.Count > 0) && (matches.Count % 4 == 0));
            Debug.Assert(matches.All(m => m.Groups["buttons"].Success));

            var measure = new MemoMeasure();

            var scenes = Enumerable.Range(0, matches.Count / 4)
                .Select(i => matches.Skip(i * 4).Take(4).ToArray())
                .Select(s => MemoScene.Parse(s.Select(m => m.Groups["buttons"].Value).ToArray()));

            var beats = matches.Where(m => m.Groups["beat"].Success)
                .Select(m => MemoBeat.Parse(m.Groups["beat"].Value));

            measure.Scenes.AddRange(scenes);
            measure.Beats.AddRange(beats);

            return measure;
        }

        public static string MeasureToString(MemoMeasure measure, MemoStyle style)
        {
            var sb = new StringBuilder();
            sb.AppendLine(measure.Ordinal.ToString());

            using (var ibeat = measure.Beats.GetEnumerator())
            {
                var hasBeat = ibeat.MoveNext();
                foreach (var scene in measure.Scenes)
                {
                    foreach (var line in MemoSceneFormatter.SceneToStrings(scene))
                    {
                        sb.Append(line);
                        if (hasBeat && (ibeat.Current.Hits.First(x => x > 0) <= scene.OfType<MemoNote>().Max(p => p.Ordinal)))
                        {
                            sb.Append(DefaultSpace);
                            sb.Append(ibeat.Current);
                            hasBeat = ibeat.MoveNext();
                        }
                        sb.AppendLine();
                    }
                    if (scene != measure.Scenes.Last())
                    {
                        sb.AppendLine();
                    }
                }
            }

            return sb.ToString();
        }
    }
}
