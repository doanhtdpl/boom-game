using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SCSEngine;
using SCSEngine.Sprite.Implements;
using SCSEngine.Sprite;
using BoomGame.Entity.Renderer.BomberStage;

namespace BoomGame.Entity.Renderer
{
    public class BomberRenderer : DefaultRenderer
    {
        private Sprite sprCurrent;
        private Sprite sprMoveLeft;
        private Sprite sprMoveRight;
        private Sprite sprMoveUp;
        private Sprite sprMoveDown;

        protected IStage stgBomberStage;
        public IStage Stage
        {
            set { this.stgBomberStage = value; }
        }

        protected Vector2 velocity;

        public int direction = Shared.Constants.DIRECTION_NONE;
        public int oldDirection = Shared.Constants.DIRECTION_NONE;

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

        public BomberRenderer(Game game, IGameEntity owner)
            : base(game)
        {
            this.owner = owner;
        }

        public override void onInit()
        {
            base.onInit();

            // Get bomber resources
            sprMoveLeft = (Sprite)resourceManagement.GetResource<ISprite>(Shared.Resources.BomberMoveLeft);
            sprMoveRight = (Sprite)resourceManagement.GetResource<ISprite>(Shared.Resources.BomberMoveRight);
            sprMoveUp = (Sprite)resourceManagement.GetResource<ISprite>(Shared.Resources.BomberMoveUp);
            sprMoveDown = (Sprite)resourceManagement.GetResource<ISprite>(Shared.Resources.BomberMoveDown);

            // Begin with move down
            sprCurrent = sprMoveDown;

            // Begin with idle stage
            stgBomberStage = IdleStage.getInstance();
            stgBomberStage.ApplyStageEffect(this);

            velocity = new Vector2(5f, 5f);
        }

        public override void Update(GameTime gameTime)
        {
            updateMovement();
            refreshAccelerator();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            scsServices.SpritePlayer.Draw(sprCurrent, Position, Color.White);

            base.Draw(gameTime);
        }

        public void onStageChange(IStage stage)
        {
            stgBomberStage = stage;
            stgBomberStage.ApplyStageEffect(this);
        }

        public void onInputProcess(int type)
        {
            onChangeDirection(type);
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

        public void refreshAccelerator()
        {
            this.accelerator.X = 0;
            this.accelerator.Y = 0;
        }

        public void updateMovement()
        {
            Rectangle bound = Owner.LogicalObj.Bound;

            // Out of Game Size
            if (bound.X < Shared.Constants.GAME_SIZE_X ||
                bound.Y < Shared.Constants.GAME_SIZE_Y ||
                bound.X + bound.Width > Shared.Constants.GAME_SIZE_X + Shared.Constants.GAME_SIZE_WIDTH ||
                bound.Y + bound.Height > Shared.Constants.GAME_SIZE_Y + Shared.Constants.GAME_SIZE_HEIGHT)
            {
                return;
            }

            this.position.X += this.Accelerator.X;
            this.position.Y += this.Accelerator.Y;

        }
    }
}
