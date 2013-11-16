using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BoomGame.FactoryElement
{
    public class ExplodeInfo
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

        public String ChopImage
        {
            get;
            set;
        }

        public String BodyImage
        {
            get;
            set;
        }

        public String CenterImage
        {
            get;
            set;
        }

        public ExplodeInfo(Vector2 position, int range, String centerImage, String chopImage, String bodyImage)
        {
            this.Position = position;
            this.Range = range;
            this.ChopImage = chopImage;
            this.BodyImage = bodyImage;
            this.CenterImage = centerImage;
        }
    }
}
