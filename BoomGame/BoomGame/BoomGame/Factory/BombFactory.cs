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
using BoomGame.Shared;

namespace BoomGame.Factory
{
    public class BombFactory : IFactory<BombEntity>
    {
        private static BombFactory inst = null;
        private static  Game game;

        public static BombFactory getInst()
        {
            if (inst == null)
            {
                inst = new BombFactory();
            }
            return inst;
        }

        public static void setGame(Game game)
        {
            BombFactory.game = game;
        }

        private BombFactory()
        {
        }

        public BombEntity create(object info)
        {
            BombEntity bomb = null;
            if (info is BombInfo)
            {
                Vector2 pos = (info as BombInfo).Position;

                Vector2 cellSize = Grid.Grid.getInst().CellSize;
                Vector2 position = new Vector2(pos.X + cellSize.X / 2, pos.Y + cellSize.Y / 2);

                Cell cell = Grid.Grid.getInst().GetCellAtPosition(position);

                bool canLocate = true;
                if (cell != null)
                {
                    pos.X = cell.Location.Y * cell.Width;
                    pos.Y = cell.Location.X * cell.Height;
                    for (int i = 0; canLocate && i < cell.Contents.Count; ++i)
                    {
                        if (cell.Contents[i] is BombEntity || cell.Contents[i] is ObstacleEntity)
                        {
                            canLocate = false;
                        }
                    }
                }
                else canLocate = false;

                if (canLocate)
                {
                    bomb = new BombEntity(game);
                    BombLogical logical = bomb.LogicalObj as BombLogical;
                    BombRenderer renderer = bomb.RendererObj as BombRenderer;
                    BombInfo bombInfo = info as BombInfo;

                    logical.Time = bombInfo.Time;
                    logical.Range = bombInfo.Range;
                    renderer.Position = pos;
                    renderer.BombType = bombInfo.Type;

                    cell.Add(bomb);

                    Global.Bomb_Number--;
                }
            }

            return bomb;
        }
    }
}
