using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoomGame.Entity.Item
{
    public class ItemCoin : IItem
    {
        public ItemCoin()
        {
        }

        public void affect(IGameEntity entity)
        {
            Shared.Global.TotalCoin++;
        }
    }
}