using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCSEngine.ScreenManagement;

namespace BoomGame.SceneFactory
{
    public class AboutSceneFactory : IGameScreenFactory
    {
        IGameScreenManager manager;
        public AboutSceneFactory(IGameScreenManager manager)
        {
            this.manager = manager;
        }

        // Singleton
        public IGameScreen CreateGameScreen()
        {
            return new Scene.AboutScene(this.manager);
        }
    }
}
