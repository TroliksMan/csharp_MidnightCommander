using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidnightCommander.Services
{
    class FileService
    {
        private readonly Dictionary<string, ConsoleColor> colors = new Dictionary<string, ConsoleColor>();

        public FileService()
        {
            colors.Add(".tmp", ConsoleColor.DarkGray);
            colors.Add(".dat", ConsoleColor.Red);
            colors.Add(".exe", ConsoleColor.Green);
            colors.Add(".bat", ConsoleColor.Green);
            colors.Add(".pl" , ConsoleColor.Cyan);
            colors.Add(".txt", ConsoleColor.DarkYellow);
            colors.Add(".xml", ConsoleColor.DarkYellow);
            colors.Add(".zip", ConsoleColor.DarkRed);
            colors.Add(".cab", ConsoleColor.DarkRed);
        }
        public ConsoleColor GetColor(string Extension)
        {
            if (colors.ContainsKey(Extension))
                return colors[Extension];
            else
                return ConsoleColor.Gray;
        }
    }
}
