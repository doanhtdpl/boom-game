using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SCSEngine.ScreenManagement
{
    public interface IGameScreen : IUpdateable, IDrawable, IGameComponent
    {
        Game Game { get; }
        string Name { get; }
        GameComponentCollection Components { get; }
        GameScreenState State { get; set; }
        IGameScreenManager Manager { get; }
    }
}