using MidnightCommander.Windows;
using MidnightCommander.Windows.Browsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidnightCommander.Components.PopUp
{
    public class Help : IPopUpWindow
    {
        public int PopUpX { get; set; }
        public int PopUpY { get; set; }
        public int PopUpWidth { get; set; }
        public void Draw()
        {
            PopUpX = Console.WindowWidth / 2 - 18;
            PopUpY = Console.WindowHeight / 2 - 5;
            PopUpWidth = 38;
            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.Write("".PadRight(PopUpWidth));
            PopUpY++;
            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" ┌".PadRight((PopUpWidth - 9) / 2, '─') + " Nápověda " + "┐ ".PadLeft((PopUpWidth - 9) / 2, '─'));
            PopUpY++;
            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" │".PadRight(PopUpWidth - 2) + "│ ");
            PopUpY++;
            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" │".PadRight((PopUpWidth - 24) / 2) + "Úplně zbytečná nápověda," + "│ ".PadLeft((PopUpWidth - 24) / 2));
            PopUpY++;
            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" │".PadRight((PopUpWidth - 26) / 2) + "ze které se nic nedozvíte." + "│ ".PadLeft((PopUpWidth - 26) / 2));
            PopUpY++;
            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" │".PadRight((PopUpWidth - 32) / 2) + "Stiskni klávesu pro vrácení zpět" + "│ ".PadLeft((PopUpWidth - 32) / 2));
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
            BrowserWindow.ActivePopUp = false;
            //BrowserWindow.browsers[((int)BrowserWindow.site)].popUp = null;
            Browser.popUp = null;
            Application.Initialize();
        }
    }
}
