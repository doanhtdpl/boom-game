using Microsoft.Xna.Framework;
using SSCEngine.GestureHandling;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SSCEngine.Control
{
    public class UIControlManager : DrawableGameComponent, IGestureDispatcher, IUIContainer
    {
        private List<IUIControl> controls = new List<IUIControl>();
        public IEnumerable<IUIControl> Components
        {
          get { return controls; }
        }

        private IGestureDispatcher gestureDispatcher;
        public IGestureDispatcher GestureDispatcher
        {
            get { return gestureDispatcher; }
        }

        public void Add(IUIControl control)
        {
            this.controls.Add(control);
            control.RegisterGestures(this);
        }

        public void Remove(IUIControl control)
        {
            if (this.controls.Remove(control))
            {
                control.LeaveGestures(this);
            }
        }

        public UIControlManager(Game game, IGestureHandlingFactory ghF)
            : base(game)
        {
            this.gestureDispatcher = ghF.CreateDispatcher();
        }

        public void AddTarget<GestureEvent>(IGestureTarget<GestureEvent> gTarget) where GestureEvent : IGestureEvent
        {
            this.gestureDispatcher.AddTarget<GestureEvent>(gTarget);
        }
        public void RemoveTarget<GestureEvent>(IGestureTarget<GestureEvent> gTarget) where GestureEvent : IGestureEvent
        {
            this.gestureDispatcher.RemoveTarget<GestureEvent>(gTarget);
        }
        public void Dispatch(IGestureEvent gEvent)
        {
            this.gestureDispatcher.Dispatch(gEvent);
        }

        public override void Update(GameTime gameTime)
        {
            var updateableControls = this.controls.OfType<IUpdateable>();
            foreach (var updateable in updateableControls)
            {
                updateable.Update(gameTime);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var control in this.controls)
            {
                control.Draw(gameTime);
            }

            base.Draw(gameTime);
        }
    }
}
