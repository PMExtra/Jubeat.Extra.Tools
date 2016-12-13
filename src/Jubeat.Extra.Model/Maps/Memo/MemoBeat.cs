using System.Collections.Generic;

namespace Jubeat.Extra.Models.Maps.Memo
{
    public class MemoBeat
    {
        public int? Bpm { get; set; }

        public List<int> Hits { get; } = new List<int>();
    }
}
