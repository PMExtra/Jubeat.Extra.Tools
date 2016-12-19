using System.Collections.Generic;
using Jubeat.Extra.Models.Formatters.Memo;

namespace Jubeat.Extra.Models.Maps.Memo
{
    public class MemoBeat
    {
        public SortedDictionary<int, int> Bpm { get; } = new SortedDictionary<int, int>();

        public List<int> Hits { get; } = new List<int>();

        public override string ToString()
        {
            return MemoBeatFormatter.BeatToString(this, MemoStyle.Analyser);
        }

        public static MemoBeat Parse(string s)
        {
            return MemoBeatFormatter.ParseBeat(s);
        }
    }
}
