using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoomGame.Entity.Item;
using Microsoft.Xna.Framework;
using BoomGame.FactoryElement;
using BoomGame.Entity;
using SCSEngine;
using SCSEngine.Sprite.Implements;
using SCSEngine.ResourceManagement;
using SCSEngine.Sprite;
using BoomGame.Entity.Renderer;

namespace BoomGame.Factory
{
    public class ItemFactory : IFactory<ItemEntity>
    {
        private static ItemFactory inst = null;
        private static Game game;

        public static ItemFactory getInst()
        {
            if (inst == null)
            {
                inst = new ItemFactory();
            }
            return inst;
        }

        public static void setGame(Game game)
        {
            ItemFactory.game = game;
        }

        private ItemFactory()
        {
        }

        public ItemEntity create(object info)
        {
            ItemEntity item = null;
            if (info is ItemInfo)
            {
                ItemInfo itemInfo = info as ItemInfo;

                IItem itemType = null;
                Sprite sprite = null;

                IResourceManager resourceManager = (IResourceManager)game.Services.GetService(typeof(IResourceManager));

                switch (itemInfo.Type)
                {
                    case Shared.Localize.ID_item_Ball:
                        itemType = new ItemBomb();
                        sprite = (Sprite)resourceManager.GetResource<ISprite>(Shared.Resources.ItemBall);
                        break;

                    case Shared.Localize.ID_item_Bottle:
                        itemType = new ItemBottle();
                        sprite = (Sprite)resourceManager.GetResource<ISprite>(Shared.Resources.ItemPower);
                        break;

                    case Shared.Localize.ID_item_Coin:
                        itemType = new ItemCoin();
                        sprite = (Sprite)resourceManager.GetResource<ISprite>(Shared.Resources.ItemCoin_1);
                        break;

                    case Shared.Localize.ID_item_Wheel:
                        itemType = new ItemWheel();
                        sprite = (Sprite)resourceManager.GetResource<ISprite>(Shared.Resources.ItemWheel);
                        break;
                }

                if (itemType != null)
                {
                    item = new ItemEntity(game);
                    (item.RendererObj as ItemRenderer).Sprite = sprite;
                    (item.RendererObj as ItemRenderer).Position = itemInfo.Position;
                }
            }

            return item;
        }
    }
}
