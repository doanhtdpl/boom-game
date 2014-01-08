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
    public class PauseScene : BaseGameScreen
    {
        private SCSServices services;
        private IResourceManager resourceManager;

        private Texture2D aboutBackground;

        private Button btnReplay;
        private Button btnResume;
        private Button btnMenu;

        private BaseGameScreen parent;

        private UIControlManager controlManager;

        public PauseScene(IGameScreenManager manager)
            : base(manager)
        {
            services = (SCSServices)manager.Game.Services.GetService(typeof(SCSServices));

            resourceManager = (IResourceManager)manager.Game.Services.GetService(typeof(IResourceManager));
        }

        public void onInit(BaseGameScreen parent)
        {
            this.parent = parent;

            aboutBackground = resourceManager.GetResource<Texture2D>(Shared.Resources.BackgroundPause);

            controlManager = new UIControlManager(Game, DefaultGestureHandlingFactory.Instance);
            Global.GestureManager.AddDispatcher(controlManager);

            btnReplay = new Button(Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.BtnReplay), resourceManager.GetResource<Texture2D>(Shared.Resources.BtnOver));
            btnReplay.Canvas.Bound.Position = new Vector2(199.0f, 229.0f);
            btnReplay.FitSizeByImage();

            btnResume = new Button(Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.BtnResume), resourceManager.GetResource<Texture2D>(Shared.Resources.BtnOver));
            btnResume.Canvas.Bound.Position = new Vector2(348.0f, 229.0f);
            btnResume.FitSizeByImage();

            btnMenu = new Button(Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.BtnMenu), resourceManager.GetResource<Texture2D>(Shared.Resources.BtnOver));
            btnMenu.Canvas.Bound.Position = new Vector2(505.0f, 229.0f);
            btnMenu.FitSizeByImage();

            btnReplay.OnPressed += new ButtonEventHandler(btnReplay_OnPressed);
            btnResume.OnPressed += new ButtonEventHandler(btnResume_OnPressed);
            btnMenu.OnPressed += new ButtonEventHandler(btnMenu_OnPressed);

            controlManager.Add(btnReplay);
            controlManager.Add(btnResume);
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

        void btnResume_OnPressed(Button button)
        {
            Global.PlaySound_Button_Effect();
            Global.GestureManager.RemoveDispatcher(controlManager);
            Global.BoomMissionManager.RemoveCurrent();
        }

        void btnReplay_OnPressed(Button button)
        {
            Global.PlaySound_Button_Effect();
            Global.GestureManager.RemoveDispatcher(controlManager);
            Global.BoomMissionManager.RemoveCurrent();

            if (this.parent is TBGamePlayScene)
            {
                (this.parent as TBGamePlayScene).Clear();
            }

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
    }
}
