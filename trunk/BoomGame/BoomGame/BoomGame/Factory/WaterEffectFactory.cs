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
            WaterEffectEntity waterEffect = new WaterEffectEntity(game);
            if (info is WaterEffectInfo)
            {
                (waterEffect.RendererObj as WaterEffectRenderer).Position = (info as WaterEffectInfo).Position;
                (waterEffect.RendererObj as WaterEffectRenderer).Rotation = (info as WaterEffectInfo).Rotation;
                (waterEffect.RendererObj as WaterEffectRenderer).Sprite = (info as WaterEffectInfo).Image;
                (waterEffect.LogicalObj as WaterEffectLogical).Time = (info as WaterEffectInfo).Delay;
            }

            return waterEffect;
        }
    }
}
