using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCSEngine.ScreenManagement;

namespace BoomGame.SceneFactory
{
    public class ChooseGameFactory : IGameScreenFactory
    {
        IGameScreenManager manager;
        public ChooseGameFactory(IGameScreenManager manager)
        {
            this.manager = manager;
        }

        // Singleton
        public IGameScreen CreateGameScreen()
        {
            return new Scene.ChooseGame(this.manager);
        }
    }
}
