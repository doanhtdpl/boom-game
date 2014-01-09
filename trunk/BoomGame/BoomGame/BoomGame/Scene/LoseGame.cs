using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCSEngine.ScreenManagement.Implement;
using SCSEngine.Services;
using SCSEngine.ResourceManagement;
using Microsoft.Xna.Framework.Graphics;
using SCSEngine.ScreenManagement;
using Microsoft.Xna.Framework;
using BoomGame.Shared;
using SSCEngine.Control;
using SSCEngine.GestureHandling;

namespace BoomGame.Scene
{
    public class LoseGame : BaseGameScreen
    {
        private SCSServices services;
        private IResourceManager resourceManager;

        private Texture2D aboutBackground;

        private Button btnReplay;
        private Button btnMenu;

        private BaseGameScreen parent;

        private UIControlManager controlManager;

        public LoseGame(IGameScreenManager manager)
            : base(manager)
        {
            services = (SCSServices)manager.Game.Services.GetService(typeof(SCSServices));

            resourceManager = (IResourceManager)manager.Game.Services.GetService(typeof(IResourceManager));
        }

        public void onInit(BaseGameScreen parent)
        {
            this.parent = parent;

            aboutBackground = resourceManager.GetResource<Texture2D>(Shared.Resources.BackgroundLose);

            controlManager = new UIControlManager(Game, DefaultGestureHandlingFactory.Instance);
            Global.GestureManager.AddDispatcher(controlManager);

            btnReplay = new Button(Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.BtnReplay), resourceManager.GetResource<Texture2D>(Shared.Resources.BtnOver));
            btnReplay.Canvas.Bound.Position = new Vector2(516, 252);
            btnReplay.FitSizeByImage();

            btnMenu = new Button(Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.BtnMenu), resourceManager.GetResource<Texture2D>(Shared.Resources.BtnOver));
            btnMenu.Canvas.Bound.Position = new Vector2(370, 252);
            btnMenu.FitSizeByImage();

            btnReplay.OnPressed += new ButtonEventHandler(btnReplay_OnPressed);
            btnMenu.OnPressed += new ButtonEventHandler(btnMenu_OnPressed);

            controlManager.Add(btnReplay);
            controlManager.Add(btnMenu);
        }

        void btnMenu_OnPressed(Button button)
        {
            if (this.parent is TBGamePlayScene)
            {
                (this.parent as TBGamePlayScene).Clear();
            }

            Global.PlaySound_Button_Effect();
            // Call to menu
            Global.GestureManager.RemoveDispatcher(controlManager);
            Global.BoomMissionManager.RemoveCurrent();

            MenuScene menu = (Global.BoomMissionManager.Bank.GetScreen(Shared.Macros.S_MENU, true) as MenuScene);
            menu.onInit();
            Global.BoomMissionManager.AddExclusive(menu);
        }

        void btnReplay_OnPressed(Button button)
        {
            if (this.parent is TBGamePlayScene)
            {
                (this.parent as TBGamePlayScene).Clear();
            }

            Global.PlaySound_Button_Effect();
            Global.GestureManager.RemoveDispatcher(controlManager);
            Global.BoomMissionManager.RemoveCurrent();

            Global.CreateCurrentMap(Convert.ToInt32(Global.CurrentMap));
        }

        public override void Update(GameTime gameTime)
        {
            controlManager.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            services.SpriteBatch.Draw(aboutBackground, Vector2.Zero, Color.White);

            controlManager.Draw(gameTime);

            base.Draw(gameTime);
        }

        void onBackButton_pressed()
        {
            // Call to menu
            Global.PlaySound_Button_Effect();
            Global.GestureManager.RemoveDispatcher(controlManager);
            Global.BoomMissionManager.RemoveCurrent();

            MenuScene menuScene = Global.BoomMissionManager.Bank.GetScreen(Shared.Macros.S_MENU) as MenuScene;
            menuScene.onInit();
            Global.BoomMissionManager.AddExclusive(menuScene);
        }
    }
}
