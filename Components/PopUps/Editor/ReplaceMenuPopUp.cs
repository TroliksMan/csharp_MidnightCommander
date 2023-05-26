using MidnightCommander.Components.PopUp;
using MidnightCommander.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidnightCommander.Components.PopUps.Editor
{
    class ReplaceMenuPopUp : IPopUpWindow
    {
        public int PopUpX { get; set; }
        public int PopUpY { get; set; }
        public int PopUpWidth { get; set; }

        private int selected = 0;
        private int selectedText = 0;
        private int cursorX;
        private int cursorY;
        private string findStr = "";
        private string replaceStr = "";

        public event Action<string, string> FindAction;
        public void Draw()
        {
            cursorX = Console.WindowWidth / 2 - 21;
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
                subPath = subPath.Substring(subPath.Length - 41, 41);
            }
            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" │  ");
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write(subPath.PadRight(PopUpWidth - 8));
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write("  │ ");
            PopUpY++;

            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" ├".PadRight((PopUpWidth - 26) / 2, '─') + " Zadejte nahrazující výraz " + "┤ ".PadLeft((PopUpWidth - 27) / 2, '─'));
            PopUpY++;

            string subReplace = replaceStr;
            if (subReplace.Length > 40)
            {
                subReplace = subReplace.Substring(subReplace.Length - 41, 41);
            }
            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" │  ");
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write(subReplace.PadRight(PopUpWidth - 8));
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write("  │ ");
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
            // ┌ ┐ └ ┘ ├ ┤ ─ │
            Console.CursorVisible = true;
            if (selectedText == 0)
                Console.SetCursorPosition(cursorX + Math.Min(findStr.Length, 41), cursorY - 2);
            else
                Console.SetCursorPosition(cursorX + Math.Min(replaceStr.Length, 41), cursorY);
        }

        public void HandleKey(ConsoleKeyInfo info)
        {
            switch (info.Key)
            {
                case ConsoleKey.UpArrow:
                    selectedText = 0;
                    break;
                case ConsoleKey.DownArrow:
                    selectedText = 1;
                    break;
                case ConsoleKey.Tab:
                    selected++;
                    selected %= 2;
                    break;
                case ConsoleKey.Enter:
                    if (selected == 0 && findStr != "")
                        this.FindAction(findStr, replaceStr);
                    else
                        EditWindow.popUpWindow = null;
                    break;
                case ConsoleKey.Escape:
                    EditWindow.popUpWindow = null;
                    break;
                case ConsoleKey.Backspace:
                    if (selectedText == 0 && findStr != "")
                        findStr = findStr.Remove(findStr.Length - 1);
                    else if (selectedText == 1 && replaceStr != "")
                        replaceStr = replaceStr.Remove(replaceStr.Length - 1);
                    break;
                default:
                    if (Char.GetUnicodeCategory(info.KeyChar) != System.Globalization.UnicodeCategory.Control)
                    {

                        if (selectedText == 0)
                            findStr += info.KeyChar.ToString();
                        else
                            replaceStr += info.KeyChar.ToString();
                    }
                    break;
            }
        }
    }
}
