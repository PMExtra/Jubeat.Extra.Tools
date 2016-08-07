using System.IO;
using System.Text;
using System.Threading.Tasks;
using Jubeat.Extra.Models.Maps.Memo;
using Xunit;

namespace Jubeat.Extra.IO.Test
{
    public class MemoMapReaderTests
    {
        [Fact]
        public void MemoBeatTest()
        {
            Assert.NotNull(@object: MemoBeat.Parse(text: "|(197)－－①－②－|"));
            Assert.Null(@object: MemoBeat.Parse(text: "口①②口"));
        }

        [Fact]
        public void ReadTest()
        {
            Task.Run(async () =>
            {
                using (var sr = new StreamReader(stream: new FileStream(path: @"D:\memo2.txt", mode: FileMode.Open), encoding: Encoding.UTF8))
                {
                    var memo = await sr.ReadMemoMapAsync();
                    Assert.NotNull(@object: memo);
                }
            });
        }
    }
}
