using System.Collections.Generic;
using System.Linq;

namespace Jubeat.Extra.Models.Extensions
{
    public static class EveNodeHelper
    {
        public static string FixWidth(object property, int width = 8)
        {
            return property.ToString().PadLeft(8);
        }

        public static IEnumerable<string> FixWidth(int width = 8, params object[] properties)
        {
            return properties.Select(p => FixWidth(p, width));
        }
    }
}
