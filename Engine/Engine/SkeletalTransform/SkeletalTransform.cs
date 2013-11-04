using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSCEngine.SkeletalTransform
{
    internal class SkeletalTransform : ISkeletalTransform
    {
        private ITransformModel myTransform;

        public SkeletalTransform(ITransformModel transform)
        {
            this.myTransform = transform;
        }

        public ISkeletalTransform Parent { get; set; }

        private List<ISkeletalTransform> childs = new List<ISkeletalTransform>();

        public Matrix SelfTransform
        {
            get
            {
                return this.myTransform.TransformMatrix;
            }
        }

        public Matrix TransformMatrix
        {
            get { return this.Parent.TransformMatrix * this.myTransform.TransformMatrix; }
        }

        public void AddChild(ISkeletalTransform child)
        {
            child.Parent = this;
            this.childs.Add(child);
        }

        public void RemoveChild(ISkeletalTransform child)
        {
            child.Parent = this;
            this.childs.Add(child);
        }

        public IEnumerable<ISkeletalTransform> Childs
        {
            get { return this.childs; }
        }
    }
}
