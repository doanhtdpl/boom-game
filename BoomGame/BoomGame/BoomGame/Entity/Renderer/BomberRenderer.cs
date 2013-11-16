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
using BoomGame.Factory;
using BoomGame.FactoryElement;
using BoomGame.Shared;
using BoomGame.Scene;

namespace BoomGame.Entity.Renderer
{
    public class BomberRenderer : DefaultRenderer
    {
        private Sprite sprCurrent;
        private Sprite sprMoveLeft;
        private Sprite sprMoveRight;
        private Sprite sprMoveUp;
        private Sprite sprMoveDown;
        private Sprite sprWrapBomb;

        protected IStage stgBomberStage;
        public IStage Stage
        {
            get { return this.stgBomberStage; }
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

        private double timeToDie = 0;
        public double TimeToDie
        {
            get { return this.timeToDie; }
            set { this.timeToDie = value; }
        }

        public BomberRenderer(Game game, IGameEntity owner)
            : base(game, owner)
        {
        }

        public override void onInit()
        {
            base.onInit();

            // Get bomber resources
            sprMoveLeft = (Sprite)resourceManagement.GetResource<ISprite>(Shared.Resources.BomberMoveLeft);
            sprMoveRight = (Sprite)resourceManagement.GetResource<ISprite>(Shared.Resources.BomberMoveRight);
            sprMoveUp = (Sprite)resourceManagement.GetResource<ISprite>(Shared.Resources.BomberMoveUp);
            sprMoveDown = (Sprite)resourceManagement.GetResource<ISprite>(Shared.Resources.BomberMoveDown);
            sprWrapBomb = (Sprite)resourceManagement.GetResource<ISprite>(Shared.Resources.BomberWrapBomb);

            // Begin with move down
            sprCurrent = sprMoveDown;

            // Begin with idle stage
            stgBomberStage = IdleStage.getInstance();
            stgBomberStage.ApplyStageEffect(this);

            velocity = new Vector2(10f, 10f);
        }

        public override void Update(GameTime gameTime)
        {
            updateMovement();
            refreshAccelerator();

            if (timeToDie != 0)
            {
                timeToDie -= gameTime.ElapsedGameTime.TotalMilliseconds;
                if (timeToDie <= 0)
                {
                    onMeetTimeToDie();
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            sprCurrent.TimeStep(gameTime);
            scsServices.SpritePlayer.Draw(sprCurrent, Position, Color.White);

            base.Draw(gameTime);
        }

        private void onMeetTimeToDie()
        {
            this.timeToDie = 0;
            (owner as BomberEntity).GonnaDie();
        }

        public void onStageChange(IStage stage)
        {
            if (stgBomberStage is WrapBombStage)
            {
                this.VelocityX *= (1f / Shared.Constants.BOMBER_VELOCITY_REDUCING);
                this.VelocityY *= (1f / Shared.Constants.BOMBER_VELOCITY_REDUCING);
            }
            stgBomberStage = stage;
            stgBomberStage.ApplyStageEffect(this);
        }

        public void onInputProcess(int type)
        {
            onChangeDirection(type);
        }

        public void wrappedBomb()
        {
            this.sprCurrent = sprWrapBomb;
        }

        public void onSetBomb()
        {
            if (!(this.stgBomberStage is WrapBombStage))
            {
                BombEntity bomb = (BombEntity)BombFactory.getInst().create(new BombInfo(this.Position, 3, 2000, 1));
                if (bomb != null)
                {
                    bomb.onInit();
                    (Global.BoomMissionManager.Current as TBGamePlayScene).GameManager.Add(bomb);
                }
            }
        }

        protected void onChangeDirection(int dir)
        {
            switch (dir)
            {
                case Shared.Constants.DIRECTION_LEFT:
                    onChangeImage(sprMoveLeft);
                    accelerator.X = -velocity.X;
                    accelerator.Y = 0f;
                    break;
                case Shared.Constants.DIRECTION_RIGHT:
                    onChangeImage(sprMoveRight);
                    accelerator.X = velocity.X;
                    accelerator.Y = 0f;
                    break;
                case Shared.Constants.DIRECTION_UP:
                    onChangeImage(sprMoveUp);
                    accelerator.X = 0f;
                    accelerator.Y = -velocity.Y;
                    break;
                case Shared.Constants.DIRECTION_DOWN:
                    onChangeImage(sprMoveDown);
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

        protected void onChangeImage(Sprite spr)
        {
            if (!(this.Stage is WrapBombStage))
            {
                this.sprCurrent = spr;
            }
        }

        public void refreshAccelerator()
        {
            this.accelerator.X = 0;
            this.accelerator.Y = 0;
        }

        public void updateMovement()
        {
            Rectangle bound = (Owner.LogicalObj as BomberLogical).Bound;

            // Out of Game Size
            float X = bound.X + Accelerator.X;
            float Y = bound.Y + Accelerator.Y;

            float tmpAceleratorX = Accelerator.X;
            float tmpAceleratorY = Accelerator.Y;
            if (X < Shared.Constants.GAME_SIZE_X)
            {
                tmpAceleratorX = Shared.Constants.GAME_SIZE_X - bound.X;
            }
            else if(Y < Shared.Constants.GAME_SIZE_Y)
            {
                tmpAceleratorY = Shared.Constants.GAME_SIZE_Y - bound.Y;
            }
            else if(X + bound.Width > Shared.Constants.GAME_SIZE_X + Shared.Constants.GAME_SIZE_WIDTH)
            {
                tmpAceleratorX = Shared.Constants.GAME_SIZE_X + Shared.Constants.GAME_SIZE_WIDTH - bound.X - bound.Width;
            }
            else if (Y + bound.Height > Shared.Constants.GAME_SIZE_Y + Shared.Constants.GAME_SIZE_HEIGHT)
            {
                tmpAceleratorY = Shared.Constants.GAME_SIZE_Y + Shared.Constants.GAME_SIZE_HEIGHT- bound.Y - bound.Height;
            }

            this.position.X += tmpAceleratorX;
            this.position.Y += tmpAceleratorY;

        }
    }
}
