using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCSEngine.ScreenManagement;

namespace BoomGame.Scene
{
    public class BasicGameSceneFactory : IGameScreenFactory
    {
        IGameScreenManager manager;
        public BasicGameSceneFactory(IGameScreenManager manager)
        {
            this.manager = manager;
        }

        // Singleton
        public IGameScreen CreateGameScreen()
        {
            return new BasicGameScene(this.manager);
        }
    }
}
