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
using BoomGame.Shared;

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
                            bool canCurrentLocate = true;
                            canLeft = isAvailable(ref leftPos, ref canCurrentLocate);
                            if(canCurrentLocate)
                                waterEffects.Add(WaterEffectFactory.getInst().create(new WaterEffectInfo(leftPos, (float)-Math.PI, Shared.Constants.WATEREFFECT_TIME_TO_LIVE, (Sprite)resourceManager.GetResource<ISprite>(path))));
                        }
                        if (canRight)
                        {
                            Vector2 rightPos = new Vector2(eInfo.Position.X + i * cellWidth, eInfo.Position.Y);
                            bool canCurrentLocate = true;
                            canRight = isAvailable(ref rightPos, ref canCurrentLocate);
                            if (canCurrentLocate)
                                waterEffects.Add(WaterEffectFactory.getInst().create(new WaterEffectInfo(rightPos, 0f, Shared.Constants.WATEREFFECT_TIME_TO_LIVE, (Sprite)resourceManager.GetResource<ISprite>(path))));
                        }
                        if (canTop)
                        {
                            Vector2 topPos = new Vector2(eInfo.Position.X, eInfo.Position.Y - i * cellHeight);
                            bool canCurrentLocate = true;
                            canTop = isAvailable(ref topPos, ref canCurrentLocate);
                            if (canCurrentLocate)
                                waterEffects.Add(WaterEffectFactory.getInst().create(new WaterEffectInfo(topPos, (float)(-Math.PI / 2), Shared.Constants.WATEREFFECT_TIME_TO_LIVE, (Sprite)resourceManager.GetResource<ISprite>(path))));
                        }
                        if (canBottom)
                        {
                            Vector2 downPos = new Vector2(eInfo.Position.X, eInfo.Position.Y + i * cellHeight);
                            bool canCurrentLocate = true;
                            canBottom = isAvailable(ref downPos, ref canCurrentLocate);
                            if (canCurrentLocate)
                                waterEffects.Add(WaterEffectFactory.getInst().create(new WaterEffectInfo(downPos, (float)(Math.PI / 2), Shared.Constants.WATEREFFECT_TIME_TO_LIVE, (Sprite)resourceManager.GetResource<ISprite>(path))));
                        }
                    }
                    else
                    {
                        waterEffects.Add(WaterEffectFactory.getInst().create(new WaterEffectInfo(eInfo.Position, 0f, Shared.Constants.WATEREFFECT_TIME_TO_LIVE, (Sprite)resourceManager.GetResource<ISprite>(eInfo.CenterImage))));
                    }
                }
            }
            if (waterEffects.Count != 0)
            {
                Global.PlaySoundEffect(Shared.Resources.Sound_Explode);
            }

            return waterEffects;
        }

        private bool isAvailable(ref Vector2 position, ref bool currentLocate)
        {
            Vector2 cellSize = Grid.Grid.getInst().CellSize;
            Vector2 pos = new Vector2(position.X + cellSize.X / 2, position.Y + cellSize.Y / 2);
            Cell cell = Grid.Grid.getInst().GetCellAtPosition(pos);

            bool canLocate = true;
            currentLocate = true;
            if(cell != null)
            {
                position = cell.Position;
                for (int i = 0; canLocate && i < cell.Contents.Count; ++i)
                {
                    if (cell.Contents[i] is ObstacleEntity)
                    {
                        canLocate = false;
                        if ((cell.Contents[i] as ObstacleEntity).IsStatic)
                        {
                            currentLocate = false;
                        }
                    }
                }
            }
            else
                canLocate = false;

            return canLocate;
        }
    }
}
