using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoomGame.Entity.Logical;
using BoomGame.Entity.Renderer;

namespace BoomGame.Entity
{
    public interface IGameEntity
    {
        DefaultLogical LogicalObj
        {
            get;
            set;
        }

        DefaultRenderer RendererObj
        {
            get;
            set;
        }

        void onInit();
    }
}
