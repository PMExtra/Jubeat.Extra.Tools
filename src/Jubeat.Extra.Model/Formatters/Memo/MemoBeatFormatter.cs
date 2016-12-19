using System.Diagnostics;
using System.Linq;
using System.Text;
using Jubeat.Extra.Models.Maps.Memo;
using static Jubeat.Extra.Models.Maps.Memo.MemoDefinitions;

namespace Jubeat.Extra.Models.Formatters.Memo
{
    internal static class MemoBeatFormatter
    {
        private const string AllValidCharset = RestCharset + CircleNumberCharset + HwNumberCharset + FwNumberCharset;
        private static readonly string BeatCharset = DefaultRest + CircleNumberCharset;

        private static readonly string[] AllNumberCharsets =
        {
            CircleNumberCharset,
            HwNumberCharset,
            FwNumberCharset
        };

        public static string BeatToString(MemoBeat beat, MemoStyle style)
        {
            var sb = new StringBuilder();
            sb.Append(DefaultSeperator);

            var hits = beat.Hits;
            var bpm = beat.Bpm;
            for (var i = 0; i < hits.Count; i++)
            {
                if (bpm.ContainsKey(i))
                {
                    sb.Append($"({bpm[i]})");
                }
                sb.Append(BeatCharset[hits[i]]);
            }

            sb.Append(DefaultSeperator);
            return sb.ToString();
        }

        public static MemoBeat ParseBeat(string s)
        {
            var beat = new MemoBeat();

            var matches = BeatRegex.Matches(s);
            Debug.Assert(matches.Count > 0);

            for (var i = 0; i < matches.Count; i++)
            {
                var match = matches[i];
                var bpm = match.Groups["bpm"];
                if (bpm.Success)
                {
                    beat.Bpm[i] = int.Parse(bpm.Value);
                }

                var hit = match.Groups["hit"].Value.Single();
                Debug.Assert(AllValidCharset.Contains(hit));
                if (RestCharset.Contains(hit))
                {
                    beat.Hits.Add(0);
                }
                foreach (var charset in AllNumberCharsets)
                {
                    var index = charset.IndexOf(hit);
                    if (index >= 0)
                    {
                        beat.Hits.Add(index + 1);
                        if (charset == HwNumberCharset)
                        {
#warning Half beat characters?
                        }
                        break;
                    }
                }
            }

            return beat;
        }
    }
}
