using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSCEngine.GestureHandling
{
    public interface IGestureDispatcher
    {
        void AddTarget<GestureEvent>(IGestureTarget<GestureEvent> gTarget) where GestureEvent : IGestureEvent;
        void RemoveTarget<GestureEvent>(IGestureTarget<GestureEvent> gTarget) where GestureEvent : IGestureEvent;

        void Dispatch(IGestureEvent gEvent);

        bool Enabled { get; set; }
    }
}