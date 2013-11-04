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

        protected Vector2 velocity;
        protected Vector2 accelerator;

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

        public void onChangeDirection(int dir)
        {
            switch (dir)
            {
                case Shared.Constants.DIRECTION_LEFT:
                    sprCurrent = sprMoveLeft;
                    break;
                case Shared.Constants.DIRECTION_RIGHT:
                    sprCurrent = sprMoveRight;
                    break;
                case Shared.Constants.DIRECTION_UP:
                    sprCurrent = sprMoveUp;
                    break;
                case Shared.Constants.DIRECTION_DOWN:
                    sprCurrent = sprMoveDown;
                    break;
            }
        }
    }
}
