using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoomGame.Entity.Renderer;

namespace BoomGame.Manager.RendererComparer
{
    public class RendererComparer : IComparer<DefaultRenderer>
    {
        private static RendererComparer inst = new RendererComparer();

        private RendererComparer()
        {
        }

        public static RendererComparer getInstance()
        {
            return inst;
        }

        public int Compare(DefaultRenderer x, DefaultRenderer y)
        {
            return (x.DrawOrder > y.DrawOrder) ? 1 : (x.DrawOrder < y.DrawOrder) ? -1 : 0;
        }
    }
}
