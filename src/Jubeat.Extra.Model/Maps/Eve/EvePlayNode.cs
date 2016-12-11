using Jubeat.Extra.Models.Extensions;

namespace Jubeat.Extra.Models.Maps.Eve
{
    public class EvePlayNode : IEveNode
    {
        public int Button { get; set; }

        public int Timestamp { get; set; }

        public string NodeName { get; } = "PLAY";

        public override string ToString()
        {
            return string.Join(",", IEveNodeHelper.FixWidth(Timestamp, NodeName, Button));
        }
    }
}
