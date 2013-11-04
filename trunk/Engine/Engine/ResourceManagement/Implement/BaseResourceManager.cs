using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCSEngine.ResourceManagement.Implement
{
    public class BaseResourceManager : IResourceManager
    {
        private Dictionary<Type, Dictionary<string, object>> resources = new Dictionary<Type, Dictionary<string, object>>();
        private Dictionary<Type, IResourceLoader> loaders = new Dictionary<Type, IResourceLoader>();

        #region IResourceManager Members

        public E GetResource<E>(string resourceName)
        {
            if (this.resources.ContainsKey(typeof(E)))
            {
                if (this.loaders[typeof(E)].IsResourceResuable && this.resources[typeof(E)].ContainsKey(resourceName))
                    return (E)resources[typeof(E)][resourceName];

                if (this.loaders.ContainsKey(typeof(E)))
                {
                    var resource = this.loaders[typeof(E)].Load(resourceName);

                    if (this.loaders[typeof(E)].IsResourceResuable &&  resource != null)
                    {
                        this.resources[typeof(E)].Add(resourceName, resource);
                    }

                    return (E)resource;
                }
            }

            return default(E);
        }

        public object GetResource(string resourceName)
        {
            foreach (var res in resources.Values)
            {
                if (res.ContainsKey(resourceName))
                    return res[resourceName];
            }

            return null;
        }

        public void AddResourceLoader(IResourceLoader resourceLoader)
        {
            this.loaders.Add(resourceLoader.ResourceType, resourceLoader);
            Dictionary<string, object> resDict = null;
            if (resourceLoader.IsResourceResuable)
            {
                resDict = new Dictionary<string, object>();
            }

            this.resources.Add(resourceLoader.ResourceType, resDict);
        }

        public void LoadResources()
        {
        }

        public void UnloadResources()
        {
        }

        #endregion
    }
}
