using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCSEngine.ScreenManagement.Implement;
using SCSEngine.ScreenManagement;
using BoomGame.Entity;
using BoomGame.Factory;
using BoomGame.FactoryElement;
using Microsoft.Xna.Framework;
using SCSEngine.Sprite.Implements;
using SCSEngine.Sprite;
using Microsoft.Xna.Framework.Input;
using BoomGame.Shared;
using Microsoft.Xna.Framework.Graphics;

namespace BoomGame.Scene
{
    public class BasicGameScene : TBGamePlayScene
    {
        private Sprite spr;
        private Texture2D backgroundLine;
        private SCSEngine.Audio.Sound s_background;

        private SpriteFont infoFont;
        private double Time
        {
            get;
            set;
        }

        private Vector2 numberBomber;
        private Vector2 clock;
        private Vector2 numberEnemy;
        private Vector2 scores;
        private Vector2 gold;

        public BasicGameScene(IGameScreenManager manager)
            : base(manager)
        {
        }

        public void onInit(String mapPath)
        {
            this.Name = Shared.Macros.S_BASIC;

            Components.Add(Grid.Grid.getInst());

            MapReader.MapReader reader = new MapReader.MapReader(mapPath);
            reader.onInit(resourceManager);
            List<IGameEntity> entities = reader.Read();
            if (entities != null)
            {
                for (int i = 0; i < entities.Count; ++i)
                {
                    entities[i].onInit();
                    GameManager.Add(entities[i]);
                }
                entities.Clear();
            }

            spr = (Sprite)resourceManager.GetResource<ISprite>(Shared.Resources.sand_green_road);

            backgroundLine = (Texture2D)resourceManager.GetResource<Texture2D>(Shared.Resources.BackgroundGame);

            infoFont = (SpriteFont)resourceManager.GetResource<SpriteFont>(Shared.Resources.Time_Font);
            numberBomber = new Vector2(26f, 0f);
            numberEnemy = new Vector2(756f, 0f);
            clock = new Vector2(183f, -1f);
            scores = new Vector2(400f, -1f);
            gold = new Vector2(596f, -1f);

            BomberEntity bomberEntity = new BomberEntity(this.Game);
            bomberEntity.onInit();
            bomberEntity.RendererObj.Position = new Vector2(Global.Bomber_Start_Position_X, Global.Bomber_Start_Position_Y);
            GameManager.Add(bomberEntity);
            InputLayer.Add(bomberEntity);
            Global.Counter_Bomber++;

            Components.Add(InputLayer);

            s_background = resourceManager.GetResource<SCSEngine.Audio.Sound>(Global.RandomBackgroundSong());
            services.AudioManager.PlaySound(s_background, true, Global.isMusicOff, Global.isMusicZuneOff);

            Time = 0;
        }

        public override void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                Time += gameTime.ElapsedGameTime.TotalSeconds;

                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                {
                    onBackButton_pressed();
                }

                InputLayer.Update(gameTime);

                if (Global.Counter_Enemy == 0)
                {
                    // Win Game
                    Global.Counter_Enemy = -1;
                    Global.PlaySoundEffect(Shared.Resources.Sound_Win);
                }
                if (Global.Counter_Bomber == 0)
                {
                    // Lose Game
                    Global.Counter_Bomber = -1;
                    Global.PlaySoundEffect(Shared.Resources.Sound_Lose);
                }

                base.Update(gameTime);
            }
        }

        public void Clear()
        {
            this.InputLayer.Pause();
            services.AudioManager.StopSound(s_background);
        }

        void onBackButton_pressed()
        {
            this.Enabled = false;

            PauseScene pause = Global.BoomMissionManager.Bank.GetScreen(Shared.Macros.S_PAUSE, true) as PauseScene;
            pause.onInit(this);
            Global.BoomMissionManager.AddExclusive(pause);
        }

        public override void Draw(GameTime gameTime)
        {
            if (Visible)
            {
                Vector2 cellSize = Grid.Grid.getInst().CellSize;

                Vector2 pos = new Vector2(0, 0);
                for (int i = 0; i < Grid.Grid.getInst().Size.Y; ++i)
                {
                    for (int j = 0; j < Grid.Grid.getInst().Size.X; ++j)
                    {
                        pos.X = Grid.Grid.getInst().Position.X + i * cellSize.X;
                        pos.Y = Grid.Grid.getInst().Position.Y + j * cellSize.Y;
                        services.SpritePlayer.Draw(spr, pos, Color.White);
                    }
                }

                base.Draw(gameTime);

                services.SpriteBatch.Draw(backgroundLine, Vector2.Zero, Color.White);

                services.SpriteBatch.DrawString(infoFont, "x" + Global.Counter_Bomber.ToString(), numberBomber, Color.Black);
                services.SpriteBatch.DrawString(infoFont, "x" + Global.Counter_Enemy.ToString(), numberEnemy, Color.Black);
                services.SpriteBatch.DrawString(infoFont, ((int)Time / 60).ToString() + ":" + ((int)Time % 60).ToString(), clock, Color.Black);
                services.SpriteBatch.DrawString(infoFont, Global.TotalCoin.ToString(), gold, Color.Black);
                services.SpriteBatch.DrawString(infoFont, Global.Counter_Scores.ToString(), scores, Color.Black);

                InputLayer.Draw(gameTime);
            }
        }
    }
}
