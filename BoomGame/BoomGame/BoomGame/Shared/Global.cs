using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoomGame.Scene;
using Microsoft.Xna.Framework;
using SSCEngine.GestureHandling;
using SCSEngine.ScreenManagement;

namespace BoomGame.Shared
{
    public static class Global
    {
        public static void Initialize(Game game)
        {
            BoomMissionManager = new TBBoomMissionManager(game);
        }

        public static TBBoomMissionManager BoomMissionManager;
        public static IGestureManager GestureManager;

        public static int Basic_Level = 0;
        public static int Mini_Level = 0;

        // Choose Scene
        public static int CurrentPage = 0;
        public static int NumberOfMap = 100;

        // Game Mode
        public static String CurrentMode = "";

        public static void CreateCurrentMap(int level)
        {
            String tailFix = ".txt";
            IGameScreen basic = null;

            if (Global.CurrentMode == Shared.Constants.BASIC_MODE)
            {
                basic = Global.BoomMissionManager.Bank.GetScreen(Shared.Macros.S_BASIC, true) as BoomGame.Scene.BasicGameScene;
                (basic as BasicGameScene).onInit(Shared.Constants.BASIC_GAME_MAP_PATH + level.ToString() + tailFix); 
            }
            else if (Global.CurrentMode == Shared.Constants.TIME_MODE)
            {
                basic = Global.BoomMissionManager.Bank.GetScreen(Shared.Macros.S_MINI_TIME, true) as BoomGame.Scene.MiniGameTime;
                (basic as MiniGameTime).onInit(Shared.Constants.TIME_GAME_MAP_PATH + level.ToString() + tailFix, 90);
            }
            else
            {
                basic = Global.BoomMissionManager.Bank.GetScreen(Shared.Macros.S_MINI_LIMIT, true) as BoomGame.Scene.MiniGameLimitBomb;
                (basic as MiniGameLimitBomb).onInit(Shared.Constants.LIMIT_GAME_MAP_PATH + level.ToString() + tailFix, 10);
            }
            Global.BoomMissionManager.AddExclusive(basic);
        }

        // Game Play - Basic
        public static int Counter_Enemy = 0;
        public static int Counter_Obstacle = 0;
        public static int Counter_Item = 0;
        public static int Counter_Bomber = 0;

        // Game Play - Mini
        public static int Bomb_Number = 0;
    }
}
