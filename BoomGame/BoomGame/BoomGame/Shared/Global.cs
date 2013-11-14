using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoomGame.Scene;
using Microsoft.Xna.Framework;
using SSCEngine.GestureHandling;

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
            BoomGame.Scene.BasicGameScene basic = 
                Global.BoomMissionManager.Bank.GetScreen(Shared.Macros.S_BASIC, true) as BoomGame.Scene.BasicGameScene;

            if (Global.CurrentMode == Shared.Constants.BASIC_MODE)
            {
                basic.onInit(Shared.Constants.BASIC_GAME_MAP_PATH + level.ToString() + tailFix);
            }
            else if (Global.CurrentMode == Shared.Constants.MINI_MODE)
            {
                basic.onInit(Shared.Constants.MINI_GAME_MAP_PATH + level.ToString() + tailFix);
            }
            Global.BoomMissionManager.AddExclusive(basic);
        }

        // Game Play
        public static int Counter_Enemy = 0;
        public static int Counter_Obstacle = 0;
        public static int Counter_Item = 0;
        public static int Counter_Bomber = 0;
    }
}
