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

        public ChooseGame(IGameScreenManager manager)
            : base(manager)
        {
            services = (SCSServices)manager.Game.Services.GetService(typeof(SCSServices));

            resourceManager = (IResourceManager)manager.Game.Services.GetService(typeof(IResourceManager));
        }

        public void onInit()
        {
            controlManager = new UIControlManager(Game, DefaultGestureHandlingFactory.Instance);
            Global.GestureManager.AddDispatcher(controlManager);

            dispatcher = DefaultGestureHandlingFactory.Instance.CreateDispatcher();
            Global.GestureManager.AddDispatcher(dispatcher);

            background = resourceManager.GetResource<Texture2D>(Shared.Resources.Menu_Background);

            // Init button next page and previous page
            btnNext = new Button(Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.Choose_NextButton), resourceManager.GetResource<Texture2D>(Shared.Resources.Choose_ButtonOver));
            btnNext.Canvas.Bound.Position = new Vector2(500, 380);
            btnNext.FitSizeByImage();
            btnNext.Visible = true;

            btnPrev = new Button(Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.Choose_PrevButton), resourceManager.GetResource<Texture2D>(Shared.Resources.Choose_ButtonOver));
            btnPrev.Canvas.Bound.Position = new Vector2(50, 380);
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
            for(int i = 0; i < Shared.Constants.NUMBER_RENDER; i++)
			{
                ModeItem item = new ModeItem(Game);
                Vector2 ir = new Vector2();
                ir.X = 40 + (i % (Shared.Constants.NUMBER_RENDER / 2)) * 215;
                ir.Y = 80 + (int)i / (Shared.Constants.NUMBER_RENDER / 2) * 205;
                item.onInit(services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.Choose_TileChoose),
                                            ir, font, (Global.CurrentPage * Shared.Constants.NUMBER_RENDER + i).ToString());
                items.Add(item);
                dispatcher.AddTarget<Tap>(item);
			}
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
            items.Clear();
			for(int i = 0; i < Shared.Constants.NUMBER_RENDER; i++)
			{
				if((Global.CurrentPage * Shared.Constants.NUMBER_RENDER) + i < Global.NumberOfMap)
                {
                    ModeItem item = new ModeItem(Game);
                    Vector2 ir = new Vector2();
                    ir.X = 40 + (i % (Shared.Constants.NUMBER_RENDER / 2)) * 215;
                    ir.Y = 80 + (int)i / (Shared.Constants.NUMBER_RENDER / 2) * 205;
                    item.onInit(services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.Choose_TileChoose), 
                                                ir, font, (Global.CurrentPage * Shared.Constants.NUMBER_RENDER + i).ToString());
                    items.Add(item);
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
        }
    }
}
