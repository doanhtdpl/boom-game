using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCSEngine.ScreenManagement;

namespace BoomGame.SceneFactory
{
    public class GameModeSceneFactory : IGameScreenFactory 
    {
        IGameScreenManager manager;
        public GameModeSceneFactory(IGameScreenManager manager)
        {
            this.manager = manager;
        }

        // Singleton
        public IGameScreen CreateGameScreen()
        {
            return new Scene.GameModeScene(this.manager);
        }
    }
}
