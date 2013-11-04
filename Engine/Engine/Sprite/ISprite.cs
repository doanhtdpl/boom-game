using System;
using Microsoft.Xna.Framework;
namespace SCSEngine.Sprite
{
    public interface ISprite
    {
        ISpriteData SpriteData { get; }

        Rectangle CurrentFrame { get; }
        void Pause();
        void Play();
        void Resume();
        void Stop();

        TimeSpan TimeDelay { get; set; }

        void TimeStep(GameTime gameTime);
    }
}
