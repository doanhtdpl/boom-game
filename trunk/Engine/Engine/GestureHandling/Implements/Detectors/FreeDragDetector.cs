using SSCEngine.GestureHandling.Implements.Events;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSCEngine.GestureHandling.Implements.Detectors
{
    class FreeDragDetector : IGestureDetector
    {
        public ICollection<IGestureEvent> DetectGesture(ICollection<ITouch> touches)
        {
            List<IGestureEvent> drags = new List<IGestureEvent>(touches.Count);
            float minDrag = 20 * 20;

            foreach (var touch in touches)
            {
                if (touch.Positions.TotalDelta.LengthSquared() >= minDrag)
                    drags.Add(new FreeDrag(touch));
            }

            return drags;
        }
    }
}
