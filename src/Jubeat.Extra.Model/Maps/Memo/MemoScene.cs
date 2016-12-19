using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Jubeat.Extra.Models.Formatters.Memo;

namespace Jubeat.Extra.Models.Maps.Memo
{
    public class MemoScene : IEnumerable<IEnumerable<MemoNote>>, IEnumerable<MemoNote>
    {
        private readonly MemoNote[,] _buttons = new MemoNote[4, 4];

        public MemoNote this[int row, int column]
        {
            get { return _buttons[row, column]; }
            set { _buttons[row, column] = value; }
        }

        public IEnumerable<MemoNote> this[int row] => _buttons.OfType<MemoNote>().Skip(row * 4).Take(4);

        IEnumerator<IEnumerable<MemoNote>> IEnumerable<IEnumerable<MemoNote>>.GetEnumerator()
        {
            for (var row = 0; row < 4; row++)
            {
                yield return this[row];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<MemoNote> GetEnumerator()
        {
            return _buttons.OfType<MemoNote>().GetEnumerator();
        }

        public IEnumerable<int> FindNote(int ordinal)
        {
            var i = 0;
            foreach (var note in this)
            {
                if (note.Ordinal == ordinal)
                {
                    yield return i;
                }
                i++;
            }
        }

        public static MemoScene Parse(IReadOnlyList<string> lines)
        {
            return MemoSceneFormatter.ParseScene(lines);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, MemoSceneFormatter.SceneToStrings(this));
        }
    }
}
