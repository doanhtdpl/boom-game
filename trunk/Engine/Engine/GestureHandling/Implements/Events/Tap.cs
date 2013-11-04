using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSCEngine.GestureHandling.Implements.Events
{
    public class Tap : BaseGestureHandling.BaseGestureEvent
    {
        public Tap(ITouch tap)
            : base(1)
        {
            this.gestureTouches.Add(tap);
        }
    }
}
