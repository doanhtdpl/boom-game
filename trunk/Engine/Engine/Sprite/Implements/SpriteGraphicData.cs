using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SCSEngine.Sprite.Implements
{
    public class SpriteGraphicData
    {
        private Texture2D texture;
        public Texture2D Texture
        {
            get { return this.texture; }
            set { this.texture = value; }
        }

        public SpriteGraphicData(Texture2D texture)
        {
            this.texture = texture;
        }
    }
}
