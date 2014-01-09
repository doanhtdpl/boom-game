using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoomGame.Scene;
using Microsoft.Xna.Framework;
using SSCEngine.GestureHandling;
using SCSEngine.ScreenManagement;
using SCSEngine.Services.Audio;
using SCSEngine.Services;
using SCSEngine.ResourceManagement;
using SCSEngine.Mathematics;
using System.Diagnostics;

namespace BoomGame.Shared
{
    public static class Global
    {
        public static void Initialize(Game game)
        {
            BoomMissionManager = new TBBoomMissionManager(game);
            Game = game;
            Services = (SCSServices)game.Services.GetService(typeof(SCSServices));
            ResourceManager = (IResourceManager)game.Services.GetService(typeof(IResourceManager));
        }

        // Game instances
        public static Game Game;
        public static SCSServices Services;
        public static IResourceManager ResourceManager;

        public static TBBoomMissionManager BoomMissionManager;
        public static IGestureManager GestureManager;

        // Sound
        public static bool isMusicOff = false;
        public static bool isMusicZuneOff = true;
        public static void PlaySound_Button_Effect()
        {
            SCSEngine.Audio.Sound button_hit = ResourceManager.GetResource<SCSEngine.Audio.Sound>(Shared.Resources.Sound_Hard_Hit);
            Services.AudioManager.PlaySound(button_hit, Shared.Global.isMusicOff, Shared.Global.isMusicZuneOff);
        }
        public static void PlaySoundEffect(String soundName)
        {
            SCSEngine.Audio.Sound sound = ResourceManager.GetResource<SCSEngine.Audio.Sound>(soundName);
            Services.AudioManager.PlaySound(sound, Shared.Global.isMusicOff, Shared.Global.isMusicZuneOff);
        }
        public static String RandomBackgroundSong()
        {
            int index = GRandom.RandomInt(2, 7);
            String result = Shared.Resources.Sound_Background_2;
            switch (index)
            {
                case 2:
                    result = Shared.Resources.Sound_Background_2;
                    break;
                case 3:
                    result = Shared.Resources.Sound_Background_3;
                    break;
                case 4:
                    result = Shared.Resources.Sound_Background_4;
                    break;
                case 5:
                    result = Shared.Resources.Sound_Background_5;
                    break;
                case 6:
                    result = Shared.Resources.Sound_Background_6;
                    break;
            }
            return result;
        }

        // Game logic
        public static int Basic_Level = 0;
        public static int Mini_Level = 0;

        // Choose Scene
        public static int CurrentPage = 0;
        public static int NumberOfMap = 0;

        // Game Mode
        public static float Bomber_Start_Position_X = 0;
        public static float Bomber_Start_Position_Y = 0;

        public static int TotalCoin = 0;
        public static String CurrentMode = "";

        public static String CurrentMap = "";
        public static void CreateCurrentMap(int level)
        {
            String tailFix = ".txt";
            IGameScreen basic = null;

            switch (Global.CurrentMode)
            {
                case Shared.Constants.BASIC_MODE:
                    basic = Global.BoomMissionManager.Bank.GetScreen(Shared.Macros.S_BASIC, true) as BoomGame.Scene.BasicGameScene;
                    (basic as BasicGameScene).onInit(Shared.Constants.BASIC_GAME_MAP_PATH + level.ToString() + tailFix); 
                    break;

                case Shared.Constants.TIME_MODE:
                    basic = Global.BoomMissionManager.Bank.GetScreen(Shared.Macros.S_MINI_TIME, true) as BoomGame.Scene.MiniGameTime;
                    (basic as MiniGameTime).onInit(Shared.Constants.TIME_GAME_MAP_PATH + level.ToString() + tailFix, 90);
                    break;

                case Shared.Constants.LIMIT_MODE:
                    basic = Global.BoomMissionManager.Bank.GetScreen(Shared.Macros.S_MINI_LIMIT, true) as BoomGame.Scene.MiniGameLimitBomb;
                    (basic as MiniGameLimitBomb).onInit(Shared.Constants.LIMIT_GAME_MAP_PATH + level.ToString() + tailFix, 10);
                    break;
            }
            Global.BoomMissionManager.AddExclusive(basic);
        }
        public static void GetNumberOfMap()
        {
            String path = "";
            switch (Global.CurrentMode)
            {
                case Shared.Constants.BASIC_MODE:
                    path = Shared.Constants.BASIC_GAME_MAP_LIST;
                    break;

                case Shared.Constants.TIME_MODE:
                    path = Shared.Constants.TIME_GAME_MAP_LIST;
                    break;

                case Shared.Constants.LIMIT_MODE:
                    path = Shared.Constants.LIMIT_GAME_MAP_LIST;
                    break;
            }
            try
            {
                using (System.IO.Stream stream = TitleContainer.OpenStream(path))
                {
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(stream))
                    {
                        Global.NumberOfMap = Convert.ToInt32(reader.ReadLine());
                    }
                }
            }
            catch (System.Exception ex)
            {
                Global.NumberOfMap = 0;
                Debug.WriteLine("File not found" + ex);
            }
        }


        // Game Play - Basic
        public static int Counter_Enemy = 0;
        public static int Counter_Obstacle = 0;
        public static int Counter_Item = 0;
        public static int Counter_Bomber = 0;
        public static int Counter_BombCanLocated = 1;
        public static int Counter_Scores = 0;

        // Game Play - Mini
        public static int Bomb_Number = 0;
    }
}
