using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SCSEngine;
using SCSEngine.Sprite.Implements;
using SCSEngine.Sprite;
using BoomGame.Entity.Logical;

namespace BoomGame.Entity.Renderer
{
    public class ObstacleRenderer : DefaultRenderer
    {
        protected Sprite sprite;
        public Sprite Sprite
        {
            set { this.sprite = value; }
        }

        protected Vector2 velocity;
        public float VelocityX
        {
            get { return this.velocity.X; }
            set { this.velocity.X = value; }
        }
        public float VelocityY
        {
            get { return this.velocity.Y; }
            set { this.velocity.Y = value; }
        }

        protected Vector2 accelerator;
        public Vector2 Accelerator
        {
            set { this.accelerator = value; }
        }

        protected int direction;
        public int Direction
        {
            get { return this.direction; }
        }

        protected int state = Shared.Constants.OBSTACLE_IDLE;
        public int State
        {
            get { return this.state; }
            set
            {
                this.state = value;
            }
        }

        public Rectangle Size
        {
            get
            {
                if (sprite != null)
                    return sprite.SpriteData.Metadata.Frames[0];
                else
                    return new Rectangle();
            }
        }

        protected Vector2 target;

        public ObstacleRenderer(Game game, ObstacleEntity owner)
            : base(game, owner)
        {
        }

        public override void onInit()
        {
            base.onInit();

            velocity = new Vector2(1f, 1f);
            accelerator = Vector2.Zero;
            target = Vector2.Zero;
        }

        public override void Update(GameTime gameTime)
        {
            if (State == Shared.Constants.OBSTACLE_CANMOVE)
            {
                isMeetTarget();
                updateMovement();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            scsServices.SpritePlayer.Draw(sprite, Position, Color.White);
            base.Draw(gameTime);
        }

        public void refreshAccelerator()
        {
            this.accelerator.X = 0;
            this.accelerator.Y = 0;
        }

        public void updateMovement()
        {
            Rectangle bound = (Owner.LogicalObj as ObstacleLogical).Bound;

            // Out of Game Size
            float X = bound.X + accelerator.X;
            float Y = bound.Y + accelerator.Y;
            if (X < Shared.Constants.GAME_SIZE_X ||
                Y < Shared.Constants.GAME_SIZE_Y ||
                X + bound.Width > Shared.Constants.GAME_SIZE_X + Shared.Constants.GAME_SIZE_WIDTH ||
                Y + bound.Height > Shared.Constants.GAME_SIZE_Y + Shared.Constants.GAME_SIZE_HEIGHT)
            {
                return;
            }

            this.position.X += this.accelerator.X;
            this.position.Y += this.accelerator.Y;
        }

        public void Move(int dir)
        {
            target = Position;

            switch (dir)
            {
                case Shared.Constants.DIRECTION_LEFT:
                    {
                        this.setAccelerator(-this.velocity.X, 0);
                        this.target.X -= Shared.Constants.OBSTACLE_MOVE_DISTANCE;
                        break;
                    }
                case Shared.Constants.DIRECTION_RIGHT:
                    {
                        this.setAccelerator(this.velocity.X, 0);
                        this.target.X += Shared.Constants.OBSTACLE_MOVE_DISTANCE;
                        break;
                    }
                case Shared.Constants.DIRECTION_UP:
                    {
                        this.setAccelerator(0, -this.velocity.Y);
                        this.target.Y -= Shared.Constants.OBSTACLE_MOVE_DISTANCE;
                        break;
                    }
                case Shared.Constants.DIRECTION_DOWN:
                    {
                        this.setAccelerator(0, this.velocity.Y);
                        this.target.Y += Shared.Constants.OBSTACLE_MOVE_DISTANCE;
                        break;
                    }
                default:
                    {
                        refreshAccelerator();
                        break;
                    }
            }
            this.direction = dir;

            Rectangle bound = (Owner.LogicalObj as ObstacleLogical).Bound;

            // Out of Game Size
            float X = bound.X + accelerator.X;
            float Y = bound.Y + accelerator.Y;
            if (X < Shared.Constants.GAME_SIZE_X ||
                Y < Shared.Constants.GAME_SIZE_Y ||
                X + bound.Width > Shared.Constants.GAME_SIZE_X + Shared.Constants.GAME_SIZE_WIDTH ||
                Y + bound.Height > Shared.Constants.GAME_SIZE_Y + Shared.Constants.GAME_SIZE_HEIGHT)
            {
                clearMovement();
            }
        }

        protected bool isMeetTarget()
        {
            switch(this.direction)
            {
            case Shared.Constants.DIRECTION_LEFT:
                if(Position.X < this.target.X)
                {
                    this.clearMovement();
                }
                break;
            case Shared.Constants.DIRECTION_RIGHT:
                if (Position.X > this.target.X)
                {
                    this.clearMovement();
                }
                break;
            case Shared.Constants.DIRECTION_UP:
                if (Position.Y < this.target.Y)
                {
                    this.clearMovement();
                }
                break;
            case Shared.Constants.DIRECTION_DOWN:
                if (Position.Y > this.target.Y)
                {
                    this.clearMovement();
                }
                break;
            }

            return false;
        }

        public void clearMovement()
        {
            this.refreshAccelerator();
            this.direction = Shared.Constants.DIRECTION_NONE;
            this.target = this.Position;
        }

        public void roolBack()
        {
            this.position.X -= this.accelerator.X;
            this.position.Y -= this.accelerator.Y;
        }

        protected void setAccelerator(float x, float y)
        {
            this.accelerator.X = x;
            this.accelerator.Y = y;
        }
    }
}
