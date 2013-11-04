using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCSEngine.ServiceManagement;

namespace SCSEngine.ScreenManagement.Implement
{
    public interface IGameScreenManagerFactory
    {
        IGameScreenBank CreateScreenBank();
    }
}
