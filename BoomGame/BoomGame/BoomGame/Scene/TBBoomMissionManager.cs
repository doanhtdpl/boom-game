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
//             BasicGameSceneFactory basicFactory = new BasicGameSceneFactory(this);
//             this.Bank.AddScreenFactory("Basic", basicFactory);

            ChooseGameFactory chooseGameFactory = new ChooseGameFactory(this);
            this.Bank.AddScreenFactory(Shared.Macros.S_CHOOSEGAME, chooseGameFactory);
        }
    }
}
