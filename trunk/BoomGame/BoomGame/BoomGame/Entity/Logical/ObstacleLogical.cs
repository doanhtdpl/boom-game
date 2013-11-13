using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using BoomGame.Entity.Renderer;

namespace BoomGame.Entity.Logical
{
    public class ObstacleLogical : DefaultLogical
    {
        public new Rectangle Bound
        {
            get
            {
                Vector2 position = this.Owner.RendererObj.Position;
                if (bound.Width == 0 && bound.Height == 0)
                {
                    Rectangle size = (this.Owner.RendererObj as ObstacleRenderer).Size;
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

        public ObstacleLogical(Game game, ObstacleEntity owner)
            : base(game, owner)
        {
        }
    }
}
