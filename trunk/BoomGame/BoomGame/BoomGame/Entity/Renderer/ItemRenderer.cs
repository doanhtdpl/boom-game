using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SCSEngine;
using SCSEngine.Sprite.Implements;
using SCSEngine.Sprite;

namespace BoomGame.Entity.Renderer
{
    public class ItemRenderer : DefaultRenderer
    {
        private Sprite sprite;
        public Sprite Sprite
        {
            set { this.sprite = value; }
        }

        public int BombType
        {
            get;
            set;
        }

        public Rectangle Size
        {
            get
            {
                if (sprite != null)
                    return sprite.SpriteData.Metadata.Frames[0];
                else
                    return new Rectangle();
            }
        }

        public ItemRenderer(Game game, ItemEntity owner)
            : base(game, owner)
        {
        }

        public override void onInit()
        {
            base.onInit();
        }

        public override void Draw(GameTime gameTime)
        {
            this.scsServices.SpritePlayer.Draw(sprite, Position, Color.White);
            base.Draw(gameTime);
        }
    }
}
