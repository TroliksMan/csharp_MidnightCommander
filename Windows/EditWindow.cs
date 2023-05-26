using MidnightCommander.Components.PopUp;
using MidnightCommander.Windows.WindowComponents.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidnightCommander.Windows
{
    public class EditWindow : Window
    {
        private EditorTable Editor { get; set; }

        public static IPopUpWindow popUpWindow;
        public static Window LastWindow;
        public EditWindow(string path, string name, Window lastWindow)
        {
            Editor = new EditorTable(path, name);
            LastWindow = lastWindow;
        }
        public override void Draw()
        {
            if (popUpWindow == null)
                Editor.Draw();
            else
                popUpWindow.Draw();
        }

        public override void HandleKey(ConsoleKeyInfo info)
        {
            if (popUpWindow == null)
                Editor.HandleKey(info);
            else
                popUpWindow.HandleKey(info);
        }
        public override void Initialize()
        {
            Editor.Draw();
            if (popUpWindow != null)
                popUpWindow.Draw();
        }
    }
}
