using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCSEngine.ResourceManagement
{
    public interface IResourceManager
    {
        E GetResource<E>(string resourceName);
        object GetResource(string resourceName);

        void AddResourceLoader(IResourceLoader resourceLoader);

        void LoadResources();
        void UnloadResources();
    }
}
