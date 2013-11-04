using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SCSEngine.Services;
using SCSEngine.ResourceManagement;

namespace BoomGame.Entity.Renderer
{
    public class DefaultRenderer : DrawableGameComponent
    {
        protected IGameEntity owner;
        public IGameEntity Owner
        {
            get { return this.owner; }
        }

        protected SCSServices scsServices;
        protected IResourceManager resourceManagement;

        protected Vector2 position;
        public Vector2 Position
        {
            get { return this.position; }
        }

        public DefaultRenderer(Game game)
            :base (game)
        {
        }

        public virtual void onInit()
        {
            scsServices = (SCSServices)this.Game.Services.GetService(typeof(SCSServices));
            resourceManagement = (IResourceManager)this.Game.Services.GetService(typeof(IResourceManager));
        }
    }
}
