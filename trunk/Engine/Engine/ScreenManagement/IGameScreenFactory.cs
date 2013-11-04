using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCSEngine.ScreenManagement
{
    public interface IGameScreenFactory
    {
        IGameScreen CreateGameScreen();
    }
}
