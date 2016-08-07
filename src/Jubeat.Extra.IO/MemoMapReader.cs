using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Jubeat.Extra.Models.Maps.Memo;

namespace Jubeat.Extra.IO
{
    public static class MemoMapReader
    {
        private static readonly Regex NumberRegex = new Regex(pattern: @"^\d+$");
        private static readonly Regex MemoLineRegex = new Regex(pattern: @"^(.{4})[ \\t]*(.+)?$");

        public static async Task<MemoMap> ReadMemoMapAsync(this StreamReader sr)
        {
            var map = new MemoMap { Measures = new List<MemoMeasure>() };

            MemoMeasure measure = null;
            MemoMeasurePart part = null;
            var newPart = true;

            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                line = line.Trim();

                if (line.Length == 0)
                {
                    newPart = true;
                    continue;
                }

                if (NumberRegex.IsMatch(input: line))
                {
                    measure = new MemoMeasure
                    {
                        Parts = new List<MemoMeasurePart>(),
                        Beats = new List<MemoBeat>()
                    };

                    map.Measures.Add(item: measure);

                    newPart = true;

                   continue;
                }

                if (newPart)
                {
                    part = new MemoMeasurePart();
                    if (measure == null)
                    {
                        Console.WriteLine(value: "[Error] Measure start number not found.");
                        return null;
                    }
                    measure.Parts.Add(item: part);
                    newPart = false;
                }

                var memoLine = MemoLineRegex.Match(input: line);
                if (memoLine.Success)
                {
                    var left = memoLine.Groups[groupnum: 1].Value;
                    var right = memoLine.Groups[groupnum: 2].Success ? memoLine.Groups[groupnum: 2].Value : null;

                    part.AddButtons(text: left);

                    if (right != null)
                    {
                        var beat = MemoBeat.Parse(text: right);

                        if (beat != null)
                        {
                            if (measure.Beats.Count == 4)
                            {
                                Console.WriteLine(value: "[Info] Detected more than 4 beats in measure, merge operation is triggered.");
                                measure.Beats.Last().Merge(second: beat);
                            }
                            else
                            {
                                measure.Beats.Add(item: beat);
                            }
                        }
                        else
                        {
                            Console.WriteLine(value: $"[Warning] Unrecognized right part: {right}.");
                        }
                    }
                    
                    continue;
                }

                Console.WriteLine(value: $"[Warning] Unrecognized data: {line}.");
            }

            return map;
        }
    }
}
