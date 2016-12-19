using System.Collections.Generic;
using System.IO;
using Jubeat.Extra.Models.Formatters.Memo;

namespace Jubeat.Extra.Models.Maps.Memo
{
    public class MemoMap
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public int BpmMin { get; set; }

        public int BpmMax { get; set; }

        public int Notes { get; set; }

        public List<MemoMeasure> Measures { get; } = new List<MemoMeasure>();

        public override string ToString()
        {
            return MemoMapFormatter.MapToString(this);
        }

        public bool Check(bool repair = false, TextWriter logWriter = null)
        {
            return MemoMapFormatter.CheckMap(this, repair, logWriter);
        }

        public static MemoMap Parse(string mapText)
        {
            using (var reader = new StringReader(mapText))
            {
                return Parse(reader);
            }
        }

        public static MemoMap Parse(TextReader reader)
        {
            return MemoMapFormatter.ParseMap(reader);
        }
    }
}
