using MidnightCommander.Components.PopUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidnightCommander.Components
{
    public class ButtonsTable : IComponent
    {
        public string Name { get; set; }
        private int pad;
        public void Draw()
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("".PadRight(Console.WindowWidth));
            pad = Console.WindowWidth / 10 - 2;
            Console.Write(" 1");
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.Write("Nápověda".PadRight(pad));
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(" 2");
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.Write("Nabídka".PadRight(pad));
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(" 3");
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.Write("Zobraz".PadRight(pad));
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(" 4");
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.Write("Upravit".PadRight(pad));
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(" 5");
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.Write("Kopie".PadRight(pad));
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(" 6");
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.Write("PřejmPřes".PadRight(pad));
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(" 7");
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.Write("Nová slož.".PadRight(pad));
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(" 8");
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.Write("Smazat".PadRight(pad));
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(" 9");
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.Write("Jiný disk".PadRight(pad));
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("10");
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.Write("Konec".PadRight(pad));
            Console.BackgroundColor = ConsoleColor.DarkBlue;
        }

        public void HandleKey(ConsoleKeyInfo info)
        {
            switch (info.Key)
            {
                case ConsoleKey.F1:

                    break;
            }
        }
    }
}
