using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoomGame.Entity.Collide;
using BoomGame.Entity.Logical;
using BoomGame.Entity.Renderer;
using Microsoft.Xna.Framework;

namespace BoomGame.Entity
{
    public class WaterEffectEntity : IGameEntity, ICollidable
    {
        protected List<ICollidable> collidableList = new List<ICollidable>();

        public Logical.DefaultLogical LogicalObj
        {
            get;
            set;
        }

        public Renderer.DefaultRenderer RendererObj
        {
            get;
            set;
        }

        public WaterEffectEntity(Game game)
        {
            this.LogicalObj = new WaterEffectLogical(game, this);
            this.RendererObj = new WaterEffectRenderer(game, this);
        }

        public void onInit()
        {
            this.RendererObj.onInit();
            this.LogicalObj.onInit();
        }

        public IGameEntity Owner
        {
            get { return this; }
        }

        public bool IsDead
        {
            get { return (this.LogicalObj as WaterEffectLogical).IsExplode; }
        }

        public List<ICollidable> CollidableList
        {
            get { return collidableList; }
        }

        public Rectangle Bound
        {
            get { return (LogicalObj as WaterEffectLogical).Bound; }
        }

        public void Collision(ICollidable obj)
        {
        }

        public void ApplyCollision()
        {
        }

        private void collisionWithObstacle(ObstacleEntity bomber)
        {
        }
    }
}
