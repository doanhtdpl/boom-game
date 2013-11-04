using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SCSEngine.ScreenManagement.Implement
{
    public class BaseGameScreen : DrawableGameComponent, IGameScreen
    {
        public BaseGameScreen(IGameScreenManager manager)
            : base(manager.Game)
        {
            this.Manager = manager;
            this.Components = new GameComponentCollection();
        }

        private class UpdateOrderComparer : IComparer<IUpdateable>
        {
            private UpdateOrderComparer()
            {
            }

            public static UpdateOrderComparer Instance { get; private set; }

            static UpdateOrderComparer()
            {
                Instance = new UpdateOrderComparer();
            }

            #region IComparer<IUpdateable> Members

            public int Compare(IUpdateable x, IUpdateable y)
            {
                if (x.UpdateOrder > y.UpdateOrder)
                    return 1;
                else if (x.UpdateOrder == y.UpdateOrder)
                    return 0;

                return -1;
            }

            #endregion
        }

        public override void Update(GameTime gameTime)
        {
            List<IUpdateable> updateList = this.Components.OfType<IUpdateable>().ToList();
            updateList.Sort(UpdateOrderComparer.Instance);

            foreach (IUpdateable updateObject in updateList)
            {
                if (updateObject.Enabled)
                {
                    updateObject.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }


        private class DrawOrderComparer : IComparer<IDrawable>
        {
            private DrawOrderComparer()
            {
            }

            public static DrawOrderComparer Instance { get; private set; }

            static DrawOrderComparer()
            {
                Instance = new DrawOrderComparer();
            }

            #region IComparer<IDrawable> Members

            public int Compare(IDrawable x, IDrawable y)
            {
                if (x.DrawOrder > y.DrawOrder)
                    return 1;
                else if (x.DrawOrder == y.DrawOrder)
                    return 0;

                return -1;
            }

            #endregion
        }

        public override void Draw(GameTime gameTime)
        {
            List<IDrawable> DrawList = this.Components.OfType<IDrawable>().ToList();
            DrawList.Sort(DrawOrderComparer.Instance);

            foreach (IDrawable DrawObject in DrawList)
            {
                if (DrawObject.Visible)
                {
                    DrawObject.Draw(gameTime);
                }
            }

            base.Draw(gameTime);
        }

        #region IGameScreen Members

        public string Name { get; internal set; }

        public GameComponentCollection Components { get; protected set; }

        public GameScreenState State
        {
            get
            {
                if (this.Enabled && this.Visible)
                {
                    return GameScreenState.Activated;
                }
                else if (!this.Enabled && this.Visible)
                {
                    return GameScreenState.Paused;
                }

                return GameScreenState.Deactivated;
            }
            set
            {
                if (this.State == value)
                    return;

                switch (value)
                {
                    case GameScreenState.Activated:
                        this.Enabled = true;
                        this.Visible = true;
                        break;
                    case GameScreenState.Paused:
                        this.Enabled = false;
                        this.Visible = true;
                        break;
                    case GameScreenState.Deactivated:
                        this.Enabled = false;
                        this.Visible = false;
                        break;
                    default:
                        break;
                }
            }
        }

        public IGameScreenManager Manager { get; private set; }

        protected virtual void OnStateChanged()
        {
        }

        #endregion
    }
}
