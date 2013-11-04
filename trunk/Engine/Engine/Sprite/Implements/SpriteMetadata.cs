using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace SCSEngine.Sprite.Implements
{
    public class SpriteMetadata
    {
        public int NumberOfFrames
        {
            get 
            {
                try
                {
                    return frameDatas.Count;
                }
                catch (System.Exception)
                {
                    return -1;
                }
            }
        }
        /*
        private int nWidth;
        public int FrameWidth
        {
            get { return nWidth; }
        }

        private int nHeight;
        public int FrameHeight
        {
            get { return this.nHeight; }
        }*/

        private List<Rectangle> frameDatas;
        public List<Rectangle> Frames
        {
            get { return this.frameDatas; }
        }

        public SpriteMetadata(/*int nWidht, int nHeight, */List<Rectangle> frameDatas)
        {
            /*this.nWidth = nWidht;
            this.nHeight = nHeight;*/
            this.frameDatas = frameDatas;
        }
    }
}
