using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoomGame.Entity.Renderer
{
    public interface IStage
    {
        void ApplyStageEffect(BomberRenderer renderer);
    }
}
