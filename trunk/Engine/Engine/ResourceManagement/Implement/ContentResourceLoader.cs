using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SCSEngine.ResourceManagement.Implement
{
    public class GameContentResourceLoader<E> : IResourceLoader
    {
        private ContentManager contentMan;

        public GameContentResourceLoader(ContentManager contentManager)
        {
            this.contentMan = contentManager;
        }

        #region IResourceLoader Members

        public object Load(string resourceName)
        {
            return this.contentMan.Load<E>(resourceName);
        }

        public Type ResourceType
        {
            get { return typeof(E); }
        }

        public bool IsResourceReuseable
        {
            get { return true; }
        }

        #endregion

        #region IResourceLoader Members



        #endregion
    }
}
