using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSCEngine.GestureHandling
{
    public interface ITouch
    {
        TouchLocation SystemTouch { get; }
        //GestureType Type { get; }

        TouchPositions Positions { get; }
    }
}
