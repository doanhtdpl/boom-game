using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSCEngine.GestureHandling
{
    public struct TouchPositions
    {
        public TouchPositions(Vector2 current, Vector2 last, Vector2 begin)
            : this()
        {
            this.Current = current;
            this.Last = last;
            this.Begin = begin;
        }

        public Vector2 Current { get; internal set; }
        public Vector2 Last { get; internal set; }
        public Vector2 Begin { get; internal set; }

        public Vector2 Delta { get { return Current - Last; } }
        public Vector2 TotalDelta { get { return Current - Begin; } }
    }
}
