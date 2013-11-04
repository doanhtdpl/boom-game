using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SCSEngine.Sprite
{
    public class SpriteFramesBank : ISpriteFramesBank
    {
        private Dictionary<string, List<Rectangle>> frames = new Dictionary<string,List<Rectangle>>();

        private SpriteFramesBank()
        {
        }

        public static SpriteFramesBank Instance { get; private set; }

        static SpriteFramesBank()
        {
            Instance = new SpriteFramesBank();
        }

        #region ISpriteFramesBank Members

        public List<Microsoft.Xna.Framework.Rectangle> GetFrames(string spriteName)
        {
            return frames[spriteName];
        }

        public void Add(string name, List<Rectangle> frames)
        {
            this.frames.Add(name, frames);
        }

        public void Remove(string name)
        {
            this.frames.Remove(name);
        }

        public bool Contains(string name)
        {
            return this.frames.ContainsKey(name);
        }

        #endregion
    }
}
