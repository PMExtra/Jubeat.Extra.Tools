using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Jubeat.Extra.Models.Maps.Memo;
using Xunit;

namespace Jubeat.Extra.IO.Test
{
    public class MemoMapReaderTests
    {
        [Fact]
        public void BeatFormatterTest()
        {
            var input = "|(90)①－(95)②(100)－|";
            var beat = MemoBeat.Parse(input.Trim('|'));
            Assert.Equal(beat.ToString(), input);
        }

        [Fact]
        public void MeasureFormatterTest()
        {
            var input = new[]
            {
                "□②①□	|①－－－|",
                "□①②□	|②－－－|",
                "⑦⑤⑥④	|③－－－|",
                "□③③□	|④⑤⑥⑦|"
            };

            var measure = MemoMeasure.Parse(input);

            Assert.Equal(measure.ToString(), measure.Ordinal + Environment.NewLine + string.Join(Environment.NewLine, input) + Environment.NewLine);
        }

        [Fact]
        public void SceneFormatterTest()
        {
            var input = new[] { "③□□□", "⑤□□②", "□⑤⑥④", "①□□⑥" };
            var scene = MemoScene.Parse(input);
            Assert.Equal(scene.ToString(), string.Join(Environment.NewLine, input));
        }

        [Fact]
        public async Task ReadMapTest()
        {
            using (var mr = new MemoMapReader(@"D:\memo2.txt"))
            {
                var map = await mr.ReadAllAsync();
            }
        }
    }
}
