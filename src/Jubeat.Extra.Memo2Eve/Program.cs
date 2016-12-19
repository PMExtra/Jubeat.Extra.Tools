using System.IO;
using Jubeat.Extra.IO;

namespace Jubeat.Extra.Memo2Eve
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var mr = new MemoMapReader(@"D:\memo2.txt"))
            {
                using (var sw = new StreamWriter(new FileStream(@"D:\2.eve", FileMode.Create)))
                {
                    var map = mr.ReadAllAsync().Result;

                    var timestamp = 0;
                    var bpm = 0;

                    foreach (var measure in map.Measures)
                    {
                        foreach (var beat in measure.Beats)
                        {
                            for (var i = 0; i < beat.Hits.Count; i++)
                            {
                                if (beat.Bpm.ContainsKey(i))
                                {
                                    bpm = beat.Bpm[i];
                                }

                                var hit = beat.Hits[i];
                                if (hit > 0)
                                {
                                    foreach (var button in measure.FindNote(hit))
                                    {
                                        sw.WriteLine($"PLAY {button} {timestamp}");
                                    }
                                }

                                // timestamp += ...
                            }
                        }
                    }
                }
            }
        }
    }
}
