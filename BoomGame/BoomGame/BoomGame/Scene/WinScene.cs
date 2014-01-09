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
using System.Diagnostics;

namespace BoomGame.Scene
{
    public class WinScene : BaseGameScreen
    {
        private SCSServices services;
        private IResourceManager resourceManager;

        private Texture2D aboutBackground;
        private Texture2D newHighScore;
        private Vector2 newHighScorePos;

        private Button btnReplay;
        private Button btnNext;
        private Button btnMenu;

        private BaseGameScreen parent;

        private UIControlManager controlManager;

        public WinScene(IGameScreenManager manager)
            : base(manager)
        {
            services = (SCSServices)manager.Game.Services.GetService(typeof(SCSServices));

            resourceManager = (IResourceManager)manager.Game.Services.GetService(typeof(IResourceManager));
        }

        public void onInit(BaseGameScreen parent)
        {
            this.parent = parent;

            aboutBackground = resourceManager.GetResource<Texture2D>(Shared.Resources.BackgroundWin);

            controlManager = new UIControlManager(Game, DefaultGestureHandlingFactory.Instance);
            Global.GestureManager.AddDispatcher(controlManager);

            btnReplay = new Button(Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.BtnReplay), resourceManager.GetResource<Texture2D>(Shared.Resources.BtnOver));
            btnReplay.Canvas.Bound.Position = new Vector2(516f, 252f);
            btnReplay.FitSizeByImage();

            btnMenu = new Button(Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.BtnMenu), resourceManager.GetResource<Texture2D>(Shared.Resources.BtnOver));
            btnMenu.Canvas.Bound.Position = new Vector2(223f, 252f);
            btnMenu.FitSizeByImage();

            btnReplay.OnPressed += new ButtonEventHandler(btnReplay_OnPressed);
            btnMenu.OnPressed += new ButtonEventHandler(btnMenu_OnPressed);

            controlManager.Add(btnReplay);
            controlManager.Add(btnMenu);

            // Exception for Next button, prevent choice next map when next map not available
            if (Convert.ToInt32(Global.CurrentMap) + 1 < Global.NumberOfMap)
            {
                btnNext = new Button(Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.BtnNextGame), resourceManager.GetResource<Texture2D>(Shared.Resources.BtnOver));
                btnNext.Canvas.Bound.Position = new Vector2(370f, 252f);
                btnNext.FitSizeByImage();

                btnNext.OnPressed += new ButtonEventHandler(btnNext_OnPressed);
                controlManager.Add(btnNext);
            }

            String gameType = "";
            if (parent is BasicGameScene)
            {
                gameType = SaveLoadGame.GAME_SCORE_BASIC;
            }
            else if(parent is MiniGameLimitBomb)
            {
                gameType = SaveLoadGame.GAME_SCORE_BOMB;
            }
            else
            {
                gameType = SaveLoadGame.GAME_SCORE_TIME;
            }

            int lastScore;
            SaveLoadGame.LoadGameScore(gameType, Convert.ToInt32(Global.CurrentMap), out lastScore);

            if (lastScore <= Global.Counter_Scores)
            {
                SaveLoadGame.SaveGameScore(gameType, Convert.ToInt32(Global.CurrentMap), Global.Counter_Scores);
                newHighScore = resourceManager.GetResource<Texture2D>(Shared.Resources.NewHighScore);
                newHighScorePos = new Vector2(486f, 24f);
            }

            SaveLoadGame.SaveLevel(Global.CurrentMode, Convert.ToInt32(Global.CurrentMap) + 1);
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

        void btnNext_OnPressed(Button button)
        {
            if (this.parent is TBGamePlayScene)
            {
                (this.parent as TBGamePlayScene).Clear();
            }

            Global.PlaySound_Button_Effect();
            Global.GestureManager.RemoveDispatcher(controlManager);
            Global.BoomMissionManager.RemoveCurrent();

            Global.CreateCurrentMap(Convert.ToInt32(Global.CurrentMap) + 1);
            Global.CurrentMap = (Convert.ToInt32(Global.CurrentMap) + 1).ToString();
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

            if(newHighScore != null)
                services.SpriteBatch.Draw(newHighScore, newHighScorePos, Color.White);

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
