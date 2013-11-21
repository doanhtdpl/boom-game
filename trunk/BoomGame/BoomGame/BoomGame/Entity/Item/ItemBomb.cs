using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoomGame.Entity.Renderer;

namespace BoomGame.Entity.Item
{
    public class ItemBomb : IItem
    {
        public ItemBomb()
        {
        }

        public void affect(IGameEntity entity)
        {
            ++Shared.Global.Counter_BombCanLocated;
        }
    }
}
