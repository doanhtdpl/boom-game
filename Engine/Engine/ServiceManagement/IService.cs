using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCSEngine.ServiceManagement
{
    public delegate void ServiceChangedHandler(IService service);

    public interface IService
    {
        Type ServiceType { get; }

        E GetService<E>();

        event ServiceChangedHandler OnServiceChanged;
    }
}
