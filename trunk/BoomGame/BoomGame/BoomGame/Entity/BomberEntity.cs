using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoomGame.Entity.Collide;
using Microsoft.Xna.Framework;
using BoomGame.Entity.Logical;
using BoomGame.Entity.Renderer;
using BoomGame.Entity.Renderer.BomberStage;

namespace BoomGame.Entity
{
    public class BomberEntity : IGameEntity, ICollidable, IGesturable
    {
        protected bool isDead = false;
        protected List<ICollidable> collidableList = new List<ICollidable>();

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

        public Rectangle Bound
        {
            get { return LogicalObj.Bound; }
        }

        public void Collision(ICollidable obj)
        {
            if (obj is BomberEntity || obj is ObstacleEntity || obj is EnemyEntity || obj is BombEntity)
            {
                this.collidableList.Add(obj);
            }
            else if(obj is WaterEffectEntity)
            {
                Rectangle collisionRect = Utilities.Collision.CollisionRange(this.Bound, obj.Bound);
                if (collisionRect.Width >= Shared.Constants.COLLISION_MIN && collisionRect.Height >= Shared.Constants.COLLISION_MIN)
                {
                    this.collidableList.Add(obj);
                }
            }
        }

        public void ApplyCollision()
        {
            for(var i = 0; i < this.collidableList.Count; ++i)
            {
                if(this.collidableList[i] is ObstacleEntity)
                {
                    this.collisionWithObstacle(this.collidableList[i] as ObstacleEntity);
                }
                else if (this.collidableList[i] is BombEntity)
                {
                    this.collisionWithBomb(this.collidableList[i] as BombEntity);
                }
                else if (this.collidableList[i] is EnemyEntity)
                {
                    this.collisionWithEnemy(this.collidableList[i] as EnemyEntity);
                }
                else if (this.collidableList[i] is WaterEffectEntity)
                {
                    this.collisionWithWaterEffect(this.collidableList[i] as WaterEffectEntity);
                }
                else if (this.collidableList[i] is BomberEntity)
                {
                    this.collisionWithBomber(this.collidableList[i] as BomberEntity);
                }
            }
            collidableList.Clear();
        }

        private void collisionWithObstacle(ObstacleEntity obstacle)
        {
            BomberRenderer renderer = RendererObj as BomberRenderer;

            #region Collision With Movement Obstacle
            // Get obstacle move if it can move
            ObstacleRenderer obsRenderer = (obstacle.RendererObj as ObstacleRenderer);
            if(obsRenderer.State == Shared.Constants.OBSTACLE_CANMOVE && obsRenderer.Direction == Shared.Constants.DIRECTION_NONE
                && (Math.Abs(renderer.Position.X - obsRenderer.Position.X) >= 20 || Math.Abs(renderer.Position.Y - obsRenderer.Position.Y) >= 20))
            {
                obsRenderer.Move(renderer.direction);
            }
            #endregion

            #region Replace Position
            // Replace bomber position
            Vector2 newPos = renderer.Position;
            switch (renderer.direction)
            {
                case Shared.Constants.DIRECTION_LEFT:
                    newPos.X = obsRenderer.Position.X + obstacle.LogicalObj.Bound.Width;
                    break;
                case Shared.Constants.DIRECTION_RIGHT:
                    newPos.X = obsRenderer.Position.X - LogicalObj.Bound.Width;
                    break;
                case Shared.Constants.DIRECTION_UP:
                    newPos.Y = obsRenderer.Position.Y + obstacle.LogicalObj.Bound.Height;
                    break;
                case Shared.Constants.DIRECTION_DOWN:
                    newPos.Y = obsRenderer.Position.Y - LogicalObj.Bound.Height;
                    break;
            }
            #endregion

            #region Adjust Position
//             if (renderer.direction == Shared.Constants.DIRECTION_DOWN || renderer.direction == Shared.Constants.DIRECTION_UP)
//             {
//                 if (Math.Abs(obsRenderer.Position.X - (renderer.Position.X + LogicalObj.Bound.Width)) <= Shared.Constants.COLLISION_MIN)
//                 {
//                     newPos.X = obsRenderer.Position.X - LogicalObj.Bound.Width;
//                 }
//                 else if (Math.Abs((obsRenderer.Position.X + obstacle.LogicalObj.Bound.Width) - renderer.Position.X) <= Shared.Constants.COLLISION_MIN)
//                 {
//                     newPos.X = obsRenderer.Position.X + obstacle.LogicalObj.Bound.Width;
//                 }
//             }
//             else if (renderer.direction == Shared.Constants.DIRECTION_LEFT || renderer.direction == Shared.Constants.DIRECTION_RIGHT)
//             {
//                 if (Math.Abs(obsRenderer.Position.Y - (renderer.Position.Y + LogicalObj.Bound.Height)) <= Shared.Constants.COLLISION_MIN)
//                 {
//                     newPos.Y = obsRenderer.Position.Y - LogicalObj.Bound.Height;
//                 }
//                 else if (Math.Abs((obsRenderer.Position.Y + obstacle.LogicalObj.Bound.Height) - renderer.Position.Y) <= Shared.Constants.COLLISION_MIN)
//                 {
//                     newPos.Y = obsRenderer.Position.Y + obstacle.LogicalObj.Bound.Height;
//                 }
//             }
            #endregion

            renderer.Position = newPos;

            renderer.refreshAccelerator();
        }

        private void collisionWithBomb(BombEntity bomb)
        {
            BomberRenderer renderer = RendererObj as BomberRenderer;
            BombRenderer obsRenderer = (bomb.RendererObj as BombRenderer);

            // Replace bomber position
            Vector2 newPos = renderer.Position;
            switch (renderer.direction)
            {
                case Shared.Constants.DIRECTION_LEFT:
                    newPos.X = obsRenderer.Position.X + bomb.LogicalObj.Bound.Width;
                    break;
                case Shared.Constants.DIRECTION_RIGHT:
                    newPos.X = obsRenderer.Position.X;
                    break;
                case Shared.Constants.DIRECTION_UP:
                    newPos.Y = obsRenderer.Position.Y + bomb.LogicalObj.Bound.Height;
                    break;
                case Shared.Constants.DIRECTION_DOWN:
                    newPos.Y = obsRenderer.Position.Y;
                    break;
            }
            renderer.Position = newPos;

            renderer.refreshAccelerator();
        }

        private void collisionWithEnemy(EnemyEntity enemy)
        {
            this.isDead = true;
        }

        private void collisionWithWaterEffect(WaterEffectEntity waterEffect)
        {
            if (!((this.RendererObj as BomberRenderer).Stage is WrapBombStage))
            {
                (this.RendererObj as BomberRenderer).onStageChange(WrapBombStage.getInstance());
            }
        }

        private void collisionWithBomber(BomberEntity bomber)
        {
            if ((this.RendererObj as BomberRenderer).Stage is WrapBombStage)
            {
                if ((bomber.LogicalObj as BomberLogical).Team == (this.LogicalObj as BomberLogical).Team)
                {
                    (this.RendererObj as BomberRenderer).onStageChange(IdleStage.getInstance());
                    (this.RendererObj as BomberRenderer).TimeToDie = 0;
                }
                else
                {
                    this.isDead = true;
                }
            }
        }

        public void GestureAffect(int gestureEvent)
        {
            if (gestureEvent == Shared.Constants.BUTTON_EVENT_SPACE)
            {
                (RendererObj as BomberRenderer).onSetBomb();
            }
            else 
                (RendererObj as BomberRenderer).onInputProcess(gestureEvent);
        }

        public void GonnaDie()
        {
            this.isDead = true;
        }
    }
}