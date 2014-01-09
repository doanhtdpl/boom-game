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

        protected Vector2 position;
        public Vector2 Position
        {
            get { return this.position; }
        }

        protected List<IGameEntity> contents;
        public List<IGameEntity> Contents
        {
            get { return this.contents; }
        }

        public bool IsWalkable
        {
            get { return contents.Count == 0; }
        }

        public CellState CellState
        {
            get;
            set;
        }

        // Path finding
        public int H
        {
            get;
            set;
        }

        public int G
        {
            get;
            set;
        }

        public Cell Parent
        {
            get;
            set;
        }


        public Cell()
        {
        }

        public void onInit(float width, float height, Vector2 location, Vector2 position)
        {
            contents = new List<IGameEntity>();
            this.width = width;
            this.height = height;
            this.location = location;

            this.position = position;
        }

        public void Add(IGameEntity entity)
        {
            this.contents.Add(entity);
        }

        public bool Remove(IGameEntity entity)
        {
            return this.contents.Remove(entity);
        }

        public void ClearAll()
        {
            this.CellState = CellState.NONE;
            PathClean();
            Clear();
        }

        public void PathClean()
        {
            this.Parent = null;
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
