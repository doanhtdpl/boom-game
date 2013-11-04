using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCSEngine.ResourceManagement
{
    public interface IResourceLoader
    {
        object Load(string resourceName);
        Type ResourceType { get; }
        bool IsResourceResuable { get; }
    }
}
