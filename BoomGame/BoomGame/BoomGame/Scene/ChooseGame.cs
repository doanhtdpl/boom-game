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
        private Texture2D chooseGameBar;
        private Vector2 chooseGameBarPosition;

        private Button btnNext;
        private Button btnPrev;

        private List<ModeItem> items = new List<ModeItem>();

        private UIControlManager controlManager;
        private IGestureDispatcher dispatcher;

        private SpriteFont font;

        public ChooseGame(IGameScreenManager manager)
            : base(manager)
        {
            services = (SCSServices)manager.Game.Services.GetService(typeof(SCSServices));

            resourceManager = (IResourceManager)manager.Game.Services.GetService(typeof(IResourceManager));
        }

        public void onInit()
        {
            this.Name = Shared.Macros.S_CHOOSEGAME;

            controlManager = new UIControlManager(Game, DefaultGestureHandlingFactory.Instance);
            Global.GestureManager.AddDispatcher(controlManager);

            dispatcher = DefaultGestureHandlingFactory.Instance.CreateDispatcher();
            Global.GestureManager.AddDispatcher(dispatcher);

            background = resourceManager.GetResource<Texture2D>(Shared.Resources.BackgroundChooseGame);
            chooseGameBar = resourceManager.GetResource<Texture2D>(Shared.Resources.BarChooseGame);
            chooseGameBarPosition = new Vector2(177, 391);

            // Init button next page and previous page
            btnNext = new Button(Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.BtnNext), resourceManager.GetResource<Texture2D>(Shared.Resources.BtnNPOver));
            btnNext.Canvas.Bound.Position = new Vector2(652, 394);
            btnNext.FitSizeByImage();
            btnNext.Visible = true;

            btnPrev = new Button(Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.BtnPrev), resourceManager.GetResource<Texture2D>(Shared.Resources.BtnNPOver));
            btnPrev.Canvas.Bound.Position = new Vector2(114, 394);
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
        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                onBackButton_pressed();
            }

            btnNext.Update(gameTime);
            btnPrev.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            services.SpriteBatch.Draw(background, Vector2.Zero, Color.White);
            services.SpriteBatch.Draw(chooseGameBar, Vector2.Zero, Color.White);

            for (int i = 0; i < items.Count; ++i)
            {
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
        }

        public void Unpause()
        {
            Global.GestureManager.AddDispatcher(this.controlManager);
            Global.GestureManager.AddDispatcher(this.dispatcher);
        }

        void btnPrev_OnPressed(Button button)
        {
            if ((Global.CurrentPage * Shared.Constants.NUMBER_RENDER) > 0)
            {
                --Global.CurrentPage;
                onChangePage();
            }
        }

        void btnNext_OnPressed(Button button)
        {
            if (((Global.CurrentPage+1) * Shared.Constants.NUMBER_RENDER) < Global.NumberOfMap)
            {
                ++Global.CurrentPage;
                onChangePage();
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
                        ModeItem item = new ModeItem(Game);
                        Vector2 ir = new Vector2();
                        ir.X = 70 + (i % (Shared.Constants.NUMBER_RENDER / 2)) * 150;
                        ir.Y = 80 + (int)i / (Shared.Constants.NUMBER_RENDER / 2) * 150;
                        item.onInit(services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.ItemChooseGame),
                                                    ir, font, (Global.CurrentPage * Shared.Constants.NUMBER_RENDER + i).ToString());
                        items.Add(item);
                        dispatcher.AddTarget(item);
                    }
                }
            }
            else
            {
                for (int i = 0; i < Shared.Constants.NUMBER_RENDER; i++)
                {
                    items[i].Text = (Global.CurrentPage * Shared.Constants.NUMBER_RENDER + i).ToString();
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
