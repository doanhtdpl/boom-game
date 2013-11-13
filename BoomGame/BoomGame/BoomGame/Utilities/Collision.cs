using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BoomGame.Utilities
{
    public static class Collision
    {
        public static bool IsCollideInDistance(Rectangle rect1, Rectangle rect2)
        {
            int direction = DirectionCollide(new Vector2(rect1.X, rect1.Y), new Vector2(rect2.X, rect2.Y));
            bool result = false;
            switch (direction)
            {
                case Shared.Constants.DIRECTION_LEFT:
                    if(Math.Abs(rect2.X - (rect1.X + rect1.Width)) > Shared.Constants.COLLISION_MIN)
                        result = true;
                    break;
                case Shared.Constants.DIRECTION_RIGHT:
                    if(Math.Abs(rect1.X - (rect2.X + rect2.Width)) > Shared.Constants.COLLISION_MIN)
                        result = true;
                    break;
                case Shared.Constants.DIRECTION_UP:
                    if(Math.Abs(rect1.Y - (rect2.Y + rect2.Height)) > Shared.Constants.COLLISION_MIN)
                        result = true;
                    break;
                case Shared.Constants.DIRECTION_DOWN:
                    if(Math.Abs(rect2.Y - (rect1.Y + rect1.Height)) > Shared.Constants.COLLISION_MIN)
                        result = true;
                    break;
            }
            return result;
        }

        public static int DirectionCollide(Vector2 pos1, Vector2 pos2)
        {
            float deltaX = Math.Abs(pos1.X - pos2.X);
            float deltaY = Math.Abs(pos1.Y - pos2.Y);

            float delta = Math.Abs(deltaX - deltaY);
            if (deltaX > deltaY && delta >= 3)
            {
                return (pos1.X < pos2.X) ? Shared.Constants.DIRECTION_RIGHT : Shared.Constants.DIRECTION_LEFT;
            }
            else if (deltaX < deltaY && delta >= 3)
            {
                return (pos1.Y > pos2.Y) ? Shared.Constants.DIRECTION_UP : Shared.Constants.DIRECTION_DOWN;
            }
            else
            {
                return Shared.Constants.DIRECTION_NONE;
            }
        }

        public static Rectangle CollisionRange(Rectangle rect1, Rectangle rect2)
        {
            Rectangle rect = new Rectangle();
            int xL = Math.Max(rect1.X, rect2.X);
            int xR = Math.Min(rect1.X + rect1.Width, rect2.X + rect2.Width);
            if (xR < xL)
                return rect;
            else
            {
                int yT = Math.Max(rect1.Y, rect2.Y);
                int yB = Math.Min(rect1.Y + rect1.Height, rect2.Y + rect2.Height);
                if (yB < yT)
                    return rect;
                else
                    return new Rectangle(xL, yT, xR - xL, yB - yT);
            }
        }
    }
}
