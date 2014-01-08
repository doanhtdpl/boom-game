using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SSCEngine.GestureHandling;
using SSCEngine.GestureHandling.Implements.Events;
using Microsoft.Xna.Framework.Graphics;
using SCSEngine.Mathematics;
using SCSEngine.Services;
using SCSEngine.ResourceManagement;

namespace BoomGame.Extends
{
    public delegate void ControlEventHandler(Controller controller);

    public class Controller : DrawableGameComponent, IGestureTarget<FreeTap>
    {
        private Rectangle bound;

        protected SCSServices services;
        protected IResourceManager resourceManager;

        private double delayBeginScene = 500;

        public event ControlEventHandler OnLeftFreeTap;
        public event ControlEventHandler OnRightFreeTap;
        public event ControlEventHandler OnUpFreeTap;
        public event ControlEventHandler OnDownFreeTap;

        private bool isTap = false;
        protected Texture2D rangeButton;
        protected Vector2 rangeButtonPosition;


        public Controller(Game game)
            : base(game)
        {
        }

        public void onInit()
        {
            this.bound = new Rectangle(0, 0, 400, 480);

            services = (SCSServices)Game.Services.GetService(typeof(SCSServices));
            resourceManager = (IResourceManager)Game.Services.GetService(typeof(IResourceManager));

            rangeButton = resourceManager.GetResource<Texture2D>(Shared.Resources.ctrlCircle);
            rangeButtonPosition = new Vector2(130, 375);
        }

        public override void Update(GameTime gameTime)
        {
            if (delayBeginScene > 0)
                delayBeginScene -= gameTime.ElapsedGameTime.TotalMilliseconds;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if(isTap)
                services.SpriteBatch.Draw(rangeButton, rangeButtonPosition, Color.White);

            base.Draw(gameTime);
        }

        public bool IsGestureCompleted
        {
            get { return false; }
        }

        public uint Priority
        {
            get { return 0; }
        }

        public bool IsHandleGesture(FreeTap gEvent)
        {
            return bound.Contains((int)gEvent.Touch.Positions.Current.X, (int)gEvent.Touch.Positions.Current.Y);
        }

        public bool ReceivedGesture(FreeTap gEvent)
        {
            if (gEvent.Begin != gEvent.Current)
            {
                float angle = (GMath.Angle(new Vector2(1, 0), gEvent.Current - gEvent.Begin) * 180) / (float)Math.PI;

                if (angle <= 45 && angle >= -45)
                {
                    OnRightFreeTap(this);
                }
                else if (angle <= -45 && angle >= -135)
                {
                    OnUpFreeTap(this);
                }
                else if (angle <= -135 && angle >= -225)
                {
                    OnLeftFreeTap(this);
                }
                else
                {
                    OnDownFreeTap(this);
                }
            }

            return false;
        }
    }
}
