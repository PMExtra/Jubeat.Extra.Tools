using Jubeat.Extra.Models.Extensions;

namespace Jubeat.Extra.Models.Maps.Eve
{
    public class EveHoldNode : IEveNode
    {
        public int Timestamp { get; set; }

        public string NodeName { get; } = "LONG";

        public int Button { get; set; }

        public EveHoldNodeType Type { get; set; }

        public int Span { get; set; }

        public int ZipParam
        {
            get { return Span << 8 + (int)Type << 4 + Button; }
            set
            {
                Span = value >> 8;
                Type = (EveHoldNodeType)((value >> 4) & 0xf);
                Button = value & 0xf;
            }
        }

        public override string ToString()
        {
            return string.Join(separator: ",", values: IEveNodeHelper.FixWidth(Timestamp, NodeName, ZipParam));
        }
    }
}
