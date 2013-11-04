using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SCSEngine.Sprite.Implements
{
    public class Sprite : ISprite
    {
        private static int DEFAULT_FRAME = 0;
        public enum SpriteState
        {
            PLAYED,
            PAUSED,
            STOPPED,
        }

        private SpriteState state;

        public ISpriteData SpriteData { get; private set; }

        public TimeSpan TimeDelay { get; set; }

        // Sprite Player field
        private TimeSpan lastTime;
        private TimeSpan currentTime;

        private int nextFrame;
        private Rectangle currentFrame;
        public Rectangle CurrentFrame
        {
            get { return this.currentFrame; }
        }

        public Sprite(ISpriteData data)
        {
            this.SpriteData = data;

            // Sprite is initialized with current state is stop
            state = SpriteState.STOPPED;

            // Init with first frame
            nextFrame = DEFAULT_FRAME;
            this.currentFrame = this.SpriteData.Metadata.Frames[nextFrame];

            // Init with smallest time
            lastTime = new TimeSpan(-100000000);
        }

        public void TimeStep(GameTime gameTime)
        {
            if (state == SpriteState.PLAYED)
            {
                currentTime = gameTime.TotalGameTime;
                if (currentTime - lastTime >= this.TimeDelay)
                {
                    if ((nextFrame + 1) >= this.SpriteData.Metadata.NumberOfFrames)
                    {
                        nextFrame = DEFAULT_FRAME;
                    }
                    this.currentFrame = this.SpriteData.Metadata.Frames[nextFrame++];

                    // Update time
                    lastTime = currentTime;
                }
            }
        }

        public void Play()
        {
            // Reset to first frame
            if (state != SpriteState.PLAYED)
            {
                state = SpriteState.PLAYED;
                this.nextFrame = DEFAULT_FRAME;
            }
        }

        public void Resume()
        {
            state = SpriteState.PLAYED;
            // Just play from previous frame--> do nothing
        }

        public void Pause()
        {
            state = SpriteState.PAUSED;
        }

        public void Stop()
        {
            state = SpriteState.STOPPED;
            // Stop and reset frame to first frame
            this.nextFrame = DEFAULT_FRAME;
        }
    }
}
