using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jubeat.Extra.Models.Maps.Memo
{
    public class MemoMeasure
    {
        public IList<MemoMeasurePart> Parts { get; set; }

        public IList<MemoBeat> Beats { get; set; }
    }
}
