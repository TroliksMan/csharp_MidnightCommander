using MidnightCommander.Windows;
using MidnightCommander.Windows.Browsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidnightCommander.Components.PopUp
{
    public class Delete : IPopUpWindow
    {
        public int PopUpX { get; set; }
        public int PopUpY { get; set; }
        public int PopUpWidth { get; set; }
        private string Name { get; set; }
        private int selected = 1;
        public event Action Click;

        public Delete(string name)
        {
            Name = name;
        }

        public void Draw()
        {
            PopUpWidth = Math.Max(30, Name.Length + 6);

            PopUpX = Console.WindowWidth / 2 - PopUpWidth / 2;
            PopUpY = Console.WindowHeight / 2 - 5;
            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("".PadRight(PopUpWidth));
            PopUpY++;
            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" ┌".PadRight((PopUpWidth - 23) / 2, '─') + " Opravdu chcete smazat " + "┐ ".PadLeft((PopUpWidth - 22) / 2, '─'));
            PopUpY++;
            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" │".PadRight((PopUpWidth - Name.Length - 1) / 2) + Name + "?" + "│ ".PadLeft((PopUpWidth - Name.Length) / 2));
            PopUpY++;
            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" ├".PadRight(PopUpWidth - 2, '─') + "┤ ");
            PopUpY++;
            Console.SetCursorPosition(PopUpX, PopUpY);
            for (int i = 0; i < 1; i++)
            {
                Console.Write(" │".PadRight((PopUpWidth - 18) / 2));
                if (i == selected)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write("[ Ano ]");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.Write("     [ Ne ]");
                }
                else
                {
                    Console.Write("[ Ano ]     ");
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("[ Ne ]");
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write("│ ".PadLeft((PopUpWidth - 17) / 2));
            }
            PopUpY++;
            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" └".PadRight(PopUpWidth - 2, '─') + "┘ ");

            PopUpY++;
            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write("".PadRight(PopUpWidth));
            Console.BackgroundColor = ConsoleColor.DarkBlue;
        }

        public void HandleKey(ConsoleKeyInfo info)
        {
            switch (info.Key)
            {
                case ConsoleKey.Tab:
                    this.selected++;
                    selected %= 2;
                    break;
                case ConsoleKey.Enter:
                    if (selected == 0)
                    {
                        BrowserWindow.ActivePopUp = false;
                        Browser.popUp = null;
                        this.Click();
                    }
                    else
                    {
                        BrowserWindow.ActivePopUp = false;
                        Browser.popUp = null;
                        Application.Initialize();
                    }
                    break;
                case ConsoleKey.Escape:
                    BrowserWindow.ActivePopUp = false;
                    Browser.popUp = null;
                    Application.Initialize();
                    break;
            }
        }
    }
}
