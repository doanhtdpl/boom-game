using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SCSEngine.ServiceManagement;
using SCSEngine.Services.Audio;
using SCSEngine.Services.Input;
using SCSEngine.Services.Sprite;

namespace SCSEngine.Services
{
    public class SCSServices
    {
        public SCSServices(Game game, SpriteBatch sprBatch)
        {
            this.Game = game;
            this.SpriteBatch = sprBatch;
            this.SpritePlayer = new SpritePlayer(this.SpriteBatch);
            this.AudioManager = new AudioManager(this.Game);
            this.InputHandle = new InputHandle();
        }

        public Game Game { get; private set; }

        public SpriteBatch SpriteBatch { get; private set; }

        public SpritePlayer SpritePlayer { get; private set; }

        public AudioManager AudioManager { get; private set; }

        public InputHandle InputHandle { get; private set; }
    }
}
