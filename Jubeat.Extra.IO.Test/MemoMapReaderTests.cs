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
            Assert.NotNull(MemoBeat.Parse("|(197)－－①－②－|"));
            Assert.Null(MemoBeat.Parse("口①②口"));
        }

        [Fact]
        public void ReadTest()
        {
            Task.Run(async () =>
            {
                using (var sr = new StreamReader(new FileStream(@"D:\memo2.txt", FileMode.Open), Encoding.UTF8))
                {
                    var memo = await sr.ReadMemoMapAsync();
                    Assert.NotNull(memo);
                }
            });
        }
    }
}
