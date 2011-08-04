using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace net.flashpunk.splash
{
    public class Engine : net.flashpunk.Engine
    {
        private Splash splash;
        public Engine()
            : base()
        {
            splash = new Splash();
            FP.world.add(splash);
            splash.start();
        }
    }
}
