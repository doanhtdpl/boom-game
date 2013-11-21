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

namespace BoomGame.Extends
{
    public delegate void ControlEventHandler(Controller controller);

    public class Controller : DrawableGameComponent, IGestureTarget<FreeTap>
    {
        private Rectangle bound;

        private double delayBeginScene = 500;

        public event ControlEventHandler OnLeftFreeTap;
        public event ControlEventHandler OnRightFreeTap;
        public event ControlEventHandler OnUpFreeTap;
        public event ControlEventHandler OnDownFreeTap;

        private bool breakTime = false;

        public Controller(Game game)
            : base(game)
        {
        }

        public void onInit(Rectangle bound)
        {
            this.bound = bound;
        }

        public override void Update(GameTime gameTime)
        {
            if (delayBeginScene > 0)
                delayBeginScene -= gameTime.ElapsedGameTime.TotalMilliseconds;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
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
