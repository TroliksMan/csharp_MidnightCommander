﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidnightCommander.Windows
{
    public abstract class Window
    {
        public virtual void Draw()
        {

        }
        public virtual void HandleKey(ConsoleKeyInfo info)
        {

        }
        public virtual void Initialize()
        {

        }
    }
}
