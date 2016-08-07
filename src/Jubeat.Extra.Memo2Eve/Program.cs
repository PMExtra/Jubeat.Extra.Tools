using System.IO;
using System.Text;
using System.Threading.Tasks;
using Jubeat.Extra.IO;

namespace Jubeat.Extra.Memo2Eve
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                using (var sr = new StreamReader(stream: new FileStream(path: @"D:\memo.txt", mode: FileMode.Open), encoding: Encoding.UTF8))
                {
                    var memo = await sr.ReadMemoMapAsync();
                }
            });
        }
    }
}
