using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoomGame.Entity.Collide;
using BoomGame.Interface.Manager;
using BoomGame.Entity;

namespace BoomGame.Manager
{
    public class CollisionManager : Interface.IManager<ICollidable>
    {
        protected List<ICollidable> contents;
        protected IGameManager parent;

        public CollisionManager()
        {
            contents = new List<ICollidable>();
        }

        public List<ICollidable> Contents
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
            for (int i = 0; i < Contents.Count; ++i)
            {
                for (int j = 0; j < Contents.Count; ++j)
                {
                    if (i != j)
                    {
                        Contents[i].Collision(Contents[j]);
                    }
                }
            }

            for (int i = 0; i < Contents.Count; ++i)
            {
                if (Contents[i].IsDead)
                    this.Parent.Remove(Contents[i] as IGameEntity);
                else
                    Contents[i].ApplyCollision();
            }
        }

        public void Add(ICollidable element)
        {
            this.contents.Add(element);
        }

        public void Remove(ICollidable element)
        {
            this.contents.Remove(element);
        }

        public void Remove(ICollidable element, IGameManager p)
        {
            this.Remove(element);
        }
    }
}
