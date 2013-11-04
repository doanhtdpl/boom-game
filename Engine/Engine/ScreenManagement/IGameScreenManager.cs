using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SCSEngine.ServiceManagement;

namespace SCSEngine.ScreenManagement
{
    public interface IGameScreenManager
    {
        Game Game { get; }
        bool SaveScreen { get; set; }

        IGameScreen Current { get; }
        List<IGameScreen> Screens { get; }
        IGameScreenBank Bank { get; }

        void AddExclusive(IGameScreen screen);
        void AddPopup(IGameScreen screen);
        void AddImmediately(IGameScreen screen);

        void RemoveCurrent();
        void RemoveScreen(IGameScreen screen);
    }
}