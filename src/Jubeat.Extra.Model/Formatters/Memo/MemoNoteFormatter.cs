using Jubeat.Extra.Models.Maps.Memo;
using static Jubeat.Extra.Models.Maps.Memo.MemoDefinitions;

namespace Jubeat.Extra.Models.Formatters.Memo
{
    internal static class MemoNoteFormatter
    {
        private static readonly string NoteCharset = DefaultEmpty + CircleNumberCharset;

        public static char NoteToChar(MemoNote note)
        {
            return NoteCharset[note.Ordinal];
        }
    }
}
