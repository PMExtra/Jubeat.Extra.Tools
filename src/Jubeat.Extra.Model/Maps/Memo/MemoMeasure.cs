using System.Collections.Generic;

namespace Jubeat.Extra.Models.Maps.Memo
{
    public class MemoMeasure
    {
        public IList<MemoMeasurePart> Parts { get; set; }

        public IList<MemoBeat> Beats { get; set; }
    }
}
