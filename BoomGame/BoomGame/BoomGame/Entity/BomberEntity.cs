using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoomGame.Entity.Collide;
using Microsoft.Xna.Framework;
using BoomGame.Entity.Logical;
using BoomGame.Entity.Renderer;
using BoomGame.Entity.Renderer.BomberStage;
using System.Diagnostics;
using BoomGame.Grid;
using BoomGame.Shared;

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
            get { return (LogicalObj as BomberLogical).Bound; }
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
            if (obstacle.isWalkable)
                return;

            BomberRenderer renderer = RendererObj as BomberRenderer;
            BomberLogical logical = LogicalObj as BomberLogical;

            ObstacleRenderer obsRenderer = (obstacle.RendererObj as ObstacleRenderer);
            ObstacleLogical obsLogical = (obstacle.LogicalObj as ObstacleLogical);

            #region Replace Position
            Vector2 newVelocity = Vector2.Zero;

            bool rightDirection = false;

            switch (renderer.direction)
            {
                case Shared.Constants.DIRECTION_LEFT:
                    rightDirection = (obsLogical.Bound.X <= renderer.Position.X) ? true : false;
                    if (rightDirection)
                        newVelocity.X = obsLogical.Bound.X + obsLogical.Bound.Width - renderer.Position.X;
                    break;
                case Shared.Constants.DIRECTION_RIGHT:
                    rightDirection = (obsLogical.Bound.X >= renderer.Position.X) ? true : false;
                    if(rightDirection)
                        newVelocity.X =  obsLogical.Bound.X - logical.Bound.X - logical.Bound.Width;
                    break;
                case Shared.Constants.DIRECTION_UP:
                    rightDirection = (obsLogical.Bound.Y <= renderer.Position.Y) ? true : false;
                    if(rightDirection)
                        newVelocity.Y = obsLogical.Bound.Y + obsLogical.Bound.Height - renderer.Position.Y;
                    break;
                case Shared.Constants.DIRECTION_DOWN:
                    rightDirection = (obsLogical.Bound.Y >= renderer.Position.Y) ? true : false;
                    if (rightDirection)
                        newVelocity.Y = obsLogical.Bound.Y - logical.Bound.Y - logical.Bound.Height;
                    break;
            }

            if (!rightDirection)
            {
                Vector2 newPos = renderer.Position;
                switch (renderer.oldDirection)
                {
                    case Shared.Constants.DIRECTION_LEFT:
                        newPos.X = obsRenderer.Position.X + obsLogical.Bound.Width;
                        break;
                    case Shared.Constants.DIRECTION_RIGHT:
                        newPos.X = obsRenderer.Position.X - logical.Bound.Width;
                        break;
                    case Shared.Constants.DIRECTION_UP:
                        newPos.Y = obsRenderer.Position.Y + obsLogical.Bound.Height;
                        break;
                    case Shared.Constants.DIRECTION_DOWN:
                        newPos.Y = obsRenderer.Position.Y - logical.Bound.Height;
                        break;
                }
                renderer.Position = newPos;
            }
            #endregion

            #region Adjust Position

            Cell obsCell = Grid.Grid.getInst().GetCellAtPosition(obsRenderer.Position);
            if (obsCell != null)
            {
                if (renderer.direction == Shared.Constants.DIRECTION_DOWN || renderer.direction == Shared.Constants.DIRECTION_UP)
                {
                    Cell cellLeft = Grid.Grid.getInst().GetCellAtLocation(new Vector2(obsCell.Location.X, obsCell.Location.Y - 1));
                    Cell cellRight = Grid.Grid.getInst().GetCellAtLocation(new Vector2(obsCell.Location.X, obsCell.Location.Y + 1));
                    if (cellLeft != null && cellLeft.Contents.Count == 0 && 
                        Math.Abs(obsRenderer.Position.X - (renderer.Position.X + LogicalObj.Bound.Width)) <= Shared.Constants.COLLISION_MIN)
                    {
                        newVelocity.X += obsRenderer.Position.X - (renderer.Position.X + LogicalObj.Bound.Width);
                    }
                    else if (cellRight != null && cellRight.Contents.Count == 0 &&
                        Math.Abs((obsRenderer.Position.X + obstacle.LogicalObj.Bound.Width) - renderer.Position.X) <= Shared.Constants.COLLISION_MIN)
                    {
                        newVelocity.X += (obsRenderer.Position.X + obstacle.LogicalObj.Bound.Width) - renderer.Position.X;
                    }
                }
                else if (renderer.direction == Shared.Constants.DIRECTION_LEFT || renderer.direction == Shared.Constants.DIRECTION_RIGHT)
                {
                    Cell cellUp = Grid.Grid.getInst().GetCellAtLocation(new Vector2(obsCell.Location.X - 1, obsCell.Location.Y));
                    Cell cellDown = Grid.Grid.getInst().GetCellAtLocation(new Vector2(obsCell.Location.X + 1, obsCell.Location.Y));
                    if (cellUp != null && cellUp.Contents.Count == 0 && 
                        Math.Abs(obsRenderer.Position.Y - (renderer.Position.Y + LogicalObj.Bound.Height)) <= Shared.Constants.COLLISION_MIN)
                    {
                        newVelocity.Y += obsRenderer.Position.Y - (renderer.Position.Y + LogicalObj.Bound.Height);
                    }
                    else if (cellDown != null && cellDown.Contents.Count == 0 && 
                        Math.Abs((obsRenderer.Position.Y + obstacle.LogicalObj.Bound.Height) - renderer.Position.Y) <= Shared.Constants.COLLISION_MIN)
                    {
                        newVelocity.Y += (obsRenderer.Position.Y + obstacle.LogicalObj.Bound.Height) - renderer.Position.Y;
                    }
                }
            }
            #endregion

//             #region Collision With Movement Obstacle
//             // Get obstacle move if it can move
//             if (rightDirection && obsRenderer.State == Shared.Constants.OBSTACLE_CANMOVE && obsRenderer.Direction == Shared.Constants.DIRECTION_NONE 
//                 && ((Math.Abs(renderer.Position.X - obsRenderer.Position.X) <= 10 && (renderer.direction == Shared.Constants.DIRECTION_UP || renderer.direction == Shared.Constants.DIRECTION_DOWN))
//                 || (Math.Abs(renderer.Position.Y - obsRenderer.Position.Y) <= 10 && (renderer.direction == Shared.Constants.DIRECTION_LEFT || renderer.direction == Shared.Constants.DIRECTION_RIGHT))))
//             {
//                 obsRenderer.Move(renderer.direction);
//             }
//             #endregion

            if (newVelocity.Length() != 0)
            {
                renderer.MoveImmediately(newVelocity);
                renderer.updateMovement();
            }

            renderer.refreshAccelerator();
        }

        private void collisionWithBomb(BombEntity bomb)
        {
            if (!bomb.IsBomberCollide(this))
                return;

            BomberRenderer renderer = RendererObj as BomberRenderer;
            BomberLogical logical = LogicalObj as BomberLogical;

            BombRenderer obsRenderer = (bomb.RendererObj as BombRenderer);
            BombLogical obsLogical = (bomb.LogicalObj as BombLogical);

            #region Replace Position
            Vector2 newVelocity = Vector2.Zero;

            bool rightDirection = false;

            switch (renderer.direction)
            {
                case Shared.Constants.DIRECTION_LEFT:
                    rightDirection = (obsLogical.Bound.X <= renderer.Position.X) ? true : false;
                    if (rightDirection)
                        newVelocity.X = obsLogical.Bound.X + obsLogical.Bound.Width - renderer.Position.X;
                    break;
                case Shared.Constants.DIRECTION_RIGHT:
                    rightDirection = (obsLogical.Bound.X >= renderer.Position.X) ? true : false;
                    if (rightDirection)
                        newVelocity.X = obsLogical.Bound.X - logical.Bound.X - logical.Bound.Width;
                    break;
                case Shared.Constants.DIRECTION_UP:
                    rightDirection = (obsLogical.Bound.Y <= renderer.Position.Y) ? true : false;
                    if (rightDirection)
                        newVelocity.Y = obsLogical.Bound.Y + obsLogical.Bound.Height - renderer.Position.Y;
                    break;
                case Shared.Constants.DIRECTION_DOWN:
                    rightDirection = (obsLogical.Bound.Y >= renderer.Position.Y) ? true : false;
                    if (rightDirection)
                        newVelocity.Y = obsLogical.Bound.Y - logical.Bound.Y - logical.Bound.Height;
                    break;
            }

            if (!rightDirection)
            {
                Vector2 newPos = renderer.Position;
                switch (renderer.oldDirection)
                {
                    case Shared.Constants.DIRECTION_LEFT:
                        newPos.X = obsRenderer.Position.X + obsLogical.Bound.Width;
                        break;
                    case Shared.Constants.DIRECTION_RIGHT:
                        newPos.X = obsRenderer.Position.X - logical.Bound.Width;
                        break;
                    case Shared.Constants.DIRECTION_UP:
                        newPos.Y = obsRenderer.Position.Y + obsLogical.Bound.Height;
                        break;
                    case Shared.Constants.DIRECTION_DOWN:
                        newPos.Y = obsRenderer.Position.Y - logical.Bound.Height;
                        break;
                }
                renderer.Position = newPos;
            }
            #endregion

            #region Adjust Position

            Cell obsCell = Grid.Grid.getInst().GetCellAtPosition(obsRenderer.Position);
            if (obsCell != null)
            {
                if (renderer.direction == Shared.Constants.DIRECTION_DOWN || renderer.direction == Shared.Constants.DIRECTION_UP)
                {
                    Cell cellLeft = Grid.Grid.getInst().GetCellAtLocation(new Vector2(obsCell.Location.X, obsCell.Location.Y - 1));
                    Cell cellRight = Grid.Grid.getInst().GetCellAtLocation(new Vector2(obsCell.Location.X, obsCell.Location.Y + 1));
                    if (cellLeft != null && cellLeft.Contents.Count == 0 &&
                        Math.Abs(obsRenderer.Position.X - (renderer.Position.X + LogicalObj.Bound.Width)) <= Shared.Constants.COLLISION_MIN)
                    {
                        newVelocity.X += obsRenderer.Position.X - (renderer.Position.X + LogicalObj.Bound.Width);
                    }
                    else if (cellRight != null && cellRight.Contents.Count == 0 &&
                        Math.Abs((obsRenderer.Position.X + bomb.LogicalObj.Bound.Width) - renderer.Position.X) <= Shared.Constants.COLLISION_MIN)
                    {
                        newVelocity.X += (obsRenderer.Position.X + bomb.LogicalObj.Bound.Width) - renderer.Position.X;
                    }
                }
                else if (renderer.direction == Shared.Constants.DIRECTION_LEFT || renderer.direction == Shared.Constants.DIRECTION_RIGHT)
                {
                    Cell cellUp = Grid.Grid.getInst().GetCellAtLocation(new Vector2(obsCell.Location.X - 1, obsCell.Location.Y));
                    Cell cellDown = Grid.Grid.getInst().GetCellAtLocation(new Vector2(obsCell.Location.X + 1, obsCell.Location.Y));
                    if (cellUp != null && cellUp.Contents.Count == 0 &&
                        Math.Abs(obsRenderer.Position.Y - (renderer.Position.Y + LogicalObj.Bound.Height)) <= Shared.Constants.COLLISION_MIN)
                    {
                        newVelocity.Y += obsRenderer.Position.Y - (renderer.Position.Y + LogicalObj.Bound.Height);
                    }
                    else if (cellDown != null && cellDown.Contents.Count == 0 &&
                        Math.Abs((obsRenderer.Position.Y + bomb.LogicalObj.Bound.Height) - renderer.Position.Y) <= Shared.Constants.COLLISION_MIN)
                    {
                        newVelocity.Y += (obsRenderer.Position.Y + bomb.LogicalObj.Bound.Height) - renderer.Position.Y;
                    }
                }
            }
            #endregion

            if (newVelocity.Length() != 0)
            {
                renderer.MoveImmediately(newVelocity);
                renderer.updateMovement();
            }

            renderer.refreshAccelerator();
        }

        private void collisionWithEnemy(EnemyEntity enemy)
        {
            BomberRenderer renderer = RendererObj as BomberRenderer;
            BomberLogical logical = LogicalObj as BomberLogical;

            EnemyRenderer obsRenderer = (enemy.RendererObj as EnemyRenderer);
            EnemyLogical obsLogical = (enemy.LogicalObj as EnemyLogical);

            Rectangle rect = Utilities.Collision.CollisionRange(enemy.Bound, this.Bound);
            if (rect.Width >= 7 && rect.Height >= 7)
            {
                GonnaDie();
            }
        }

        private void collisionWithWaterEffect(WaterEffectEntity waterEffect)
        {
            if (!((this.RendererObj as BomberRenderer).Stage is WrapBombStage))
            {
                (this.RendererObj as BomberRenderer).onStageChange(WrapBombStage.getInstance());

                // Play sound bomber die
                Global.PlaySoundEffect(Shared.Resources.Sound_Inside_Bomb);
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
            if (!this.isDead)
            {
                this.isDead = true;

                // Play sound bomber die
                Global.PlaySoundEffect(Shared.Resources.Sound_Lose);
            }
        }
    }
}