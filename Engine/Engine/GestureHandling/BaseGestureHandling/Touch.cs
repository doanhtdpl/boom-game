using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSCEngine.GestureHandling.BaseGestureHandling
{
    class Touch : ITouch
    {
        public Touch(TouchLocation location, /*GestureType type, */TouchPositions positions)
        {
            this.SystemTouch = location;
            //this.Type = type;
            this.Positions = positions;
        }

        public TouchLocation SystemTouch { get; private set; }

        //public GestureType Type { get; private set; }

        public TouchPositions Positions { get; private set; }
    }
}
