using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BoomGame.FactoryElement
{
    public class ItemInfo
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

        public ItemInfo(Vector2 position, int type)
        {
            this.Position = position;
            this.Type = type;
        }
    }
}
