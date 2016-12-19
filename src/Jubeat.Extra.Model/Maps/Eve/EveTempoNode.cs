using Jubeat.Extra.Models.Extensions;

namespace Jubeat.Extra.Models.Maps.Eve
{
    public class EveTempoNode : IEveNode
    {
        public int Tempo { get; set; }

        public int Timestamp { get; set; }

        public string NodeName { get; } = "TEMPO";

        public override string ToString()
        {
            return string.Join(",", EveNodeHelper.FixWidth(Timestamp, NodeName, Tempo));
        }
    }
}
