using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCSEngine.ScreenManagement;

namespace BoomGame.SceneFactory
{
    public class MenuSceneFactory : IGameScreenFactory
    {
        IGameScreenManager manager;
        public MenuSceneFactory(IGameScreenManager manager)
        {
            this.manager = manager;
        }

        // Singleton
        public IGameScreen CreateGameScreen()
        {
            return new Scene.MenuScene(this.manager);
        }
    }
}
