using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SCSEngine.Utils.GameObject.MPB
{
    public interface IProcessor<E>
    {
        void Update(E e, GameTime gameTime);
    }
}