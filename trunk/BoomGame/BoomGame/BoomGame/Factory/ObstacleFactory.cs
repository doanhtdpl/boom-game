using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoomGame.Entity;
using Microsoft.Xna.Framework;
using BoomGame.FactoryElement;
using BoomGame.Entity.Logical;
using BoomGame.Entity.Renderer;
using BoomGame.Grid;

namespace BoomGame.Factory
{
    public class ObstacleFactory : IFactory<ObstacleEntity>
    {
        private static ObstacleFactory inst = null;
        private static Game game;

        public static ObstacleFactory getInst()
        {
            if (inst == null)
            {
                inst = new ObstacleFactory();
            }
            return inst;
        }

        public static void setGame(Game game)
        {
            ObstacleFactory.game = game;
        }

        private ObstacleFactory()
        {
        }

        public ObstacleEntity create(object info)
        {
            ObstacleEntity obstacle = null;
            if (info is ObstacleInfo)
            {
                Vector2 pos = (info as ObstacleInfo).Position;

                Vector2 cellSize = Grid.Grid.getInst().CellSize;
                Vector2 position = new Vector2(pos.X + cellSize.X / 2, pos.Y + cellSize.Y / 2);
                Cell cell = Grid.Grid.getInst().GetCellAtPosition(position);

                position = cell.Position;

                bool canLocate = true;
                for (int i = 0; cell != null && canLocate && i < cell.Contents.Count; ++i)
                {
                    if (cell.Contents[i] is ObstacleEntity)
                    {
                        canLocate = false;
                    }
                }

                obstacle = new ObstacleEntity(game);
                ObstacleInfo obsInfo = (info as ObstacleInfo);
                ObstacleRenderer renderer = obstacle.RendererObj as ObstacleRenderer;

                renderer.Position = obsInfo.Position;
                renderer.State = obsInfo.Type;
                renderer.Sprite = obsInfo.Image;
                obstacle.IsStatic = obsInfo.IsStatic;
            }

            return obstacle;
        }
    }
}
