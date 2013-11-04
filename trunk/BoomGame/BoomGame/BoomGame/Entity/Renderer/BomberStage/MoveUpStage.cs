using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoomGame.Entity.Renderer.BomberStage
{
    public class MoveUpStage : IStage
    {
        private static MoveUpStage inst;

        public static MoveUpStage getInstance()
        {
            if (inst == null)
            {
                inst = new MoveUpStage();
            }
            return inst;
        }

        private MoveUpStage()
        {
        }

        public void ApplyStageEffect(BomberRenderer renderer)
        {
            renderer.onChangeDirection(Shared.Constants.DIRECTION_UP);
        }
    }
}
