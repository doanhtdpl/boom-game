using Microsoft.Xna.Framework;
using SSCEngine.Utils.Mathematics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SSCEngine.Control
{
    public interface IListViewGestureHandler
    {
        void Move(Vector2 delta);
        void Release(Vector2 delta);

        void UpdateOffset(CRectangleF offset);
    }

    public class VerticalListViewGestureHandler : IListViewGestureHandler
    {
        private float velocity;
        private float deccelerator;
        private float baseDecelerator;

        public VerticalListViewGestureHandler(float baseDecel)
        {
            this.baseDecelerator = baseDecel;
            this.velocity = 0f;
            this.deccelerator = 0f;
        }

        public void Move(Vector2 delta)
        {
            this.velocity += delta.Y;
            this.deccelerator += velocity;
        }

        public void Release(Vector2 delta)
        {
            this.velocity += delta.Y;
            this.deccelerator += ((this.velocity >= 0)?1:-1)*baseDecelerator;
        }

        public void UpdateOffset(CRectangleF offset)
        {
            if (this.velocity != 0)
            {
                offset.Position.Y += this.velocity;
                float oldVel = this.velocity;
                this.velocity -= this.deccelerator;

                if (oldVel * this.velocity <= 0)
                {
                    this.velocity = 0;
                    this.deccelerator = 0;
                }
            }
        }
    }

    public class HorizontalListViewGestureHandler : IListViewGestureHandler
    {
        private float velocity;
        private float deccelerator;
        private float baseDecelerator;
        private float lastVel;

        public HorizontalListViewGestureHandler(float baseDecel)
        {
            this.baseDecelerator = baseDecel;
            this.velocity = 0f;
            this.deccelerator = 0f;
        }

        public void Move(Vector2 delta)
        {
            this.velocity = delta.X;
        }

        public void Release(Vector2 delta)
        {
            this.velocity = lastVel;
            this.deccelerator = baseDecelerator;
        }

        private const float minVel = 6;
        public void UpdateOffset(CRectangleF offset)
        {
            if (this.velocity != 0)
            {
                Debug.WriteLine("Offset:{0} - Velocity:{1} - Decel:{2}", offset.Position, velocity, deccelerator);
                offset.Position.X += this.velocity;
                this.lastVel = this.velocity;
                if (this.deccelerator != 0)
                {
                    this.velocity /= this.deccelerator;
                    if (Math.Abs(this.velocity) <= minVel)
                    {
                        this.velocity = 0;
                        this.deccelerator = 0;
                    }
                }
                else
                {
                    this.velocity = 0;
                }
            }
        }
    }
}
