using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCSEngine.ScreenManagement;

namespace BoomGame.SceneFactory
{
    public class MiniGameLimitBombFactory : IGameScreenFactory
    {
        IGameScreenManager manager;
        public MiniGameLimitBombFactory(IGameScreenManager manager)
        {
            this.manager = manager;
        }

        // Singleton
        public IGameScreen CreateGameScreen()
        {
            return new Scene.MiniGameLimitBomb(this.manager);
        }
    }
}
