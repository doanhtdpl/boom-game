using Microsoft.Xna.Framework;
using SSCEngine.GestureHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSCEngine.Control
{
    public class BaseUIFactory
    {
        private BaseUIFactory()
        {
        }

        public static BaseUIFactory Instance { get; private set; }

        static BaseUIFactory()
        {
            Instance = new BaseUIFactory();
        }

        public IUIContainer CreateControlManager(Game game, IGestureHandlingFactory ghF)
        {
            return new UIControlManager(game, ghF);
        }
    }
}
