using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoomGame.Entity;
using Microsoft.Xna.Framework;
using BoomGame.FactoryElement;
using BoomGame.Entity.Logical;
using BoomGame.Entity.Renderer;

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
            ObstacleEntity bomb = new ObstacleEntity(game);
            if (info is ObstacleInfo)
            {
                (bomb.RendererObj as ObstacleRenderer).Position = (info as ObstacleInfo).Position;
                (bomb.RendererObj as ObstacleRenderer).State = (info as ObstacleInfo).Type;
                (bomb.RendererObj as ObstacleRenderer).Sprite = (info as ObstacleInfo).Image;
            }

            return bomb;
        }
    }
}
