using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoomGame.Entity.Renderer.BomberStage
{
    public class IdleStage : IStage
    {
        private static IdleStage inst;

        public static IdleStage getInstance()
        {
            if (inst == null)
            {
                inst = new IdleStage();
            }
            return inst;
        }

        private IdleStage()
        {
        }

        public void ApplyStageEffect(BomberRenderer renderer)
        {

        }
    }
}
