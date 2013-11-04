using Microsoft.Xna.Framework;
using SSCEngine.Utils.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSCEngine.Control.Clipping
{
    public class RectangleClipping
    {
        public bool Clip(ICanvas canvas, CRectangleF clipRect)
        {
            if (canvas.Bound.Left < clipRect.Left)
            {
                canvas.Bound.Left = clipRect.Left;
                canvas.Content.Left = clipRect.Left - canvas.Bound.Left;
            }

            if (canvas.Bound.Top < clipRect.Top)
            {
                canvas.Bound.Top = clipRect.Top;
                canvas.Content.Top = clipRect.Top - canvas.Bound.Top;
            }

            if (canvas.Bound.Right < clipRect.Right)
            {
                canvas.Bound.Right = clipRect.Right;
                canvas.Content.Right = clipRect.Right - canvas.Bound.Right;
            }

            if (canvas.Bound.Bottom < clipRect.Bottom)
            {
                canvas.Bound.Bottom = clipRect.Bottom;
                canvas.Content.Bottom = clipRect.Bottom - canvas.Bound.Bottom;
            }

            return !canvas.Bound.IsEmpty;
        }

        public CRectangleF OffsetClip(CRectangleF inRect, CRectangleF outRect)
        {
            CRectangleF intersection = new CRectangleF(inRect);
            intersection.Position -= outRect.Position;

            return outRect.Intersect(intersection);
        }
    }
}
