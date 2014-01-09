using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoomGame.Entity.Collide;
using BoomGame.Interface.Manager;
using BoomGame.Entity;
using Microsoft.Xna.Framework;
using BoomGame.Shared;
using BoomGame.Grid;

namespace BoomGame.Manager
{
    public class CollisionManager : Interface.IManager<ICollidable>
    {
        protected List<ICollidable> contents;
        protected IGameManager parent;

        public CollisionManager(IGameManager parent)
        {
            contents = new List<ICollidable>();
            this.Parent = parent;
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
                    if (i != j && Contents[i].Bound.Intersects(Contents[j].Bound))
                    {
                        Contents[i].Collision(Contents[j]);
                    }
                }
            }

            for (int i = 0; i < Contents.Count; ++i)
            {
                Contents[i].ApplyCollision();
            }

            for (int i = 0; i < Contents.Count; ++i)
            {
                if (Contents[i].IsDead)
                {
                    if (Contents[i] is EnemyEntity)
                    {
                        Global.Counter_Enemy--;
                    }
                    else if (Contents[i] is BomberEntity)
                    {
                        Global.Counter_Bomber--;
                    }
                    else if (Contents[i] is ObstacleEntity)
                    {
                        Grid.Grid.getInst().Remove(Contents[i] as IGameEntity);
                        Cell cell = Grid.Grid.getInst().GetCellAtPosition((Contents[i] as IGameEntity).RendererObj.Position);
                        if (cell != null)
                            cell.Contents.Remove(Contents[i] as IGameEntity);
                    }
                    //...
                    this.Parent.Remove(Contents[i] as IGameEntity);
                }
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
