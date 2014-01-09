using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SCSEngine.Sprite.Implements;

namespace BoomGame.FactoryElement
{
    public class EnemyInfo
    {
        public Vector2 Position
        {
            get;
            set;
        }

        public int Type
        {
            get;
            set;
        }

        public Vector2 Velocity
        {
            get;
            set;
        }

        public bool IsBoss
        {
            get;
            set;
        }

        public EnemyInfo(Vector2 position, Vector2 velocity, bool isBoss)
        {
            this.Position = position;
            this.Velocity = velocity;
            this.IsBoss = isBoss;
        }
    }
}
