using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCSEngine.ScreenManagement
{
    public interface IGameScreenBank
    {
        IGameScreen GetScreen(string name);
        IGameScreen GetNewScreen(string name);
        IGameScreen GetScreen(string name, bool createNew);

        void AddScreenFactory(string name, IGameScreenFactory gsFactory);
    }
}
