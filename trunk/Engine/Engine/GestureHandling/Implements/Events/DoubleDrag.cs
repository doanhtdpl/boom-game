using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SSCEngine.GestureHandling.Implements.Events
{
    public class DoubleDrag : BaseGestureHandling.BaseGestureEvent
    {
        public DoubleDrag(ITouch touch1, ITouch touch2)
            : base(2)
        {
            this.gestureTouches.Add(touch1);
            this.gestureTouches.Add(touch2);
        }

        public ITouch FirstTouch
        {
            get { return this.gestureTouches[0]; }
        }

        public ITouch SecondTouch
        {
            get { return this.gestureTouches[1]; }
        }

        public Vector2 Delta
        {
            get { return FirstTouch.Positions.Delta; }
        }
    }
}
