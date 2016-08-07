using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using static Jubeat.Extra.Models.Maps.Memo.MemoMap;

namespace Jubeat.Extra.Models.Maps.Memo
{
    public class MemoBeat
    {
        private static readonly Regex BeatRegex = new Regex(pattern: @"^\|(\(\d+\))?(.+)\|$");

        public int? Bpm { get; set; }

        public int[] Hits { get; set; }

        public static MemoBeat Parse(string text)
        {
            var rightMatch = BeatRegex.Match(input: text);
            if (!rightMatch.Success)
            {
                return null;
            }
            var beat = new MemoBeat
            {
                Hits = rightMatch.Groups[groupnum: 2].Value.SelectMany(c =>
                {
                    var index = NumberCharacters.IndexOf(value: c) + 1;
                    if (index > 0)
                    {
                        return new[] { index, 0 };
                    }
                    index = HalfNumberCharacters.IndexOf(value: c) + 1;
                    if (index > 0)
                    {
                        return new[] { index };
                    }
                    if (HalfRestCharacters.Contains(value: c))
                    {
                        return new[] { 0 };
                    }
                    if (RestCharacters.Contains(value: c))
                    {
                        return new[] { 0, 0 };
                    }
                    Console.WriteLine(value: $"[Warning] Unrecognized beats part: {text}, sign: {c}.");
                    return new[] { 0, 0 };
                }).ToArray()
            };
            if (rightMatch.Groups[groupnum: 1].Success)
            {
                beat.Bpm = int.Parse(s: rightMatch.Groups[groupnum: 1].Value.Trim('(', ')'));
            }

            return beat;
        }

        public void Merge(MemoBeat second)
        {
            Hits = Hits.Concat(second: second.Hits).ToArray();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(value: '|');
            if (Bpm != null)
            {
                sb.Append(value: $"({Bpm})");
            }

            for (var i = 0; i < Hits?.Length; )
            {
                var hit = Hits[i];
                int? next = null;
                if (Hits.Length > i + 1)
                {
                    next = Hits[i + 1];
                }
                if (hit > 0)
                {
                    sb.Append(value: next > 0 ? HalfNumberCharacters[index: hit - 1] : NumberCharacters[index: hit - 1]);
                }
                else
                {
                    sb.Append(value: next > 0 ? HalfRestCharacters.First() : RestCharacters.First());
                }
                i += next > 0 ? 1 : 2;
            }
            sb.Append(value: '|');
            return sb.ToString();
        }
    }
}
