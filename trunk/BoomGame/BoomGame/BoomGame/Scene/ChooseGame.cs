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
    public class ChooseGame : BaseGameScreen
    {
        private SCSServices services;
        private IResourceManager resourceManager;

        private Texture2D background;

        private Button btnNext;
        private Button btnPrev;

        private List<ModeItem> items = new List<ModeItem>();

        private UIControlManager controlManager;
        private IGestureDispatcher dispatcher;

        private SpriteFont font;

        private double delayBeginScene = 1000;

        public static SCSEngine.Audio.Sound s_background;

        private static int currentLevel = 0;

        public ChooseGame(IGameScreenManager manager)
            : base(manager)
        {
            services = (SCSServices)manager.Game.Services.GetService(typeof(SCSServices));

            resourceManager = (IResourceManager)manager.Game.Services.GetService(typeof(IResourceManager));
        }

        public void onInit()
        {
            SaveLoadGame.LoadLevel(Global.CurrentMode, out currentLevel);

            // Get the number of map for initialize menu choose level
            Global.GetNumberOfMap();

            // Save this name
            this.Name = Shared.Macros.S_CHOOSEGAME;

            // Loading background
            background = resourceManager.GetResource<Texture2D>(Shared.Resources.BackgroundChooseGame);

            // Create UI manager for button next & previous
            controlManager = new UIControlManager(Game, DefaultGestureHandlingFactory.Instance);
            Global.GestureManager.AddDispatcher(controlManager);

            // Dispatcher for menu item
            dispatcher = DefaultGestureHandlingFactory.Instance.CreateDispatcher();
            Global.GestureManager.AddDispatcher(dispatcher);

            // Init button next page and previous page
            btnNext = new Button(Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.BtnNext), resourceManager.GetResource<Texture2D>(Shared.Resources.BtnNext));
            btnNext.Canvas.Bound.Position = new Vector2(708.0f, 110.0f);
            btnNext.FitSizeByImage();
            btnNext.Visible = true;

            btnPrev = new Button(Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.BtnPrev), resourceManager.GetResource<Texture2D>(Shared.Resources.BtnPrev));
            btnPrev.Canvas.Bound.Position = new Vector2(-8.0f, 110.0f);
            btnPrev.FitSizeByImage();
            btnPrev.Visible = false;

            btnNext.OnPressed += new ButtonEventHandler(btnNext_OnPressed);
            btnPrev.OnPressed += new ButtonEventHandler(btnPrev_OnPressed);

            controlManager.Add(btnNext);
            controlManager.Add(btnPrev);

            // Load font tile
            font = (SpriteFont)resourceManager.GetResource<SpriteFont>(Shared.Resources.Choose_TileFont);

            // Reset to first page
            Global.CurrentPage = 0;

            // Init first page
            onChangePage();

            s_background = (SCSEngine.Audio.Sound)resourceManager.GetResource<SCSEngine.Audio.Sound>(Shared.Resources.Sound_Background_5);
            s_background.IsLooped = true;
            services.AudioManager.PlaySound(s_background, Global.isMusicOff, Global.isMusicZuneOff);
        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                onBackButton_pressed();
            }

            if (delayBeginScene > 0)
                delayBeginScene -= gameTime.ElapsedGameTime.TotalMilliseconds;

            btnNext.Update(gameTime);
            btnPrev.Update(gameTime);

            for (int i = 0; i < items.Count; ++i)
            {
                items[i].Update(gameTime);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            services.SpriteBatch.Draw(background, Vector2.Zero, Color.White);

            for (int i = 0; i < items.Count; ++i)
            {
                if(items[i].Visible)
                    items[i].Draw(gameTime);
            }

            if(btnNext.Visible)
                btnNext.Draw(gameTime);
            if(btnPrev.Visible)
                btnPrev.Draw(gameTime);

            base.Draw(gameTime);
        }

        public void Pause()
        {
            Global.GestureManager.RemoveDispatcher(this.controlManager);
            Global.GestureManager.RemoveDispatcher(this.dispatcher);

            services.AudioManager.StopSound(s_background);
        }

        public void Unpause()
        {
            Global.GestureManager.AddDispatcher(this.controlManager);
            Global.GestureManager.AddDispatcher(this.dispatcher);
        }

        void btnPrev_OnPressed(Button button)
        {
            if (delayBeginScene <= 0 && (Global.CurrentPage * Shared.Constants.NUMBER_RENDER) > 0)
            {
                --Global.CurrentPage;
                onChangePage();

                Global.PlaySound_Button_Effect();
            }
        }

        void btnNext_OnPressed(Button button)
        {
            if (delayBeginScene <= 0 && ((Global.CurrentPage + 1) * Shared.Constants.NUMBER_RENDER) < Global.NumberOfMap)
            {
                ++Global.CurrentPage;
                onChangePage();

                Global.PlaySound_Button_Effect();
            }
        }

        void onChangePage()
        {
            if (items.Count == 0)
            {
                for (int i = 0; i < Shared.Constants.NUMBER_RENDER; i++)
                {
                    if ((Global.CurrentPage * Shared.Constants.NUMBER_RENDER) + i < Global.NumberOfMap)
                    {
                        bool isLock = (Global.CurrentPage * Shared.Constants.NUMBER_RENDER) + i > currentLevel;
                        ModeItem item = new ModeItem(Game);
                        Vector2 ir = new Vector2();
                        ir.X = 106 + (i % (Shared.Constants.NUMBER_RENDER / 2)) * 123;
                        ir.Y = 38 + (int)i / (Shared.Constants.NUMBER_RENDER / 2) * 146;
                        item.onInit(services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.ItemChooseGame),
                                                    ir, font, (Global.CurrentPage * Shared.Constants.NUMBER_RENDER + i).ToString(), isLock);
                        items.Add(item);
                        dispatcher.AddTarget(item);
                    }
                }
            }
            else
            {
                for (int i = 0; i < Shared.Constants.NUMBER_RENDER; i++)
                {
                    if ((Global.CurrentPage * Shared.Constants.NUMBER_RENDER) + i < Global.NumberOfMap)
                    {
                        items[i].Text = (Global.CurrentPage * Shared.Constants.NUMBER_RENDER + i).ToString();
                        items[i].IsLock = (Global.CurrentPage * Shared.Constants.NUMBER_RENDER) + i > currentLevel;
                        items[i].Visible = true;
                    }
                    else
                    {
                        items[i].Visible = false;
                    }
                }
            }

            if ((Global.CurrentPage+1) * Shared.Constants.NUMBER_RENDER >= Global.NumberOfMap)
				btnNext.Visible	= false;
			else
                btnNext.Visible = true;

            if (Global.CurrentPage * Shared.Constants.NUMBER_RENDER <= 0)
				btnPrev.Visible	= false;
			else
                btnPrev.Visible = true;
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
