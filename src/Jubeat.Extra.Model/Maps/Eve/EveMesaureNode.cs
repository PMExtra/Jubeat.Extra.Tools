using Jubeat.Extra.Models.Extensions;

namespace Jubeat.Extra.Models.Maps.Eve
{
    public class EveMesaureNode : IEveNode
    {
        public int Timestamp { get; set; }

        public string NodeName { get; } = "MEASURE";

        public override string ToString()
        {
            return string.Join(",", EveNodeHelper.FixWidth(Timestamp, NodeName, 0));
        }
    }
}
