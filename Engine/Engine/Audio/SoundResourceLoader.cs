using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using SCSEngine.ResourceManagement;
using SCSEngine.ResourceManagement.Implement;

namespace SCSEngine.Audio
{
    public class SoundResourceLoader : IResourceLoader
    {
        private IResourceManager resourceManager;

        public SoundResourceLoader(IResourceManager resMan)
        {
            this.resourceManager = resMan;
        }

        #region IResourceLoader Members

        public object Load(string resourceName)
        {
            return new Sound(resourceManager.GetResource<SoundEffect>(resourceName));
        }

        public Type ResourceType
        {
            get { return typeof(Sound); }
        }

        public bool IsResourceResuable
        {
            get { return false; }
        }

        #endregion
    }
}
