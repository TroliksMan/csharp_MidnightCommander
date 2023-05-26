using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidnightCommander.Components
{
    public interface IComponent
    {
        public string Name { get; set; }

        public void Draw();
        public void HandleKey(ConsoleKeyInfo info);
    }
}
