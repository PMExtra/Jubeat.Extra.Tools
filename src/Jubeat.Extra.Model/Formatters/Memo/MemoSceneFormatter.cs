using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Jubeat.Extra.Models.Maps.Memo;
using static Jubeat.Extra.Models.Maps.Memo.MemoDefinitions;

namespace Jubeat.Extra.Models.Formatters.Memo
{
    internal static class MemoSceneFormatter
    {
        private const string AllPathCharset = HorizontalCharset + VerticalCharset + CrossCharset;
        private const string AllEmptyCharset = EmptyCharset + AllPathCharset;
        private const string AllArrowCharset = LeftCharset + UpCharset + RightCharset + DownCharset;
        private const string AllValidCharset = AllEmptyCharset + AllArrowCharset + CircleNumberCharset + HwNumberCharset + FwNumberCharset;

        private static readonly string[] AllNumberCharsets =
        {
            CircleNumberCharset,
            HwNumberCharset,
            FwNumberCharset
        };

        private static readonly Dictionary<MemoNote.Direction, int> RowOffset = new Dictionary<MemoNote.Direction, int>
        {
            { MemoNote.Direction.Left, 0 },
            { MemoNote.Direction.Up, -1 },
            { MemoNote.Direction.Right, 0 },
            { MemoNote.Direction.Down, 1 }
        };

        private static readonly Dictionary<MemoNote.Direction, int> ColOffset = new Dictionary<MemoNote.Direction, int>
        {
            { MemoNote.Direction.Left, -1 },
            { MemoNote.Direction.Up, 0 },
            { MemoNote.Direction.Right, 1 },
            { MemoNote.Direction.Down, 0 }
        };

        private static readonly Dictionary<MemoNote.Direction, char> DefaultArrow = new Dictionary<MemoNote.Direction, char>
        {
            { MemoNote.Direction.Left, DefaultLeft },
            { MemoNote.Direction.Up, DefaultUp },
            { MemoNote.Direction.Right, DefaultRight },
            { MemoNote.Direction.Down, DefaultDown }
        };

        private static readonly Dictionary<MemoNote.Direction, char> DefulatPath = new Dictionary<MemoNote.Direction, char>
        {
            { MemoNote.Direction.Left, DefaultHorizontal },
            { MemoNote.Direction.Up, DefaultVertical },
            { MemoNote.Direction.Right, DefaultHorizontal },
            { MemoNote.Direction.Down, DefaultVertical }
        };

        private static void FillHoldPath(ref char[][] output, int row, int col, MemoNote.Direction direction, int span)
        {
            var rowOffset = RowOffset[direction];
            var colOffset = ColOffset[direction];
            for (var i = 0; i < span; i++)
            {
                row -= rowOffset;
                col -= colOffset;

                Debug.Assert(!CircleNumberCharset.Contains(output[row][col]));

                output[row][col] = output[row][col] == DefaultEmpty ? DefulatPath[direction] : DefaultCross;
            }
            output[row][col] = DefaultArrow[direction];
        }

        public static string[] SceneToStrings(MemoScene scene)
        {
            var scene2D = scene as IEnumerable<IEnumerable<MemoNote>>;
            var output = scene2D.Select(line => line.Select(MemoNoteFormatter.NoteToChar).ToArray()).ToArray();

            for (var row = 0; row < 4; row++)
            {
                for (var col = 0; col < 4; col++)
                {
                    var note = scene[row, col];
                    if (note.Type == MemoNote.NoteType.Hold)
                    {
                        FillHoldPath(ref output, row, col, note.HoldDirection, note.HoldSpan);
                    }
                }
            }

            return output.Select(line => new string(line)).ToArray();
        }

        public static MemoScene ParseScene(IReadOnlyList<string> lines)
        {
            var scene = new MemoScene();

            Debug.Assert(lines.Count == 4);
            Debug.Assert(lines.All(l => l.Length >= 4));

            for (var row = 0; row < 4; row++)
            {
                for (var col = 0; col < 4; col++)
                {
                    var c = lines[row][col];

                    Debug.Assert(AllValidCharset.Contains(c));

                    foreach (var charset in AllNumberCharsets)
                    {
                        var index = charset.IndexOf(c);
                        if (index >= 0)
                        {
                            scene[row, col] = new MemoNote
                            {
                                Type = MemoNote.NoteType.Click,
                                Ordinal = index + 1
                            };
                            break;
                        }
                    }
                }
            }

            for (var row = 0; row < 4; row++)
            {
                for (var col = 0; col < 4; col++)
                {
                    var direction = GetDirection(lines[row][col]);
                    if (direction != MemoNote.Direction.Undefined)
                    {
                        SetHoldNote(scene, row, col, direction);
                    }
                }
            }

            return scene;
        }

        private static MemoNote.Direction GetDirection(char c)
        {
            if (LeftCharset.Contains(c))
            {
                return MemoNote.Direction.Left;
            }
            if (UpCharset.Contains(c))
            {
                return MemoNote.Direction.Up;
            }
            if (RightCharset.Contains(c))
            {
                return MemoNote.Direction.Right;
            }
            if (DownCharset.Contains(c))
            {
                return MemoNote.Direction.Down;
            }
            return MemoNote.Direction.Undefined;
        }

        private static void SetHoldNote(MemoScene scene, int row, int col, MemoNote.Direction direction)
        {
            var rowOffset = RowOffset[direction];
            var colOffset = ColOffset[direction];

            for (var span = 1;; span++)
            {
                row += rowOffset;
                col += colOffset;

                if (scene[row, col].Type == MemoNote.NoteType.Click)
                {
                    scene[row, col] = new MemoNote
                    {
                        Type = MemoNote.NoteType.Hold,
                        Ordinal = scene[row, col].Ordinal,
                        HoldSpan = span,
                        HoldDirection = direction
                    };
                    break;
                }
            }
        }
    }
}
