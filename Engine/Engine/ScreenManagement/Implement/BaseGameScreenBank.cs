using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCSEngine.ScreenManagement.Implement
{
    public class BaseGameScreenBank : IGameScreenBank
    {
        private Dictionary<string, IGameScreenFactory> factories = new Dictionary<string,IGameScreenFactory>();
        private Dictionary<string, IGameScreen> screens = new Dictionary<string, IGameScreen>();

        #region IGameScreenBank Members

        public IGameScreen GetScreen(string name)
        {
            if (!this.screens.ContainsKey(name) && this.factories.ContainsKey(name))
            {
                this.screens.Add(name, factories[name].CreateGameScreen());
            }

            if (this.screens.ContainsKey(name))
            {
                return this.screens[name];
            }

            return null;
        }

        public IGameScreen GetNewScreen(string name)
        {
            if (this.factories.ContainsKey(name))
            {
                if (this.screens.ContainsKey(name))
                {
                    this.screens.Remove(name);
                }
                this.screens.Add(name, factories[name].CreateGameScreen());
            }

            if (this.screens.ContainsKey(name))
            {
                return this.screens[name];
            }

            return null;
        }

        public IGameScreen GetScreen(string name, bool createNew)
        {
            if (createNew)
                return this.GetNewScreen(name);

            return this.GetScreen(name);
        }

        public void AddScreenFactory(string name, IGameScreenFactory gsFactory)
        {
            this.factories.Add(name, gsFactory);
        }

        #endregion
    }
}
