using MidnightCommander.Components.PopUp;
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
    public class Folder : IComponent
    {
        public string Name { get; set; }
        public string Size { get; set; }
        public string EditDateTime { get; set; }
        public string FullName { get; set; }
        private string RealName { get; set; }
        public int Padding;

        public event Action<string> Click;

        public Folder(string name, string size, string editDateTime, string fullName)
        {
            RealName = name;
            Name = '/' + name;
            Size = size;
            EditDateTime = editDateTime;
            FullName = fullName;
        }

        public void Draw()
        {
            this.Padding = Console.WindowWidth / 2 - 23;

            string partFolderName = this.Name;
            if (this.Name.Length > Padding - 3)
                partFolderName = this.Name.Substring(0, Padding - 3) + "..";

            Console.Write(partFolderName.PadRight(this.Padding) + '│' + this.Size.PadLeft(7) + '│' + this.EditDateTime.PadLeft(12));
        }

        public void HandleKey(ConsoleKeyInfo info)
        {
            switch (info.Key)
            {
                case ConsoleKey.Enter:
                    this.Click(FullName);
                    break;
                case ConsoleKey.F3:
                    this.Click(FullName);
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
                    {
                        otherPathMove = BrowserWindow.Browsers[1].Table.CurrentDir;
                    }
                    else
                    {
                        otherPathMove = BrowserWindow.Browsers[0].Table.CurrentDir;
                    }
                    BrowserWindow.ActivePopUp = true;
                    MoveRename move = new MoveRename(otherPathMove, RealName);
                    Browser.popUp = move;
                    move.MoveAction += Move_MoveAction;
                    break;
                case ConsoleKey.F8:
                    BrowserWindow.ActivePopUp = true;
                    Delete delete = new Delete(Name);
                    Browser.popUp = delete;
                    delete.Click += Delete_Click;
                    break;
            }
        }

        private void Move_MoveAction(string obj)
        {
            DirectoryInfo dirOld = new DirectoryInfo(this.FullName);
            DirectoryInfo dirNew = new DirectoryInfo(obj);
            try
            {
                if (dirOld.Root.Name != dirNew.Root.Name)
                {
                    if (obj.EndsWith('\\'))
                        CopyRecursive(this.FullName, obj, this.RealName);
                    else
                    {
                        string path = obj.Substring(0, obj.LastIndexOf('\\'));
                        string name = obj.Substring(obj.LastIndexOf('\\'));
                        CopyRecursive(this.FullName, path, name);
                    }
                    dirOld.Delete(true);
                }
                else
                {
                    dirOld.MoveTo(obj);
                }

            }
            catch (Exception)
            {
                Error error = new Error("Neurčená chyba");
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

            this.CopyRecursive(this.FullName, obj, this.RealName);
            if (BrowserWindow.Site == ActiveBrowser.leftBrowser)
            {
                BrowserWindow.Browsers[1].GetData(BrowserWindow.Browsers[1].Table.CurrentDir);
            }
            else
            {
                BrowserWindow.Browsers[0].GetData(BrowserWindow.Browsers[0].Table.CurrentDir);

            }
        }

        private void CopyRecursive(string pathOld, string pathNew, string name)
        {
            DirectoryInfo dirOld = new DirectoryInfo(pathOld);
            try
            {
                dirOld.GetDirectories();
            }
            catch (Exception)
            {
                Error error = new Error("Neurčená chyba");
                error.Draw();
                Application.Initialize();
                return;
            }

            DirectoryInfo dirNew = new DirectoryInfo(pathNew + name);
            dirNew.Create();

            foreach (DirectoryInfo item in dirOld.GetDirectories())
            {
                this.CopyRecursive(item.FullName, pathNew + '\\' + name, '\\' + item.Name);
            }
            foreach (FileInfo item in dirOld.GetFiles())
            {
                item.CopyTo(pathNew + name + '\\' + item.Name);
            }
        }
        private void Delete_Click()
        {
            DirectoryInfo dir = new DirectoryInfo(FullName);
            //if (dir.GetDirectories().Length != 0 || dir.GetFiles().Length != 0)
            //{
            //    Console.Clear();
            //    Console.WriteLine("více složek");
            //    Console.ReadKey();
            //}
            //else
            //{
            //    Console.Clear();
            //    Console.WriteLine("bez složek");
            //    Console.ReadKey();
            //}
            //return;


            dir.Delete(true);
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
