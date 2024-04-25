using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Core.Helper
{
    public static class WWWRootGetPaths
    {
        public static string GetwwwrootPath
        {
            get
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                return path;

            }
        }
    }
}
