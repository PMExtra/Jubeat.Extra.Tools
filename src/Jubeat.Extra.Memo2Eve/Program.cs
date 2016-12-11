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
                using (var sr = new StreamReader(new FileStream(@"D:\memo.txt", FileMode.Open), Encoding.UTF8))
                {
                    var memo = await sr.ReadMemoMapAsync();
                }
            });
        }
    }
}
