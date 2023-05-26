using MidnightCommander.Services;
using MidnightCommander.Windows;
using MidnightCommander.Windows.Browsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidnightCommander.Components.PopUp
{
    public class DiskChange : IPopUpWindow
    {
        public int PopUpX { get; set; }
        public int PopUpY { get; set; }
        public int PopUpWidth { get; set; }
        private int selectedDrive = 0;
        private List<string> Drives { get; set; }

        public event Action<string> ClickChange;


        public DiskChange()
        {
            Drives = DirectoryService.GetDrives();
        }

        public void Draw()
        {
            PopUpWidth = Math.Max((Drives.Count) * 5 + Drives.Count + 3, 17);
            PopUpX = Console.WindowWidth / 2 - PopUpWidth / 2;
            PopUpY = Console.WindowHeight / 2 - 4;

            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("".PadRight(PopUpWidth));
            PopUpY++;
            Console.SetCursorPosition(PopUpX, PopUpY);

            Console.Write(" ┌".PadRight((PopUpWidth - 12) / 2, '─') + " Změna disku " + "┐ ".PadLeft((PopUpWidth - 12) / 2, '─'));
            PopUpY++;
            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" │".PadRight(PopUpWidth - 2) + "│ ");
            PopUpY++;

            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" │");
            for (int i = 0; i < Drives.Count; i++)
            {
                if (Drives.Count == 2)
                    Console.Write(" ");
                if (this.selectedDrive == i)
                    Console.BackgroundColor = ConsoleColor.Blue;
                Console.Write($"[{Drives[i]}]");
                Console.BackgroundColor = ConsoleColor.Gray;
                if (i != Drives.Count - 1 && Drives.Count != 2)
                    Console.Write(" ");
                if (Drives.Count == 2 && i == Drives.Count - 1)
                    Console.Write(" ");
            }
            Console.Write("│ ");

            PopUpY++;
            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" └".PadRight(PopUpWidth - 2, '─') + "┘ ");

            PopUpY++;
            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write("".PadRight(PopUpWidth));
            Console.BackgroundColor = ConsoleColor.DarkBlue;
        }
        public void HandleKey(ConsoleKeyInfo info)
        {
            switch (info.Key)
            {
                case ConsoleKey.Tab:
                    this.selectedDrive++;
                    this.selectedDrive = selectedDrive % Drives.Count;
                    break;
                case ConsoleKey.Enter:
                    BrowserWindow.ActivePopUp = false;
                    Browser.popUp = null;
                    Application.Initialize();
                    this.ClickChange(Drives[selectedDrive]);
                    break;
                case ConsoleKey.Escape:
                    BrowserWindow.ActivePopUp = false;
                    Browser.popUp = null;
                    Application.Initialize();
                    break;
            }
        }
    }
}
