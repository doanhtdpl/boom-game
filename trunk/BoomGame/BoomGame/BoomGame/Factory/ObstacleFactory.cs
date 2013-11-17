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
            ObstacleEntity bomb = null;
            if (info is ObstacleInfo)
            {
                bomb = new ObstacleEntity(game);
                ObstacleInfo obsInfo = (info as ObstacleInfo);
                ObstacleRenderer renderer = bomb.RendererObj as ObstacleRenderer;

                renderer.Position = obsInfo.Position;
                renderer.State = obsInfo.Type;
                renderer.Sprite = obsInfo.Image;
                bomb.IsStatic = obsInfo.IsStatic;
            }

            return bomb;
        }
    }
}
