using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCSEngine.Sprite.Implements
{
    public class SpriteData : SCSEngine.Sprite.ISpriteData
    {
        private SCSEngine.Sprite.Implements.SpriteGraphicData sprGraphicData;
        private SCSEngine.Sprite.Implements.SpriteMetadata metaData;

        public SpriteData(SCSEngine.Sprite.Implements.SpriteGraphicData sprGraphicData, SCSEngine.Sprite.Implements.SpriteMetadata metaData)
        {
            this.sprGraphicData = sprGraphicData;
            this.metaData = metaData;
        }

        public SpriteData(SpriteData spriteData)
        {
            this.Data = spriteData.Data;
            this.Metadata = spriteData.Metadata;
        }

        public SCSEngine.Sprite.Implements.SpriteGraphicData Data
        {
            get
            {
                return this.sprGraphicData;
            }
            set
            {
                this.sprGraphicData = value;
            }
        }

        public SCSEngine.Sprite.Implements.SpriteMetadata Metadata
        {
            get
            {
                return this.metaData;
            }
            set
            {
                this.metaData = value;
            }
        }
    }
}
