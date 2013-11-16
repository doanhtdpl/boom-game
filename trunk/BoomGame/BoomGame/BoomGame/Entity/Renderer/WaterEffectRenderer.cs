using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SCSEngine;
using SCSEngine.Sprite.Implements;
using SCSEngine.Sprite;
using Microsoft.Xna.Framework.Graphics;

namespace BoomGame.Entity.Renderer
{
    public class WaterEffectRenderer : DefaultRenderer
    {
        public Sprite Sprite
        {
            get;
            set;
        }

        public int BombType
        {
            get;
            set;
        }

        public float Rotation
        {
            get;
            set;
        }

        private Vector2 center;
        public Vector2 Center
        {
            get 
            {
                center.X = Size.Width / 2;
                center.Y = Size.Height / 2;
                return center;
            }
        }

        public Rectangle Size
        {
            get
            {
                if (Sprite != null)
                    return Sprite.SpriteData.Metadata.Frames[0];
                else
                    return new Rectangle();
            }
        }

        public WaterEffectRenderer(Game game, WaterEffectEntity owner)
            : base(game, owner)
        {
        }

        public override void onInit()
        {
            base.onInit();

            // Get bomb image animation
            center = new Vector2(Size.X/2, Size.Y/2);
        }

        public override void Draw(GameTime gameTime)
        {
            Sprite.TimeStep(gameTime);
            this.scsServices.SpritePlayer.Draw(Sprite, Position + Center, Rotation, 1.0f, Center, Color.White, SpriteEffects.None, 1.0f);
            base.Draw(gameTime);
        }
    }
}
