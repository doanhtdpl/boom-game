using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCSEngine.ScreenManagement.Implement;
using SCSEngine.Services;
using Microsoft.Xna.Framework.Graphics;
using SSCEngine.Control;
using SCSEngine.ScreenManagement;
using SCSEngine.ResourceManagement;
using SCSEngine.Sprite;
using Microsoft.Xna.Framework;
using SSCEngine.GestureHandling;
using BoomGame.Shared;
using Microsoft.Xna.Framework.Input;

namespace BoomGame.Scene
{
    public class MenuScene : BaseGameScreen
    {
        private SCSServices services;
        private IResourceManager resourceManager;

        private Texture2D background;

        private Button btnBasicGame;
        private Button btnChallengeTime;
        private Button btnChallengeLimit;
        private Button btnOption;
        private Button btnAbout;
        private Button btnHelp;

        private UIControlManager controlManager;

        public MenuScene(IGameScreenManager manager)
            : base(manager)
        {
            services = (SCSServices)manager.Game.Services.GetService(typeof(SCSServices));

            resourceManager = (IResourceManager)manager.Game.Services.GetService(typeof(IResourceManager));
        }

        public void onInit()
        {
            this.Name = Shared.Macros.S_MENU;

            controlManager = new UIControlManager(Game, DefaultGestureHandlingFactory.Instance);
            Global.GestureManager.AddDispatcher(controlManager);

            background = resourceManager.GetResource<Texture2D>(Shared.Resources.Menu_Background);

            btnBasicGame = new Button(Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.Menu_BasicGameButton), resourceManager.GetResource<Texture2D>(Shared.Resources.Menu_ButtonOver));
            btnBasicGame.Canvas.Bound.Position = new Vector2(100, 380);
            btnBasicGame.FitSizeByImage();

            btnChallengeTime = new Button(Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.Menu_ChallengeTimeButton), resourceManager.GetResource<Texture2D>(Shared.Resources.Menu_ButtonOver));
            btnChallengeTime.Canvas.Bound.Position = new Vector2(200, 380);
            btnChallengeTime.FitSizeByImage();

            btnChallengeLimit = new Button(Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.Menu_ChallengeLimitButton), resourceManager.GetResource<Texture2D>(Shared.Resources.Menu_ButtonOver));
            btnChallengeLimit.Canvas.Bound.Position = new Vector2(200, 300);
            btnChallengeLimit.FitSizeByImage();

            btnOption = new Button(Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.Menu_OptionButton), resourceManager.GetResource<Texture2D>(Shared.Resources.Menu_ButtonOver));
            btnOption.Canvas.Bound.Position = new Vector2(300, 380);
            btnOption.FitSizeByImage();

            btnAbout = new Button(Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.Menu_AboutButton), resourceManager.GetResource<Texture2D>(Shared.Resources.Menu_ButtonOver));
            btnAbout.Canvas.Bound.Position = new Vector2(400, 380);
            btnAbout.FitSizeByImage();

            btnHelp = new Button(Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.Menu_HelpButton), resourceManager.GetResource<Texture2D>(Shared.Resources.Menu_ButtonOver));
            btnHelp.Canvas.Bound.Position = new Vector2(500, 380);
            btnHelp.FitSizeByImage();

            // Init event
            btnBasicGame.OnPressed += new ButtonEventHandler(btnBasicGame_OnPressed);
            btnChallengeTime.OnPressed += new ButtonEventHandler(btnChallengeTime_OnPressed);
            btnChallengeLimit.OnPressed += new ButtonEventHandler(btnChallengeLimit_OnPressed);
            btnOption.OnPressed += new ButtonEventHandler(btnOption_OnPressed);
            btnAbout.OnPressed += new ButtonEventHandler(btnAbout_OnPressed);
            btnHelp.OnPressed += new ButtonEventHandler(btnHelp_OnPressed);

            controlManager.Add(btnBasicGame);
            controlManager.Add(btnChallengeTime);
            controlManager.Add(btnChallengeLimit);
            controlManager.Add(btnOption);
            controlManager.Add(btnAbout);
            controlManager.Add(btnHelp);
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
        }

        public void Unpause()
        {
            Global.GestureManager.AddDispatcher(this.controlManager);
        }

        void onBackButton_pressed()
        {
            Game.Exit();
        }

        void btnHelp_OnPressed(Button button)
        {
            this.Pause();
            Global.BoomMissionManager.RemoveCurrent();

            HelpScene helpScene = Global.BoomMissionManager.Bank.GetScreen(Shared.Macros.S_HELP) as HelpScene;
            helpScene.onInit();
            Global.BoomMissionManager.AddExclusive(helpScene);
        }

        void btnAbout_OnPressed(Button button)
        {
            this.Pause();
            Global.BoomMissionManager.RemoveCurrent();

            AboutScene aboutScene = Global.BoomMissionManager.Bank.GetScreen(Shared.Macros.S_ABOUT, true) as AboutScene;
            aboutScene.onInit();
            Global.BoomMissionManager.AddExclusive(aboutScene);
        }

        void btnOption_OnPressed(Button button)
        {
            // Stop/Resume music
        }

        void btnChallengeLimit_OnPressed(Button button)
        {
            Global.CurrentMode = Shared.Constants.LIMIT_MODE;
            onChangeToNextGame();
        }

        void btnChallengeTime_OnPressed(Button button)
        {
            Global.CurrentMode = Shared.Constants.TIME_MODE;
            onChangeToNextGame();
        }

        void btnBasicGame_OnPressed(Button button)
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
        }
    }
}
