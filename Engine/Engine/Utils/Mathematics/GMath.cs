using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SCSEngine.Mathematics
{
    public static class GMath
    {
        /// <summary>
        /// Rounding a float number
        /// </summary>
        /// <param name="number">Number to round</param>
        /// <returns>Rounded number</returns>
        public static int Round(float number)
        {
            return (int) Math.Round((double)number, 0);
        }

        /// <summary>
        /// Rounding a float number
        /// </summary>
        /// <param name="number">Number to round</param>
        public static void Round(ref float number)
        {
            number = (float)Math.Round((double)number, 0);
        }

        /// <summary>
        /// Rounding a vector
        /// </summary>
        /// <param name="vector">Vector to round</param>
        /// <returns>Rounded vector</returns>
        public static Vector2 Round(Vector2 vector)
        {
            return new Vector2(GMath.Round(vector.X), GMath.Round(vector.Y));
        }

        /// <summary>
        /// Rounding a vector
        /// </summary>
        /// <param name="vector">Vector to round</param>
        public static void Round(ref Vector2 vector)
        {
            GMath.Round(ref vector.X);
            GMath.Round(ref vector.Y);
        }

        /// <summary>
        /// Convert from Point to Vector2
        /// </summary>
        /// <param name="p">Point to convert</param>
        /// <returns>Converted vector</returns>
        public static Vector2 ToVector2(Point p)
        {
            return new Vector2(p.X, p.Y);
        }

        public static float FloatMod(float a, float b)
        {
            return (a - (b * (float) Math.Floor(a / b)));
        }

        /// <summary>
        /// Get a Vector by negative Angle
        /// </summary>
        /// <param name="angle">Angle of vector</param>
        public static Vector2 DirectedVector(float angle)
        {
            return Transform(-Vector2.UnitY, Vector2.Zero, angle);
        }

        /// <summary>
        /// Rotate a vector (point) by angle
        /// </summary>
        /// <param name="point">Vector (point) to angle (use as reference)</param>
        /// <param name="origin">Original point of rotation</param>
        /// <param name="angle">Angle of rotation</param>
        public static void Transform(ref Vector2 point, Vector2 origin, float angle)
        {
            point -= origin;
            point = Vector2.Transform(point, Matrix.CreateRotationZ(angle));
            point += origin;
        }

        /// <summary>
        /// Rotate and scale a vector (point)
        /// </summary>
        /// <param name="point">Vector (point) to scale and rotate (use as reference)</param>
        /// <param name="origin">Original of transform</param>
        /// <param name="scale">Proportionality of scale</param>
        /// <param name="angle">Angle to rotation</param>
        public static void Transform(ref Vector2 point, Vector2 origin, float scale, float angle)
        {
            point -= origin;
            point = Vector2.Transform(point, Matrix.CreateScale(scale) * Matrix.CreateRotationZ(angle));
            point += origin;
        }

        /// <summary>
        /// Rotate and scale a vector (point)
        /// </summary>
        /// <param name="point">Vector (point) to scale and rotate (use as reference)</param>
        /// <param name="origin">Original of transform</param>
        public static void Transform(ref Vector2 point, Vector2 origin)
        {
            point -= origin;
            point = Vector2.Transform(point, Matrix.CreateScale(1f) * Matrix.CreateRotationZ(0f));
            point += origin;
        }

        /// <summary>
        /// Translate, rotate and scale a vector (point)
        /// </summary>
        /// <param name="point">Vector (point) to transform (use as reference)</param>
        /// <param name="origin">Original of transform</param>
        /// <param name="translation">Translation vector</param>
        /// <param name="scale">Proportionality of scale</param>
        /// <param name="angle">Angle to rotation</param>
        public static void Transform(ref Vector2 point, Vector2 origin, Vector2 translation, float scale, float angle)
        {
            point += translation;
            point -= origin;
            point = Vector2.Transform(point, Matrix.CreateScale(scale) * Matrix.CreateRotationZ(angle));
            point += origin;
        }

        /// <summary>
        /// Translate, rotate and scale a vector (point)
        /// </summary>
        /// <param name="point">Vector (point) to transform (use as reference)</param>
        /// <param name="origin">Original of transform</param>
        /// <param name="translation">Translation vector</param>
        public static void Transform(ref Vector2 point, Vector2 origin, Vector2 translation)
        {
            point += translation;
            point -= origin;
            point = Vector2.Transform(point, Matrix.CreateScale(1f) * Matrix.CreateRotationZ(0f));
            point += origin;
        }

        /// <summary>
        /// Rotate a vector (point) by angle
        /// </summary>
        /// <param name="point">Vector (point) to angle</param>
        /// <param name="origin">Original point of rotation</param>
        /// <param name="angle">Angle of rotation</param>
        /// <returns>New vector after transform</returns>
        public static Vector2 Transform(Vector2 point, Vector2 origin, float angle)
        {
            point -= origin;
            point = Vector2.Transform(point, Matrix.CreateRotationZ(angle));
            point += origin;

            return point;
        }

        /// <summary>
        /// Rotate and scale a vector (point)
        /// </summary>
        /// <param name="point">Vector (point) to scale and rotate</param>
        /// <param name="origin">Original of transform</param>
        /// <param name="scale">Proportionality of scale</param>
        /// <param name="angle">Angle to rotation</param>
        /// <returns>New vector after transform</returns>
        public static Vector2 Transform(Vector2 point, Vector2 origin, float scale, float angle)
        {
            point -= origin;
            point = Vector2.Transform(point, Matrix.CreateScale(scale) * Matrix.CreateRotationZ(angle));
            point += origin;

            return point;
        }

        /// <summary>
        /// Rotate and scale a vector (point)
        /// </summary>
        /// <param name="point">Vector (point) to scale and rotate</param>
        /// <param name="origin">Original of transform</param>
        /// <param name="scale">Proportionality of scale</param>
        public static Vector2 Transform(Vector2 point, Vector2 origin)
        {
            point -= origin;
            point = Vector2.Transform(point, Matrix.CreateScale(1f) * Matrix.CreateRotationZ(0f));
            point += origin;

            return point;
        }

        /// <summary>
        /// Translate, rotate and scale a vector (point)
        /// </summary>
        /// <param name="point">Vector (point) to transform (use as reference)</param>
        /// <param name="origin">Original of transform</param>
        /// <param name="translation">Translation vector</param>
        /// <param name="scale">Proportionality of scale</param>
        /// <param name="angle">Angle to rotation</param>
        /// <returns>New vector after transform</returns>
        public static Vector2 Transform(Vector2 point, Vector2 origin, Vector2 translation, float scale, float angle)
        {
            point += translation;
            point -= origin;
            point = Vector2.Transform(point, Matrix.CreateScale(scale) * Matrix.CreateRotationZ(angle));
            point += origin;

            return point;
        }

        /// <summary>
        /// Translate, rotate and scale a vector (point)
        /// </summary>
        /// <param name="point">Vector (point) to transform (use as reference)</param>
        /// <param name="origin">Original of transform</param>
        /// <param name="translation">Translation vector</param>
        /// <param name="scale">Proportionality of scale</param>
        /// <param name="angle">Angle to rotation</param>
        /// <returns>New vector after transform</returns>
        public static Vector2 Transform(Vector2 point, Vector2 origin, Vector2 translation)
        {
            point += translation;
            point -= origin;
            point = Vector2.Transform(point, Matrix.CreateScale(1f) * Matrix.CreateRotationZ(0f));
            point += origin;

            return point;
        }

        /// <summary>
        /// Scaling a rectangle
        /// </summary>
        /// <param name="rect">Rectangle to scale</param>
        /// <param name="origin">Origin of scale</param>
        /// <param name="scale">Ratio of scale</param>
        /// <returns>New rectangle after scale</returns>
        public static Rectangle Transform(Rectangle rect, Vector2 origin, float scale)
        {
            rect.X -= (int)origin.X;
            rect.Y -= (int)origin.Y;

            #region Get Corners
            Vector2 topleft = new Vector2(rect.X, rect.Y);
            Vector2 rightbottom = new Vector2(rect.X + rect.Width, rect.Y + rect.Height);
            #endregion

            #region Get Transform Matrix
            Matrix transform = Matrix.CreateScale(scale);
            #endregion

            #region Transform
            topleft = Vector2.Transform(topleft, transform);
            rightbottom = Vector2.Transform(rightbottom, transform);
            #endregion

            #region Recover rectangle
            rect.X = (int) topleft.X;
            rect.Y = (int) topleft.Y;
            rect.Width = (int) (rightbottom.X - topleft.X);
            rect.Height = (int) (rightbottom.Y - topleft.Y);

            rect.X += (int)origin.X;
            rect.Y += (int)origin.Y;
            #endregion

            return rect;
        }

        /// <summary>
        /// Translate a rectangle
        /// </summary>
        /// <param name="rect">Recntagle to translation</param>
        /// <param name="translation">Translation vector</param>
        /// <returns>New rectangle after translation</returns>
        public static Rectangle Transform(Rectangle rect, Vector2 translation)
        {
            rect.X += (int) translation.X;
            rect.Y += (int) translation.Y;

            return rect;
        }

        /// <summary>
        /// Transform a rectangle
        /// </summary>
        /// <param name="rect">Rectangle to transform</param>
        /// <param name="translation">Translation vector</param>
        /// <param name="origin">Origin of scale</param>
        /// <param name="scale">Ratio of scale</param>
        /// <returns>New rectangle after transform</returns>
        public static Rectangle Transform(Rectangle rect, Vector2 translation, Vector2 origin, float scale)
        {
            rect.X += (int)translation.X;
            rect.Y += (int)translation.Y;

            rect.X -= (int)origin.X;
            rect.Y -= (int)origin.Y;

            #region Get Corners
            Vector2 topleft = new Vector2(rect.X, rect.Y);
            Vector2 rightbottom = new Vector2(rect.X + rect.Width, rect.Y + rect.Height);
            #endregion

            #region Get Transform Matrix
            Matrix transform = Matrix.CreateScale(scale);
            #endregion

            #region Transform
            topleft = Vector2.Transform(topleft, transform);
            rightbottom = Vector2.Transform(rightbottom, transform);
            #endregion

            #region Recover rectangle
            rect.X = (int)topleft.X;
            rect.Y = (int)topleft.Y;
            rect.Width = (int)(rightbottom.X - topleft.X);
            rect.Height = (int)(rightbottom.Y - topleft.Y);

            rect.X += (int)origin.X;
            rect.Y += (int)origin.Y;
            #endregion

            return rect;
        }

        /// <summary>
        /// Scaling a rectangle
        /// </summary>
        /// <param name="rect">Rectangle to scale (use as reference)</param>
        /// <param name="origin">Origin of scale</param>
        /// <param name="scale">Ratio of scale</param>
        public static void Transform(ref Rectangle rect, Vector2 origin, float scale)
        {
            rect.X -= (int)origin.X;
            rect.Y -= (int)origin.Y;

            #region Get Corners
            Vector2 topleft = new Vector2(rect.X, rect.Y);
            Vector2 rightbottom = new Vector2(rect.X + rect.Width, rect.Y + rect.Height);
            #endregion

            #region Get Transform Matrix
            Matrix transform = Matrix.CreateScale(scale);
            #endregion

            #region Transform
            topleft = Vector2.Transform(topleft, transform);
            rightbottom = Vector2.Transform(rightbottom, transform);
            #endregion

            #region Recover rectangle
            rect.X = (int)topleft.X;
            rect.Y = (int)topleft.Y;
            rect.Width = (int)(rightbottom.X - topleft.X);
            rect.Height = (int)(rightbottom.Y - topleft.Y);

            rect.X += (int)origin.X;
            rect.Y += (int)origin.Y;
            #endregion
        }

        /// <summary>
        /// Translate a rectangle
        /// </summary>
        /// <param name="rect">Recntagle to translation (use as reference)</param>
        /// <param name="translation">Translation vector</param>
        public static void Transform(ref Rectangle rect, Vector2 translation)
        {
            rect.X += (int)translation.X;
            rect.Y += (int)translation.Y;
        }

        /// <summary>
        /// Transform a rectangle
        /// </summary>
        /// <param name="rect">Rectangle to transform (use as reference)</param>
        /// <param name="translation">Translation vector</param>
        /// <param name="origin">Origin of scale</param>
        /// <param name="scale">Ratio of scale</param>
        public static void Transform(ref Rectangle rect, Vector2 translation, Vector2 origin, float scale)
        {
            rect.X += (int)translation.X;
            rect.Y += (int)translation.Y;

            rect.X -= (int)origin.X;
            rect.Y -= (int)origin.Y;

            #region Get Corners
            Vector2 topleft = new Vector2(rect.X, rect.Y);
            Vector2 rightbottom = new Vector2(rect.X + rect.Width, rect.Y + rect.Height);
            #endregion

            #region Get Transform Matrix
            Matrix transform = Matrix.CreateScale(scale);
            #endregion

            #region Transform
            topleft = Vector2.Transform(topleft, transform);
            rightbottom = Vector2.Transform(rightbottom, transform);
            #endregion

            #region Recover rectangle
            rect.X = (int)topleft.X;
            rect.Y = (int)topleft.Y;
            rect.Width = (int)(rightbottom.X - topleft.X);
            rect.Height = (int)(rightbottom.Y - topleft.Y);

            rect.X += (int)origin.X;
            rect.Y += (int)origin.Y;
            #endregion
        }

        /// <summary>
        /// Get Angle of (0, -1) and specified vector
        /// </summary>
        /// <param name="dir">Vector to angling</param>
        /// <returns>Angle result</returns>
        public static float NegativeAngle(Vector2 dir)
        {
            return (float) Math.Atan2(dir.X, -dir.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <returns></returns>
        public static float Angle(Vector2 vector1, Vector2 vector2)
        {
            return NegativeAngle(vector2) - NegativeAngle(vector1);
        }

        /// <summary>
        /// Checking Intersect of two transformed rectangles
        /// </summary>
        /// <param name="rect1">First rectangle</param>
        /// <param name="origin1">Origin of first rectangle's transform</param>
        /// <param name="angle1">Angle of first rectangle's rotation</param>
        /// <param name="scale1">Ratio of first rectangle's scale</param>
        /// <param name="trans1">Translation of first rectangle</param>
        /// <param name="extra1">Extra transform for first rectangle</param>
        /// <param name="rect2">Second rectangle</param>
        /// <param name="origin2">Origin of second rectangle's transform</param>
        /// <param name="angle2">Angle of second rectangle's rotation</param>
        /// <param name="scale2">Ratio of second rectangle's scale</param>
        /// <param name="trans2">Translation of second rectangle</param>
        /// <param name="extra2">Extra transform for second rectangle</param>
        /// <returns>True if two transformed rectangles are intersect</returns>
        public static bool Intersects(Rectangle rect1, Vector2 origin1, float angle1, float scale1, Vector2 trans1, Matrix extra1, Rectangle rect2, Vector2 origin2, float angle2, float scale2, Vector2 trans2, Matrix extra2)
        {
            #region Get Corner
            Vector2 lefttop1 = new Vector2(rect1.Left, rect1.Top);
            Vector2 leftbottom1 = new Vector2(rect1.Left, rect1.Bottom);
            Vector2 righttop1 = new Vector2(rect1.Right, rect1.Top);
            Vector2 rightbottom1 = new Vector2(rect1.Right, rect1.Bottom);

            Vector2 lefttop2 = new Vector2(rect2.Left, rect2.Top);
            Vector2 leftbottom2 = new Vector2(rect2.Left, rect2.Bottom);
            Vector2 righttop2 = new Vector2(rect2.Right, rect2.Top);
            Vector2 rightbottom2 = new Vector2(rect2.Right, rect2.Bottom);
            #endregion

            #region Get Transform Matrix
            Matrix transform1 = Matrix.CreateTranslation(new Vector3(trans1, 0f));
            transform1 *= Matrix.CreateTranslation(new Vector3(-origin1, 0f));
            transform1 *= Matrix.CreateScale(scale1);
            transform1 *= Matrix.CreateRotationZ(angle1);
            transform1 *= Matrix.CreateTranslation(new Vector3(origin1, 0f));
            transform1 *= extra1;

            Matrix transform2 = Matrix.CreateTranslation(new Vector3(trans2, 0f));
            transform2 *= Matrix.CreateTranslation(new Vector3(-origin2, 0f));
            transform2 *= Matrix.CreateScale(scale2);
            transform2 *= Matrix.CreateRotationZ(angle2);
            transform2 *= Matrix.CreateTranslation(new Vector3(origin2, 0f));
            transform2 *= extra2;

            Matrix detransform = transform2 * Matrix.Invert(transform1);
            #endregion

            #region De-Transform
            lefttop2 = Vector2.Transform(lefttop2, detransform);
            leftbottom2 = Vector2.Transform(leftbottom2, detransform);
            righttop2 = Vector2.Transform(righttop2, detransform);
            rightbottom2 = Vector2.Transform(rightbottom2, detransform);
            #endregion

            #region Get Bound Rect2
            int minX = (int)Math.Min(Math.Min(Math.Min(lefttop2.X, leftbottom2.X), righttop2.X), rightbottom2.X);
            int minY = (int)Math.Min(Math.Min(Math.Min(lefttop2.Y, leftbottom2.Y), righttop2.Y), rightbottom2.Y);
            int maxX = (int)Math.Max(Math.Max(Math.Max(lefttop2.X, leftbottom2.X), righttop2.X), rightbottom2.X);
            int maxY = (int)Math.Max(Math.Max(Math.Max(lefttop2.Y, leftbottom2.Y), righttop2.Y), rightbottom2.Y);

            Rectangle bound2 = new Rectangle(minX, minY, maxX - minX, maxY - minY);
            #endregion

            #region Check Bound
            if (!rect1.Intersects(bound2))
                return false;
            #endregion

            #region Check In
            if (Intersects(rect1, lefttop2) ||
                Intersects(rect1, leftbottom2) ||
                Intersects(rect1, righttop2) ||
                Intersects(rect1, rightbottom2))
                return true;
            #endregion

            #region Check Edge
            if (Intersects(rect1, lefttop2, righttop2))
                return true;
            if (Intersects(rect1, lefttop2, leftbottom2))
                return true;
            if (Intersects(rect1, righttop2, rightbottom2))
                return true;
            if (Intersects(rect1, leftbottom2, rightbottom2))
                return true;
            #endregion

            return false;
        }

        /// <summary>
        /// Checking Intersect of two translated rectangles
        /// </summary>
        /// <param name="rect1">First rectangle</param>
        /// <param name="trans1">Translation of first rectangle</param>
        /// <param name="rect2">Second rectangle</param>
        /// <param name="trans2">Translation of second rectangle</param>
        /// <returns>True if two translated rectangles are intersect</returns>
        public static bool Intersects(Rectangle rect1, Vector2 trans1, Rectangle rect2, Vector2 trans2)
        {
            return Intersects(rect1, Vector2.Zero, 0f, 1f, trans1, Matrix.Identity, rect2, Vector2.Zero, 0f, 0f, trans2, Matrix.Identity);
        }

        /// <summary>
        /// Checking Intersect of two transformed rectangles
        /// </summary>
        /// <param name="rect1">First rectangle</param>
        /// <param name="origin1">Origin of first rectangle's transform</param>
        /// <param name="angle1">Angle of first rectangle's rotation</param>
        /// <param name="scale1">Ratio of first rectangle's scale</param>
        /// <param name="trans1">Translation of first rectangle</param>
        /// <param name="rect2">Second rectangle</param>
        /// <param name="origin2">Origin of second rectangle's transform</param>
        /// <param name="angle2">Angle of second rectangle's rotation</param>
        /// <param name="scale2">Ratio of second rectangle's scale</param>
        /// <param name="trans2">Translation of second rectangle</param>
        /// <returns>True if two transformed rectangles are intersect</returns>
        public static bool Intersects(Rectangle rect1, Vector2 origin1, float angle1, float scale1, Vector2 trans1, Rectangle rect2, Vector2 origin2, float angle2, float scale2, Vector2 trans2)
        {
            return Intersects(rect1, origin1, angle1, scale1, trans1, Matrix.Identity, rect2, origin2, angle2, scale2, trans2, Matrix.Identity);
        }

        /// <summary>
        /// Checking Intersect of two transformed rectangles
        /// </summary>
        /// <param name="rect1">First rectangle</param>
        /// <param name="origin1">Origin of first rectangle's transform</param>
        /// <param name="angle1">Angle of first rectangle's rotation</param>
        /// <param name="trans1">Translation of first rectangle</param>
        /// <param name="rect2">Second rectangle</param>
        /// <param name="origin2">Origin of second rectangle's transform</param>
        /// <param name="angle2">Angle of second rectangle's rotation</param>
        /// <param name="trans2">Translation of second rectangle</param>
        /// <returns>True if two transformed rectangles are intersect</returns>
        public static bool Intersects(Rectangle rect1, Vector2 origin1, float angle1, Vector2 trans1, Rectangle rect2, Vector2 origin2, float angle2, Vector2 trans2)
        {
            return Intersects(rect1, origin1, angle1, 1f, trans1, Matrix.Identity, rect2, origin2, angle2, 1f, trans2, Matrix.Identity);
        }

        /// <summary>
        /// Checking Intersect of two rotated rectangles
        /// </summary>
        /// <param name="rect1">First rectangle</param>
        /// <param name="origin1">Origin of first rectangle's rotation</param>
        /// <param name="angle1">Angle of first rectangle's rotation</param>
        /// <param name="rect2">Second rectangle</param>
        /// <param name="origin2">Origin of second rectangle's rotation</param>
        /// <param name="angle2">Angle of second rectangle's rotation</param>
        /// <returns>True if two rotated rectangles are intersect</returns>
        public static bool Intersects(Rectangle rect1, Vector2 origin1, float angle1, Rectangle rect2, Vector2 origin2, float angle2)
        {
            return Intersects(rect1, origin1, angle1, 1f, Vector2.Zero, Matrix.Identity, rect2, origin2, angle2, 1f, Vector2.Zero, Matrix.Identity);
        }

        /// <summary>
        /// Checking Intersect of two transformed rectangles
        /// </summary>
        /// <param name="rect1">First rectangle</param>
        /// <param name="origin1">Origin of first rectangle's transform</param>
        /// <param name="angle1">Angle of first rectangle's rotation</param>
        /// <param name="scale1">Ratio of first rectangle's scale</param>
        /// <param name="rect2">Second rectangle</param>
        /// <param name="origin2">Origin of second rectangle's transform</param>
        /// <param name="angle2">Angle of second rectangle's rotation</param>
        /// <param name="scale2">Ratio of second rectangle's scale</param>
        /// <returns>True if two transformed rectangles are intersect</returns>
        public static bool Intersects(Rectangle rect1, Vector2 origin1, float angle1, float scale1, Rectangle rect2, Vector2 origin2, float angle2, float scale2)
        {
            return Intersects(rect1, origin1, angle1, scale1, Vector2.Zero, Matrix.Identity, rect2, origin2, angle2, scale2, Vector2.Zero, Matrix.Identity);
        }

        /// <summary>
        /// Checking Intersect a point (Vector2) and a rectangle (without transform)
        /// </summary>
        /// <param name="rect">Rectangle to intersected</param>
        /// <param name="point">Point to intersected</param>
        /// <returns>True if a point in or on rectangle</returns>
        public static bool Intersects(Rectangle rect, Vector2 point)
        {
            return (((int)point.X >= rect.Left) &&
                    ((int)point.X <= rect.Bottom) &&
                    ((int)point.Y >= rect.Top) &&
                    ((int)point.Y <= rect.Bottom));
        }

        /// <summary>
        /// Checking intersect of a rectangle and a segment created by two point (without transform)
        /// </summary>
        /// <param name="rect">Rectangle to intersected</param>
        /// <param name="point1">First point of segment</param>
        /// <param name="point2">Second point of segment</param>
        /// <returns>True if segment intersect rectangle</returns>
        public static bool Intersects(Rectangle rect, Vector2 point1, Vector2 point2)
        {
            Vector2 cpoint1;
            Vector2 cpoint2;
            Vector2 sub = point2 - point1;
            int intersection;

            #region Check Top
            cpoint1 = point1;
            cpoint2 = point2;
            cpoint1.X -= rect.Left;
            cpoint2.X -= rect.Left;
            cpoint1.Y -= rect.Top;
            cpoint2.Y -= rect.Top;

            if (cpoint1.Y == cpoint2.Y)
            {
                if (cpoint1.Y == 0)
                    return true;
            }
            else if ((cpoint1.Y * cpoint2.Y) <= 0f)
            {
                intersection = (int)((-sub.X) * cpoint1.Y / sub.Y + cpoint1.X);
                if ((0 <= intersection) && (intersection <= rect.Width))
                    return true;
            }
            #endregion

            #region Check Bottom
            cpoint1 = point1;
            cpoint2 = point2;
            cpoint1.X -= rect.Left;
            cpoint2.X -= rect.Left;
            cpoint1.Y -= rect.Bottom;
            cpoint2.Y -= rect.Bottom;

            if (cpoint1.Y == cpoint2.Y)
            {
                if (cpoint1.Y == 0)
                    return true;
            }
            else if ((cpoint1.Y * cpoint2.Y) <= 0f)
            {
                intersection = (int)((-sub.X) * cpoint1.Y / sub.Y + cpoint1.X);
                if ((0 <= intersection) && (intersection <= rect.Width))
                    return true;
            }
            #endregion

            #region Check Left
            cpoint1 = point1;
            cpoint2 = point2;
            cpoint1.X -= rect.Left;
            cpoint2.X -= rect.Left;
            cpoint1.Y -= rect.Top;
            cpoint2.Y -= rect.Top;

            if (cpoint1.X == cpoint2.X)
            {
                if (cpoint1.X == 0)
                    return true;
            }
            else if ((cpoint1.X * cpoint2.X) <= 0f)
            {
                intersection = (int)((-sub.Y) * cpoint1.X / sub.X + cpoint1.Y);
                if ((0 <= intersection) && (intersection <= rect.Height))
                    return true;
            }
            #endregion

            #region Check Right
            cpoint1 = point1;
            cpoint2 = point2;
            cpoint1.X -= rect.Right;
            cpoint2.X -= rect.Right;
            cpoint1.Y -= rect.Top;
            cpoint2.Y -= rect.Top;

            if (cpoint1.X == cpoint2.X)
            {
                if (cpoint1.X == 0)
                    return true;
            }
            else if ((cpoint1.X * cpoint2.X) <= 0f)
            {
                intersection = (int)((-sub.Y) * cpoint1.X / sub.X + cpoint1.Y);
                if ((0 <= intersection) && (intersection <= rect.Height))
                    return true;
            }
            #endregion

            return false;
        }

        /// <summary>
        /// Checking a specified Vector2 (point) is contained ("in" not "on") by a rectangle
        /// </summary>
        /// <param name="point">Point to check contained</param>
        /// <param name="rect">Rectangle to check contain</param>
        /// <returns>True if that point is contained by rectangle</returns>
        public static bool IsContained(Vector2 point, Rectangle rect)
        {
            return ((point.X >= rect.Left) && (point.X <= rect.Right) &&
                    (point.Y >= rect.Top) && (point.Y <= rect.Bottom));
        }

        /// <summary>
        /// Checking a specified Vector2 (point) is contained ("in" not "on") by a transformed rectangle
        /// </summary>
        /// <param name="point">Point to check contained</param>
        /// <param name="rect">Rectangle to check contain</param>
        /// <param name="origin">Origin of transform</param>
        /// <param name="angle">Rotate's angle</param>
        /// <param name="scale">Scale's ratio</param>
        /// <param name="trans">Translation vector</param>
        /// <param name="extra">Extra transform</param>
        /// <returns>True if that point is contained by transformed rectangle</returns>
        public static bool IsContained(Vector2 point, Rectangle rect, Vector2 origin, float angle, float scale, Vector2 trans, Matrix extra)
        {
            #region Get Transform Matrix
            Matrix transform = Matrix.Identity; 
            transform *= Matrix.CreateTranslation(new Vector3(-origin, 0f));
            transform *= Matrix.CreateScale(scale);
            transform *= Matrix.CreateRotationZ(angle);
            transform *= Matrix.CreateTranslation(new Vector3(scale*origin.X, scale*origin.Y, 0f));
            transform *= Matrix.CreateTranslation(new Vector3(trans, 0f));
            transform *= extra;

            Matrix detransform = Matrix.Invert(transform);
            #endregion

            #region De-Transform
            point = Vector2.Transform(point, detransform);
            #endregion

            return IsContained(point, rect);
        }

        /// <summary>
        /// Checking a specified Vector2 (point) is contained ("in" not "on") by a transformed rectangle
        /// </summary>
        /// <param name="point">Point to check contained</param>
        /// <param name="rect">Rectangle to check contain</param>
        /// <param name="origin">Origin of transform</param>
        /// <param name="angle">Rotate's angle</param>
        /// <param name="scale">Scale's ratio</param>
        /// <param name="trans">Translation vector</param>
        /// <returns>True if that point is contained by transformed rectangle</returns>
        public static bool IsContained(Vector2 point, Rectangle rect, Vector2 origin, float angle, float scale, Vector2 trans)
        {
            return IsContained(point, rect, origin, angle, scale, trans, Matrix.Identity);
        }

        /// <summary>
        /// Checking a specified Vector2 (point) is contained ("in" not "on") by a transformed rectangle
        /// </summary>
        /// <param name="point">Point to check contained</param>
        /// <param name="rect">Rectangle to check contain</param>
        /// <param name="origin">Origin of transform</param>
        /// <param name="angle">Rotate's angle</param>
        /// <param name="trans">Translation vector</param>
        /// <returns>True if that point is contained by transformed rectangle</returns>
        public static bool IsContained(Vector2 point, Rectangle rect, Vector2 origin, float angle, Vector2 trans)
        {
            return IsContained(point, rect, origin, angle, 1f, trans, Matrix.Identity);
        }

        /// <summary>
        /// Checking a specified Vector2 (point) is contained ("in" not "on") by a transformed rectangle
        /// </summary>
        /// <param name="point">Point to check contained</param>
        /// <param name="rect">Rectangle to check contain</param>
        /// <param name="origin">Origin of transform</param>
        /// <param name="angle">Rotate's angle</param>
        /// <param name="scale">Scale's ratio</param>
        /// <returns>True if that point is contained by transformed rectangle</returns>
        public static bool IsContained(Vector2 point, Rectangle rect, Vector2 origin, float angle, float scale)
        {
            return IsContained(point, rect, origin, angle, scale, Vector2.Zero, Matrix.Identity);
        }

        /// <summary>
        /// Checking a specified Vector2 (point) is contained ("in" not "on") by a transformed rectangle
        /// </summary>
        /// <param name="point">Point to check contained</param>
        /// <param name="rect">Rectangle to check contain</param>
        /// <param name="origin">Origin of transform</param>
        /// <param name="angle">Rotate's angle</param>
        /// <returns>True if that point is contained by transformed rectangle</returns>
        public static bool IsContained(Vector2 point, Rectangle rect, Vector2 origin, float angle)
        {
            return IsContained(point, rect, origin, angle, 1f, Vector2.Zero, Matrix.Identity);
        }

        /// <summary>
        /// Checking a specified Vector2 (point) is contained ("in" not "on") by a transformed rectangle
        /// </summary>
        /// <param name="point">Point to check contained</param>
        /// <param name="rect">Rectangle to check contain</param>
        /// <param name="scale">Scale's ratio</param>
        /// <param name="trans">Translation vector</param>
        /// <returns>True if that point is contained by transformed rectangle</returns>
        public static bool IsContained(Vector2 point, Rectangle rect, float scale, Vector2 trans)
        {
            return IsContained(point, rect, Vector2.Zero, 0f, scale, trans, Matrix.Identity);
        }

        /// <summary>
        /// Checking a specified Vector2 (point) is contained ("in" not "on") by a transformed rectangle
        /// </summary>
        /// <param name="point">Point to check contained</param>
        /// <param name="rect">Rectangle to check contain</param>
        /// <param name="scale">Scale's ratio</param>
        /// <returns>True if that point is contained by transformed rectangle</returns>
        public static bool IsContained(Vector2 point, Rectangle rect, float scale)
        {
            return IsContained(point, rect, Vector2.Zero, 0f, scale, Vector2.Zero, Matrix.Identity);
        }

        /// <summary>
        /// Checking if a transformed rectangle is contained by another transformed rectangle or not
        /// </summary>
        /// <param name="rect1">First rectangle</param>
        /// <param name="origin1">Origin of first rectangle's transform</param>
        /// <param name="angle1">Angle of first rectangle's rotation</param>
        /// <param name="scale1">Ratio of first rectangle's scale</param>
        /// <param name="trans1">Translation of first rectangle</param>
        /// <param name="extra1">Extra transform for first rectangle</param>
        /// <param name="rect2">Second rectangle</param>
        /// <param name="origin2">Origin of second rectangle's transform</param>
        /// <param name="angle2">Angle of second rectangle's rotation</param>
        /// <param name="scale2">Ratio of second rectangle's scale</param>
        /// <param name="trans2">Translation of second rectangle</param>
        /// <param name="extra2">Extra transform for second rectangle</param>
        /// <returns>True if second transformed rectangle is contained by first transformed rectangle</returns>
        public static bool IsContained(Rectangle rect1, Vector2 origin1, float angle1, float scale1, Vector2 trans1, Matrix extra1, Rectangle rect2, Vector2 origin2, float angle2, float scale2, Vector2 trans2, Matrix extra2)
        {
            #region Get Corner
            Vector2 lefttop1 = new Vector2(rect1.Left, rect1.Top);
            Vector2 leftbottom1 = new Vector2(rect1.Left, rect1.Bottom);
            Vector2 righttop1 = new Vector2(rect1.Right, rect1.Top);
            Vector2 rightbottom1 = new Vector2(rect1.Right, rect1.Bottom);

            Vector2 lefttop2 = new Vector2(rect2.Left, rect2.Top);
            Vector2 leftbottom2 = new Vector2(rect2.Left, rect2.Bottom);
            Vector2 righttop2 = new Vector2(rect2.Right, rect2.Top);
            Vector2 rightbottom2 = new Vector2(rect2.Right, rect2.Bottom);
            #endregion

            #region Get Transform Matrix
            Matrix transform1 = Matrix.CreateTranslation(new Vector3(trans1, 0f));
            transform1 *= Matrix.CreateTranslation(new Vector3(-origin1, 0f));
            transform1 *= Matrix.CreateScale(scale1);
            transform1 *= Matrix.CreateRotationZ(angle1);
            transform1 *= Matrix.CreateTranslation(new Vector3(origin1, 0f));
            transform1 *= extra1;

            Matrix transform2 = Matrix.CreateTranslation(new Vector3(trans2, 0f));
            transform2 *= Matrix.CreateTranslation(new Vector3(-origin2, 0f));
            transform2 *= Matrix.CreateScale(scale2);
            transform2 *= Matrix.CreateRotationZ(angle2);
            transform2 *= Matrix.CreateTranslation(new Vector3(origin2, 0f));
            transform2 *= extra2;

            Matrix detransform = transform2 * Matrix.Invert(transform1);
            #endregion

            #region De-Transform
            lefttop2 = Vector2.Transform(lefttop2, detransform);
            leftbottom2 = Vector2.Transform(leftbottom2, detransform);
            righttop2 = Vector2.Transform(righttop2, detransform);
            rightbottom2 = Vector2.Transform(rightbottom2, detransform);
            #endregion

            #region Check Contain
            if (!IsContained(lefttop2, rect1))
                return false;

            if (!IsContained(leftbottom2, rect1))
                return false;

            if (!IsContained(righttop2, rect1))
                return false;

            if (!IsContained(rightbottom2, rect1))
                return false;
            #endregion

            return true;
        }

        /// <summary>
        /// Checking if a translated rectangle is contained by another translated rectangle or not
        /// </summary>
        /// <param name="rect1">First rectangle</param>
        /// <param name="trans1">Translation of first rectangle</param>
        /// <param name="rect2">Second rectangle</param>
        /// <param name="trans2">Translation of second rectangle</param>
        /// <returns>True if second translated rectangle is contained by first translated rectangle</returns>
        public static bool IsContained(Rectangle rect1, Matrix trans1, Rectangle rect2, Matrix trans2)
        {
            return IsContained(rect1, Vector2.Zero, 0f, 1f, Vector2.Zero, trans1, rect2, Vector2.Zero, 0f, 0f, Vector2.Zero, trans2);
        }

        /// <summary>
        /// Checking if a transformed rectangle is contained by another transformed rectangle or not
        /// </summary>
        /// <param name="rect1">First rectangle</param>
        /// <param name="origin1">Origin of first rectangle's transform</param>
        /// <param name="angle1">Angle of first rectangle's rotation</param>
        /// <param name="scale1">Ratio of first rectangle's scale</param>
        /// <param name="trans1">Translation of first rectangle</param>
        /// <param name="rect2">Second rectangle</param>
        /// <param name="origin2">Origin of second rectangle's transform</param>
        /// <param name="angle2">Angle of second rectangle's rotation</param>
        /// <param name="scale2">Ratio of second rectangle's scale</param>
        /// <param name="trans2">Translation of second rectangle</param>
        /// <returns>True if second transformed rectangle is contained by first transformed rectangle</returns>
        public static bool IsContained(Rectangle rect1, Vector2 origin1, float angle1, float scale1, Vector2 trans1, Rectangle rect2, Vector2 origin2, float angle2, float scale2, Vector2 trans2)
        {
            return IsContained(rect1, origin1, angle1, scale1, trans1, Matrix.Identity, rect2, origin2, angle2, scale2, trans2, Matrix.Identity);
        }

        /// <summary>
        /// Checking if a transformed rectangle is contained by another transformed rectangle or not
        /// </summary>
        /// <param name="rect1">First rectangle</param>
        /// <param name="origin1">Origin of first rectangle's transform</param>
        /// <param name="angle1">Angle of first rectangle's rotation</param>
        /// <param name="trans1">Translation of first rectangle</param>
        /// <param name="rect2">Second rectangle</param>
        /// <param name="origin2">Origin of second rectangle's transform</param>
        /// <param name="angle2">Angle of second rectangle's rotation</param>
        /// <param name="trans2">Translation of second rectangle</param>
        /// <returns>True if second transformed rectangle is contained by first transformed rectangle</returns>
        public static bool IsContained(Rectangle rect1, Vector2 origin1, float angle1, Vector2 trans1, Rectangle rect2, Vector2 origin2, float angle2, Vector2 trans2)
        {
            return IsContained(rect1, origin1, angle1, 1f, trans1, Matrix.Identity, rect2, origin2, angle2, 1f, trans2, Matrix.Identity);
        }

        /// <summary>
        /// Checking if a rotated rectangle is contained by another transformed rotated or not
        /// </summary>
        /// <param name="rect1">First rectangle</param>
        /// <param name="origin1">Origin of first rectangle's rotation</param>
        /// <param name="angle1">Angle of first rectangle's rotation</param>
        /// <param name="rect2">Second rectangle</param>
        /// <param name="origin2">Origin of second rectangle's rotation</param>
        /// <param name="angle2">Angle of second rectangle's rotation</param>
        /// <returns>True if second rotated rectangle is contained by first rotated rectangle</returns>
        public static bool IsContained(Rectangle rect1, Vector2 origin1, float angle1, Rectangle rect2, Vector2 origin2, float angle2)
        {
            return IsContained(rect1, origin1, angle1, 1f, Vector2.Zero, Matrix.Identity, rect2, origin2, angle2, 1f, Vector2.Zero, Matrix.Identity);
        }

        /// <summary>
        /// Checking if a transformed rectangle is contained by another transformed rectangle or not
        /// </summary>
        /// <param name="rect1">First rectangle</param>
        /// <param name="origin1">Origin of first rectangle's transform</param>
        /// <param name="angle1">Angle of first rectangle's rotation</param>
        /// <param name="scale1">Ratio of first rectangle's scale</param>
        /// <param name="rect2">Second rectangle</param>
        /// <param name="origin2">Origin of second rectangle's transform</param>
        /// <param name="angle2">Angle of second rectangle's rotation</param>
        /// <param name="scale2">Ratio of second rectangle's scale</param>
        /// <returns>True if second transformed rectangle is contained by first transformed rectangle</returns>
        public static bool IsContained(Rectangle rect1, Vector2 origin1, float angle1, float scale1, Rectangle rect2, Vector2 origin2, float angle2, float scale2)
        {
            return IsContained(rect1, origin1, angle1, scale1, Vector2.Zero, Matrix.Identity, rect2, origin2, angle2, scale2, Vector2.Zero, Matrix.Identity);
        }

        /// <summary>
        /// Return blended color from color A and B with specified gamma blend ratio
        /// </summary>
        /// <param name="A">Color A</param>
        /// <param name="B">Color B</param>
        /// <param name="gamma">Gamma ratio</param>
        /// <returns>Blended Color</returns>
        public static Color GammaBlend(Color A, Color B, float gamma)
        {
            Color blended = Color.White;
            blended.A = (byte)(gamma * A.A + (1 - gamma) * B.A);
            blended.R = (byte)(gamma * A.R + (1 - gamma) * B.R);
            blended.G = (byte)(gamma * A.G + (1 - gamma) * B.G);
            blended.B = (byte)(gamma * A.B + (1 - gamma) * B.B);

            return blended;
        }

        /// <summary>
        /// Return color from blended color using gamma blend and one other source color
        /// </summary>
        /// <param name="blended">Blended Color</param>
        /// <param name="source">Other source color</param>
        /// <param name="gamma">Gamma ratio</param>
        /// <returns>Source color</returns>
        public static Color DeGammaBlend(Color blended, Color source, float gamma)
        {
            Color result = Color.White;
            result.A = (byte)((blended.A - ((1 - gamma) * source.A)) / gamma);
            result.R = (byte)((blended.R - ((1 - gamma) * source.R)) / gamma);
            result.G = (byte)((blended.G - ((1 - gamma) * source.G)) / gamma);
            result.B = (byte)((blended.B - ((1 - gamma) * source.B)) / gamma);

            return result;
        }

        public static Rectangle AABB(Rectangle R, Matrix transform)
        {
            Vector2 lefttop = new Vector2(R.Left, R.Top);
            Vector2 leftbottom = new Vector2(R.Left, R.Bottom);
            Vector2 righttop = new Vector2(R.Right, R.Top);
            Vector2 rightbottom = new Vector2(R.Right, R.Bottom);

            lefttop = Vector2.Transform(lefttop, transform);
            leftbottom = Vector2.Transform(leftbottom, transform);
            righttop = Vector2.Transform(righttop, transform);
            rightbottom = Vector2.Transform(rightbottom, transform);

            int minX = (int)Math.Min(Math.Min(Math.Min(lefttop.X, leftbottom.X), righttop.X), rightbottom.X);
            int minY = (int)Math.Min(Math.Min(Math.Min(lefttop.Y, leftbottom.Y), righttop.Y), rightbottom.Y);
            int maxX = (int)Math.Max(Math.Max(Math.Max(lefttop.X, leftbottom.X), righttop.X), rightbottom.X);
            int maxY = (int)Math.Max(Math.Max(Math.Max(lefttop.Y, leftbottom.Y), righttop.Y), rightbottom.Y);

            Rectangle bound = new Rectangle(minX, minY, maxX - minX, maxY - minY);

            return bound;
        }

        public static Rectangle AABB(Rectangle R, float angle)
        {
            Vector2 center = new Vector2(R.Width / 2, R.Height / 2);
            Matrix transform = Matrix.Identity;
            transform *= Matrix.CreateTranslation(new Vector3(-center, 0f));
            transform *= Matrix.CreateRotationZ(angle);
            transform *= Matrix.CreateTranslation(new Vector3(center, 0f));

            Vector2 lefttop = new Vector2(R.Left, R.Top);
            Vector2 leftbottom = new Vector2(R.Left, R.Bottom);
            Vector2 righttop = new Vector2(R.Right, R.Top);
            Vector2 rightbottom = new Vector2(R.Right, R.Bottom);

            lefttop = Vector2.Transform(lefttop, transform);
            leftbottom = Vector2.Transform(leftbottom, transform);
            righttop = Vector2.Transform(righttop, transform);
            rightbottom = Vector2.Transform(rightbottom, transform);

            int minX = (int)Math.Min(Math.Min(Math.Min(lefttop.X, leftbottom.X), righttop.X), rightbottom.X);
            int minY = (int)Math.Min(Math.Min(Math.Min(lefttop.Y, leftbottom.Y), righttop.Y), rightbottom.Y);
            int maxX = (int)Math.Max(Math.Max(Math.Max(lefttop.X, leftbottom.X), righttop.X), rightbottom.X);
            int maxY = (int)Math.Max(Math.Max(Math.Max(lefttop.Y, leftbottom.Y), righttop.Y), rightbottom.Y);

            Rectangle bound = new Rectangle(minX, minY, maxX - minX, maxY - minY);

            return bound;
        }

        public static void Smooth(this List<Vector2> pointList)
        {
            List<Vector2> smoothedPoints = new List<Vector2>();

            for (int i = 1; i < pointList.Count; i++)
            {
                if (Vector2.Distance(pointList[i - 1], pointList[i]) < 30f)
                {
                    pointList.RemoveAt(i);
                    i--;
                }
            }

            if (pointList.Count < 4) return;

            smoothedPoints.Add(pointList[0]);

            for (int i = 1; i < pointList.Count - 2; i++)
            {
                smoothedPoints.Add(pointList[i]);

                smoothedPoints.Add(Vector2.CatmullRom(pointList[i - 1], pointList[i], pointList[i + 1], pointList[i + 2], .5f));
            }

            smoothedPoints.Add(pointList[pointList.Count - 2]);
            smoothedPoints.Add(pointList[pointList.Count - 1]);

            pointList.Clear();
            pointList.AddRange(smoothedPoints);
        }
    }
}
