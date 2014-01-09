using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using BoomGame.Utilities;
using System.Diagnostics;
using SCSEngine.Sprite.Implements;
using SCSEngine.Sprite;

namespace BoomGame.Entity.Renderer
{
    public class ChasingEnemyRenderer : EnemyRenderer
    {
        private BomberEntity mainTarget = null;
        public BomberEntity MainTarget
        {
            set { mainTarget = value; }
        }

        private bool hasPath = false;

        private Vector2 currentTarget;
        private double currentTime = 0;
        private List<Vector2> path;
        private int currentNode = 0;
        private double pathFindingCounter = 0;

        public ChasingEnemyRenderer(Game game, IGameEntity owner)
            : base(game, owner)
        {
            isAutoRandom = false;
        }

        public override void onInit()
        {
            base.onInit();

            // Get bomber resources
            sprMoveLeft = (Sprite)resourceManagement.GetResource<ISprite>(Shared.Resources.Boss_3);
            sprMoveRight = (Sprite)resourceManagement.GetResource<ISprite>(Shared.Resources.Boss_3);
            sprMoveUp = (Sprite)resourceManagement.GetResource<ISprite>(Shared.Resources.Boss_3);
            sprMoveDown = (Sprite)resourceManagement.GetResource<ISprite>(Shared.Resources.Boss_3);

            // Begin with move down
            sprCurrent = sprMoveDown;
        }

        public override void Update(GameTime gameTime)
        {
            pathFindingCounter += gameTime.ElapsedGameTime.TotalSeconds;

            if (pathFindingCounter >= 2 && mainTarget != null)
            {
                // call to path finding
                if(!hasPath)
                {
                    path = AStar.getInst().PathFinding(new Vector2(this.Position.X + 25, this.Position.Y + 25), 
                                                    new Vector2(mainTarget.RendererObj.Position.X + 25, mainTarget.RendererObj.Position.Y + 25));
                    if (path != null && path.Count > 0)
                    {
                        currentNode = 0;
                        hasPath = true;

                        // reset time
                        pathFindingCounter = 0;

                        // next
                        nextNode();
                    }
                }
            }

            // Follow path
            if (hasPath)
            {
                if((this.direction == Shared.Constants.DIRECTION_LEFT && this.position.X < currentTarget.X) || 
                    (this.direction == Shared.Constants.DIRECTION_RIGHT && this.position.X > currentTarget.X) ||
                    (this.direction == Shared.Constants.DIRECTION_UP && this.position.Y < currentTarget.Y) ||
                    (this.direction == Shared.Constants.DIRECTION_DOWN && this.position.Y > currentTarget.Y))
                {
                    nextNode();
                }

                base.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        private void nextNode()
        {
            if (currentNode + 1 < path.Count)
            {
                // update target
                currentTarget = path[currentNode];

                // increase by one
                ++currentNode;

                // update has path
                hasPath = true;

                Vector2 distance = new Vector2();
                distance.X = Math.Abs(currentTarget.X - position.X);
                distance.Y = Math.Abs(currentTarget.Y - position.Y);

                // Make movement direction
                if (distance.X > distance.Y)
                {
                    this.onChangeDirection((currentTarget.X < this.Position.X) ? Shared.Constants.DIRECTION_LEFT : Shared.Constants.DIRECTION_RIGHT);
                }
                else
                {
                    this.onChangeDirection((currentTarget.Y < this.Position.Y) ? Shared.Constants.DIRECTION_UP : Shared.Constants.DIRECTION_DOWN);
                }
            }
            else
            {
                hasPath = false;
                currentNode = 0;
            }
        }
    }
}
