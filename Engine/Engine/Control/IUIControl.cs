using Microsoft.Xna.Framework;
using SSCEngine.GestureHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSCEngine.Control
{
    public interface IUIControl : IDrawable
    {
        ICanvas Canvas { get; }

        void RegisterGestures(IGestureDispatcher dispatcher);
        void LeaveGestures(IGestureDispatcher dispatcher);
    }
}
