using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoomGame.Entity.Collide;
using Microsoft.Xna.Framework;
using BoomGame.Entity.Logical;
using BoomGame.Entity.Renderer;

namespace BoomGame.Entity
{
    public class BomberEntity : IGameEntity, ICollidable
    {
        protected bool isDead = false;
        protected List<ICollidable> collidableList;

        public BomberEntity(Game game)
        {
            this.LogicalObj = new BomberLogical(game, this);
            this.RendererObj = new BomberRenderer(game, this);
        }

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
            get { return isDead; }
        }

        public List<ICollidable> CollidableList
        {
            get { return collidableList; }
        }

        public void Collision(ICollidable obj)
        {

        }

        public void ApplyCollision()
        {
//             for (int i = 0; i < CollidableList.Count; ++i)
//             {
// 
//             }
        }
    }
}
