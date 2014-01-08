using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using BoomGame.Entity;
using System.Diagnostics;


namespace BoomGame.Grid
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Grid : Microsoft.Xna.Framework.GameComponent
    {
        protected Vector2 position;
        public Vector2 Position
        {
            get { return this.position; }
        }

        protected Vector2 size;
        public Vector2 Size
        {
            get { return this.size; }
        }

        protected List<Cell> cells = new List<Cell>();
        public List<Cell> Cells
        {
            get { return this.cells; }
        }

        protected Vector2 cellSize;
        public Vector2 CellSize
        {
            get { return this.cellSize; }
        }

        protected List<IGameEntity> entities = new List<IGameEntity>();

        private static Grid inst;
        public static Game game;

        private Grid(Game game)
            : base(game)
        {
        }

        public static Grid getInst()
        {
            if (inst == null)
            {
                inst = new Grid(game);
            }
            return inst;
        }

        public void onInit(Vector2 position, int row, int colum, float cellWidth, float cellHeight)
        {
            this.ClearAll();
            this.size = new Vector2(row, colum);
            this.cellSize = new Vector2(cellWidth, cellHeight);
            this.position = position;

            for (int i = 0; i < (row * colum); ++i)
            {
                Vector2 cellLocation = new Vector2(i/colum, i%colum);
                Cell cell = new Cell();
                cell.onInit(cellWidth, cellHeight, cellLocation, new Vector2(position.X + cellLocation.Y * cellSize.X, position.Y + cellLocation.X * cellSize.Y));

                cells.Add(cell);
                Debug.WriteLine(cellLocation.X + ", " + cellLocation.Y);
            }
        }

        public override void Update(GameTime gameTime)
        {
            this.Clear();

            foreach (IGameEntity entity in entities)
            {
                AddAtPositionWithoutAddContent(entity, entity.RendererObj.Position);
            }

            base.Update(gameTime);
        }

        public void AddAtLocation(IGameEntity entity, Vector2 location)
        {
            entities.Add(entity);
            AddAtLocationWithoutAddContent(entity, location);
        }

        protected void AddAtLocationWithoutAddContent(IGameEntity entity, Vector2 location)
        {
            Cell cell = GetCellAtLocation(location);
            if (cell != null)
                cell.Add(entity);
        }

        public void AddAtPosition(IGameEntity entity, Vector2 position)
        {
            entities.Add(entity);
            AddAtPositionWithoutAddContent(entity, position);
        }

        protected void AddAtPositionWithoutAddContent(IGameEntity entity, Vector2 position)
        {
            Cell cell = GetCellAtPosition(position);
            if(cell != null)
                cell.Add(entity);
        }

        public bool Remove(IGameEntity entity)
        {
            return entities.Remove(entity);
        }

        public Cell GetCellAtPosition(Vector2 position)
        {
            int posY = (int)((position.X - this.position.X) / cellSize.Y);
            int posX = (int)((position.Y - this.position.Y) / cellSize.X);
            if (position.X >= 0 && position.Y >= 0 && posX >= 0 && posY >= 0 && posX < size.X && posY < size.Y)
            {
                return cells[posX * (int)size.Y + posY];
            }
            return null;
        }

        public Cell GetCellAtLocation(Vector2 location)
        {
            try
            {
                return cells[(int)(location.X * (int)size.Y + location.Y)];
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public void Clear()
        {
            foreach (Cell cell in cells)
            {
                cell.Clear();
            }
        }

        public void ClearAll()
        {
            this.entities.Clear();
            foreach (Cell cell in cells)
            {
                cell.Clear();
            }
        }
    }
}
