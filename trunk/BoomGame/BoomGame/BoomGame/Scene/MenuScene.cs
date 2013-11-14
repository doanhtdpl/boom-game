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

namespace BoomGame.Scene
{
    public class MenuScene : BaseGameScreen
    {
        private SCSServices services;
        private IResourceManager resourceManager;

        private Texture2D background;

        private Button btnBasicGame;
        private Button btnMiniGame;
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
            controlManager = new UIControlManager(Game, DefaultGestureHandlingFactory.Instance);
            Global.GestureManager.AddDispatcher(controlManager);

            background = resourceManager.GetResource<Texture2D>(Shared.Resources.Menu_Background);

            btnBasicGame = new Button(Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.Menu_BasicGameButton), resourceManager.GetResource<Texture2D>(Shared.Resources.Menu_ButtonOver));
            btnBasicGame.Canvas.Bound.Position = new Vector2(100, 380);
            btnBasicGame.FitSizeByImage();

            btnMiniGame = new Button(Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.Menu_MiniGameButton), resourceManager.GetResource<Texture2D>(Shared.Resources.Menu_ButtonOver));
            btnMiniGame.Canvas.Bound.Position = new Vector2(200, 380);
            btnMiniGame.FitSizeByImage();

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
            btnMiniGame.OnHold += new ButtonEventHandler(btnMiniGame_OnHold);
            btnOption.OnHold += new ButtonEventHandler(btnOption_OnHold);
            btnAbout.OnHold += new ButtonEventHandler(btnAbout_OnHold);
            btnHelp.OnPressed += new ButtonEventHandler(btnHelp_OnPressed);

            controlManager.Add(btnBasicGame);
            controlManager.Add(btnMiniGame);
            controlManager.Add(btnOption);
            controlManager.Add(btnAbout);
            controlManager.Add(btnHelp);
        }

        public override void Update(GameTime gameTime)
        {
            controlManager.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            services.SpriteBatch.Draw(background, Vector2.Zero, Color.White);

            controlManager.Draw(gameTime);

            base.Draw(gameTime);
        }

        void btnHelp_OnPressed(Button button)
        {
        }

        void btnAbout_OnHold(Button button)
        {
        }

        void btnOption_OnHold(Button button)
        {
        }

        void btnMiniGame_OnHold(Button button)
        {
            Global.CurrentMode = Shared.Constants.MINI_MODE;
        }

        void btnBasicGame_OnPressed(Button button)
        {
            Global.CurrentMode = Shared.Constants.BASIC_MODE;
            BoomGame.Scene.ChooseGame choose = Global.BoomMissionManager.Bank.GetScreen(Shared.Macros.S_CHOOSEGAME, true) as BoomGame.Scene.ChooseGame;
            choose.onInit();
            Global.BoomMissionManager.AddExclusive(choose);
        }
    }
}
