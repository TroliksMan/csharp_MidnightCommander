using MidnightCommander.Components.PopUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidnightCommander.Components.PopUps.Editor
{
    public enum ExitValue
    {
        SAVE,
        EXIT,
        CANCEL
    }

    class ExitPopUp : IPopUpWindow
    {
        public int PopUpX { get; set; }
        public int PopUpY { get; set; }
        public int PopUpWidth { get; set; }

        private int selected = 2;

        public event Action<int> ExitAction;
        public void Draw()
        {
            string name = "Uložit před uzavřením?";
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.CursorVisible = false;
            this.PopUpWidth = 60;
            this.PopUpX = (Console.WindowWidth / 2) - (PopUpWidth / 2);
            this.PopUpY = Console.WindowHeight / 2 - 6;

            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write("".PadRight(PopUpWidth));
            PopUpY++;

            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" ┌".PadRight(PopUpWidth - 2, '─') + "┐ ");
            PopUpY++;

            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" │");
            Console.Write("".PadRight(PopUpWidth - 4));
            Console.Write("│ ");
            Console.SetCursorPosition((Console.WindowWidth / 2) - name.Length / 2, PopUpY);
            Console.Write(name);
            PopUpY++;

            Console.SetCursorPosition(PopUpX, PopUpY);

            Console.Write(" ├".PadRight(PopUpWidth - 2, '─') + "┤ ");
            PopUpY++;
            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" │".PadRight(10));
            if (selected == 0)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("[   Ano  ]");
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else
                Console.Write("[   Ano  ]");
            Console.Write("     ");
            if (selected == 1)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("[   Ne   ]");
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else
                Console.Write("[   Ne   ]");
            Console.Write("     ");
            if (selected == 2)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("[ Zrušit ]");
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else
                Console.Write("[ Zrušit ]");
            Console.Write("│ ".PadLeft(10));
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
            if (info.Key == ConsoleKey.Tab)
            {
                selected++;
                selected %= 3;
            }
            else if (info.Key == ConsoleKey.Enter)
            {
                if (selected == 0)
                    this.ExitAction(((int)ExitValue.SAVE));
                else if (selected == 1)
                    this.ExitAction(((int)ExitValue.EXIT));
                else
                    this.ExitAction(((int)ExitValue.CANCEL));
            }
            else if (info.Key == ConsoleKey.Escape)
                this.ExitAction(((int)ExitValue.CANCEL));
        }
    }
}
