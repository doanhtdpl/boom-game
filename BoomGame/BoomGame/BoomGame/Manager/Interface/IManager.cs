using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using BoomGame.Entity;
using BoomGame.Interface.Manager;

namespace BoomGame.Manager.Interface
{
    public interface IManager<T>
    {
        List<T> Contents
        {
            get;
        }

        IGameManager Parent
        {
            get;
            set;
        }

        void UpdateRealTime();

        void Add(T element);
        void Remove(T element);
    }
}
