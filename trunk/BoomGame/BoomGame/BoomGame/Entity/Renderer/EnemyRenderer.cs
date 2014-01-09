using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SCSEngine;
using SCSEngine.Sprite.Implements;
using SCSEngine.Sprite;
using BoomGame.Entity.Renderer.BomberStage;
using BoomGame.Entity.Logical;
using SCSEngine.Mathematics;
using System.Diagnostics;

namespace BoomGame.Entity.Renderer
{
    public class EnemyRenderer : DefaultRenderer
    {
        protected Sprite sprCurrent;
        protected Sprite sprMoveLeft;
        protected Sprite sprMoveRight;
        protected Sprite sprMoveUp;
        protected Sprite sprMoveDown;

        protected Vector2 velocity;
        public Vector2 Velocity
        {
            get { return this.velocity; }
            set { this.velocity = value; }
        }
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

        public int direction = Shared.Constants.DIRECTION_NONE;
        public int oldDirection = Shared.Constants.DIRECTION_NONE;

        protected bool isAutoRandom = true;

        protected Vector2 accelerator;
        public Vector2 Accelerator
        {
            get { return this.accelerator; }
            set { this.accelerator = value; }
        }

        public Rectangle Size
        {
            get
            {
                if (sprCurrent != null)
                    return sprCurrent.SpriteData.Metadata.Frames[0];
                else
                    return new Rectangle();
            }
        }

        private double timeCounter = 0;

        public EnemyRenderer(Game game, IGameEntity owner)
            : base(game, owner)
        {
        }

        public override void onInit()
        {
            base.onInit();

            // Get bomber resources
            sprMoveLeft = (Sprite)resourceManagement.GetResource<ISprite>(Shared.Resources.EnemyMoveLeft);
            sprMoveRight = (Sprite)resourceManagement.GetResource<ISprite>(Shared.Resources.EnemyMoveRight);
            sprMoveUp = (Sprite)resourceManagement.GetResource<ISprite>(Shared.Resources.EnemyMoveUp);
            sprMoveDown = (Sprite)resourceManagement.GetResource<ISprite>(Shared.Resources.EnemyMoveDown);

            // Begin with move down
            sprCurrent = sprMoveDown;

            onChangeDirection(Shared.Constants.DIRECTION_RIGHT);
        }

        public override void Update(GameTime gameTime)
        {
            if (isAutoRandom)
            {
                timeCounter += gameTime.ElapsedGameTime.TotalSeconds;
                if (timeCounter > 5)
                {
                    randomDirection();
                    timeCounter = 0;
                }
            }

            updateMovement();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            sprCurrent.TimeStep(gameTime);
            scsServices.SpritePlayer.Draw(sprCurrent, Position, Color.White);

            base.Draw(gameTime);
        }

        protected void onChangeDirection(int dir)
        {
            switch (dir)
            {
                case Shared.Constants.DIRECTION_LEFT:
                    sprCurrent = sprMoveLeft;
                    accelerator.X = -velocity.X;
                    accelerator.Y = 0f;
                    break;
                case Shared.Constants.DIRECTION_RIGHT:
                    sprCurrent = sprMoveRight;
                    accelerator.X = velocity.X;
                    accelerator.Y = 0f;
                    break;
                case Shared.Constants.DIRECTION_UP:
                    sprCurrent = sprMoveUp;
                    accelerator.X = 0f;
                    accelerator.Y = -velocity.Y;
                    break;
                case Shared.Constants.DIRECTION_DOWN:
                    sprCurrent = sprMoveDown;
                    accelerator.X = 0f;
                    accelerator.Y = velocity.Y;
                    break;
            }
            sprCurrent.Play();

            if (this.oldDirection != this.direction)
            {
                this.oldDirection = this.direction;
            }
            this.direction = dir;
        }

        public void ChangeNegativeDirection(int dir)
        {
            switch (dir)
            {
                case Shared.Constants.DIRECTION_LEFT:
                    onChangeDirection(Shared.Constants.DIRECTION_RIGHT);
                    break;
                case Shared.Constants.DIRECTION_RIGHT:
                    onChangeDirection(Shared.Constants.DIRECTION_LEFT);
                    break;
                case Shared.Constants.DIRECTION_UP:
                    onChangeDirection(Shared.Constants.DIRECTION_DOWN);
                    break;
                case Shared.Constants.DIRECTION_DOWN:
                    onChangeDirection(Shared.Constants.DIRECTION_UP);
                    break;
            }
        }

        public void randomDirection()
        {
            int dir = this.direction;
            while (dir == this.direction)
            {
                dir = GRandom.RandomInt(Shared.Constants.DIRECTION_LEFT, Shared.Constants.DIRECTION_DOWN + 1);
            }
            onChangeDirection(dir);
        }

        public void refreshAccelerator()
        {
            this.accelerator.X = 0;
            this.accelerator.Y = 0;
        }

        public void updateMovement()
        {
            // Out of Game Size
            while(this.isOutScreen())
            {
                // Random direction
                randomDirection();
            }

            this.position.X += this.Accelerator.X;
            this.position.Y += this.Accelerator.Y;
        }

        private bool isOutScreen()
        {
            Rectangle bound = (Owner.LogicalObj as EnemyLogical).Bound;

            float X = bound.X + Accelerator.X;
            float Y = bound.Y + Accelerator.Y;
            return (X < Shared.Constants.GAME_SIZE_X ||
                Y < Shared.Constants.GAME_SIZE_Y ||
                X + bound.Width > Shared.Constants.GAME_SIZE_X + Shared.Constants.GAME_SIZE_WIDTH ||
                Y + bound.Height > Shared.Constants.GAME_SIZE_Y + Shared.Constants.GAME_SIZE_HEIGHT);
        }
    }
}
