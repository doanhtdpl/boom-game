using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCSEngine.ScreenManagement;

namespace BoomGame.SceneFactory
{
    public class MiniGameTimeFactory : IGameScreenFactory
    {
        IGameScreenManager manager;
        public MiniGameTimeFactory(IGameScreenManager manager)
        {
            this.manager = manager;
        }

        // Singleton
        public IGameScreen CreateGameScreen()
        {
            return new Scene.MiniGameTime(this.manager);
        }
    }
}
