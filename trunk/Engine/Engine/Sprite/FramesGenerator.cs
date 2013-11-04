using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SCSEngine.Sprite
{
    public static class FramesGenerator
    {
        public static List<Rectangle> Generate(int frameWidth, int frameHeight, int maxWidth, int nFrames)
        {
            List<Rectangle> frames = new List<Rectangle>(nFrames);

            int x = 0, y = 0;

            for (int i = 0; i < nFrames; ++i)
            {
                frames.Add(NextFrame(frameWidth, frameHeight, maxWidth, ref x, ref y));
            }

            return frames;
        }

        private static Rectangle NextFrame(int frameWidth, int frameHeight, int maxWidth, ref int x, ref int y)
        {
            Rectangle frame = new Rectangle(x, y, frameWidth - 1, frameHeight - 1);

            x += frameWidth;

            if (x >= maxWidth)
            {
                x = 0;
                y += frameHeight;
            }

            return frame;
        }
    }
}
