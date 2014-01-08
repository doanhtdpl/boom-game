using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCSEngine.ScreenManagement.Implement;
using SCSEngine.Services;
using SCSEngine.ResourceManagement;
using Microsoft.Xna.Framework.Graphics;
using SSCEngine.Control;
using SCSEngine.ScreenManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using BoomGame.Extends;
using SSCEngine.GestureHandling;
using BoomGame.Shared;
using SSCEngine.GestureHandling.Implements.Events;

namespace BoomGame.Scene
{
    public class GameModeScene : BaseGameScreen
    {
        private SCSServices services;
        private IResourceManager resourceManager;

        private Texture2D background;

        private UIControlManager controlManager;

        private Button btnAdventure;
        private Button btnTime;
        private Button btnBomb;

        public GameModeScene(IGameScreenManager manager)
            : base(manager)
        {
            services = (SCSServices)manager.Game.Services.GetService(typeof(SCSServices));

            resourceManager = (IResourceManager)manager.Game.Services.GetService(typeof(IResourceManager));
        }

        public void onInit()
        {
            // Get the number of map for initialize menu choose level
            Global.GetNumberOfMap();

            // Save this name
            this.Name = Shared.Macros.S_CHOOSEGAME;

            background = resourceManager.GetResource<Texture2D>(Shared.Resources.BackgroundMode);

            // Create UI manager for button next & previous
            controlManager = new UIControlManager(Game, DefaultGestureHandlingFactory.Instance);
            Global.GestureManager.AddDispatcher(controlManager);

            // Create button
            btnAdventure = new Button(this.Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.BtnAdventure), resourceManager.GetResource<Texture2D>(Shared.Resources.BtnAdventureOver));
            btnAdventure.Canvas.Bound.Position = new Vector2(512.0f, 219.0f);
            btnAdventure.FitSizeByImage();

            btnBomb = new Button(this.Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.BtnBomb), resourceManager.GetResource<Texture2D>(Shared.Resources.BtnBombOver));
            btnBomb.Canvas.Bound.Position = new Vector2(599.0f, 280.0f);
            btnBomb.FitSizeByImage();

            btnTime = new Button(this.Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.BtnTime), resourceManager.GetResource<Texture2D>(Shared.Resources.BtnTimeOver));
            btnTime.Canvas.Bound.Position = new Vector2(610.0f, 158.0f);
            btnTime.FitSizeByImage();

            btnAdventure.OnPressed += new ButtonEventHandler(btnAdventure_OnPressed);
            btnBomb.OnPressed += new ButtonEventHandler(btnBomb_OnPressed);
            btnTime.OnPressed += new ButtonEventHandler(btnTime_OnPressed);

            controlManager.Add(btnAdventure);
            controlManager.Add(btnBomb);
            controlManager.Add(btnTime);

            // Reset to first page
            Global.CurrentPage = 0;
        }

        void btnTime_OnPressed(Button button)
        {
            Global.CurrentMode = Shared.Constants.TIME_MODE;
            onChangeToNextGame();
        }

        void btnBomb_OnPressed(Button button)
        {
            Global.CurrentMode = Shared.Constants.LIMIT_MODE;
            onChangeToNextGame();
        }

        void btnAdventure_OnPressed(Button button)
        {
            Global.CurrentMode = Shared.Constants.BASIC_MODE;
            onChangeToNextGame();
        }

        void onChangeToNextGame()
        {
            this.Pause();
            Global.BoomMissionManager.RemoveCurrent();

            BoomGame.Scene.ChooseGame choose = Global.BoomMissionManager.Bank.GetScreen(Shared.Macros.S_CHOOSEGAME, true) as BoomGame.Scene.ChooseGame;
            choose.onInit();
            Global.BoomMissionManager.AddExclusive(choose);

            Global.PlaySound_Button_Effect();
        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                onBackButton_pressed();
            }

            controlManager.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            services.SpriteBatch.Draw(background, Vector2.Zero, Color.White);

            controlManager.Draw(gameTime);

            base.Draw(gameTime);
        }

        public void Pause()
        {
            Global.GestureManager.RemoveDispatcher(this.controlManager);

            services.AudioManager.StopSound(MenuScene.s_background);
        }

        public void Unpause()
        {
            Global.GestureManager.AddDispatcher(this.controlManager);
        }

        void onBackButton_pressed()
        {
            // Call to menu
            this.Pause();
            Global.BoomMissionManager.RemoveCurrent();

            MenuScene menu = (Global.BoomMissionManager.Bank.GetScreen(Shared.Macros.S_MENU, true) as MenuScene);
            menu.onInit();
            Global.BoomMissionManager.AddExclusive(menu);
        }
    }
}
