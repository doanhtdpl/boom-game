using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSCEngine.GestureHandling.Implements
{
    public class TapGestureHandler : IGestureHandler
    {
        public List<ITouch> touches = new List<ITouch>();

        public GestureType GestureType
        {
            get { return GestureType.Tap; }
        }

        public ICollection<ITouch> GestureTouches
        {
            get { return this.touches; }
        }

        public ITouch GestureTouch
        {
            get { return (this.touches.Count > 0) ? touches.First() : null; }
        }

        public void DetectGesture(ICollection<ITouch> touches)
        {
            this.touches.Clear();
            this.touches.AddRange(touches);

            this.handlerOnHasGesture();
        }

        private void handlerOnHasGesture()
        {
            if (this.OnHasGesture != null)
            {
                this.OnHasGesture(this, null);
            }
        }

        event OnHasGestureHandler OnHasGesture;

        event OnHasGestureHandler IGestureHandler.OnHasGesture
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }
    }
}
