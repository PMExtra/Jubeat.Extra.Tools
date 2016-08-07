using Jubeat.Extra.Models.Extensions;

namespace Jubeat.Extra.Models.Maps.Eve
{
    public class EvePlayNode : IEveNode
    {
        public int Timestamp { get; set; }

        public string NodeName { get; } = "PLAY";

        public int Button { get; set; }

        public override string ToString()
        {
            return string.Join(separator: ",", values: IEveNodeHelper.FixWidth(Timestamp, NodeName, Button));
        }
    }
}
