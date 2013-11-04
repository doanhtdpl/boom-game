using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCSEngine.ServiceManagement
{
    public interface IServiceCollection
    {
        void AddService(IService Service);
        E GetService<E>() where E : IService;
    }
}