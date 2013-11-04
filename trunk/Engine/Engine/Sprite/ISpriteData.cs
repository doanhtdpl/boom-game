using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCSEngine.Sprite.Implements;

namespace SCSEngine.Sprite
{
    public interface ISpriteData
    {
        SpriteGraphicData Data
        {
            get;
            set;
        }

        SpriteMetadata Metadata
        {
            get;
            set;
        }
    }
}
