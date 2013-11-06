using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoomGame.Entity.Renderer.BomberStage
{
    public class MoveLeftStage : IStage
    {
        private static MoveLeftStage inst;

        public static MoveLeftStage getInstance()
        {
            if(inst == null)
            {
                inst = new MoveLeftStage();
            }
            return inst;
        }

        private MoveLeftStage()
        {
        }

        public void ApplyStageEffect(BomberRenderer renderer)
        {
            renderer.onInputProcess(Shared.Constants.DIRECTION_LEFT);
        }
    }
}
