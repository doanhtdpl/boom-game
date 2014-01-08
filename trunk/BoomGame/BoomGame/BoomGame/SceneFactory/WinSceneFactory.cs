using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCSEngine.ScreenManagement;

namespace BoomGame.SceneFactory
{
    public class WinSceneFactory : IGameScreenFactory
    {
        IGameScreenManager manager;
        public WinSceneFactory(IGameScreenManager manager)
        {
            this.manager = manager;
        }

        // Singleton
        public IGameScreen CreateGameScreen()
        {
            return new Scene.WinScene(this.manager);
        }
    }
}
