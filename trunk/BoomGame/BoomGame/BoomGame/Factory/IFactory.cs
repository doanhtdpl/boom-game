using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoomGame.Factory
{
    interface IFactory<T>
    {
        T create(Object info);
    }
}
