using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidnightCommander.Services
{
    public class WindowService
    {
        public static int ConsoleWidth = Console.WindowWidth;
        public static int ConsoleHeight = Console.WindowHeight;
        public static bool ConsoleSizeChange()
        {
            if (WindowService.ConsoleWidth != Console.WindowWidth || WindowService.ConsoleHeight != Console.WindowHeight)
            {
                WindowService.ConsoleWidth = Console.WindowWidth;
                WindowService.ConsoleHeight = Console.WindowHeight;
                return true;
            }
            return false;
        }
    }
}
