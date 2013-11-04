using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSCEngine.SkeletalTransform
{
    public interface ISkeletalTransform : ITransformModel
    {
        ISkeletalTransform Parent { get; set; }
        IEnumerable<ISkeletalTransform> Childs { get; }

        void AddChild(ISkeletalTransform child);
        void RemoveChild(ISkeletalTransform child);

        Matrix SelfTransform { get; }
    }
}
