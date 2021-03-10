using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTimer
{
    public class Utils
    {
        static public string FormatTime(string s)
        {
            string[] p = s.Split(':');
            return string.Format("{0:00}:{1:00}:{2:00}", int.Parse(p[0]), int.Parse(p[1]), int.Parse(p[2]));
        }
    }
}
