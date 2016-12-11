using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using static Jubeat.Extra.Models.Maps.Memo.MemoMap;

namespace Jubeat.Extra.Models.Maps.Memo
{
    public class MemoBeat
    {
        private static readonly Regex BeatRegex = new Regex(@"^\|(\(\d+\))?(.+)\|$");

        public int? Bpm { get; set; }

        public int[] Hits { get; set; }

        public static MemoBeat Parse(string text)
        {
            var rightMatch = BeatRegex.Match(text);
            if (!rightMatch.Success)
            {
                return null;
            }
            var beat = new MemoBeat
            {
                Hits = rightMatch.Groups[2].Value.SelectMany(c =>
                {
                    var index = NumberCharacters.IndexOf(c) + 1;
                    if (index > 0)
                    {
                        return new[] { index, 0 };
                    }
                    index = HalfNumberCharacters.IndexOf(c) + 1;
                    if (index > 0)
                    {
                        return new[] { index };
                    }
                    if (HalfRestCharacters.Contains(c))
                    {
                        return new[] { 0 };
                    }
                    if (RestCharacters.Contains(c))
                    {
                        return new[] { 0, 0 };
                    }
                    Console.WriteLine($"[Warning] Unrecognized beats part: {text}, sign: {c}.");
                    return new[] { 0, 0 };
                }).ToArray()
            };
            if (rightMatch.Groups[1].Success)
            {
                beat.Bpm = int.Parse(rightMatch.Groups[1].Value.Trim('(', ')'));
            }

            return beat;
        }

        public void Merge(MemoBeat second)
        {
            Hits = Hits.Concat(second.Hits).ToArray();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append('|');
            if (Bpm != null)
            {
                sb.Append($"({Bpm})");
            }

            for (var i = 0; i < Hits?.Length; ++i)
            {
                var hit = Hits[i];
                int? next = null;
                if (Hits.Length > i + 1)
                {
                    next = Hits[i + 1];
                }
                if (hit > 0)
                {
                    sb.Append(next > 0 ? HalfNumberCharacters[hit - 1] : NumberCharacters[hit - 1]);
                }
                else
                {
                    sb.Append(next > 0 ? HalfRestCharacters.First() : RestCharacters.First());
                }

                if (next <= 0)
                {
                    ++i;
                }
            }
            sb.Append('|');
            return sb.ToString();
        }
    }
}
