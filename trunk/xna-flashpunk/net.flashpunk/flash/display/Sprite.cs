using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using net.flashpunk;

namespace flash.display
{
    public class Sprite
    {
        public Graphics graphics
        {
            get { return new Graphics(FP.graphicsDeviceManager.GraphicsDevice); }
        }
    }
}
