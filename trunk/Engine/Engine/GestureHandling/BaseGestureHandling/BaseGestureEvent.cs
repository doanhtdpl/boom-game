using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSCEngine.GestureHandling.BaseGestureHandling
{
    public class BaseGestureEvent : IGestureEvent
    {
        public ITouch Touch
        {
            get { try { return GestureTouches.First(); } catch (Exception) { return null; } }
            internal set { if (GestureTouches.Count > 0) { this.gestureTouches[0] = value; } }
        }

        protected List<ITouch> gestureTouches;
        public ICollection<ITouch> GestureTouches
        {
            get
            {
                return this.gestureTouches;
            }
        }

        public BaseGestureEvent(int nTouch)
        {
            this.gestureTouches = new List<ITouch>(nTouch);
        }
    }
}
