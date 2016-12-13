using System.Collections.Generic;

namespace Jubeat.Extra.Models.Maps.Memo
{
    public class MemoMeasure
    {
        public IList<MemoMeasurePart> Parts { get; } = new List<MemoMeasurePart>();

        public IList<MemoBeat> Beats { get; } = new List<MemoBeat>();
    }
}
