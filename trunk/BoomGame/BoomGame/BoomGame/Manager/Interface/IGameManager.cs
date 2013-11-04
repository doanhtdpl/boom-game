using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using BoomGame.Entity;

namespace BoomGame.Interface.Manager
{
    public interface IGameManager
    {
        void Add(IGameEntity element);
        void Remove(IGameEntity element);
    }
}
