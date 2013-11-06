using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoomGame.Entity
{
    public interface IGesturable
    {
        void GestureAffect(int gestureEvent);
    }
}
