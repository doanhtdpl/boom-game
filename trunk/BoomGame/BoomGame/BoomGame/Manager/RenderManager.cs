using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoomGame.Entity.Renderer;
using Microsoft.Xna.Framework;
using BoomGame.Entity;
using BoomGame.Interface.Manager;

namespace BoomGame.Manager
{
    public class RenderManager : Interface.IManager<DefaultRenderer>
    {
        protected List<DefaultRenderer> contents;
        protected IGameManager parent;

        public RenderManager(IGameManager parent)
        {
            contents = new List<DefaultRenderer>();
            this.Parent = parent;
        }

        public List<DefaultRenderer> Contents
        {
            get { return contents; }
        }

        public IGameManager Parent
        {
            get
            {
                return parent;
            }
            set
            {
                parent = value;
            }
        }

        public void UpdateRealTime()
        {
            Contents.Sort(RendererComparer.RendererComparer.getInstance());
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < Contents.Count; ++i)
            {
                if (Contents[i].Visible)
                    Contents[i].Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            for (int i = 0; i < Contents.Count; ++i)
            {
                if(Contents[i].Visible)
                    Contents[i].Draw(gameTime);
            }
        }

        public void Add(DefaultRenderer element)
        {
            this.contents.Add(element);
        }

        public void Remove(DefaultRenderer element)
        {
            this.contents.Remove(element);
        }

        public void Remove(DefaultRenderer element, IGameManager p)
        {
            this.Remove(element);
        }
    }
}
