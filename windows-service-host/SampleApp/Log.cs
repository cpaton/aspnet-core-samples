using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApp
{
    public static class Log
    {
        public static void Message(string message)
        {
            using (var writer = new StreamWriter(@"C:\_cp\Temp\asp-net-core-windows-host.txt", true))
            {
                writer.WriteLine($"[{DateTime.UtcNow:u}] {message}");
            }
        }
    }
}
