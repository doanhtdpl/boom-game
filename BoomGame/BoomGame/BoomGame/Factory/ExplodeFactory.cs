using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoomGame.Entity;
using Microsoft.Xna.Framework;
using BoomGame.FactoryElement;
using SCSEngine.ResourceManagement;
using SCSEngine.Sprite;
using SCSEngine.Sprite.Implements;
using BoomGame.Grid;

namespace BoomGame.Factory
{
    public class ExplodeFactory : IFactory<List<WaterEffectEntity>>
    {
        private static ExplodeFactory inst = null;
        private static Game game;
        private static IResourceManager resourceManager;

        public static ExplodeFactory getInst()
        {
            if (inst == null)
            {
                inst = new ExplodeFactory();
            }
            return inst;
        }

        public static void setGame(Game game)
        {
            ExplodeFactory.game = game;
            resourceManager = (IResourceManager)game.Services.GetService(typeof(IResourceManager));
        }

        private ExplodeFactory()
        {
        }

        public List<WaterEffectEntity> create(object info)
        {
            List<WaterEffectEntity> waterEffects = new List<WaterEffectEntity>();
            if(info is ExplodeInfo)
            {
                ExplodeInfo eInfo = info as ExplodeInfo;
                bool    canTop = true,
                        canLeft = true,
                        canRight = true,
                        canBottom = true;
                int cellWidth = (int)Grid.Grid.getInst().CellSize.X, cellHeight = (int)Grid.Grid.getInst().CellSize.Y;
                String path;
                for (int i = 0; i < eInfo.Range; ++i)
                {
                    if (i != 0)
                    {
                        if (i != eInfo.Range - 1)
                            path = eInfo.BodyImage;
                        else
                            path = eInfo.ChopImage;

                        if (canLeft)
                        {
                            Vector2 leftPos = new Vector2(eInfo.Position.X - i * cellWidth, eInfo.Position.Y);
                            canLeft = isAvailable(ref leftPos);
                            waterEffects.Add(WaterEffectFactory.getInst().create(new WaterEffectInfo(leftPos, (float)-Math.PI, 500f, (Sprite)resourceManager.GetResource<ISprite>(path))));
                        }
                        if (canRight)
                        {
                            Vector2 rightPos = new Vector2(eInfo.Position.X + i * cellWidth, eInfo.Position.Y);
                            canRight = isAvailable(ref rightPos);
                            waterEffects.Add(WaterEffectFactory.getInst().create(new WaterEffectInfo(rightPos, 0f, 500f, (Sprite)resourceManager.GetResource<ISprite>(path))));
                        }
                        if (canTop)
                        {
                            Vector2 topPos = new Vector2(eInfo.Position.X, eInfo.Position.Y - i * cellHeight);
                            canTop = isAvailable(ref topPos);
                            waterEffects.Add(WaterEffectFactory.getInst().create(new WaterEffectInfo(topPos, (float)(-Math.PI / 2), 500f, (Sprite)resourceManager.GetResource<ISprite>(path))));
                        }
                        if (canBottom)
                        {
                            Vector2 downPos = new Vector2(eInfo.Position.X, eInfo.Position.Y + i * cellHeight);
                            canBottom = isAvailable(ref downPos);
                            waterEffects.Add(WaterEffectFactory.getInst().create(new WaterEffectInfo(downPos, (float)(Math.PI / 2), 500f, (Sprite)resourceManager.GetResource<ISprite>(path))));
                        }
                    }
                    else
                    {
                        waterEffects.Add(WaterEffectFactory.getInst().create(new WaterEffectInfo(eInfo.Position, 0f, 500f, (Sprite)resourceManager.GetResource<ISprite>(eInfo.BodyImage))));
                    }
                }
            }
            return waterEffects;
        }

        private bool isAvailable(ref Vector2 position)
        {
            Vector2 cellSize = Grid.Grid.getInst().CellSize;
            Vector2 pos = new Vector2(position.X + cellSize.X / 2, position.Y + cellSize.Y / 2);
            Cell cell = Grid.Grid.getInst().GetCellAtPosition(pos);

            bool canLocate = true;
            if(cell != null)
            {
                position.X = cell.Location.Y * cell.Width;
                position.Y = cell.Location.X * cell.Height;
                for (int i = 0; canLocate && i < cell.Contents.Count; ++i)
                {
                    if (cell.Contents[i] is ObstacleEntity)
                    {
                        canLocate = false;
                    }
                }
            }
            else
                canLocate = false;


            return canLocate;
        }
    }
}
