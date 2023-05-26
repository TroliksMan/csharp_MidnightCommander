using MidnightCommander.Components.PopUp;
using MidnightCommander.Services;
using MidnightCommander.Windows;
using MidnightCommander.Windows.Browsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidnightCommander.Components
{
    public class File : IComponent
    {
        public string Name { get; set; }
        public string Size { get; set; }
        public string EditDateTime { get; set; }
        public string FullName { get; set; }
        public string Extension { get; set; }
        private string RealName { get; set; }
        public int Padding;
        private ConsoleColor FileColor { get; set; }

        //public event Action<string> Click;

        public File(string name, string size, string editDateTime, string fullName, string extension)
        {
            FileService fileService = new FileService();

            RealName = name;
            Name = name;
            Size = size;
            EditDateTime = editDateTime;
            FullName = fullName;
            Extension = extension;

            if (Extension == ".exe" || Extension == ".bat")
                Name = '*' + Name;
            else
                Name = ' ' + Name;
            FileColor = fileService.GetColor(Extension);
        }


        public void Draw()
        {
            if (this.Size.Length > 7)
                this.Size = this.Size.Substring(0, 5) + "..";

            if (Console.WindowWidth % 2 == 0)
            {
                this.Padding = Console.WindowWidth / 2 - 23;
            }
            else
                this.Padding = (Console.WindowWidth + 1) / 2 - 24;

            string partFileName = this.Name;
            if (this.Name.Length > Padding - 3)
                partFileName = this.Name.Substring(0, Padding - 3) + "..";


            Console.ForegroundColor = FileColor;
            Console.Write(partFileName.PadRight(this.Padding));
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write('│');
            Console.ForegroundColor = FileColor;
            Console.Write(this.Size.PadLeft(7));
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write('│');
            Console.ForegroundColor = FileColor;
            Console.Write(this.EditDateTime.PadLeft(12));
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void HandleKey(ConsoleKeyInfo info)
        {

            switch (info.Key)
            {
                //case ConsoleKey.Enter:
                //    this.Click(FullName);
                //    break;
                //case ConsoleKey.F3:
                //    this.Click(FullName);
                //    break;
                case ConsoleKey.F4:
                    if (Extension == ".txt")
                    {
                        Application.windowType = WindowType.Editor;
                        //Application.Window[1] = new EditWindow(this.FullName, this.Name);
                        Application.window = new EditWindow(this.FullName, this.Name, Application.window);
                    }

                    break;
                case ConsoleKey.F5:
                    string otherPathCopy;
                    if (BrowserWindow.Site == ActiveBrowser.leftBrowser)
                        otherPathCopy = BrowserWindow.Browsers[1].Table.CurrentDir;
                    else
                        otherPathCopy = BrowserWindow.Browsers[0].Table.CurrentDir;
                    BrowserWindow.ActivePopUp = true;
                    Copy copy = new Copy(otherPathCopy, RealName);
                    Browser.popUp = copy;
                    copy.CopyAction += Copy_CopyAction;
                    break;
                case ConsoleKey.F6:
                    string otherPathMove;
                    if (BrowserWindow.Site == ActiveBrowser.leftBrowser)
                        otherPathMove = BrowserWindow.Browsers[1].Table.CurrentDir;
                    else
                        otherPathMove = BrowserWindow.Browsers[0].Table.CurrentDir;

                    BrowserWindow.ActivePopUp = true;
                    MoveRename move = new MoveRename(otherPathMove, RealName);
                    Browser.popUp = move;
                    move.MoveAction += Move_MoveAction;
                    break;
                case ConsoleKey.F8:
                    BrowserWindow.ActivePopUp = true;
                    Delete delete = new Delete(RealName);
                    Browser.popUp = delete;
                    delete.Click += Delete_Click;
                    break;
            }
        }

        private void Move_MoveAction(string obj)
        {
            FileInfo fileInfo = new FileInfo(this.FullName);
            string newPath = obj;
            try
            {
                fileInfo.MoveTo(newPath);
            }
            catch (Exception)
            {
                Application.Initialize();
                Error error = new Error("Složka nenalezena");
                error.Draw();
                return;
            }
            foreach (Browser item in BrowserWindow.Browsers)
            {
                item.Table.top = 0;
                item.Table.selected = 0;
                item.GetData(item.Table.CurrentDir);
            }
        }

        private void Copy_CopyAction(string obj)
        {
            FileInfo fileInfo = new FileInfo(this.FullName);
            try
            {
                fileInfo.CopyTo(obj + RealName, true);

            }
            catch (Exception)
            {
                Application.Initialize();
                Error error = new Error("Složka nenalezena");
                error.Draw();
                return;
            }

            if (BrowserWindow.Site == ActiveBrowser.leftBrowser)
            {
                BrowserWindow.Browsers[1].GetData(BrowserWindow.Browsers[1].Table.CurrentDir);
            }
            else
            {
                BrowserWindow.Browsers[0].GetData(BrowserWindow.Browsers[0].Table.CurrentDir);

            }
        }

        private void Delete_Click()
        {
            FileInfo fileInfo = new FileInfo(FullName);

            fileInfo.Delete();

            if (BrowserWindow.Site == ActiveBrowser.leftBrowser)
            {
                BrowserWindow.Browsers[0].Table.selected = 0;
                BrowserWindow.Browsers[0].GetData(BrowserWindow.Browsers[0].Table.CurrentDir);
            }
            else
            {
                BrowserWindow.Browsers[1].Table.selected = 0;
                BrowserWindow.Browsers[1].GetData(BrowserWindow.Browsers[1].Table.CurrentDir);
            }
            Application.Initialize();
        }
    }
}
