﻿using MidnightCommander.Windows;
using MidnightCommander.Windows.Browsers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidnightCommander.Components.PopUp
{
    public class MoveRename : IPopUpWindow
    {
        public int PopUpX { get; set; }
        public int PopUpY { get; set; }
        public int PopUpWidth { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        private int selected = 0;

        private bool nameActive = true;

        private int cursorXUp;
        private int cursorYUp;

        private int cursorXDown;
        private int cursorYDown;

        public event Action<string> MoveAction;

        public MoveRename(string path, string name)
        {
            //Path = path + '\\' + name ;
            Path = path + '\\';
            Name = name;
            //cursorXUp = Console.WindowWidth / 2 - 21 + Math.Min(Name.Length, 40);
            //cursorYUp = Console.WindowHeight / 2 - 6;

            //cursorXDown = Console.WindowWidth / 2 - 21 + Math.Min(Path.Length, 40);
            //cursorYDown = Console.WindowHeight / 2 - 3;
        }
        public void Draw()
        {
            cursorXUp = Console.WindowWidth / 2 - 21 + Math.Min(Name.Length, 40);
            cursorYUp = Console.WindowHeight / 2 - 6;

            cursorXDown = Console.WindowWidth / 2 - 21 + Math.Min(Path.Length, 40);
            cursorYDown = Console.WindowHeight / 2 - 3;


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
            Console.Write(" ┌".PadRight((PopUpWidth - 25) / 2, '─') + " Opravdu chcete přesunout " + "┐ ".PadLeft((PopUpWidth - 26) / 2, '─'));
            PopUpY++;

            string subName = Name;
            if (Name.Length > 40)
                subName = /*".." + */subName.Substring(subName.Length - 40, 40);
            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" │  ");
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write(subName.PadRight(PopUpWidth - 8));
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write("  │ ");
            //Console.Write(" │".PadRight((PopUpWidth - Name.Length - 2) / 2) + $" {subName} " + "│ ".PadLeft((PopUpWidth - Name.Length - 1) / 2));
            PopUpY++;

            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" ├".PadRight(PopUpWidth - 2, '─') + "┤ ");
            PopUpY++;

            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" │".PadRight((PopUpWidth - 4) / 2) + " do " + "│ ".PadLeft((PopUpWidth - 4) / 2));
            PopUpY++;


            string subPath = Path;
            if (subPath.Length > 40)
            {
                subPath = subPath.Substring(subPath.Length - 40, 40);
            }
            Console.SetCursorPosition(PopUpX, PopUpY);
            Console.Write(" │  ");
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write(subPath.PadRight(PopUpWidth - 8));
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

            if (nameActive)
                Console.SetCursorPosition(cursorXUp, cursorYUp);
            else
                Console.SetCursorPosition(cursorXDown, cursorYDown);

        }

        public void HandleKey(ConsoleKeyInfo info)
        {
            if (nameActive)
            {
                switch (info.Key)
                {
                    case ConsoleKey.DownArrow:
                        this.nameActive = false;
                        break;
                    case ConsoleKey.Escape:
                        Console.CursorVisible = false;
                        BrowserWindow.ActivePopUp = false;
                        Browser.popUp = null;
                        Application.Initialize();
                        break;
                    case ConsoleKey.Enter:
                        Console.CursorVisible = false;
                        if (this.selected == 0)
                        {

                            if (Path.EndsWith('\\') == false)
                                Path += '\\';
                            this.MoveAction(Path + Name);
                        }

                        BrowserWindow.ActivePopUp = false;
                        Browser.popUp = null;
                        Application.Initialize();
                        break;
                    case ConsoleKey.Tab:
                        this.selected++;
                        this.selected = selected % 2;
                        break;
                    case ConsoleKey.Backspace:
                        if (Name != "")
                            Name = Name.Remove(Name.Length - 1);
                        break;
                    default:
                        if (Char.GetUnicodeCategory(info.KeyChar) != UnicodeCategory.Control)
                            this.Name += info.KeyChar;
                        break;
                }
            }
            else
            {
                switch (info.Key)
                {
                    case ConsoleKey.UpArrow:
                        this.nameActive = true;
                        break;
                    case ConsoleKey.Escape:
                        Console.CursorVisible = false;
                        BrowserWindow.ActivePopUp = false;
                        Browser.popUp = null;
                        Application.Initialize();
                        break;
                    case ConsoleKey.Enter:
                        Console.CursorVisible = false;
                        if (this.selected == 0)
                        {
                            if (Path.EndsWith('\\') == false)
                                Path += '\\';
                            this.MoveAction(Path + Name);
                        }
                        BrowserWindow.ActivePopUp = false;
                        Browser.popUp = null;
                        Application.Initialize();
                        break;
                    case ConsoleKey.Tab:
                        this.selected++;
                        this.selected = selected % 2;
                        break;
                    case ConsoleKey.Backspace:
                        if (Path != "")
                            Path = Path.Remove(Path.Length - 1);
                        break;
                    default:
                        if (Char.GetUnicodeCategory(info.KeyChar) != UnicodeCategory.Control)
                            this.Path += info.KeyChar;
                        break;
                }
            }
        }
    }
}
