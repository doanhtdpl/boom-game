using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace SCSEngine.Audio
{
    public class Sound
    {
        protected enum SoundEffectState
        {
            PLAYING,
            PAUSED,
            STOPPED
        }

        private List<SoundEffectInstance> instances;
        private SoundEffect soundEffect;
        private SoundEffectState state;
            
        public Sound(SoundEffect soundEffect)
        {
            instances = new List<SoundEffectInstance>();

            this.soundEffect = soundEffect;

            this.IsLooped = false;
        }

        public bool IsLooped
        {
            get;
            set;
        }

        public SoundState State
        {
            get
            {
                switch (state)
                {
                    case SoundEffectState.PLAYING: return SoundState.Playing;
                    case SoundEffectState.PAUSED: return SoundState.Paused;
                    case SoundEffectState.STOPPED: return SoundState.Stopped;
                    default: return SoundState.Playing;
                }
            }
        }

        public SoundEffectInstance CreateInstance()
        {
            return soundEffect.CreateInstance();
        }

        public void Play()
        {
            state = SoundEffectState.PLAYING;
            SoundEffectInstance newInstance = soundEffect.CreateInstance();
            instances.Add(newInstance);

            foreach (SoundEffectInstance sei in instances)
            {
                sei.Play();
            }
        }

        public void Pause()
        {
            state = SoundEffectState.PAUSED;
            foreach (SoundEffectInstance sei in instances)
            {
                sei.Pause();
            }
        }

        public void Stop()
        {
            state = SoundEffectState.STOPPED;
            foreach (SoundEffectInstance sei in instances)
            {
                sei.Stop();
            }
            this.Dispose();
        }

        public void Dispose()
        {
            foreach (SoundEffectInstance sei in instances)
            {
                sei.Dispose();
            }
            //soundEffect.Dispose();
            instances.Clear();
        }
    }
}
