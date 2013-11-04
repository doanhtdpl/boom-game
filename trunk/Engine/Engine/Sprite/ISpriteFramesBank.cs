using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SCSEngine.Sprite
{
    public interface ISpriteFramesBank
    {
        List<Rectangle> GetFrames(string spriteName);
        bool Contains(string spriteName);
    }
}
