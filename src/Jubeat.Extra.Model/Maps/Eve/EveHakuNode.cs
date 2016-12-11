using Jubeat.Extra.Models.Extensions;

namespace Jubeat.Extra.Models.Maps.Eve
{
    public class EveHakuNode : IEveNode
    {
        public int Timestamp { get; set; }

        public string NodeName { get; } = "HAKU";

        public override string ToString()
        {
            return string.Join(",", IEveNodeHelper.FixWidth(Timestamp, NodeName, 0));
        }
    }
}
