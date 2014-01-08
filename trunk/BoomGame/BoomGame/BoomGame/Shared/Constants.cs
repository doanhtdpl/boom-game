using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BoomGame.Shared
{
    public static class Constants
    {
        // Bomber attributed
        public const float BOMBER_VELOCITY_REDUCING = 0.05f;
        public const float BOMBER_VELOCITY_INCREASING = 0.9f;
        public const float BOMBER_TIME_TO_DIE = 5000;

        // Bomber direction
        public const int DIRECTION_NONE = 0;
        public const int DIRECTION_LEFT = 1;
        public const int DIRECTION_RIGHT = 2;
        public const int DIRECTION_UP = 3;
        public const int DIRECTION_DOWN = 4;

        // Obstacle 
        public const float OBSTACLE_TIME_TO_DIE = 100f;

        // Water Effect
        public const float WATEREFFECT_TIME_TO_LIVE = 200f;

        // Button event
        public const int BUTTON_EVENT_LEFT = 1;
        public const int BUTTON_EVENT_RIGHT = 2;
        public const int BUTTON_EVENT_UP = 3;
        public const int BUTTON_EVENT_DOWN = 4;
        public const int BUTTON_EVENT_SPACE = 5;

        // Game
        public static int GAME_SIZE_X = 0;
        public static int GAME_SIZE_Y = 0;
        public static int GAME_SIZE_WIDTH = 550;
        public static int GAME_SIZE_HEIGHT = 450;

        // Obstacle stage
        public const int OBSTACLE_IDLE = 0;
        public const int OBSTACLE_CANMOVE = 1;
        public const int OBSTACLE_MOVE_DISTANCE = 50;

        // Game collision
        public const int COLLISION_MIN = 25;

        // Save file name
        public const String BASIC_GAME_FILE = "Basic.txt";
        public const String MINI_GAME_FILE = "Mini.txt";

        // Choose Scene
        public const int NUMBER_RENDER = 10;

        // Game Play
        public const String BASIC_MODE = "M_BASIC";
        public const String TIME_MODE = "M_TIME";
        public const String LIMIT_MODE = "M_LIMIT";

        public const String BASIC_GAME_MAP_LIST = "Map_Resources/Basic/MapList.txt";
        public const String TIME_GAME_MAP_LIST = "Map_Resources/ChallengeTime/MapList.txt";
        public const String LIMIT_GAME_MAP_LIST = "Map_Resources/ChallengeLimit/MapList.txt";

        public const String BASIC_GAME_MAP_PATH = "Map_Resources/Basic/Map_";
        public const String TIME_GAME_MAP_PATH = "Map_Resources/ChallengeTime/Map_";
        public const String LIMIT_GAME_MAP_PATH = "Map_Resources/ChallengeLimit/Map_";
    }
}
