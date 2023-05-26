using MidnightCommander.Components.PopUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidnightCommander.Components.PopUps
{
    public class Universal : IPopUpWindow
    {
        public int PopUpX { get; set; }
        public int PopUpY { get; set; }
        public int PopUpWidth { get; set; }

        private string Name;
        private ConsoleColor BgColor;
        private ConsoleColor FgColor;
        private bool Buttons;
        private int selected = 1;

        public event Action<bool> Click;
        public Universal(string label, ConsoleColor bgColor, ConsoleColor fgColor, bool buttons)
        {
            this.Name = label;
            this.BgColor = bgColor;
            this.FgColor = fgColor;
            this.Buttons = buttons;
        }

        public void Draw()
        {
            Console.BackgroundColor = BgColor;
            Console.ForegroundColor = FgColor;
            Console.CursorVisible = false;
            this.PopUpWidth = Math.Max(Name.Length + 8, 40);
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
            Console.SetCursorPosition((Console.WindowWidth / 2) - Name.Length / 2, PopUpY);
            Console.Write(Name);
            PopUpY++;

            Console.SetCursorPosition(PopUpX, PopUpY);

            if (Buttons)
            {
                Console.Write(" ├".PadRight(PopUpWidth - 2, '─') + "┤ ");
                PopUpY++;
                Console.SetCursorPosition(PopUpX, PopUpY);
                DrawButtons();
                PopUpY++;
                Console.SetCursorPosition(PopUpX, PopUpY);
                Console.Write(" └".PadRight(PopUpWidth - 2, '─') + "┘ ");
            }
            else
            {
                Console.Write(" │");
                Console.Write("".PadRight(PopUpWidth - 4));
                Console.Write("│ ");
                PopUpY++;
                Console.SetCursorPosition(PopUpX, PopUpY);
                Console.Write(" └".PadRight(PopUpWidth - 2, '─') + "┘ ");

            }
            PopUpY++;
            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write("".PadRight(PopUpWidth));
            // ┌ ┐ └ ┘ ├ ┤ ─ │
        }
        private void DrawButtons()
        {
            Console.Write(" │".PadRight((PopUpWidth - 25) / 2));
            if (selected == 0)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("[   Ok   ]");
                Console.ForegroundColor = FgColor;
                Console.BackgroundColor = BgColor;
                Console.Write("     [ Storno ]");
            }
            else
            {
                Console.Write("[   Ok   ]     ");
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("[ Storno ]");
                Console.ForegroundColor = FgColor;
                Console.BackgroundColor = BgColor;
            }
            Console.Write("│ ".PadLeft((PopUpWidth - 24) / 2));
        }

        public void HandleKey(ConsoleKeyInfo info)
        {
            if (Buttons)
            {
                if (info.Key == ConsoleKey.Tab)
                {
                    selected++;
                    selected %= 2;
                }
                else if (info.Key == ConsoleKey.Enter)
                {
                    if (selected == 0)
                        this.Click(true);
                    else
                        this.Click(false);
                }
                else if (info.Key == ConsoleKey.Escape)
                {
                    this.Click(false);
                }
            }
        }
    }
}
