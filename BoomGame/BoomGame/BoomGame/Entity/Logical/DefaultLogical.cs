using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BoomGame.Entity.Logical
{
    public class DefaultLogical : GameComponent
    {
        protected IGameEntity owner;
        public IGameEntity Owner
        {
            get { return this.owner; }
        }

        public DefaultLogical(Game game)
            : base(game)
        {
        }

        protected Rectangle bound;
        public Rectangle Bound
        {
            get { return bound; }
        }

        public virtual void onInit()
        {
        }
    }
}
