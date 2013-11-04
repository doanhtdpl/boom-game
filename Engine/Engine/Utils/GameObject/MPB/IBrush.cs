using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SCSEngine.Utils.GameObject.MPB
{
    public interface IBrush<E>
    {
        void Draw(E e, GameTime gameTime);
    }
}
