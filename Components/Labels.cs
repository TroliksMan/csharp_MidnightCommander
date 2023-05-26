using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidnightCommander.Components
{
    class Labels : IComponent
    {
        public string Name { get; set; }
        public string Size { get; set; }
        public string Date { get; set; }
        public int Padding;


        public Labels()
        {
            Name = "Název";
            Size = "Velikos";
            Date = "Datum Změny";
        }
        public void Draw()
        {

            if (Console.WindowWidth % 2 == 0)
            {
                this.Padding = Console.WindowWidth / 2 - 24;
            }
            else
                this.Padding = (Console.WindowWidth + 1) / 2 - 25;
            Console.Write($"│{Name.PadRight(Padding / 2 + 3).PadLeft(Padding + 1)}│{Size}│{Date.PadRight(12)}│");
        }

        public void HandleKey(ConsoleKeyInfo info)
        {

        }

    }
}
