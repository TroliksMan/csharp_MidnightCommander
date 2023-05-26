using MidnightCommander.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidnightCommander
{
    public enum WindowType
    {
        Browser,
        Editor
    }

    public class Application
    {
        public static Window window;
        //public static Window[] Window = new Window[2];
        public static WindowType windowType = WindowType.Browser;

        public static void HandleKey(ConsoleKeyInfo info)
        {
            //Application.Window[(int)Application.windowType].HandleKey(info);
            window.HandleKey(info);
        }

        public static void Initialize()
        {
            //Application.Window[(int)Application.windowType].Initialize();
            window.Initialize();
        }
        public static void Draw()
        {
            //Application.Window[(int)Application.windowType].Draw();
            window.Draw();
        }
        public static void DrawMiddle()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            if (Console.WindowWidth % 2 != 0)
            {
                for (int i = 0; i <= Console.WindowHeight - 3; i++)
                {
                    Console.SetCursorPosition(Console.WindowWidth / 2, i);
                    Console.Write(" ");
                }
            }
        }
    }
}
