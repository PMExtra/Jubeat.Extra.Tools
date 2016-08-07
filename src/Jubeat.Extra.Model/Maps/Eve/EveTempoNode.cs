using Jubeat.Extra.Models.Extensions;

namespace Jubeat.Extra.Models.Maps.Eve
{
    public class EveTempoNode : IEveNode
    {
        public int Timestamp { get; set; }

        public string NodeName { get; } = "TEMPO";

        public int Tempo { get; set; }

        public override string ToString()
        {
            return string.Join(separator: ",", values: IEveNodeHelper.FixWidth(Timestamp, NodeName, Tempo));
        }
    }
}
