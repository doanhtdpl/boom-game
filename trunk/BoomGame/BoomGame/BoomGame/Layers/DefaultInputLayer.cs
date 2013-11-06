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

namespace BoomGame.Layers
{
    public class DefaultInputLayer : DrawableGameComponent
    {
        private IGestureManager igm;
        private UIControlManager controlManager;

        protected SCSServices services;
        protected IResourceManager resourceManager;

        protected Button btnUp;
        protected Button btnDown;
        protected Button btnLeft;
        protected Button btnRight;

        protected List<IGesturable> gesturables = new List<IGesturable>();

        Texture2D tex;
        Vector2 position;

        GameTime time;

        int counter = 0;

        public DefaultInputLayer(Game game)
            : base(game)
        {
            igm = DefaultGestureHandlingFactory.Instance.CreateManager(game);
            DefaultGestureHandlingFactory.Instance.InitDetectors(igm);

            controlManager = new UIControlManager(game, DefaultGestureHandlingFactory.Instance);
            igm.AddDispatcher(controlManager);
        }

        public void onInit()
        {
            services = (SCSServices)Game.Services.GetService(typeof(SCSServices));
            resourceManager = (IResourceManager)Game.Services.GetService(typeof(IResourceManager));

            btnUp = new Button(Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.ButtonUp), resourceManager.GetResource<Texture2D>(Shared.Resources.ButtonUp));
            btnUp.Canvas.Bound.Position = new Vector2(100, 350);
            btnUp.FitSizeByImage();
            btnDown = new Button(Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.ButtonDown), resourceManager.GetResource<Texture2D>(Shared.Resources.ButtonDown));
            btnDown.Canvas.Bound.Position = new Vector2(100, 420);
            btnDown.FitSizeByImage();
            btnLeft = new Button(Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.ButtonLeft), resourceManager.GetResource<Texture2D>(Shared.Resources.ButtonLeft));
            btnLeft.Canvas.Bound.Position = new Vector2(40, 380);
            btnLeft.FitSizeByImage();
            btnRight = new Button(Game, services.SpriteBatch, resourceManager.GetResource<Texture2D>(Shared.Resources.ButtonRight), resourceManager.GetResource<Texture2D>(Shared.Resources.ButtonRight));
            btnRight.Canvas.Bound.Position = new Vector2(140, 380);
            btnRight.FitSizeByImage();

            btnUp.OnHold += new ButtonEventHandler(btnDirection_onHold);
            btnDown.OnHold += new ButtonEventHandler(btnDirection_onHold);
            btnLeft.OnHold += new ButtonEventHandler(btnDirection_onHold);
            btnRight.OnHold += new ButtonEventHandler(btnDirection_onHold);

            controlManager.Add(btnUp);
            controlManager.Add(btnDown);
            controlManager.Add(btnLeft);
            controlManager.Add(btnRight);

            tex = resourceManager.GetResource<Texture2D>("HD/box@2x");
            position = new Vector2(0, 0);
        }

        public override void Update(GameTime gameTime)
        {
            igm.Update(gameTime);
            time = gameTime;
            controlManager.Update(gameTime);

            base.Update(gameTime);

            counter++;
        }

        public override void Draw(GameTime gameTime)
        {
            controlManager.Draw(gameTime);
            services.SpriteBatch.Draw(tex, position, Color.White);
            base.Draw(gameTime);
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
                position.Y -= 0.1f * (float)time.ElapsedGameTime.TotalMilliseconds;
            }
            else if (button == btnDown)
            {
                direction = Shared.Constants.DIRECTION_DOWN;
                position.Y += 0.1f * (float)time.ElapsedGameTime.TotalMilliseconds;
            }
            else if (button == btnLeft)
            {
                direction = Shared.Constants.DIRECTION_LEFT;
                position.X -= 0.1f * (float)time.ElapsedGameTime.TotalMilliseconds;
            }
            else if (button == btnRight)
            {
                direction = Shared.Constants.DIRECTION_RIGHT;
                position.X += 0.1f * (float)time.ElapsedGameTime.TotalMilliseconds;
            }

            for (int i = 0; i < gesturables.Count; ++i)
            {
                //gesturables[i].GestureAffect(direction);
            }

            Debug.WriteLine(position.X + "," + position.Y);
            Debug.WriteLine(counter);
            Debug.WriteLine(time.ElapsedGameTime.TotalMilliseconds);
        }
    }
}
