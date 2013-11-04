using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCSEngine.Utils.GameObject
{
    public delegate void OnCompletedHandler(object sender, ICompleteable completedObject, EventArgs args);

    public interface ICompleteable
    {
        bool IsCompleted { get; }

        event OnCompletedHandler OnCompleted;
    }
}
