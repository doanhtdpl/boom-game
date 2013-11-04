using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSCEngine.GestureHandling
{
    internal interface ITouchController
    {
        ICollection<ITouch> Touches { get; }

        void Update(TouchCollection toucheCollection, GameTime gameTime);
    }
}
