using MidnightCommander.Components;
using MidnightCommander.Components.PopUp;
using MidnightCommander.Windows.Browsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidnightCommander.Windows
{
    public enum ActiveBrowser
    {
        leftBrowser = 0,
        rightBrowser = 1
    }

    public class BrowserWindow : Window
    {
        public static List<Browser> Browsers { get; set; }
        public static ActiveBrowser Site { get; set; }
        public static bool ActivePopUp = false;

        ButtonsTable buttons;

        public BrowserWindow()
        {
            Site = ActiveBrowser.leftBrowser;
            Browsers = new List<Browser>
            {
                new Browser(BrowserSite.LEFT),
                new Browser(BrowserSite.RIGHT)
            };

            Browsers[0].Table.active = true;

            Browsers[0].Click += BrowserWindowLEFT_Click;
            Browsers[1].Click += BrowserWindowRIGHT_Click;

            buttons = new ButtonsTable();
        }

        private void BrowserWindowLEFT_Click()
        {
            Site = ActiveBrowser.rightBrowser;
            Browsers[0].Table.active = false;
            Browsers[1].Table.active = true;
            Application.Initialize();
        }
        private void BrowserWindowRIGHT_Click()
        {
            Site = ActiveBrowser.leftBrowser;
            Browsers[0].Table.active = true;
            Browsers[1].Table.active = false;
            Application.Initialize();
        }

        public override void Draw()
        {
            Browsers[((int)Site)].Draw();
            //Application.DrawMiddle();

        }
        public override void HandleKey(ConsoleKeyInfo info)
        {
            Browsers[((int)Site)].HandleKey(info);
        }

        public override void Initialize()
        {
            Application.DrawMiddle();
            foreach (Browser item in Browsers)
            {
                item.Table.Draw();
                if (ActivePopUp)
                {
                    item.Draw();
                }
            }
            buttons.Draw();
        }
    }
}
