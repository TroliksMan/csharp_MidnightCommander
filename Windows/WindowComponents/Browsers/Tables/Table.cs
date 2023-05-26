using MidnightCommander.Components.PopUp;
using MidnightCommander.Services;
using MidnightCommander.Windows.Browsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidnightCommander.Components
{
    public enum BrowserSite
    {
        LEFT,
        RIGHT
    }

    public class Table
    {
        public Stack<int> SelectedStack { get; set; }
        public int selected = 0;
        public Stack<int> TopStack { get; set; }
        public int top = 0;
        public string CurrentDir { get; set; }
        public IComponent Label { get; set; }
        public bool active;
        public int drawMax;

        public List<IComponent> Components { get; set; }
        public BrowserSite Site { get; set; }

        //public IComponent selectedComponent { get => Components[selected]; }

        public Table(BrowserSite siteGet)
        {
            this.SelectedStack = new Stack<int>();
            this.TopStack = new Stack<int>();

            this.Site = siteGet;
            this.Components = new List<IComponent>();
        }

        protected IComponent SelectedComponent
        {
            get
            {
                return this.Components[this.selected];
            }
        }

        public void Draw()
        {
            string partDir;
            if (CurrentDir.Length > Console.WindowWidth / 2 - 15)
                partDir = CurrentDir.Substring(0, Console.WindowWidth / 2 - 15) + "..";
            else
                partDir = CurrentDir;
            Console.Title = CurrentDir;

            if (this.Site == BrowserSite.LEFT)
            {
                DrawTable(partDir, 0);
            }
            else
            {
                int width;
                if (Console.WindowWidth % 2 == 0)
                {
                    width = Console.WindowWidth / 2;
                }
                else
                {
                    width = (Console.WindowWidth / 2) + 1;
                }
                DrawTable(partDir, width);
            }
            Console.SetCursorPosition(0, Console.WindowHeight - 2);
        }
        private void DrawTable(string partDir, int pad)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            int paddingTopBot;
            if (Console.WindowWidth % 2 == 0)
            {
                paddingTopBot = Console.WindowWidth / 2 - 2;
            }
            else
                paddingTopBot = (Console.WindowWidth + 1) / 2 - 3;

            Console.SetCursorPosition(pad, 0);
            Console.Write('┌');
            Console.Write($"────── {partDir} ".PadRight(paddingTopBot, '─'));
            Console.Write("┐");
            if (active == true)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;

                Console.SetCursorPosition(pad + 7, 0);
                Console.Write(' ' + partDir + ' ');

                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.DarkBlue;
            }

            Console.SetCursorPosition(pad, 1);
            Label.Draw();
            int index = 2;
            int neededEmpty = 0;
            if (Components.Count < Console.WindowHeight - 7)
            {
                neededEmpty = (Console.WindowHeight - 7) - Components.Count;
            }

            drawMax = Math.Min(Components.Count, Console.WindowHeight - 7);
            //Změna velikosti - oprava spadnutí
            if (selected > top + drawMax)
                selected = top + drawMax - 1;

            if (top + drawMax > Components.Count)
                top = Components.Count - drawMax;

            for (int i = top; i < drawMax + top; i++)
            {
                Console.SetCursorPosition(pad, index);
                Console.Write("│");
                if (i == this.selected && this.active)
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;

                }
                Components[i].Draw();
                Console.BackgroundColor = ConsoleColor.DarkBlue;

                Console.Write("│");
                index++;
            }
            for (int i = 0; i < neededEmpty; i++)
            {
                Console.SetCursorPosition(pad, index);
                int padding;
                if (Console.WindowWidth % 2 == 0)
                {
                    padding = Console.WindowWidth / 2 - 22;
                }
                else
                    padding = (Console.WindowWidth + 1) / 2 - 23;
                Console.Write("│".PadRight(padding) + "│" + "".PadRight(7) + "│" + "│".PadLeft(13));
                index++;
            }

            Console.SetCursorPosition(pad, index);
            Console.Write("├".PadRight(paddingTopBot + 1, '─') + '┤');
            index++;

            Console.SetCursorPosition(pad, index);

            string selectedComponent = Components[selected].Name;
            if (Components[selected].Name.Length > Console.WindowWidth / 2 - 5)
                selectedComponent = selectedComponent.Substring(0, Console.WindowWidth / 2 - 5) + "..";
            Console.Write('│' + (selectedComponent).PadRight(paddingTopBot) + '│');
            index++;

            Console.SetCursorPosition(pad, index);
            Console.Write('└' + (DirectoryService.Space(CurrentDir).PadLeft(paddingTopBot, '─') + '┘'));
        }
        public void HandleKey(ConsoleKeyInfo info)
        {
            switch (info.Key)
            {
                case ConsoleKey.UpArrow:
                    if (this.selected == 0)
                        break;
                    if (this.selected == this.top)
                        this.top--;
                    this.selected--;
                    break;
                case ConsoleKey.DownArrow:
                    if (this.selected + 1 == this.Components.Count)
                        break;
                    if (this.selected + 1 == this.drawMax + top)
                    {
                        this.top++;
                    }
                    this.selected++;
                    break;
                default:
                    SelectedComponent.HandleKey(info);
                    break;
            }
        }
    }
}
