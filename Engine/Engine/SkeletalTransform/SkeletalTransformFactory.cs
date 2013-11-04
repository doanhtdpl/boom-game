using SCSEngine.Utils.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSCEngine.SkeletalTransform
{
    public class SkeletalTransformFactory
    {
        public ISkeletalTransform CreateProduct(ITransformModel rootModel)
        {
            return new SkeletalTransform(rootModel);
        }
    }
}
