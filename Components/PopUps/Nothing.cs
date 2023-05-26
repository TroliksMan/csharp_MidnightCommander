using MidnightCommander.Windows;
using MidnightCommander.Windows.Browsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidnightCommander.Components.PopUp
{
    public class Nothing : IPopUpWindow
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
            Console.Write(" ┌".PadRight((PopUpWidth - 9) / 2, '─') + " Nabídka " + "┐ ".PadLeft((PopUpWidth - 7) / 2, '─'));
            PopUpY++;
            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" │".PadRight(PopUpWidth - 2) + "│ ");
            PopUpY++;
            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" │".PadRight((PopUpWidth - 25) / 2) + "Další úplně zbytečná věc. " + "│ ".PadLeft((PopUpWidth - 25) / 2));
            PopUpY++;
            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" │".PadRight((PopUpWidth - 19) / 2) + "Fakt to nic nedělá. " + "│ ".PadLeft((PopUpWidth - 19) / 2));
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
            //BrowserWindow.browsers[((int)BrowserWindow.site)].popUp = null;
            BrowserWindow.ActivePopUp = false;
            Browser.popUp = null;
            Application.Initialize();
        }
    }
}
