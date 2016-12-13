using System.Collections.Generic;

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
    }
}
