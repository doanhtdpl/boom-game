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

        protected Dictionary<BomberEntity, Boolean> bomberEntities = new Dictionary<BomberEntity, Boolean>();

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
            get { return (LogicalObj as BombLogical).Bound; }
        }

        public void Collision(ICollidable obj)
        {
            if (obj is BombEntity || obj is BomberEntity)
            {
                this.collidableList.Add(obj);
            }
        }

        public void ApplyCollision()
        {
            for (var i = 0; i < this.collidableList.Count; ++i)
            {
                if (this.collidableList[i] is BombEntity)
                {
                    this.collisionWithBomb(this.collidableList[i] as BombEntity);
                }
                else if (this.collidableList[i] is BomberEntity)
                {
                    this.collisionWithBomber(this.collidableList[i] as BomberEntity);
                }
            }
            collidableList.Clear();

            List<BomberEntity> bombers = bomberEntities.Keys.ToList<BomberEntity>();
            for (int i = 0; i < bombers.Count; ++i)
            {
                if (!bombers[i].Bound.Intersects(this.Bound))
                {
                    bomberEntities[bombers[i]] = true;
                }
            }
        }

        private void collisionWithBomb(BombEntity bomber)
        {
            (this.LogicalObj as BombLogical).suddenlyMeetTime();
        }

        private void collisionWithBomber(BomberEntity bomber)
        {
            if (!bomberEntities.ContainsKey(bomber))
            {
                bomberEntities.Add(bomber, false);
            }
        }

        public bool IsBomberCollide(BomberEntity bomber)
        {
            if (bomberEntities.ContainsKey(bomber))
            {
                return bomberEntities[bomber];
            }
            return false;
        }
    }
}
