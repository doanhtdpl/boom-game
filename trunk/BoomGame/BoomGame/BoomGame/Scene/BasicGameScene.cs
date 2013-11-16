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

namespace BoomGame.Scene
{
    public class BasicGameScene : TBGamePlayScene
    {
        private Sprite spr;

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

            if(Global.Counter_Enemy == 0)
            {
                // Win Game
            }
            if (Global.Counter_Bomber == 0)
            {
                // Lose Game
            }

            base.Update(gameTime);
        }

        void onBackButton_pressed()
        {
            // Remove to menu
            this.InputLayer.Pause();
            Global.BoomMissionManager.RemoveCurrent();

            MenuScene menu = (Global.BoomMissionManager.Bank.GetScreen(Shared.Macros.S_MENU, true) as MenuScene);
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
                    services.SpritePlayer.Draw(spr, pos, Color.White);
                }
            }
            base.Draw(gameTime);

            InputLayer.Draw(gameTime);
        }
    }
}
