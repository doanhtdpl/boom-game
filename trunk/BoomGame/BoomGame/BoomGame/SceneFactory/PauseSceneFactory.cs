using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCSEngine.ScreenManagement;

namespace BoomGame.SceneFactory
{
    public class PauseSceneFactory : IGameScreenFactory
    {
        IGameScreenManager manager;
        public PauseSceneFactory(IGameScreenManager manager)
        {
            this.manager = manager;
        }

        // Singleton
        public IGameScreen CreateGameScreen()
        {
            return new Scene.PauseScene(this.manager);
        }
    }
}
