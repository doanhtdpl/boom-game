using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SCSEngine.ServiceManagement;
namespace SCSEngine.ScreenManagement.Implement
{
    public class BaseGameScreenManager : DrawableGameComponent, IGameScreenManager
    {
        public BaseGameScreenManager(Game game, IGameScreenManagerFactory gsFactory)
            : base(game)
        {
            this.SaveScreen = false;
            this.Screens = new List<IGameScreen>();
            this.Bank = gsFactory.CreateScreenBank();
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = this.Screens.Count - 1; i >= 0; --i)
            {
                if (this.Screens[i].Enabled)
                {
                    this.Screens[i].Update(gameTime);
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            for (int i = this.Screens.Count - 1; i >= 0; --i)
            {
                if (this.Screens[i].Visible)
                {
                    this.Screens[i].Draw(gameTime);
                }
            }

            base.Draw(gameTime);
        }

        #region IGameScreenManager Members

        public bool SaveScreen { get; set; }
        public IGameScreen Current
        {
            get
            {
                if (this.Screens.Count > 0)
                {
                    return this.Screens.Last();
                }

                return null;
            }
        }

        public List<IGameScreen> Screens { get; private set; }

        public IGameScreenBank Bank { get; private set; }

        public void AddExclusive(IGameScreen screen)
        {
            if (this.Current != null)
            {
                this.Current.State = GameScreenState.Deactivated;
            }

            screen.State = GameScreenState.Activated;
            this.AddScreen(screen);
        }

        public void AddPopup(IGameScreen screen)
        {
            if (this.Current != null)
            {
                this.Current.State = GameScreenState.Paused;
            }

            screen.State = GameScreenState.Activated;
            this.AddScreen(screen);
        }

        public void AddImmediately(IGameScreen screen)
        {
            this.AddScreen(screen);
        }

        public void RemoveCurrent()
        {
            this.Current.State = GameScreenState.Deactivated;
            this.Screens.RemoveAt(this.Screens.Count - 1);

            if (this.Screens.Count > 0)
            {
                this.Current.State = GameScreenState.Activated;
            }
        }

        public void RemoveScreen(IGameScreen screen)
        {
            for (int i = 0; i < this.Screens.Count; )
            {
                if (this.Screens[i].Name == screen.Name)
                {
                    this.Screens.RemoveAt(i);
                }
                else
                {
                    ++i;
                }
            }
        }

        #endregion

        private void AddScreen(IGameScreen screen)
        {
            if (this.SaveScreen)
            {
                this.RemoveScreen(screen);
            }

            this.Screens.Add(screen);
        }
    }
}
