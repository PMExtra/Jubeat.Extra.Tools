using Jubeat.Extra.Models.Formatters.Memo;

namespace Jubeat.Extra.Models.Maps.Memo
{
    public struct MemoNote
    {
        public enum NoteType
        {
            None,
            Click,
            Hold
        }

        public enum Direction
        {
            Undefined,
            Left,
            Up,
            Right,
            Down
        }

        public int Ordinal { get; set; }

        public NoteType Type { get; set; }

        public Direction HoldDirection { get; set; }

        public int HoldSpan { get; set; }

        public override string ToString()
        {
            return MemoNoteFormatter.NoteToChar(this).ToString();
        }
    }
}
