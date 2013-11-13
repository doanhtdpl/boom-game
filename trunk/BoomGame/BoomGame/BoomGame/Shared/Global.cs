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
    }
}
