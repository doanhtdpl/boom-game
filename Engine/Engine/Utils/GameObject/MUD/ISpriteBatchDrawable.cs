using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SCSEngine.Utils.GameObject.MUD
{
    public interface ISpriteBatchDrawable
    {
        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}