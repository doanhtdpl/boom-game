using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using SCSEngine.ResourceManagement;
using SCSEngine.Sprite.Implements;

namespace SCSEngine.Sprite
{
    public class SpriteResourceLoader : IResourceLoader
    {
        private ISpriteFramesBank framesBank;
        private IResourceManager resMan;

        public SpriteResourceLoader(IResourceManager resManager, ISpriteFramesBank framesBank)
        {
            this.framesBank = framesBank;
            this.resMan = resManager;
        }

        #region IResourceLoader Members

        public object Load(string resourceName)
        {
            if (framesBank.Contains(resourceName))
            {
                return new Implements.Sprite(new Implements.SpriteData(new SpriteGraphicData(resMan.GetResource<Texture2D>(resourceName)), new SpriteMetadata(framesBank.GetFrames(resourceName))));
            }
            else
            {
                Texture2D texture = resMan.GetResource<Texture2D>(resourceName);
                return new Implements.Sprite(new Implements.SpriteData(new SpriteGraphicData(texture), new SpriteMetadata(FramesGenerator.Generate(texture.Width, texture.Height, texture.Width, 1))));
            }
        }

        public Type ResourceType
        {
            get { return typeof(ISprite); }
        }

        public bool IsResourceResuable
        {
            get { return false; }
        }

        #endregion
    }
}
