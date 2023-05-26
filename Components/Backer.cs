using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidnightCommander.Components
{
    class Backer : IComponent
    {
        private string BackButton { get; set; }
        private string Size { get; set; }
        private string EditDateTime { get; set; }
        private int Padding { get; set; }
        public string Name { get; set; }

        public event Action Click;

        public Backer()
        {
            Name = "$Backer$";
            BackButton = "/..";
            Size = "";
            EditDateTime = "";
        }
        public void Draw()
        {
            if (Console.WindowWidth % 2 == 0)
            {
                this.Padding = Console.WindowWidth / 2 - 23;
            }
            else
                this.Padding = (Console.WindowWidth + 1) / 2 - 24;

            Console.Write(BackButton.PadRight(this.Padding) + '│' + Size.PadRight(7) + '│' + EditDateTime.PadRight(12));
        }

        public void HandleKey(ConsoleKeyInfo info)
        {
            if (info.Key == ConsoleKey.Enter || info.Key == ConsoleKey.F3)
            {
                this.Click();

            }
        }
    }
}
