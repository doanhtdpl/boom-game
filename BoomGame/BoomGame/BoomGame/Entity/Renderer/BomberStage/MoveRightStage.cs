using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoomGame.Entity.Renderer.BomberStage
{
    public class MoveRightStage : IStage
    {
        private static MoveRightStage inst;

        public static MoveRightStage getInstance()
        {
            if (inst == null)
            {
                inst = new MoveRightStage();
            }
            return inst;
        }

        private MoveRightStage()
        {
        }

        public void ApplyStageEffect(BomberRenderer renderer)
        {
            renderer.onChangeDirection(Shared.Constants.DIRECTION_RIGHT);
        }
    }
}
