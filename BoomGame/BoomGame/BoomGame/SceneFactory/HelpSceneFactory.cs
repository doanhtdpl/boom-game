using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCSEngine.ScreenManagement;

namespace BoomGame.SceneFactory
{
    public class HelpSceneFactory : IGameScreenFactory
    {
        IGameScreenManager manager;
        public HelpSceneFactory(IGameScreenManager manager)
        {
            this.manager = manager;
        }

        // Singleton
        public IGameScreen CreateGameScreen()
        {
            return new Scene.HelpScene(this.manager);
        }
    }
}
