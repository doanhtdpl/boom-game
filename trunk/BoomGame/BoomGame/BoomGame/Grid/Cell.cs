using System;
using System.Collections.Generic;
using System.Linq;
using BoomGame.Entity;
using Microsoft.Xna.Framework;


namespace BoomGame.Grid
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Cell
    {
        protected float width;
        public float Width
        {
            get { return this.width; }
        }

        protected float height;
        public float Height
        {
            get { return this.height; }
        }

        protected Vector2 location;
        public Vector2 Location
        {
            get { return this.location; }
        }

        protected List<IGameEntity> contents;
        public List<IGameEntity> Contents
        {
            get { return this.contents; }
        }

        public Cell()
        {
        }

        public void onInit(float width, float height, Vector2 location)
        {
            contents = new List<IGameEntity>();
            this.width = width;
            this.height = height;
            this.location = location;
        }

        public void Add(IGameEntity entity)
        {
            this.contents.Add(entity);
        }

        public bool Remove(IGameEntity entity)
        {
            return this.contents.Remove(entity);
        }

        public void Clear()
        {
            this.contents.Clear();
        }

        public bool isContain(IGameEntity entity)
        {
            return this.contents.Contains(entity);
        }
    }
}
