using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SCSEngine.Mathematics;
using Microsoft.Xna.Framework.Input.Touch;

namespace SSCEngine.GestureHandling.Implements.Events
{
    public class DoubleFlick : BaseGestureHandling.BaseGestureEvent
    {
        public DoubleFlick(ITouch touch1, ITouch touch2)
            : base(2)
        {
            this.gestureTouches.Add(touch1);
            this.gestureTouches.Add(touch2);
            if (touch1.SystemTouch.State == TouchLocationState.Released)
                Direction = touch1.Positions.Delta;
            else
                Direction = touch2.Positions.Delta;
        }

        public ITouch FirstTouch
        {
            get { return this.gestureTouches[0]; }
        }

        public ITouch SecondTouch
        {
            get { return this.gestureTouches[1]; }
        }

        public float HorizontalDirection { get { return Direction.X; } }

        public float VerticalDirection { get { return Direction.Y; } }

        public Vector2 Direction { get; private set; }
    }
}
