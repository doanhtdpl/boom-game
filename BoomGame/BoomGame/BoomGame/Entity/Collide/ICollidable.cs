using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        void Collision(ICollidable obj);

        void ApplyCollision();
    }
}
