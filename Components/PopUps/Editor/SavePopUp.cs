using MidnightCommander.Components.PopUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidnightCommander.Components.PopUps.Editor
{
    class SavePopUp : IPopUpWindow
    {
        public int PopUpX { get; set; }
        public int PopUpY { get; set; }
        public int PopUpWidth { get; set; }

        private int selected = 1;
        public event Action<bool> SaveAction;
        public void Draw()
        {
            string name = "Opravdu chcete uložit soubor?";
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.CursorVisible = false;
            this.PopUpWidth = 40;
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
            Console.Write(" │".PadRight((PopUpWidth - 25) / 2));
            if (selected == 0)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("[   Ok   ]");
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("     [ Storno ]");
            }
            else
            {
                Console.Write("[   Ok   ]     ");
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("[ Storno ]");
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            Console.Write("│ ".PadLeft((PopUpWidth - 24) / 2));
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
                selected %= 2;
            }
            else if (info.Key == ConsoleKey.Enter)
            {
                if (selected == 0)
                    this.SaveAction(true);
                else
                    this.SaveAction(false);
            }
            else if (info.Key == ConsoleKey.Escape)
            {
                this.SaveAction(false);
            }
        }
    }
}
