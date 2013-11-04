using Microsoft.Xna.Framework;
using SSCEngine.GestureHandling;
using System;
using System.Collections.Generic;

namespace SSCEngine.Control
{
    public interface IUIContainer : IGestureDispatcher, IDrawable
    {
        IEnumerable<IUIControl> Components { get; }

        void Add(IUIControl control);
        void Remove(IUIControl control);
    }
}
