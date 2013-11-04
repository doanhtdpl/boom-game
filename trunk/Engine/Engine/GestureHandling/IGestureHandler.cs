using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSCEngine.GestureHandling
{
    public delegate void OnHasGestureHandler(IGestureHandler gesture, EventArgs args);

    public interface IGestureHandler
    {
        GestureType GestureType { get; }
        ICollection<ITouch> GestureTouches { get; }
        ITouch GestureTouch { get; }

        void DetectGesture(ICollection<ITouch> touches);

        event OnHasGestureHandler OnHasGesture;
    }
}
