using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ConsoleApp
{
    public static class TimeStampHelper
    {
        public static string GetFormattedElapsed(this Stopwatch sw)
        {
            var ts = sw.Elapsed;

            return $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds:00}";
        }
    }
}
