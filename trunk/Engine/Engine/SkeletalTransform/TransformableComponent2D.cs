using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSCEngine.SkeletalTransform
{
    public class TransformableComponent2D : ITransformModel
    {
        private Matrix transform;
        private bool isTransformChanged = false;
        public Matrix TransformMatrix
        {
            get
            {
                if (isTransformChanged)
                {
                    this.UpdateMatrix();
                    this.isTransformChanged = false;
                }

                return this.transform;
            }
        }

        private Vector2 position, scale, origin;
        private float rotation;

        public Vector2 Origin
        {
            get { return origin; }
            set { this.isTransformChanged = true; origin = value; }
        }

        public Vector2 Scale
        {
            get { return scale; }
            set { this.isTransformChanged = true; scale = value; }
        }

        public float Rotation
        {
            get { return rotation; }
            set { this.isTransformChanged = true; rotation = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { this.isTransformChanged = true; position = value; }
        }

        private void UpdateMatrix()
        {
            this.transform = Matrix.CreateTranslation(new Vector3(-origin, 0f)) * Matrix.CreateScale(scale.X, scale.Y, 1f) * Matrix.CreateRotationZ(rotation) * Matrix.CreateTranslation(new Vector3(position + origin, 0f));
        }
    }
}
