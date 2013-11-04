using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCSEngine.ServiceManagement.Implement
{
    public class BaseServiceCollection : IServiceCollection
    {
        private Dictionary<Type, IService> services = new Dictionary<Type,IService>();

        #region IServiceCollection Members

        public void AddService(IService service)
        {
            this.services.Add(typeof(IService), service);
        }

        public E GetService<E>() where E : IService
        {
            if (this.services.ContainsKey(typeof(E)))
                return (E) services[typeof(E)];

            return default(E);
        }

        #endregion
    }
}
