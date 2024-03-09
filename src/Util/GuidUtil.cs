using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NowPlayingMonitor
{
    public class GuidUtil
    {
        public static Guid? Parse(string? id)
        {
            if (String.IsNullOrEmpty(id)) 
                return null;
            if(Guid.TryParse(id, out var result))
            {
                return result;
            }
            else 
            { 
                return null; 
            }
        }

        public static Guid New() 
        { 
            return Guid.NewGuid(); 
        }

        public static string NewS(string prefix)
        {
            return prefix + Guid.NewGuid().ToString();
        }

    }
}
