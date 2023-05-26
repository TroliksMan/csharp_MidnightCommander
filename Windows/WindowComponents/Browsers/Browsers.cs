using MidnightCommander.Components;
using MidnightCommander.Components.PopUp;
using MidnightCommander.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidnightCommander.Windows.Browsers
{

    public class Browser
    {
        public event Action Click;

        public DirectoryService Dir { get; set; }
        public Table Table { get; set; }
        public static IPopUpWindow popUp;

        public Browser(BrowserSite site)
        {
            Dir = new DirectoryService();
            Table = new Table(site);
            this.Table.Label = new Labels();
            Backer backer = new Backer();
            this.Table.Components.Add(backer);
            backer.Click += Backer_Click;

            GetData(@"X:\");
        }

        public void GetData(string path)
        {
            this.Table.Components = new List<IComponent>();
            Backer backer = new Backer();
            backer.Click += Backer_Click;
            this.Table.Components.Add(backer);
            this.Table.CurrentDir = path;
            foreach (DirectoryInfo item in this.Dir.GetDirectory(path))
            {
                Folder folder = new Folder(
                    item.Name,
                    "",
                    item.LastWriteTime.ToString("M/MMM HH:mm"),
                    item.FullName
                );
                folder.Click += Folder_Click;
                this.Table.Components.Add(folder);
            }
            foreach (FileInfo item in this.Dir.GetFiles(path))
            {
                Components.File file = new Components.File(
                    item.Name,
                    item.Length.ToString(),
                    item.LastWriteTime.ToString("M/MMM HH:mm"),
                    item.FullName,
                    item.Extension
                    );
                //file.Click += Folder_Click;
                this.Table.Components.Add(file);
            }
        }

        public void Draw()
        {
            if (!BrowserWindow.ActivePopUp)
            {
                Console.CursorVisible = false;
                Table.Draw();
            }
            else
                popUp.Draw();
        }
        public void HandleKey(ConsoleKeyInfo info)
        {
            if (BrowserWindow.ActivePopUp)
            {
                popUp.HandleKey(info);
            }
            else
            {
                switch (info.Key)
                {
                    case ConsoleKey.Tab:
                        this.Click();
                        return;
                    case ConsoleKey.F1:
                        BrowserWindow.ActivePopUp = true;
                        popUp = new Help();
                        popUp.Draw();
                        break;
                    case ConsoleKey.F2:
                        BrowserWindow.ActivePopUp = true;
                        popUp = new Nothing();
                        popUp.Draw();
                        break;
                    //case ConsoleKey.F3:

                    //  break;
                    //case ConsoleKey.F4:

                    //break;
                    //case ConsoleKey.F5:

                    //break;
                    //case ConsoleKey.F6:

                    //break;
                    case ConsoleKey.F7:
                        BrowserWindow.ActivePopUp = true;
                        NewFolder newFolder = new NewFolder();
                        popUp = newFolder;
                        newFolder.Click += NewFolder_Click;
                        break;
                    //case ConsoleKey.F8:

                    //  break;
                    case ConsoleKey.F9:
                        BrowserWindow.ActivePopUp = true;
                        DiskChange diskChange = new DiskChange();
                        popUp = diskChange;
                        diskChange.ClickChange += DiskChange_Click;
                        break;
                    case ConsoleKey.F10:
                        Console.Beep();
                        Environment.Exit(1);
                        break;
                    default:
                        Table.HandleKey(info);
                        break;
                }
            }
        }

        private void NewFolder_Click(string obj)
        {
            DirectoryInfo dir = new DirectoryInfo(Table.CurrentDir + @"\" + obj);
            try
            {
                dir.Create();
            }
            catch (Exception)
            {
                Error error = new Error("Wrong folder name");
                error.Draw();
                Application.Initialize();
                return;
            }
            this.Folder_Click(Table.CurrentDir);
            Application.Initialize();
        }

        private void DiskChange_Click(string obj)
        {
            Table.selected = 0;
            Table.top = 0;
            GetData(obj);
        }

        private void Backer_Click()
        {
            Table.Components = new List<IComponent>();
            if (Table.SelectedStack.Count == 0)
                Table.selected = 0;
            else
                Table.selected = Table.SelectedStack.Pop();
            if (Table.TopStack.Count != 0)
                Table.top = Table.TopStack.Pop();
            Backer backer = new Backer();
            backer.Click += Backer_Click;
            Table.Components.Add(backer);
            GetData(Dir.GetUpperDir(Table.CurrentDir));
        }
        private void Folder_Click(string str)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(str);
                dir.GetDirectories();
            }
            catch (Exception)
            {
                Error error = new Error("Access denied");
                error.Draw();
                return;
            }
            Table.SelectedStack.Push(Table.selected);
            Table.selected = 0;
            Table.TopStack.Push(Table.top);
            Table.top = 0;
            GetData(str);
        }
    }
}
