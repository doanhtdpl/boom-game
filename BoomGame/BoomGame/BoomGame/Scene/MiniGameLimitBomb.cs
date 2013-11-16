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
    public class MiniGameLimitBomb : TBGamePlayScene
    {
        private Sprite spr;
        private SpriteFont font;
        private Vector2 position;

        public MiniGameLimitBomb(IGameScreenManager manager)
            : base(manager)
        {
        }

        // Time in seconds
        public void onInit(String mapPath, int bombNumber)
        {
            this.Name = Shared.Macros.S_MINI_LIMIT;

            Global.Bomb_Number = bombNumber;

            font = resourceManager.GetResource<SpriteFont>(Shared.Resources.Time_Font);

            position = new Vector2(400, 5);

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

            BomberEntity bomberEntity = new BomberEntity(this.Game);
            bomberEntity.onInit();
            bomberEntity.RendererObj.Position = new Vector2(Shared.Constants.GAME_SIZE_X, Shared.Constants.GAME_SIZE_Y);
            GameManager.Add(bomberEntity);
            InputLayer.Add(bomberEntity);
            Global.Counter_Bomber++;

            Components.Add(InputLayer);
        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                onBackButton_pressed();
            }

            InputLayer.Update(gameTime);

            if (Global.Bomb_Number < 0)
            {
                onLimitBomb();
            }

            if (Global.Counter_Enemy == 0)
            {
                // Win Game
            }
            if (Global.Counter_Bomber == 0)
            {
                // Lose Game
            }

            base.Update(gameTime);
        }

        void onLimitBomb()
        {
            // Game finish
            if (Global.Counter_Enemy == 0)
            {
                // Win Game
            }
            else
            {
                // Lose Game
            }
        }

        void onBackButton_pressed()
        {
            // Remove to menu
            this.InputLayer.Pause();
            Global.BoomMissionManager.RemoveCurrent();

            MenuScene menu = (Global.BoomMissionManager.Bank.GetScreen(Shared.Macros.S_MENU) as MenuScene);
            menu.onInit();
            Global.BoomMissionManager.AddExclusive(menu);
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 cellSize = new Vector2(30, 30);

            Vector2 pos = new Vector2(0, 0);
            for (int i = 0; i < Grid.Grid.getInst().Size.Y; ++i)
            {
                for (int j = 0; j < Grid.Grid.getInst().Size.X; ++j)
                {
                    pos.X = i * 50;
                    pos.Y = j * 50;
                    services.SpritePlayer.Draw(spr, pos, Color.Red);
                }
            }
            base.Draw(gameTime);

            services.SpriteBatch.DrawString(font, Global.Bomb_Number.ToString(), position, Color.White);

            InputLayer.Draw(gameTime);
        }
    }
}
