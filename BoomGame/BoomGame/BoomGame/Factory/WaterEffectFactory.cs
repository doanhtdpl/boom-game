using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoomGame.Entity;
using Microsoft.Xna.Framework;
using BoomGame.Entity.Logical;
using BoomGame.Entity.Renderer;
using BoomGame.FactoryElement;

namespace BoomGame.Factory
{
    public class WaterEffectFactory : IFactory<WaterEffectEntity>
    {
        private static WaterEffectFactory inst = null;
        private static  Game game;

        public static WaterEffectFactory getInst()
        {
            if (inst == null)
            {
                inst = new WaterEffectFactory();
            }
            return inst;
        }

        public static void setGame(Game game)
        {
            WaterEffectFactory.game = game;
        }

        private WaterEffectFactory()
        {
        }

        public WaterEffectEntity create(object info)
        {
            WaterEffectEntity waterEffect = null;
            if (info is WaterEffectInfo)
            {
                waterEffect = new WaterEffectEntity(game);
                WaterEffectLogical logical = waterEffect.LogicalObj as WaterEffectLogical;
                WaterEffectRenderer renderer = waterEffect.RendererObj as WaterEffectRenderer;
                WaterEffectInfo wefInfo = info as WaterEffectInfo;

                renderer.Position = wefInfo.Position;
                renderer.Rotation = wefInfo.Rotation;
                renderer.Sprite = wefInfo.Image;
                logical.Time = wefInfo.Delay;
            }

            return waterEffect;
        }
    }
}
