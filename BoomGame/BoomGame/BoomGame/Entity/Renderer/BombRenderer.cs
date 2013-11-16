using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SCSEngine;
using SCSEngine.Sprite.Implements;
using SCSEngine.Sprite;


namespace BoomGame.Entity.Renderer
{
    public class BombRenderer : DefaultRenderer
    {
        private Sprite sprite;

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

        public BombRenderer(Game game, BombEntity owner)
            : base(game, owner)
        {
        }

        public override void onInit()
        {
            base.onInit();

            // Get bomb image animation
            this.sprite = (Sprite)resourceManagement.GetResource<ISprite>(Shared.Resources.Bomb);
            this.sprite.Play();
        }

        public override void Draw(GameTime gameTime)
        {
            sprite.TimeStep(gameTime);
            this.scsServices.SpritePlayer.Draw(sprite, Position, Color.White);
            base.Draw(gameTime);
        }
    }
}
