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
        private static readonly Regex NumberRegex = new Regex(@"^\d+$");
        private static readonly Regex MemoLineRegex = new Regex(@"^(.{4})[ \\t]*(.+)?$");

        public static async Task<MemoMap> ReadMemoMapAsync(this StreamReader sr)
        {
            var map = new MemoMap { Measures = new List<MemoMeasure>() };

            MemoMeasure measure = null;
            MemoMeasurePart part = null;
            var newPart = true;

            while (!sr.EndOfStream)
            {
                var line = await sr.ReadLineAsync();
                line = line.Trim();

                if (line.Length == 0)
                {
                    newPart = true;
                    continue;
                }

                if (NumberRegex.IsMatch(line))
                {
                    measure = new MemoMeasure
                    {
                        Parts = new List<MemoMeasurePart>(),
                        Beats = new List<MemoBeat>()
                    };

                    map.Measures.Add(measure);

                    newPart = true;

                    continue;
                }

                if (newPart)
                {
                    part = new MemoMeasurePart();
                    if (measure == null)
                    {
                        Console.WriteLine("[Error] Measure start number not found.");
                        return null;
                    }
                    measure.Parts.Add(part);
                    newPart = false;
                }

                var memoLine = MemoLineRegex.Match(line);
                if (memoLine.Success)
                {
                    var left = memoLine.Groups[1].Value;
                    var right = memoLine.Groups[2].Success ? memoLine.Groups[2].Value : null;

                    part.AddButtons(left);

                    if (right != null)
                    {
                        var beat = MemoBeat.Parse(right);

                        if (beat != null)
                        {
                            if (measure.Beats.Count == 4)
                            {
                                Console.WriteLine("[Info] Detected more than 4 beats in measure, merge operation is triggered.");
                                measure.Beats.Last().Merge(beat);
                            }
                            else
                            {
                                measure.Beats.Add(beat);
                            }
                        }
                        else
                        {
                            Console.WriteLine($"[Warning] Unrecognized right part: {right}.");
                        }
                    }

                    continue;
                }

                Console.WriteLine($"[Warning] Unrecognized data: {line}.");
            }

            return map;
        }
    }
}
