using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SSCEngine.GestureHandling.Implements.Events;
using Microsoft.Xna.Framework.Input.Touch;

namespace SSCEngine.GestureHandling.Implements.Detectors
{
    public class FreeTapDetector : IGestureDetector
    {
        public ICollection<IGestureEvent> DetectGesture(ICollection<ITouch> touches)
        {
            List<IGestureEvent> taps = new List<IGestureEvent>(touches.Count);

            foreach (var touch in touches)
            {
                taps.Add(new FreeTap(touch, (touch.SystemTouch.State == TouchLocationState.Released)));
            }

            return taps;
        }
    }
}
