using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSCEngine.GestureHandling
{
    public interface IGestureEvent
    {
        ITouch Touch { get; }
        ICollection<ITouch> GestureTouches { get; }
    }
}
