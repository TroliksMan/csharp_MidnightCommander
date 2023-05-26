using MidnightCommander.Components.PopUp;
using MidnightCommander.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidnightCommander.Components.PopUps.Editor
{
    public enum ReplaceValue
    {
        REPLACE,
        ALL,
        SKIP
    }

    class ReplaceActionPopUp : IPopUpWindow
    {
        public int PopUpX { get; set; }
        public int PopUpY { get; set; }
        public int PopUpWidth { get; set; }

        private string findStr;
        private string replaceStr;
        private int selected = 0;

        public event Action<int, string, string> ReplaceAction;

        public ReplaceActionPopUp(string findStr, string replaceStr)
        {
            this.findStr = findStr;
            this.replaceStr = replaceStr;
        }
        public void Draw()
        {
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
            Console.Write(" ┌".PadRight((PopUpWidth - 9) / 2, '─') + " Nahradit " + "┐ ".PadLeft((PopUpWidth - 9) / 2, '─'));
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
            subPath = '"' + subPath + '"';
            Console.Write(subPath.PadRight(PopUpWidth - 8));
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write("  │ ");
            PopUpY++;

            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" │  " + "Změnit na " + "│ ".PadLeft(PopUpWidth - 14));
            PopUpY++;

            string subReplace = replaceStr;
            if (subReplace.Length > 40)
            {
                subReplace = subReplace.Substring(subReplace.Length - 40, 40);
            }
            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" │  ");
            subReplace = '"' + subReplace + '"';
            Console.Write(subReplace.PadRight(PopUpWidth - 8));
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write("  │ ");
            PopUpY++;

            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" ├".PadRight(PopUpWidth - 2, '─') + "┤ ");
            PopUpY++;

            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" │".PadRight(5));
            if (selected == 0)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write("[ Nahraď ]");
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.Write("     [  Vše  ]");
                Console.Write("     [ Přeskoč ]");
            }
            else if (selected == 1)
            {
                Console.Write("[ Nahraď ]     ");
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write("[  Vše  ]");
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.Write("     [ Přeskoč ]");
            }
            else
            {
                Console.Write("[ Nahraď ]     ");
                Console.Write("[  Vše  ]     ");
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write("[ Přeskoč ]");
                Console.BackgroundColor = ConsoleColor.Gray;
            }
            Console.Write("│ ".PadLeft(5));
            PopUpY++;

            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" └".PadRight(PopUpWidth - 2, '─') + "┘ ");
            PopUpY++;

            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write("".PadRight(PopUpWidth));
            // ┌ ┐ └ ┘ ├ ┤ ─ │
        }

        public void HandleKey(ConsoleKeyInfo info)
        {
            switch (info.Key)
            {
                case ConsoleKey.Tab:
                    selected++;
                    selected %= 3;
                    break;
                case ConsoleKey.Enter:
                    this.ReplaceAction(selected, findStr, replaceStr);
                    break;
                case ConsoleKey.Escape:
                    EditWindow.popUpWindow = null;
                    break;
            }
        }
    }
}
