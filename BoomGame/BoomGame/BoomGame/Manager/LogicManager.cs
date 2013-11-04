using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoomGame.Entity.Logical;
using Microsoft.Xna.Framework;
using BoomGame.Interface.Manager;

namespace BoomGame.Manager
{
    public class LogicManager : Interface.IManager<DefaultLogical>
    {
        protected List<DefaultLogical> contents;
        protected IGameManager parent;

        public LogicManager()
        {
            contents = new List<DefaultLogical>();
        }

        public List<DefaultLogical> Contents
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
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < Contents.Count; ++i)
            {
                if(Contents[i].Enabled)
                    Contents[i].Update(gameTime);
            }
        }

        public void Add(DefaultLogical element)
        {
            this.contents.Add(element);
        }

        public void Remove(DefaultLogical element)
        {
            this.contents.Remove(element);
        }
    }
}
