using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCSEngine;
using SCSEngine.Sprite.Implements;
using Microsoft.Xna.Framework;

namespace BoomGame.FactoryElement
{
    public class ObstacleInfo
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

        public Sprite Image
        {
            get;
            set;
        }

        public bool IsStatic
        {
            get;
            set;
        }

        public ObstacleInfo(Vector2 position, int type, Sprite image, bool isStatic)
        {
            this.Type = type;
            this.Image = image;
            this.Position = position;
            this.IsStatic = isStatic;
        }
    }
}
