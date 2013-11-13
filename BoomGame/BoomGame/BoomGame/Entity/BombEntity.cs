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
    public class BombEntity : IGameEntity, ICollidable
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

        public BombEntity(Game game)
        {
            this.LogicalObj = new BombLogical(game, this);
            this.RendererObj = new BombRenderer(game, this);
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
            get { return (this.LogicalObj as BombLogical).IsExplode; }
        }

        public List<ICollidable> CollidableList
        {
            get { return collidableList; }
        }

        public Rectangle Bound
        {
            get { return LogicalObj.Bound; }
        }

        public void Collision(ICollidable obj)
        {
            if (obj is BomberEntity)
            {
                this.collidableList.Add(obj);
            }
        }

        public void ApplyCollision()
        {
            for (var i = 0; i < this.collidableList.Count; ++i)
            {
                if (this.collidableList[i] is BomberEntity)
                {
                    this.collisionWithBomber((BomberEntity)this.collidableList[i]);
                }
            }
            collidableList.Clear();
        }

        private void collisionWithBomber(BomberEntity bomber)
        {
            // Prevent bomber move across
        }
    }
}
