using MidnightCommander.Components;
using MidnightCommander.Components.PopUps;
using MidnightCommander.Services;
using MidnightCommander.Windows;
using MidnightCommander.Windows.Browsers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace MidnightCommander
{
    class Program
    {
        static void Main(string[] args)
        {
            // ┌ ┐ └ ┘ ├ ┤ ─ │

            //Console.ReadKey();
            //Console.Clear();

            Console.CursorVisible = false;
            //Application.Window[0] = new BrowserWindow();
            Application.window = new BrowserWindow();
            Application.Initialize();
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo info = Console.ReadKey(true);
                    //if (info.Key == ConsoleKey.Escape)
                    //    Application.Draw();
                    Application.HandleKey(info);
                    Application.Draw();
                }
                if (WindowService.ConsoleSizeChange())
                {
                    Console.Clear();
                    Application.Initialize();
                }
            }
        }
    }
}
