using MidnightCommander.Windows;
using MidnightCommander.Windows.Browsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace MidnightCommander.Components.PopUp
{
    public class NewFolder : IPopUpWindow
    {
        public int PopUpX { get; set; }
        public int PopUpY { get; set; }
        public int PopUpWidth { get; set; }
        private int cursorX;
        private int cursorY;


        public event Action<string> Click;
        private int selected = 0;
        private string folderName = "";

        public void Draw()
        {
            cursorX = (Console.WindowWidth / 2 - 22) + folderName.Length;
            cursorY = Console.WindowHeight / 2 - 2;
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;

            PopUpX = Console.WindowWidth / 2 - 25;
            PopUpY = Console.WindowHeight / 2 - 5;
            PopUpWidth = 50;

            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write("".PadRight(PopUpWidth));
            PopUpY++;

            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" ┌".PadRight((PopUpWidth / 2) - 11, '─'));
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(" Vytvořit novou složku ");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("┐ ".PadLeft((PopUpWidth / 2) - 12, '─'));
            PopUpY++;

            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" │ Zadejte název složky: (Max 40 znaků)".PadRight(PopUpWidth - 2) + "│ ");
            PopUpY++;

            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" │ ");
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write(folderName.PadRight(PopUpWidth - 6));
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write(" │ ");
            PopUpY++;

            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" ├".PadRight(PopUpWidth - 2, '─') + "┤ ");
            PopUpY++;

            Console.SetCursorPosition(PopUpX, PopUpY);
            for (int i = 0; i < 1; i++)
            {
                Console.Write(" │".PadRight((PopUpWidth - 25) / 2));
                if (i == selected)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write("[   Ok   ]");
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write("     [ Storno ]");
                }
                else
                {
                    Console.Write("[   Ok   ]     ");
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write("[ Storno ]");
                    Console.BackgroundColor = ConsoleColor.Gray;
                }
                Console.Write("│ ".PadLeft((PopUpWidth - 24) / 2));
            }
            PopUpY++;
            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" └".PadRight(PopUpWidth - 2, '─') + "┘ ");

            PopUpY++;
            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write("".PadRight(PopUpWidth));

            Console.CursorVisible = true;
            Console.SetCursorPosition(cursorX, cursorY);
        }

        public void HandleKey(ConsoleKeyInfo info)
        {
            switch (info.Key)
            {
                case ConsoleKey.Escape:
                    Console.CursorVisible = false;
                    BrowserWindow.ActivePopUp = false;
                    Browser.popUp = null;
                    Application.Initialize();
                    break;
                case ConsoleKey.Enter:
                    Console.CursorVisible = false;
                    if (this.selected == 0)
                        this.Click(folderName);
                    BrowserWindow.ActivePopUp = false;
                    Browser.popUp = null;
                    Application.Initialize();
                    break;
                case ConsoleKey.Tab:
                    this.selected++;
                    selected %= 2;
                    break;
                case ConsoleKey.Backspace:
                    if (this.folderName != "")
                        this.folderName = this.folderName.Remove(this.folderName.Length - 1);
                    break;
                default:
                    if (this.folderName.Length < 40 && Char.GetUnicodeCategory(info.KeyChar) != UnicodeCategory.Control)
                        this.folderName += info.KeyChar;
                    break;
            }
        }
    }
}
