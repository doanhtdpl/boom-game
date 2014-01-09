using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using BoomGame.Entity.Renderer;

namespace BoomGame.Entity
{
    public class ChasingEnemyEntity : EnemyEntity
    {
        private BomberEntity target;
        public BomberEntity Target
        {
            set 
            {
                this.target = value;
                if (this.RendererObj != null)
                {
                    (this.RendererObj as ChasingEnemyRenderer).MainTarget = value;
                }
            }
        }

        protected int blood = 50;

        public ChasingEnemyEntity(Game game)
            : base(game)
        {
            this.RendererObj = new ChasingEnemyRenderer(game, this);
        }

        protected override void collisionWithWaterEffect(WaterEffectEntity waterEffect)
        {
            --blood;
            if(blood <= 0)
                base.collisionWithWaterEffect(waterEffect);
        }
    }
}
