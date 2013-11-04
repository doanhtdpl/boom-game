using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSCEngine.Utils.Mathematics
{
    public struct RectangleF : IEquatable<RectangleF>
    {
        public float X, Y, Width, Height;

        public float Left { get { return X; } set { X = value; } }
        public float Top { get { return Y; } set { Y = value; } }
        public float Right { get { return X + Width; } set { Width = value - X; } }
        public float Bottom { get { return Y + Height; } set { Height = value - Y; } }

        public Vector2 Position { get { return new Vector2(X, Y); } set { X = value.X; Y = value.Y; } }
        public Vector2 Size { get { return new Vector2(Width, Height); } set { Width = value.X; Height = value.Y; } }
        public Vector2 Center { get { return new Vector2(X + Width / 2, Y + Height / 2); } }

        public Rectangle Rectangle { get { return new Rectangle((int)X, (int)Y, (int)Width, (int)Height); } }
        
        //
        // Summary:
        //     Initializes a new instance of Rectangle.
        //
        // Parameters:
        //   x:
        //     The x-coordinate of the rectangle.
        //
        //   y:
        //     The y-coordinate of the rectangle.
        //
        //   width:
        //     Width of the rectangle.
        //
        //   height:
        //     Height of the rectangle.
        public RectangleF(float x, float y, float width, float height)
            : this()
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        static RectangleF()
        {
            Empty = new RectangleF(0f, 0f, 0f, 0f);
        }

        // Summary:
        //     Compares two rectangles for inequality.
        //
        // Parameters:
        //   a:
        //     Source rectangle.
        //
        //   b:
        //     Source rectangle.
        //public static bool operator !=(RectangleF a, RectangleF b)
        //{
        //    return !a.Equals(b);
        //}
        //
        // Summary:
        //     Compares two rectangles for equality.
        //
        // Parameters:
        //   a:
        //     Source rectangle.
        //
        //   b:
        //     Source rectangle.
        //public static bool operator ==(Rectangle a, Rectangle b)
        //{
        //    return a.Equals(b);
        //}

        public static RectangleF Empty { get; private set; }

        //
        // Summary:
        //     Gets a value that indicates whether the Rectangle is empty.
        public bool IsEmpty { get { return this.Size.X <= 0 || this.Size.Y <= 0; } }

        // Summary:
        //     Determines whether this Rectangle contains a specified Point.
        //
        // Parameters:
        //   value:
        //     The Point to evaluate.
        public bool Contains(Vector2 value)
        {
            return (this.X <= value.X && this.X + this.Width >= value.X &&
                this.Y <= value.Y && this.Y + this.Height >= value.Y);
        }
        //
        // Summary:
        //     Determines whether this Rectangle entirely contains a specified Rectangle.
        //
        // Parameters:
        //   value:
        //     The Rectangle to evaluate.
        public bool Contains(RectangleF value)
        {
            return (this.X <= value.X && this.X + this.Width >= value.X + value.Width &&
                this.Y <= value.Y && this.Y + this.Height >= value.Y + value.Height);
        }

        //
        // Summary:
        //     Determines whether this Rectangle contains a specified point represented
        //     by its x- and y-coordinates.
        //
        // Parameters:
        //   x:
        //     The x-coordinate of the specified point.
        //
        //   y:
        //     The y-coordinate of the specified point.
        public bool Contains(float x, float y)
        {
            return (this.X <= x && this.X + this.Width >= x &&
                this.Y <= y && this.Y + this.Height >= y);
        }

        //
        // Summary:
        //     Retrieves a string representation of the current object.
        public override string ToString()
        {
            return string.Format("{X={0} Y={1} Width={2} Height={3}}", X, Y, Width, Height);
        }

        public bool Equals(RectangleF other)
        {
            return (this.X == other.X && this.Y == other.Y && this.Width == other.Width && this.Height == other.Height);
        }

        public bool IsIntersect(RectangleF other)
        {
            return !(this.Left > other.Right
                || this.Right < other.Left
                || this.Top > other.Bottom
                || this.Bottom < other.Top
                );
        }

        public void ToEmpty()
        {
            this.Position = Vector2.Zero;
            this.Size = Vector2.Zero;
        }

        public void Alter(RectangleF r)
        {
            this.X = r.X;
            this.Y = r.Y;
            this.Width = r.Width;
            this.Height = r.Height;
        }

        public RectangleF Intersect(RectangleF r)
        {
            RectangleF intersection = this;

            intersection.Left = Math.Max(intersection.Left, r.Left);
            intersection.Top = Math.Max(intersection.Top, r.Top);
            intersection.Right = Math.Min(intersection.Right, r.Right);
            intersection.Bottom = Math.Min(intersection.Bottom, r.Bottom);

            return intersection;
        }
    }

    public class CRectangleF : IEquatable<RectangleF>
    {
        public Vector2 Position, Size;

        public float Left { get { return Position.X; } set { Position.X = value; } }
        public float Top { get { return Position.Y; } set { Position.Y = value; } }
        public float Right { get { return Position.X + Size.X; } set { Size.X = value - Position.X; } }
        public float Bottom { get { return Position.Y + Size.Y; } set { Size.Y = value - Position.Y; } }
        
        public float X { get { return Position.X; } set { Position.X = value; } }
        public float Y { get { return Position.Y; } set { Position.Y = value; } }
        public float Width { get { return Size.X; } set { Size.X = value; } }
        public float Height { get { return Size.Y; } set { Size.Y = value; } }

        public Vector2 Center { get { return new Vector2(Position.X + Size.X / 2, Position.Y + Size.Y / 2); } }
        public Rectangle Rectangle { get { return new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y); } }
        public RectangleF RectangleF { get { return new RectangleF(Position.X, Position.Y, Size.X, Size.Y); } }

        //
        // Summary:
        //     Initializes a new instance of Rectangle.
        //
        // Parameters:
        //   x:
        //     The x-coordinate of the rectangle.
        //
        //   y:
        //     The y-coordinate of the rectangle.
        //
        //   width:
        //     Width of the rectangle.
        //
        //   height:
        //     Height of the rectangle.
        public CRectangleF(float x, float y, float width, float height)
        {
            this.Position.X = x;
            this.Position.Y = y;
            this.Size.X = width;
            this.Size.Y = height;
        }

        public CRectangleF(CRectangleF r)
        {
            this.Position = r.Position;
            this.Size = r.Size;
        }

        static CRectangleF()
        {
        }

        // Summary:
        //     Compares two rectangles for inequality.
        //
        // Parameters:
        //   a:
        //     Source rectangle.
        //
        //   b:
        //     Source rectangle.
        //public static bool operator !=(RectangleF a, RectangleF b)
        //{
        //    return !a.Equals(b);
        //}
        //
        // Summary:
        //     Compares two rectangles for equality.
        //
        // Parameters:
        //   a:
        //     Source rectangle.
        //
        //   b:
        //     Source rectangle.
        //public static bool operator ==(Rectangle a, Rectangle b)
        //{
        //    return a.Equals(b);
        //}

        public static CRectangleF Empty
        {
            get
            {
                return new CRectangleF(0f, 0f, 0f, 0f);
            }
        }

        //
        // Summary:
        //     Gets a value that indicates whether the Rectangle is empty.
        public bool IsEmpty { get { return this.Size.X <= 0 || this.Size.Y <= 0; } }

        // Summary:
        //     Determines whether this Rectangle contains a specified Point.
        //
        // Parameters:
        //   value:
        //     The Point to evaluate.
        public bool Contains(Vector2 value)
        {
            return (this.Position.X <= value.X && this.Position.X + this.Size.X >= value.X &&
                this.Position.Y <= value.Y && this.Position.Y + this.Size.Y >= value.Y);
        }
        //
        // Summary:
        //     Determines whether this Rectangle entirely contains a specified Rectangle.
        //
        // Parameters:
        //   value:
        //     The Rectangle to evaluate.
        public bool Contains(RectangleF value)
        {
            return (this.Position.X <= value.Position.X && this.Position.X + this.Size.X >= value.Position.X + value.Size.X &&
                this.Position.Y <= value.Position.Y && this.Position.Y + this.Size.Y >= value.Position.Y + value.Size.Y);
        }

        //
        // Summary:
        //     Determines whether this Rectangle contains a specified point represented
        //     by its x- and y-coordinates.
        //
        // Parameters:
        //   x:
        //     The x-coordinate of the specified point.
        //
        //   y:
        //     The y-coordinate of the specified point.
        public bool Contains(float x, float y)
        {
            return (this.Position.X <= x && this.Position.X + this.Size.X >= x &&
                this.Position.Y <= y && this.Position.Y + this.Size.Y >= y);
        }

        //
        // Summary:
        //     Retrieves a string representation of the current object.
        public override string ToString()
        {
            return string.Format("{X={0} Y={1} Width={2} Height={3}}", Position.X, Position.Y, Size.X, Size.Y);
        }

        public bool Equals(RectangleF other)
        {
            return (this.Position.X == other.Position.X && this.Position.Y == other.Position.Y && this.Size.X == other.Size.X && this.Size.Y == other.Size.Y);
        }

        public bool IsIntersect(CRectangleF other)
        {
            return !(this.Left > other.Right
                || this.Right < other.Left
                || this.Top > other.Bottom
                || this.Bottom < other.Top
                );
        }

        public void ToEmpty()
        {
            this.Position = Vector2.Zero;
            this.Size = Vector2.Zero;
        }

        public void Alter(CRectangleF r)
        {
            this.Position = r.Position;
            this.Size = r.Size;
        }

        public CRectangleF Intersect(CRectangleF r)
        {
            CRectangleF intersection = new CRectangleF(this);

            intersection.Left = Math.Max(intersection.Left, r.Left);
            intersection.Top = Math.Max(intersection.Top, r.Top);
            intersection.Right = Math.Min(intersection.Right, r.Right);
            intersection.Bottom = Math.Min(intersection.Bottom, r.Bottom);

            return intersection;
        }
    }
}
