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
using BoomGame.Extends;
using SCSEngine.Mathematics;

namespace BoomGame.Layers
{
    public class DefaultInputLayer : DrawableGameComponent
    {
        private UIControlManager controlManager;
        private IGestureDispatcher dispatcher;

        protected SCSServices services;
        protected IResourceManager resourceManager;

        protected Texture2D rangeButton;
        protected Vector2 rangeButtonPosition;

        protected Controller controller;

        protected Button btnSpace;

        protected List<IGesturable> gesturables = new List<IGesturable>();

        public DefaultInputLayer(Game game)
            : base(game)
        {
            controlManager = new UIControlManager(game, DefaultGestureHandlingFactory.Instance);
            Global.GestureManager.AddDispatcher(controlManager);

            dispatcher = DefaultGestureHandlingFactory.Instance.CreateDispatcher();
            Global.GestureManager.AddDispatcher(dispatcher);
        }

        public void onInit()
        {
            services = (SCSServices)Game.Services.GetService(typeof(SCSServices));
            resourceManager = (IResourceManager)Game.Services.GetService(typeof(IResourceManager));

            rangeButton = resourceManager.GetResource<Texture2D>(Shared.Resources.ctrlCircle);
            rangeButtonPosition = new Vector2(130, 375);

            controller = new Controller(Game);
            controller.onInit(new Rectangle((int)rangeButtonPosition.X - 100, (int)rangeButtonPosition.Y - 100, rangeButton.Width + 200, rangeButton.Height + 200));

            btnSpace = new Button(Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.ctrlButtonSpace), resourceManager.GetResource<Texture2D>(Shared.Resources.ctrlButtonSpace));
            btnSpace.Canvas.Bound.Position = new Vector2(622, 392);
            btnSpace.FitSizeByImage();

            controller.OnLeftFreeTap += new ControlEventHandler(btnUp_OnLeftFreeTap);
            controller.OnRightFreeTap += new ControlEventHandler(controller_OnRightFreeTap);
            controller.OnUpFreeTap += new ControlEventHandler(controller_OnUpFreeTap);
            controller.OnDownFreeTap += new ControlEventHandler(controller_OnDownFreeTap);

            btnSpace.OnPressed += new ButtonEventHandler(btnSpace_onClick);

            dispatcher.AddTarget(controller);

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
            controller.Draw(gameTime);

            base.Draw(gameTime);
        }

        public void Pause()
        {
            Global.GestureManager.RemoveDispatcher(this.controlManager);
            Global.GestureManager.RemoveDispatcher(dispatcher);
        }

        public void UnPause()
        {
            Global.GestureManager.AddDispatcher(this.controlManager);
            Global.GestureManager.AddDispatcher(dispatcher);
        }

        public void Add(IGesturable obj)
        {
            gesturables.Add(obj);
        }

        public void Remove(IGesturable obj)
        {
            gesturables.Remove(obj);
        }

        void controller_OnDownFreeTap(Controller controller)
        {
            int direction = Shared.Constants.DIRECTION_DOWN;
            onController(direction);
        }

        void controller_OnUpFreeTap(Controller controller)
        {
            int direction = Shared.Constants.DIRECTION_UP;
            onController(direction);
        }

        void controller_OnRightFreeTap(Controller controller)
        {
            int direction = Shared.Constants.DIRECTION_RIGHT;
            onController(direction);
        }

        void btnUp_OnLeftFreeTap(Controller controller)
        {
            int direction = Shared.Constants.DIRECTION_LEFT;
            onController(direction);
        }

        private void onController(int dir)
        {
            for (int i = 0; i < gesturables.Count; ++i)
            {
                gesturables[i].GestureAffect(dir);
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