using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using BoomGame.Entity.Renderer;
using BoomGame.Factory;
using BoomGame.FactoryElement;
using BoomGame.Scene;
using BoomGame.Shared;
using BoomGame.Entity.Item;

namespace BoomGame.Entity.Logical
{
    public class ObstacleLogical : DefaultLogical
    {
        public new Rectangle Bound
        {
            get
            {
                Vector2 position = this.Owner.RendererObj.Position;
                if (bound.Width == 0 && bound.Height == 0)
                {
                    Rectangle size = (this.Owner.RendererObj as ObstacleRenderer).Size;
                    bound = new Rectangle((int)position.X, (int)position.Y, size.Width, size.Height);
                }
                else
                {
                    bound.X = (int)position.X;
                    bound.Y = (int)position.Y;
                }
                return bound;
            }
        }

        public float TimeToDie
        {
            get;
            set;
        }

        public int ItemTypeContained
        {
            get;
            set;
        }

        public ObstacleLogical(Game game, ObstacleEntity owner)
            : base(game, owner)
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (TimeToDie > 0)
            {
                TimeToDie -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (TimeToDie <= 0)
                {
                    onMeetTimeToDie();
                }
            }

            base.Update(gameTime);
        }

        public void onMeetTimeToDie()
        {
            // Create Item
            if (ItemTypeContained > 0)
            {
                ItemEntity itemEntity = ItemFactory.getInst().create(new ItemInfo((owner.RendererObj as ObstacleRenderer).Position, ItemTypeContained));
                itemEntity.onInit();

                IItem item = null;

                switch (ItemTypeContained)
                {
                    case Shared.Localize.ID_item_Ball:
                        item = new ItemBomb();
                	    break;

                    case Shared.Localize.ID_item_Bottle:
                        item = new ItemBottle();
                        break;

                    case Shared.Localize.ID_item_Coin:
                        item = new ItemCoin();
                        break;

                    case Shared.Localize.ID_item_Wheel:
                        item = new ItemWheel();
                        break;
                }
                if (item != null)
                    itemEntity.ItemType = item;

                (Global.BoomMissionManager.Current as TBGamePlayScene).GameManager.Add(itemEntity);
            }
        }
    }
}