
#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;
using SCSEngine.Audio;
#endregion

namespace SCSEngine.Services.Audio
{
    /// <summary>
    /// Component that manages audio playback for all sounds.
    /// </summary>
    public class AudioManager : GameComponent
    {
        #region Audio Data
        #endregion

        #region Initialization Methods

        public AudioManager(Game game)
            : base(game)
        {
        }

        #endregion

        #region Loading Methodes
        ///// <summary>
        ///// Loads a sounds and organizes them for future usage
        ///// </summary>
        //public void LoadSounds(string[,] soundNames)
        //{
        //    string soundLocation = "Sounds/";
        //    soundNames = soundNames;

        //    soundBank = new Dictionary<string, Sound>();

        //    for (int i = 0; i < soundNames.GetLength(0); i++)
        //    {
        //        SoundEffect se = this.Game.Content.Load<SoundEffect>(
        //            soundLocation + soundNames[i, 0]);
        //        soundBank.Add(soundNames[i, 1], new Sound(se));
        //    }
        //}
        #endregion

        #region Sound Methods
        /// <summary>
        /// Plays a sound by name.
        /// </summary>
        /// <param name="soundName">The sound to play</param>
        public void PlaySound(Sound sound, bool isSoundOff, bool isPauseMusicZune)
        {
            if (isSoundOff || !isPauseMusicZune)
                return;
            // If the sound exists, start it
            sound.Play();
        }

        /// <summary>
        /// Plays a sound by name.
        /// Auto-detect
        /// </summary>
        /// <param name="soundName">The sound to play</param>
        /// <param name="isLooped">Indicates if the sound should loop</param>
        public void PlaySound(Sound sound, bool isLooped, bool isSoundOff, bool isPauseMusicZune)
        {
            if (isSoundOff || !isPauseMusicZune)
                return;

            if (sound.IsLooped != isLooped)
                sound.IsLooped = isLooped;
            
            sound.Play();
        }


        /// <summary>
        /// Stops a sound mid-play. If the sound is not playing, this
        /// method does nothing.
        /// </summary>
        /// <param name="soundName">The name of the sound to stop</param>
        public void StopSound(Sound sound)
        {
            // If the sound exists, stop it
            sound.Stop();
        }
        
        // please: auto-detect
        public void PlaySong(Song song, bool isSoundOff, bool isPauseMusicZune)
        {
            if (isSoundOff || !isPauseMusicZune)
                return;

            if (MediaPlayer.GameHasControl && MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.Play(song);
            }
            MediaPlayer.IsRepeating = true;
        }
        public void StopSong()
        {
            MediaPlayer.Stop();
        }
        public void PauseSong()
        {
            MediaPlayer.Pause();
        }
        public void ResumeSong()
        {
            MediaPlayer.Resume();
        }
        #endregion
    }
}