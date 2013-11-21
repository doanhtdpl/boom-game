using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoomGame.Entity.Renderer;

namespace BoomGame.Entity.Item
{
    public class ItemBottle : IItem
    {
        public ItemBottle()
        {
        }

        public void affect(IGameEntity entity)
        {
            (entity.RendererObj as BomberRenderer).Range++;
        }
    }
}
