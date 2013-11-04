using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BoomGame.Entity.Renderer
{
    public class DefaultRenderer : DrawableGameComponent
    {
        protected IGameEntity owner;
        public IGameEntity Owner
        {
            get { return this.owner; }
        }

        public DefaultRenderer(Game game)
            :base (game)
        {
        }
    }
}
