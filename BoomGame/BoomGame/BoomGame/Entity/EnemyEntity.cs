using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoomGame.Entity.Collide;
using Microsoft.Xna.Framework;
using BoomGame.Entity.Logical;
using BoomGame.Entity.Renderer;
using System.Diagnostics;
using BoomGame.Shared;

namespace BoomGame.Entity
{
    public class EnemyEntity : IGameEntity, ICollidable
    {
        protected bool isDead = false;
        protected List<ICollidable> collidableList = new List<ICollidable>();

        protected bool isChangeDirection = false;

        public EnemyEntity(Game game)
        {
            this.LogicalObj = new EnemyLogical(game, this);
            this.RendererObj = new EnemyRenderer(game, this);
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

        public Rectangle Bound
        {
            get { return (LogicalObj as EnemyLogical).Bound; }
        }

        public void Collision(ICollidable obj)
        {
            if (obj is ObstacleEntity || obj is WaterEffectEntity || obj is BombEntity)
            {
                this.collidableList.Add(obj);
            }
        }

        public void ApplyCollision()
        {
            for (var i = 0; i < this.collidableList.Count; ++i)
            {
                if (this.collidableList[i] is ObstacleEntity)
                {
                    this.collisionWithObstacle(this.collidableList[i] as ObstacleEntity);
                }
                else if (this.collidableList[i] is BombEntity)
                {
                    this.collisionWithBomb(this.collidableList[i] as BombEntity);
                }
                else if (this.collidableList[i] is WaterEffectEntity)
                {
                    this.collisionWithWaterEffect(collidableList[i] as WaterEffectEntity);
                }
            }
            isChangeDirection = false;
            collidableList.Clear();
        }

        private void collisionWithObstacle(ObstacleEntity obstacle)
        {
            Rectangle collisionRect = Utilities.Collision.CollisionRange(obstacle.Bound, this.Bound);
            if (collisionRect.Width >= 6 && collisionRect.Height >= 6)
            {
                if (!isChangeDirection)
                {
                    (this.RendererObj as EnemyRenderer).ChangeNegativeDirection((this.RendererObj as EnemyRenderer).direction);
                    //(this.RendererObj as EnemyRenderer).randomDirection();
                    isChangeDirection = true;
                }
            }
        }

        private void collisionWithBomb(BombEntity bomb)
        {
            //(this.RendererObj as EnemyRenderer).randomDirection();
            (this.RendererObj as EnemyRenderer).ChangeNegativeDirection((this.RendererObj as EnemyRenderer).direction);
        }

        virtual protected void collisionWithWaterEffect(WaterEffectEntity waterEffect)
        {
            this.isDead = true;
            Global.Counter_Scores += 100;
        }
    }
}
