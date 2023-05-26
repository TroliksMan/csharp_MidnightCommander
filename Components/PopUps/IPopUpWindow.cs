using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidnightCommander.Components.PopUp
{
    public interface IPopUpWindow
    {
        //public event Action Click;
        public int PopUpX { get; set; }
        public int PopUpY { get; set; }
        public int PopUpWidth { get; set; }
        public void Draw();
        public void HandleKey(ConsoleKeyInfo info);
    }
}
