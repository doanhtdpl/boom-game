using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using BoomGame.Entity;
using BoomGame.FactoryElement;
using BoomGame.Entity.Logical;
using BoomGame.Entity.Renderer;
using BoomGame.Grid;

namespace BoomGame.Factory
{
    public class EnemyFactory : IFactory<EnemyEntity>
    {
        private static EnemyFactory inst = null;
        private static  Game game;

        public static EnemyFactory getInst()
        {
            if (inst == null)
            {
                inst = new EnemyFactory();
            }
            return inst;
        }

        public static void setGame(Game game)
        {
            EnemyFactory.game = game;
        }

        private EnemyFactory()
        {
        }

        public EnemyEntity create(object info)
        {
            EnemyEntity enemy = new EnemyEntity(game);
            if (info is EnemyInfo)
            {
                Vector2 pos = (info as EnemyInfo).Position;

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
                if (canLocate)
                {
                    enemy = new EnemyEntity(game);
                    (enemy.RendererObj as EnemyRenderer).Position = position;
                    (enemy.RendererObj as EnemyRenderer).Velocity = (info as EnemyInfo).Velocity;
                }
            }
            return enemy;
        }
    }
}
