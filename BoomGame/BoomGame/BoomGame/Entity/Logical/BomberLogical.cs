using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using BoomGame.Entity.Renderer;
using BoomGame.Debuger;

namespace BoomGame.Entity.Logical
{
    public class BomberLogical : DefaultLogical
    {
        protected int direction = Shared.Constants.DIRECTION_NONE;

        protected int team = 0;
        public int Team
        {
            get { return this.team; }
        }

        public Rectangle Bound
        {
            get
            {
                Vector2 position = this.Owner.RendererObj.Position;
                if(bound.Width == 0 && bound.Height == 0)
                {
                    Rectangle size = (this.Owner.RendererObj as BomberRenderer).Size;
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

        public BomberLogical(Game game, IGameEntity owner)
            : base(game)
        {
            this.owner = owner;
        }

        public override void onInit()
        {
            base.onInit();
            StringDebuger.setString(this.Bound.ToString(), new Vector2(0, 0));
        }

        public override void Update(GameTime gameTime)
        {
            // check input here

            base.Update(gameTime);
        }
    }
}
