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
using System.Threading;
using BoomGame.Entity.Renderer;


namespace BoomGame.Entity.Logical
{
    public class WaterEffectLogical : DefaultLogical
    {
        private bool isExplode = false;
        public bool IsExplode
        {
            get { return isExplode; }
        }

        public float Time
        {
            get;
            set;
        }

        public int Range
        {
            get;
            set;
        }

        public new Rectangle Bound
        {
            get
            {
                Vector2 position = this.Owner.RendererObj.Position;
                if (bound.Width == 0 && bound.Height == 0)
                {
                    Rectangle size = (this.Owner.RendererObj as WaterEffectRenderer).Size;
                    bound = new Rectangle((int)position.X, (int)position.Y, size.Width, size.Height);
                }
                else
                {
                    bound.X = (int)position.X;
                    bound.Y = (int)position.Y;
                }
                return bound;
            }
        }

        public WaterEffectLogical(Game game, WaterEffectEntity owner)
            : base(game, owner)
        {
        }

        public override void Update(GameTime gameTime)
        {
            if(Time > 0)
            {
                Time -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            else if (Time < 0)
            {
                onMeetTime();
            }

            base.Update(gameTime);
        }

        private void onMeetTime()
        {
            this.isExplode = true;
        }
    }
}
