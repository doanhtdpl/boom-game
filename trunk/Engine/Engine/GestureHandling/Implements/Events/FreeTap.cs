using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SSCEngine.GestureHandling.Implements.Events
{
    public class FreeTap : BaseGestureHandling.BaseGestureEvent
    {
        public FreeTap(ITouch tap, bool IsFinish)
            : base(1)
        {
            this.gestureTouches.Add(tap);
            this.IsFinish = IsFinish;
        }

        public Vector2 Begin
        {
            get { return this.Touch.Positions.Begin;}
        }

        public Vector2 Current
        {
            get { return this.Touch.Positions.Current; }
        }

        public bool IsFinish { get; private set; }
    }
}
