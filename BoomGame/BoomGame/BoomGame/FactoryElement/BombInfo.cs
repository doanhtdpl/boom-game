using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SCSEngine;
using SCSEngine.Sprite.Implements;

namespace BoomGame.FactoryElement
{
    public class BombInfo
    {
        public Vector2 Position
        {
            get;
            set;
        }

        public int Range
        {
            get;
            set;
        }

        public int Type
        {
            get;
            set;
        }

        public int Time
        {
            get;
            set;
        }

        public Sprite Image
        {
            get;
            set;
        }

        public BombInfo(Vector2 position, int range, int time, int type)
        {
            this.Position = position;
            this.Range = range;
            this.Type = type;
            this.Time = time;
        }
    }
}
