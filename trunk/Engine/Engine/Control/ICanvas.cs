using Microsoft.Xna.Framework;
using SSCEngine.Utils.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSCEngine.Control
{
    //public interface ICanvas
    //{
    //    RectangleF AABB { get; }
    //    IRegion Region { get; }
    //}

    public interface ICanvas
    {
        CRectangleF Bound { get; }
        CRectangleF Content { get; }
    }
}