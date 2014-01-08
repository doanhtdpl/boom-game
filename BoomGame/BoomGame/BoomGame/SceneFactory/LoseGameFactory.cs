using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCSEngine.ScreenManagement;

namespace BoomGame.SceneFactory
{
    public class LoseGameFactory : IGameScreenFactory
    {
        IGameScreenManager manager;
        public LoseGameFactory(IGameScreenManager manager)
        {
            this.manager = manager;
        }

        // Singleton
        public IGameScreen CreateGameScreen()
        {
            return new Scene.LoseGame(this.manager);
        }
    }
}
