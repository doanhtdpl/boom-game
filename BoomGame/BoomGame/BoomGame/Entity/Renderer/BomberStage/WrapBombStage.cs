using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoomGame.Entity.Renderer.BomberStage
{
    public class WrapBombStage : IStage
    {
        private static WrapBombStage inst;

        public static WrapBombStage getInstance()
        {
            if (inst == null)
            {
                inst = new WrapBombStage();
            }
            return inst;
        }

        private WrapBombStage()
        {
        }

        public void ApplyStageEffect(BomberRenderer renderer)
        {
            renderer.wrappedBomb();
            renderer.TimeToDie = Shared.Constants.BOMBER_TIME_TO_DIE;
            renderer.VelocityX *= Shared.Constants.BOMBER_VELOCITY_REDUCING;
            renderer.VelocityY *= Shared.Constants.BOMBER_VELOCITY_REDUCING;
        }
    }
}
