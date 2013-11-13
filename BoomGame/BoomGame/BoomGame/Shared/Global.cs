using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoomGame.Scene;
using Microsoft.Xna.Framework;

namespace BoomGame.Shared
{
    public static class Global
    {
        public static void Initialize(Game game)
        {
            BoomMissionManager = new TBBoomMissionManager(game);
        }

        public static TBBoomMissionManager BoomMissionManager;
    }
}
