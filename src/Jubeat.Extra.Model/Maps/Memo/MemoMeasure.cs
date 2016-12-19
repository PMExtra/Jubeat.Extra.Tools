using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Jubeat.Extra.Models.Formatters.Memo;

namespace Jubeat.Extra.Models.Maps.Memo
{
    public class MemoMeasure
    {
        public int Ordinal { get; set; }

        public List<MemoScene> Scenes { get; } = new List<MemoScene>();

        public List<MemoBeat> Beats { get; } = new List<MemoBeat>();

        public IEnumerable<int> FindNote(int ordinal)
        {
            return Scenes.SelectMany(s => s.FindNote(ordinal));
        }

        public string ToString(MemoStyle style)
        {
            return MemoMeasureFormatter.MeasureToString(this, style);
        }

        public override string ToString()
        {
            return ToString(MemoStyle.Analyser);
        }

        public static MemoMeasure Parse(IReadOnlyList<string> lines)
        {
            return MemoMeasureFormatter.ParseMeasure(lines);
        }

        public static MemoMeasure Parse(IReadOnlyList<Match> matches)
        {
            return MemoMeasureFormatter.ParseMeasure(matches);
        }
    }
}
