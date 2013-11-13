using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SCSEngine.Sprite.Implements;

namespace BoomGame.FactoryElement
{
    public class WaterEffectInfo
    {
        public Vector2 Position
        {
            get;
            set;
        }

        public float Rotation
        {
            get;
            set;
        }

        public Sprite Image
        {
            get;
            set;
        }

        public float Delay
        {
            get;
            set;
        }

        public WaterEffectInfo(Vector2 position, float rotation, float delay, Sprite image)
        {
            this.Position = position;
            this.Rotation = rotation;
            this.Image = image;
            this.Delay = delay;
        }
    }
}
