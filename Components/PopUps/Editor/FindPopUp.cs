using MidnightCommander.Components.PopUp;
using MidnightCommander.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidnightCommander.Components.PopUps.Editor
{
    class FindPopUp : IPopUpWindow
    {
        public int PopUpX { get; set; }
        public int PopUpY { get; set; }
        public int PopUpWidth { get; set; }

        private int selected = 0;
        private string findStr = "";
        public event Action<string> FindAction;
        private int cursorX;
        private int cursorY;

        public void Draw()
        {
            cursorX = Console.WindowWidth / 2 - 21 + Math.Min(findStr.Length, 40);
            cursorY = Console.WindowHeight / 2 - 3;
            Console.CursorVisible = false;
            PopUpWidth = 50;

            PopUpX = Console.WindowWidth / 2 - PopUpWidth / 2;
            PopUpY = Console.WindowHeight / 2 - 8;
            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("".PadRight(PopUpWidth));
            PopUpY++;
            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" ┌".PadRight((PopUpWidth - 22) / 2, '─') + " Zadejte hledaný výraz " + "┐ ".PadLeft((PopUpWidth - 23) / 2, '─'));
            PopUpY++;


            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" ├".PadRight(PopUpWidth - 2, '─') + "┤ ");
            PopUpY++;

            string subPath = findStr;
            if (subPath.Length > 40)
            {
                subPath = subPath.Substring(subPath.Length - 40, 40);
            }
            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" │  ");
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write(subPath.PadRight(PopUpWidth - 8));
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write("  │ ");
            PopUpY++;

            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" ├".PadRight(PopUpWidth - 2, '─') + "┤ ");
            PopUpY++;

            Console.SetCursorPosition(PopUpX, PopUpY);
                Console.Write(" │".PadRight((PopUpWidth - 25) / 2));
                if (0 == selected)
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
            PopUpY++;

            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" └".PadRight(PopUpWidth - 2, '─') + "┘ ");
            PopUpY++;

            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write("".PadRight(PopUpWidth));
            // ┌ ┐ └ ┘ ├ ┤ ─ │
            Console.CursorVisible = true;

            Console.SetCursorPosition(cursorX, cursorY - 2);
        }

        public void HandleKey(ConsoleKeyInfo info)
        {
            switch (info.Key)
            {
                case ConsoleKey.Tab:
                    selected++;
                    selected %= 2;
                    break;
                case ConsoleKey.Enter:
                    if (selected == 0 && findStr != "")
                        this.FindAction(findStr);
                    EditWindow.popUpWindow = null;
                    break;
                case ConsoleKey.Escape:
                    EditWindow.popUpWindow = null;
                    break;
                //------------------------------------
                case ConsoleKey.Backspace:
                    if (findStr != "")
                        findStr = findStr.Remove(findStr.Length - 1);
                    break;
                default:
                    if (Char.GetUnicodeCategory(info.KeyChar) != System.Globalization.UnicodeCategory.Control)
                        findStr += info.KeyChar.ToString();
                    break;
            }
        }
    }
}
