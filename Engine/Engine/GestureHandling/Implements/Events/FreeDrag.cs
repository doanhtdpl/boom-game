using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSCEngine.GestureHandling.Implements.Events
{
    public class FreeDrag : BaseGestureHandling.BaseGestureEvent
    {
        public FreeDrag(ITouch drag)
            : base(1)
        {
            this.gestureTouches.Add(drag);
        }
    }
}