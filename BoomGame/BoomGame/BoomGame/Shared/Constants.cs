using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BoomGame.Shared
{
    public static class Constants
    {
        public const float BOMBER_VELOCITY_REDUCING = 0.05f;
        public const float BOMBER_TIME_TO_DIE = 5000;

        public const int DIRECTION_NONE = 0;
        public const int DIRECTION_LEFT = 1;
        public const int DIRECTION_RIGHT = 2;
        public const int DIRECTION_UP = 3;
        public const int DIRECTION_DOWN = 4;

        public const int BUTTON_EVENT_LEFT = 1;
        public const int BUTTON_EVENT_RIGHT = 2;
        public const int BUTTON_EVENT_UP = 3;
        public const int BUTTON_EVENT_DOWN = 4;
        public const int BUTTON_EVENT_SPACE = 5;

        public static int GAME_SIZE_X = 0;
        public static int GAME_SIZE_Y = 0;
        public static int GAME_SIZE_WIDTH = 550;
        public static int GAME_SIZE_HEIGHT = 450;

        public const int OBSTACLE_IDLE = 0;
        public const int OBSTACLE_CANMOVE = 1;
        public const int OBSTACLE_MOVE_DISTANCE = 50;

        public const int COLLISION_MIN = 20;
    }
}
