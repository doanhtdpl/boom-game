using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSCEngine.Control.Clipping
{
    public interface IClipRegion
    {
        IRegion FullRegion { get; }
        IRegion ClipRegion { get; set; }
    }
}