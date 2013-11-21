using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoomGame.Entity.Item
{
    public interface IItem
    {
        void affect(IGameEntity entity);
    }
}
