using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoomGame.Entity.Collide;
using Microsoft.Xna.Framework;
using BoomGame.Entity.Logical;
using BoomGame.Entity.Renderer;
using BoomGame.Shared;

namespace BoomGame.Entity
{
    public class ObstacleEntity : IGameEntity, ICollidable
    {
        protected bool isDead = false;
        protected List<ICollidable> collidableList = new List<ICollidable>();

        public bool IsStatic
        {
            get;
            set;
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

        public ObstacleEntity(Game game)
        {
            LogicalObj = new ObstacleLogical(game, this);
            RendererObj = new ObstacleRenderer(game, this);
        }

        public void onInit()
        {
            LogicalObj.onInit();
            RendererObj.onInit();
        }

        public IGameEntity Owner
        {
            get { return this; }
        }

        public bool IsDead
        {
            get { return (isDead && (LogicalObj as ObstacleLogical).TimeToDie <= 0); }
        }

        public List<ICollidable> CollidableList
        {
            get { return collidableList; }
        }

        public Rectangle Bound
        {
            get { return (LogicalObj as ObstacleLogical).Bound; }
        }

        public void Collision(ICollidable obj)
        {
            if (obj is WaterEffectEntity)
            {
                Rectangle collisionRect = Utilities.Collision.CollisionRange(this.Bound, obj.Bound);
                if (collisionRect.Width >= Shared.Constants.COLLISION_MIN && collisionRect.Height >= Shared.Constants.COLLISION_MIN)
                {
                    collidableList.Add(obj);
                }
            }
            else if (obj is ObstacleEntity)
                collidableList.Add(obj);
        }

        public void ApplyCollision()
        {
            for (int i = 0; i < collidableList.Count; ++i)
            {
                if (!IsStatic && collidableList[i] is WaterEffectEntity)
                {
                    collisionWithWaterEffect(collidableList[i] as WaterEffectEntity);
                }
                else if (collidableList[i] is ObstacleEntity)
                {
                    collisionWithObstacle(collidableList[i] as ObstacleEntity);
                }
            }

            collidableList.Clear();
        }

        private void collisionWithWaterEffect(WaterEffectEntity wef)
        {
            if (!this.isDead)
            {
                (LogicalObj as ObstacleLogical).TimeToDie = Shared.Constants.OBSTACLE_TIME_TO_DIE;

                // Play sound break obstacle
                Global.PlaySoundEffect(Shared.Resources.Sound_Obstacle_Break);
            }
        }

        private void collisionWithObstacle(ObstacleEntity entity)
        {
            if ((this.RendererObj as ObstacleRenderer).State == Shared.Constants.OBSTACLE_CANMOVE)
            {
                ObstacleRenderer renderer = this.RendererObj as ObstacleRenderer;
                switch (renderer.Direction)
                {
                    case Shared.Constants.DIRECTION_LEFT:
                        if (entity.RendererObj.Position.X < this.RendererObj.Position.X)
                        {
                            renderer.rollBack();
                            renderer.clearMovement();
                        }
                        break;
                    case Shared.Constants.DIRECTION_RIGHT:
                        if (entity.RendererObj.Position.X > this.RendererObj.Position.X)
                        {
                            renderer.rollBack();
                            renderer.clearMovement();
                        }
                        break;
                    case Shared.Constants.DIRECTION_UP:
                        if (entity.RendererObj.Position.Y < this.RendererObj.Position.Y)
                        {
                            renderer.rollBack();
                            renderer.clearMovement();
                        }
                        break;
                    case Shared.Constants.DIRECTION_DOWN:
                        if (entity.RendererObj.Position.Y > this.RendererObj.Position.Y)
                        {
                            renderer.rollBack();
                            renderer.clearMovement();
                        }
                        break;
                }
            }
        }

        public void GonnaDie()
        {
            this.isDead = true;
        }
    }
}