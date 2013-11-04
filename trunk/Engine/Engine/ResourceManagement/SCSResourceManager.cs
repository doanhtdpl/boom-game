using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using SCSEngine.Audio;
using SCSEngine.ResourceManagement.Implement;
using SCSEngine.Sprite;

namespace SCSEngine.ResourceManagement
{
    public class SCSResourceManager : BaseResourceManager
    {
        public SCSResourceManager(ContentManager content)
            : base()
        {
            this.AddResourceLoader(new GameContentResourceLoader<Texture2D>(content));
            this.AddResourceLoader(new GameContentResourceLoader<SpriteFont>(content));
            this.AddResourceLoader(new GameContentResourceLoader<SoundEffect>(content));
            this.AddResourceLoader(new GameContentResourceLoader<Song>(content));
            this.AddResourceLoader(new SoundResourceLoader(this));
            this.AddResourceLoader(new SpriteResourceLoader(this, SpriteFramesBank.Instance));
        }
    }
}