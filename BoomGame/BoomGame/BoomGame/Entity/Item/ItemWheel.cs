using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoomGame.Entity.Renderer;

namespace BoomGame.Entity.Item
{
    public class ItemWheel : IItem
    {
        public ItemWheel()
        {
        }

        public void affect(IGameEntity entity)
        {
            (entity.RendererObj as BomberRenderer).VelocityX /= Shared.Constants.BOMBER_VELOCITY_INCREASING;
            (entity.RendererObj as BomberRenderer).VelocityY /= Shared.Constants.BOMBER_VELOCITY_INCREASING;
        }
    }
}
