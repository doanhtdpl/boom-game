using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSCEngine.GestureHandling
{
    public interface IGestureManager : IUpdateable, IGameComponent
    {
        void AddDetector(IGestureDetector gDetector);
        void RemoveDetector(IGestureDetector gDetector);

        void AddDispatcher(IGestureDispatcher gDispatcher);
        void RemoveDispatcher(IGestureDispatcher gDispatcher);
    }
}