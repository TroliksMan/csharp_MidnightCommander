using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MidnightCommander.Components.PopUp
{
    class Error : IPopUpWindow
    {
        public int PopUpX { get; set; }
        public int PopUpY { get; set; }
        public int PopUpWidth { get; set; }
        private string Note { get; set; }

        public Error(string str)
        {
            Note = str;
        }

        public void Draw()
        {
            PopUpX = Console.WindowWidth / 2 - 17;
            PopUpY = Console.WindowHeight / 2 - 4;
            PopUpWidth = 34;

            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("".PadRight(PopUpWidth));
            PopUpY++;
            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" ┌".PadRight((PopUpWidth - 7) / 2, '─') + " Error " + "┐ ".PadLeft((PopUpWidth - 5) / 2, '─'));
            PopUpY++;
            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" │".PadRight(PopUpWidth - 2) + "│ ");
            PopUpY++;
            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" │".PadRight((PopUpWidth - Note.Length) / 2) + Note + "│ ".PadLeft((PopUpWidth - Note.Length + 1) / 2));
            PopUpY++;
            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" └".PadRight(PopUpWidth - 2, '─') + "┘ ");
            PopUpY++;
            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write("".PadRight(PopUpWidth));
            Console.BackgroundColor = ConsoleColor.DarkBlue;

            Thread.Sleep(1000);

            Application.Initialize();
        }

        public void HandleKey(ConsoleKeyInfo info)
        {

        }
    }
}
