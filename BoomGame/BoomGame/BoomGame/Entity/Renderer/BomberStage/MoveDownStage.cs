using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoomGame.Entity.Renderer.BomberStage
{
    public class MoveDownStage : IStage
    {
        private static MoveDownStage inst;

        public static MoveDownStage getInstance()
        {
            if (inst == null)
            {
                inst = new MoveDownStage();
            }
            return inst;
        }

        private MoveDownStage()
        {
        }

        public void ApplyStageEffect(BomberRenderer renderer)
        {
            renderer.onInputProcess(Shared.Constants.DIRECTION_DOWN);
        }
    }
}
