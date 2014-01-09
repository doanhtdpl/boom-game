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
using SCSEngine.Services.Audio;

namespace BoomGame.Scene
{
    public class MenuScene : BaseGameScreen
    {
        private SCSServices services;
        private IResourceManager resourceManager;

        private Texture2D background;

        private Button btnPlay;
        private Button btnSoundOn;
        private Button btnSoundOff;
        private Button btnAbout;
        private Button btnHelp;

        public static SCSEngine.Audio.Sound s_background;

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

            background = resourceManager.GetResource<Texture2D>(Shared.Resources.BackgroundMenu);

            btnPlay = new Button(Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.BtnPlay), resourceManager.GetResource<Texture2D>(Shared.Resources.BtnPlayOver));
            btnPlay.Canvas.Bound.Position = new Vector2(662.0f, 68.0f);
            btnPlay.FitSizeByImage();

            btnSoundOn = new Button(Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.BtnSound), resourceManager.GetResource<Texture2D>(Shared.Resources.BtnSoundOver));
            btnSoundOn.Canvas.Bound.Position = new Vector2(512.0f, 129.0f);
            btnSoundOn.FitSizeByImage();

            btnSoundOff = new Button(Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.BtnMute), resourceManager.GetResource<Texture2D>(Shared.Resources.BtnMuteOver));
            btnSoundOff.Canvas.Bound.Position = new Vector2(512.0f, 129.0f);
            btnSoundOff.FitSizeByImage();

            btnAbout = new Button(Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.BtnAbout), resourceManager.GetResource<Texture2D>(Shared.Resources.BtnAbout));
            btnAbout.Canvas.Bound.Position = new Vector2(627.0f, 185.0f);
            btnAbout.FitSizeByImage();

            btnHelp = new Button(Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.BtnHelp), resourceManager.GetResource<Texture2D>(Shared.Resources.BtnHelp));
            btnHelp.Canvas.Bound.Position = new Vector2(666.0f, 244.0f);
            btnHelp.FitSizeByImage();

            // Init event
            btnPlay.OnPressed += new ButtonEventHandler(btnPlay_OnPressed);
            btnSoundOn.OnPressed += new ButtonEventHandler(btnOption_OnPressed);
            btnSoundOff.OnPressed += new ButtonEventHandler(btnOption_OnPressed);
            btnAbout.OnPressed += new ButtonEventHandler(btnAbout_OnPressed);
            btnHelp.OnPressed += new ButtonEventHandler(btnHelp_OnPressed);

            controlManager.Add(btnPlay);
            controlManager.Add(btnSoundOn);
            controlManager.Add(btnSoundOff);
            controlManager.Add(btnAbout);
            controlManager.Add(btnHelp);

            s_background = (SCSEngine.Audio.Sound)resourceManager.GetResource<SCSEngine.Audio.Sound>(Shared.Resources.Sound_Background_1);
            updateButtonSound(Global.isMusicOff);
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
            Global.PlaySound_Button_Effect();
            Game.Exit();
        }

        void btnHelp_OnPressed(Button button)
        {
            this.Pause();
            Global.BoomMissionManager.RemoveCurrent();

            HelpScene helpScene = Global.BoomMissionManager.Bank.GetScreen(Shared.Macros.S_HELP) as HelpScene;
            helpScene.onInit();
            Global.BoomMissionManager.AddExclusive(helpScene);

            Global.PlaySound_Button_Effect();
        }

        void btnAbout_OnPressed(Button button)
        {
            this.Pause();
            Global.BoomMissionManager.RemoveCurrent();

            AboutScene aboutScene = Global.BoomMissionManager.Bank.GetScreen(Shared.Macros.S_ABOUT, true) as AboutScene;
            aboutScene.onInit();
            Global.BoomMissionManager.AddExclusive(aboutScene);

            Global.PlaySound_Button_Effect();
        }

        void btnOption_OnPressed(Button button)
        {
            // Stop/Resume music
            Global.PlaySound_Button_Effect();

            updateButtonSound(!Global.isMusicOff);
        }

        void updateButtonSound(bool toState)
        {
            if (toState)
            {
                Global.isMusicOff = toState;

                btnSoundOn.Visible = true;
                btnSoundOff.Visible = false;

                services.AudioManager.StopSound(s_background);
            }
            else
            {
                Global.isMusicOff = toState;

                btnSoundOn.Visible = false;
                btnSoundOff.Visible = true;

                services.AudioManager.PlaySound(s_background, true, Shared.Global.isMusicOff, Shared.Global.isMusicZuneOff);
            }
        }

        void btnPlay_OnPressed(Button button)
        {
            this.Pause();
            Global.BoomMissionManager.RemoveCurrent();

            GameModeScene gameModeScene = Global.BoomMissionManager.Bank.GetScreen(Shared.Macros.S_MODE, true) as GameModeScene;
            gameModeScene.onInit();
            Global.BoomMissionManager.AddExclusive(gameModeScene);

            Global.PlaySound_Button_Effect();
        }
    }
}
