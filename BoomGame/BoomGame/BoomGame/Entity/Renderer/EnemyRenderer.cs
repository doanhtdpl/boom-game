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

        protected bool isAutoRandom = true;

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

            randomDirection();
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
                default:
                    sprCurrent = sprMoveDown;
                    accelerator.X = 0f;
                    accelerator.Y = 0f;
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

        private bool isValidCell(float x, float y)
        {
            BoomGame.Grid.Cell c = Grid.Grid.getInst().GetCellAtLocation(x, y);
            if (c != null && c.IsWalkable)
            {
                return true;
            }
            return false;
        }

        public void randomDirection()
        {
            //ChangeNegativeDirection(this.direction);
            //             int dir = this.direction;
            //             while (dir == this.direction)
            //             {
            //                 dir = GRandom.RandomInt(Shared.Constants.DIRECTION_LEFT, Shared.Constants.DIRECTION_DOWN + 1);
            //             }
            //             onChangeDirection(dir);

            int dir = Shared.Constants.DIRECTION_NONE;
            int[] order = { Shared.Constants.DIRECTION_LEFT, Shared.Constants.DIRECTION_RIGHT, Shared.Constants.DIRECTION_UP, Shared.Constants.DIRECTION_DOWN };
            int[] list = { 0, 0, 0, 0 };

            BoomGame.Grid.Cell cell = Grid.Grid.getInst().GetCellAtPosition(this.position.X + 25, this.position.Y + 25);
            if (cell != null)
            {
                // left
                BoomGame.Grid.Cell cellLeft = Grid.Grid.getInst().GetCellAtLocation(cell.Location.X, cell.Location.Y - 1);
                if (cellLeft == null || !cellLeft.IsWalkable)
                {
                    list[0] = 4;
                }
                // right
                BoomGame.Grid.Cell cellRight = Grid.Grid.getInst().GetCellAtLocation(cell.Location.X, cell.Location.Y + 1);
                if (cellRight == null || !cellRight.IsWalkable)
                {
                    list[1] = 4;
                }
                // up
                BoomGame.Grid.Cell cellUp = Grid.Grid.getInst().GetCellAtLocation(cell.Location.X - 1, cell.Location.Y);
                if (cellUp == null || !cellUp.IsWalkable)
                {
                    list[2] = 4;
                }
                // down
                BoomGame.Grid.Cell cellDown = Grid.Grid.getInst().GetCellAtLocation(cell.Location.X + 1, cell.Location.Y);
                if (cellDown == null || !cellDown.IsWalkable)
                {
                    list[3] = 4;
                }
                for (int i = -1; i <= 1; ++i)
                {
                    if (i != 0)
                    {
                        if (cellLeft != null && !isValidCell(cellLeft.Location.X + i, cellLeft.Location.Y))
                        {
                            list[0]++;
                        }
                        if (cellRight != null && !isValidCell(cellRight.Location.X + i, cellRight.Location.Y))
                        {
                            list[1]++;
                        }
                        if (cellUp != null && !isValidCell(cellUp.Location.X, cellUp.Location.Y + i))
                        {
                            list[2]++;
                        }
                        if (cellDown != null && !isValidCell(cellDown.Location.X, cellDown.Location.Y + i))
                        {
                            list[3]++;
                        }
                    }
                }
            }

            if (list.Min() <= 3)
            {
                int min = list.Min();
                List<int> randomDir = new List<int>();
                for (int i = 0; i < 4; ++i)
                {
                    if (list[i] == min)
                        randomDir.Add(i);
                }
                dir = randomDir[GRandom.RandomInt(0, randomDir.Count)] + 1;
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
            if (this.isOutScreen())
            {
                // Random direction
                //randomDirection();
                ChangeNegativeDirection(this.direction);
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
