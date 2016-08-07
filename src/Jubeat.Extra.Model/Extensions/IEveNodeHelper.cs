using System.Collections.Generic;
using System.Linq;

namespace Jubeat.Extra.Models.Extensions
{
    public static class IEveNodeHelper
    {
        public static string FixWidth(this object property, int width = 8)
        {
            return property.ToString().PadLeft(totalWidth: 8);
        }

        public static IEnumerable<string> FixWidth(int width = 8, params object[] properties)
        {
            return properties.Select(p => p.FixWidth(width: width));
        }
    }
}
