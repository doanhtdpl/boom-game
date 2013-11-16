using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SSCEngine.GestureHandling;
using SSCEngine.GestureHandling.Implements.Events;
using Microsoft.Xna.Framework;
using SSCEngine.Control;
using SCSEngine.Services;
using SCSEngine.ResourceManagement;
using Microsoft.Xna.Framework.Graphics;
using BoomGame.Entity;
using System.Diagnostics;
using BoomGame.Shared;

namespace BoomGame.Layers
{
    public class DefaultInputLayer : DrawableGameComponent
    {
        private UIControlManager controlManager;

        protected SCSServices services;
        protected IResourceManager resourceManager;

        protected Texture2D rangeButton;
        protected Vector2 rangeButtonPosition;

        protected Button btnUp;
        protected Button btnDown;
        protected Button btnLeft;
        protected Button btnRight;

        protected Button btnSpace;

        protected List<IGesturable> gesturables = new List<IGesturable>();

        public DefaultInputLayer(Game game)
            : base(game)
        {
            controlManager = new UIControlManager(game, DefaultGestureHandlingFactory.Instance);
            Global.GestureManager.AddDispatcher(controlManager);
        }

        public void onInit()
        {
            services = (SCSServices)Game.Services.GetService(typeof(SCSServices));
            resourceManager = (IResourceManager)Game.Services.GetService(typeof(IResourceManager));

            rangeButton = resourceManager.GetResource<Texture2D>(Shared.Resources.ctrlCircle);
            rangeButtonPosition = new Vector2(130, 375);

            btnUp = new Button(Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.ctrlButtonUp), resourceManager.GetResource<Texture2D>(Shared.Resources.ctrlButtonUp));
            btnUp.Canvas.Bound.Position = new Vector2(167, 382);
            btnUp.FitSizeByImage();
            btnDown = new Button(Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.ctrlButtonDown), resourceManager.GetResource<Texture2D>(Shared.Resources.ctrlButtonDown));
            btnDown.Canvas.Bound.Position = new Vector2(167, 439);
            btnDown.FitSizeByImage();
            btnLeft = new Button(Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.ctrlButtonLeft), resourceManager.GetResource<Texture2D>(Shared.Resources.ctrlButtonLeft));
            btnLeft.Canvas.Bound.Position = new Vector2(139, 411);
            btnLeft.FitSizeByImage();
            btnRight = new Button(Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.ctrlButtonRight), resourceManager.GetResource<Texture2D>(Shared.Resources.ctrlButtonRight));
            btnRight.Canvas.Bound.Position = new Vector2(194, 411);
            btnRight.FitSizeByImage();

            btnSpace = new Button(Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.ctrlButtonSpace), resourceManager.GetResource<Texture2D>(Shared.Resources.ctrlButtonSpace));
            btnSpace.Canvas.Bound.Position = new Vector2(622, 392);
            btnSpace.FitSizeByImage();

            btnUp.OnHold += new ButtonEventHandler(btnDirection_onHold);
            btnDown.OnHold += new ButtonEventHandler(btnDirection_onHold);
            btnLeft.OnHold += new ButtonEventHandler(btnDirection_onHold);
            btnRight.OnHold += new ButtonEventHandler(btnDirection_onHold);
            btnSpace.OnPressed += new ButtonEventHandler(btnSpace_onClick);

            controlManager.Add(btnUp);
            controlManager.Add(btnDown);
            controlManager.Add(btnLeft);
            controlManager.Add(btnRight);
            controlManager.Add(btnSpace);
        }

        public override void Update(GameTime gameTime)
        {
            controlManager.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            controlManager.Draw(gameTime);
            services.SpriteBatch.Draw(rangeButton, rangeButtonPosition, Color.White);

            base.Draw(gameTime);
        }

        public void Pause()
        {
            Global.GestureManager.RemoveDispatcher(this.controlManager);
        }

        public void UnPause()
        {
            Global.GestureManager.AddDispatcher(this.controlManager);
        }

        public void Add(IGesturable obj)
        {
            gesturables.Add(obj);
        }

        public void Remove(IGesturable obj)
        {
            gesturables.Remove(obj);
        }

        private void btnDirection_onHold(Button button)
        {
            int direction = Shared.Constants.DIRECTION_NONE;

            if (button == btnUp)
            {
                direction = Shared.Constants.DIRECTION_UP;
            }
            else if (button == btnDown)
            {
                direction = Shared.Constants.DIRECTION_DOWN;
            }
            else if (button == btnLeft)
            {
                direction = Shared.Constants.DIRECTION_LEFT;
            }
            else if (button == btnRight)
            {
                direction = Shared.Constants.DIRECTION_RIGHT;
            }

            for (int i = 0; i < gesturables.Count; ++i)
            {
                gesturables[i].GestureAffect(direction);
            }
        }

        private void btnSpace_onClick(Button button)
        {
            for (int i = 0; i < gesturables.Count; ++i)
            {
                gesturables[i].GestureAffect(Shared.Constants.BUTTON_EVENT_SPACE);
            }
        }
    }
}
