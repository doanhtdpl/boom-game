using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BoomGame.Entity.Collide
{
    public interface ICollidable
    {
        IGameEntity Owner
        {
            get;
        }

        bool IsDead
        {
            get;
        }

        List<ICollidable> CollidableList
        {
            get;
        }

        Rectangle Bound
        {
            get;
        }

        void Collision(ICollidable obj);

        void ApplyCollision();
    }
}
