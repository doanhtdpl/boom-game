using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCSEngine.ScreenManagement.Implement;
using Microsoft.Xna.Framework;
using BoomGame.SceneFactory;

namespace BoomGame.Scene
{
    public class TBBoomMissionManager : BaseGameScreenManager
    {
        public TBBoomMissionManager(Game game)
            : base(game, BaseGameScreenManagerFactory.Instance)
        {
            MenuSceneFactory menuFactory = new MenuSceneFactory(this);
            this.Bank.AddScreenFactory(Shared.Macros.S_MENU, menuFactory);

            GameModeSceneFactory gameModeFactory = new GameModeSceneFactory(this);
            this.Bank.AddScreenFactory(Shared.Macros.S_MODE, gameModeFactory);

            ChooseGameFactory chooseGameFactory = new ChooseGameFactory(this);
            this.Bank.AddScreenFactory(Shared.Macros.S_CHOOSEGAME, chooseGameFactory);

            BasicGameSceneFactory basicGameFactory = new BasicGameSceneFactory(this);
            this.Bank.AddScreenFactory(Shared.Macros.S_BASIC, basicGameFactory);

            MiniGameLimitBombFactory miniLimitGameFactory = new MiniGameLimitBombFactory(this);
            this.Bank.AddScreenFactory(Shared.Macros.S_MINI_LIMIT, miniLimitGameFactory);

            MiniGameTimeFactory miniTimeGameFactory = new MiniGameTimeFactory(this);
            this.Bank.AddScreenFactory(Shared.Macros.S_MINI_TIME, miniTimeGameFactory);

            AboutSceneFactory aboutFactory = new AboutSceneFactory(this);
            this.Bank.AddScreenFactory(Shared.Macros.S_ABOUT, aboutFactory);

            HelpSceneFactory helpFactory = new HelpSceneFactory(this);
            this.Bank.AddScreenFactory(Shared.Macros.S_HELP, helpFactory);

            PauseSceneFactory pauseFactory = new PauseSceneFactory(this);
            this.Bank.AddScreenFactory(Shared.Macros.S_PAUSE, pauseFactory);

            WinSceneFactory winFactory = new WinSceneFactory(this);
            this.Bank.AddScreenFactory(Shared.Macros.S_WIN, winFactory);

            LoseGameFactory loseFactory = new LoseGameFactory(this);
            this.Bank.AddScreenFactory(Shared.Macros.S_LOSE, loseFactory);
        }
    }
}
