using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SCSEngine.Mathematics
{
    /// <summary>
    /// Random everything
    /// </summary>
    public static class GRandom 
    {
        private static Random rand = new Random(System.DateTime.Now.Millisecond);

        /// <summary>
        /// Change seed of random.
        /// </summary>
        /// <param name="seed">The seed value.</param>
        public static void ChangeSeed(int seed)
        {
            rand = new Random(seed);
        }

        /// <summary>
        /// Change seed of random.
        /// </summary>
        public static void ChangeSeed()
        {
            int seed = (int) (System.DateTime.Now.TimeOfDay.Ticks % int.MaxValue);
            rand = new Random(seed);
        }

        /// <summary>
        /// Returns a nonnegative random integer.
        /// </summary>
        /// <returns></returns>
        public static int RandomInt()
        {
            return rand.Next();
        }

        /// <summary>
        /// Return a negative random integer.
        /// </summary>
        /// <returns></returns>
        public static int RandomNegativeInt()
        {
            return rand.Next() * -1;
        }

        /// <summary>
        /// Returns a nonnegative random integer less than the maximum value.
        /// </summary>
        /// <param name="max">The maximum value.</param>
        /// <returns></returns>
        public static int RandomInt(int max)
        {
            if (max < 0)
                return 0;
            return rand.Next(0, max);
        }

        /// <summary>
        /// Returns a negative random integer greater than the minimum value.
        /// </summary>
        /// <param name="min">The minimum value.</param>
        /// <returns></returns>
        public static int RandomNegativeInt(int min)
        {
            if (min > 0)
                return 0;
            return rand.Next(0, -min) * -1;
        }

        /// <summary>
        /// Returns a random integer within a specified range.
        /// </summary>
        /// <param name="min">The begining value of specified range.</param>
        /// <param name="max">The ending value of specified range. (Greater the begining value)</param>
        /// <returns></returns>
        public static int RandomInt(int min, int max)
        {
            if ((max < 0) || (min > max))
                return 0;
            return rand.Next(min, max);
        }

        /// <summary>
        /// Returns a nonnegative random long.
        /// </summary>
        /// <returns></returns>
        public static long RandomLong()
        {
            byte[] lbyte = new byte[8];
            rand.NextBytes(lbyte);
            lbyte[7] /= 2;
            return BitConverter.ToInt64(lbyte, 0);
        }

        /// <summary>
        /// Returns a negative random long.
        /// </summary>
        /// <returns></returns>
        public static long RandomNegativeLong()
        {
            byte[] lbyte = new byte[8];
            rand.NextBytes(lbyte);
            lbyte[7] /= 2;
            return BitConverter.ToInt64(lbyte, 0) * -1;
        }

        /// <summary>
        /// Returns a nonnegative random long less than the maximum value.
        /// </summary>
        /// <param name="max">The maximum value.</param>
        /// <returns></returns>
        public static long RandomLong(long max)
        {
            if (max < 0)
                return 0;
            return (RandomLong() % max);
        }

        /// <summary>
        /// Returns a negative random long greater than the minimum value.
        /// </summary>
        /// <param name="min">The minimum value.</param>
        /// <returns></returns>
        public static long RandomNegativeLong(long min)
        {
            if (min > 0)
                return 0;
            return (RandomNegativeLong() % min);
        }

        /// <summary>
        /// Returns a random long within a specified range.
        /// </summary>
        /// <param name="min">The begining value of specified range.</param>
        /// <param name="max">The ending value of specified range. (Greater the begining value)</param>
        public static long RandomLong(long min, long max)
        {
            if (min > max)
                return 0;

            return (RandomLong(max - min) + min);
        }

        /// <summary>
        /// Returns a nonnegative random double between 0.0 and 1.0.
        /// </summary>
        /// <returns></returns>
        public static double RandomDouble()
        {
            return rand.NextDouble();
        }

        /// <summary>
        /// Return a negative random double between -1.0 and 0.0.
        /// </summary>
        /// <returns></returns>
        public static double RandomNegativeDouble()
        {
            return rand.NextDouble() * -1;
        }

        /// <summary>
        /// Returns a nonnegative random double less than the maximum value.
        /// </summary>
        /// <param name="max">The maximum value.</param>
        /// <returns></returns>
        public static double RandomDouble(double max)
        {
            if (max < 0)
                return 0;
            return rand.NextDouble() * max;
        }

        /// <summary>
        /// Returns a negative random double greater than the minimum value.
        /// </summary>
        /// <param name="min">The minimum value.</param>
        /// <returns></returns>
        public static double RandomNegativeDouble(double min)
        {
            if (min > 0)
                return 0;
            return rand.NextDouble() * min;
        }

        /// <summary>
        /// Returns a random double within a specified range.
        /// </summary>
        /// <param name="min">The begining value of specified range.</param>
        /// <param name="max">The ending value of specified range. (Greater the begining value)</param>
        public static double RandomDouble(double min, double max)
        {
            if (min > max)
                return 0;
            return rand.NextDouble() * (max - min) + min;
        }

        /// <summary>
        /// Returns a nonnegative random float between 0f and 1f
        /// </summary>
        /// <returns></returns>
        public static float RandomFloat()
        {
            return (float)rand.NextDouble();
        }

        /// <summary>
        /// Returns a negative random float.
        /// </summary>
        /// <returns></returns>
        public static float RandomNegativeFloat()
        {
            return (float)rand.NextDouble() * -1;
        }

        /// <summary>
        /// Returns a nonnegative random float less than the maximum value.
        /// </summary>
        /// <param name="max">The maximum value.</param>
        /// <returns></returns>
        public static float RandomFloat(float max)
        {
            if (max < 0)
                return 0;
            return (float)(rand.NextDouble() * max);
        }

        /// <summary>
        /// Returns a negative random float greater than the minimum value.
        /// </summary>
        /// <param name="min">The minimum value.</param>
        /// <returns></returns>
        public static float RandomNegativeFloat(float min)
        {
            if (min > 0)
                return 0;
            return (float)(rand.NextDouble() * min);
        }

        /// <summary>
        /// Returns a random float within a specified range.
        /// </summary>
        /// <param name="min">The begining value of specified range.</param>
        /// <param name="max">The ending value of specified range. (Greater the begining value)</param>
        public static float RandomFloat(float min, float max)
        {
            if (min > max)
                return 0;
            return (float)(rand.NextDouble() * (max - min) + min);
        }

        /// <summary>
        /// Returns a random byte.
        /// </summary>
        /// <returns></returns>
        public static byte RandomByte()
        {
            return (byte)rand.Next();
        }

        /// <summary>
        /// Returns a random byte less than the maximum value.
        /// </summary>
        /// <param name="max">The maximum value.</param>
        /// <returns></returns>
        public static byte RandomByte(byte max)
        {
            return (byte)rand.Next(0, max);
        }

        /// <summary>
        /// Returns a nonnegative random byte within a specified range.
        /// </summary>
        /// <param name="min">The begining value of specified range.</param>
        /// <param name="max">The ending value of specified range. (Greater the begining value)</param>
        public static byte RandomByte(byte min, byte max)
        {
            return (byte)(rand.Next(max-min) + min);
        }

        /// <summary>
        /// Returns a random vector with nonnegative co-ordinate less than the co-ordinate value.
        /// </summary>
        /// <param name="x">The X co-ordinate value.</param>
        /// <param name="y">The Y co-ordinate value.</param>
        public static Vector2 RandomVector(float x, float y)
        {
            if ((x < 0) || (y < 0))
                return Vector2.Zero;
            return new Vector2(RandomFloat(x), RandomFloat(y));
        }

        /// <summary>
        /// Returns a random vector with nonnegative co-ordinate within specified range.
        /// </summary>
        /// <param name="minx">The X co-ordinate value.</param>
        /// <param name="maxx">The Y co-ordinate value.</param>
        /// <param name="miny">The X co-ordinate value.</param>
        /// <param name="maxy">The Y co-ordinate value.</param>
        public static Vector2 RandomVector(float minx, float maxx, float miny, float maxy)
        {
            return new Vector2(RandomFloat(minx, maxx), RandomFloat(miny, maxy));
        }

        /// <summary>
        /// Returns a random vector with negative co-ordinate more than the co-ordinate value.
        /// </summary>
        /// <param name="x">The X co-ordinate value.</param>
        /// <param name="y">The Y co-ordinate value.</param>
        public static Vector2 RandomNegativeVector(float x, float y)
        {
            if ((x > 0) || (y > 0))
                return Vector2.Zero;
            return new Vector2(RandomNegativeFloat(x), RandomNegativeFloat(y));
        }

        /// <summary>
        /// Returns a random vector within specified region
        /// </summary>
        /// <param name="region">Region containts of returned vector</param>
        /// <returns>Random Vector</returns>
        public static Vector2 RandomVector(Rectangle region)
        {
            return RandomVector(region.X, region.X + region.Width, region.Y, region.Y + region.Height);
        }

        /// <summary>
        /// Returns a random unit vector
        /// </summary>
        /// <returns>Unit vector</returns>
        public static Vector2 RandomVector()
        {
            Vector2 result = new Vector2(RandomFloat(), RandomFloat());
            result.Normalize();
            return result;
        }

        /// <summary>
        /// Returns a random vector with nonnegative co-ordinate as length as a value vector.
        /// </summary>
        /// <param name="v">The value vector.</param>
        /// <returns></returns>
        public static Vector2 RandomVector(Vector2 v)
        {
            Vector2 result = new Vector2(RandomFloat(), RandomFloat());
            result.Normalize();
            result *= v.Length();
            return result;
        }

        /// <summary>
        /// Returns a random vector with negative co-ordinate as length as a value vector.
        /// </summary>
        /// <param name="v">The value vector</param>
        /// <returns></returns>
        public static Vector2 RandomNegativeVector(Vector2 v)
        {
            Vector2 result = new Vector2(RandomNegativeFloat(), RandomNegativeFloat());
            result.Normalize();
            result *= v.Length();
            return result;
        }

        /// <summary>
        /// Return a random vector have length within a specified range of two other vector.
        /// </summary>
        /// <param name="v1">The begining value of specified range.</param>
        /// <param name="v2">The ending value of specified range. (Greater the begining value)</param>
        /// <returns></returns>
        public static Vector2 RandomVector(Vector2 v1, Vector2 v2)
        {
            float delta1 = 0;
            float delta2 = 0;
            float delta = 0;

            delta1 = v1.Length();
            delta2 = v2.Length();
            delta += delta2 - delta1;

            Vector2 result = new Vector2(RandomFloat(), RandomFloat());
            result.Normalize();
            result *= delta;

            return result;
        }

        /// <summary>
        /// Returns a random rectangle with nonnegative co-ordinate and size less than the maximum value. 
        /// </summary>
        /// <param name="posX">The X co-ordinate value.</param>
        /// <param name="posY">The Y co-ordinate value.</param>
        /// <param name="width">The width value.</param>
        /// <param name="height">The height value</param>
        /// <returns></returns>
        public static Rectangle RandomRectangle(int posX, int posY, int width, int height)
        {
            if ((posX < 0) || (posY < 0) || (width < 0) || (height < 0))
                return Rectangle.Empty;
            return new Rectangle(RandomInt(posX), RandomInt(posY), RandomInt(width), RandomInt(height));
        }
		
        /// <summary>
        /// Returns a random color with byte value less than the value.
        /// </summary>
        /// <param name="r">The R value.</param>
        /// <param name="g">The G value.</param>
        /// <param name="b">The B value.</param>
        /// <param name="a">The A value.</param>
        /// <returns></returns>
        public static Color RandomColor(byte r, byte g, byte b, byte a)
        {
            return new Color(RandomByte(r), RandomByte(g), RandomByte(b), RandomByte(a));
        }

        /// <summary>
        /// Returns a random color with byte value less than the value and default A value (255).
        /// </summary>
        /// <param name="r">The R value.</param>
        /// <param name="g">The G value.</param>
        /// <param name="b">The B value.</param>
        /// <returns></returns>
        public static Color RandomColor(byte r, byte g, byte b)
        {
            return new Color(RandomByte(r), RandomByte(g), RandomByte(b), 255);
        }

        /// <summary>
        /// Random A Color
        /// </summary>
        /// <returns></returns>
        public static Color RandomColor()
        {
            return new Color(RandomByte(), RandomByte(), RandomByte(), RandomByte());
        }

        /// <summary>
        /// Random A Solid Color
        /// </summary>
        /// <returns></returns>
        public static Color RandomSolidColor()
        {
            return new Color(RandomByte(), RandomByte(), RandomByte(), 255);
        }

        /// <summary>
        /// Random a logic (boolean) value with specified true ratio
        /// </summary>
        /// <param name="trueRatio">True ratio (default is 0.5)</param>
        /// <returns>Random logic value</returns>
        public static bool RandomLogic(double trueRatio)
        {
            return (rand.NextDouble() < trueRatio);
        }

        /// <summary>
        /// Random a logic (boolean) value with specified true ratio
        /// </summary>
        /// <param name="trueRatio">True ratio (default is 0.5)</param>
        /// <returns>Random logic value</returns>
        public static bool RandomLogic()
        {
            return (rand.NextDouble() < 0.5);
        }
    }
}
