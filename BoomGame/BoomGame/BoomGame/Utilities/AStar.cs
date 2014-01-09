using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using BoomGame.Grid;

namespace BoomGame.Utilities
{
    public class AStar
    {
        private List<Cell> openList = new List<Cell>();
        private List<Cell> closedList = new List<Cell>();

        public Grid.Grid grid;

        private static AStar inst;

        private AStar()
        {
        }

        // Singleton
        public static AStar getInst()
        {
            if (inst == null)
                inst = new AStar();
            return inst;
        }

        public void onInit(int row, int colum, float x, float y, float width, float height)
        {
            grid = Grid.Grid.getInst();
            grid.onInit(new Vector2(x, y), row, colum, width, height);
        }

        public AStar(Grid.Grid grid)
        {
            this.grid = grid;
        }

        public void SetWall(params Vector2[] walls)
        {
            for (int i = 0; i < walls.Length; ++i)
            {
                Cell cell = (Cell)grid.GetCellAtPosition(walls[i]);
                cell.CellState = CellState.WALL;
            }
        }

        public void SetWall(Vector2 position)
        {
            Cell cell = (Cell)grid.GetCellAtPosition(position);
            if(cell != null)
                cell.CellState = CellState.WALL;
        }

        public List<Vector2> PathFinding(Vector2 startPoint, Vector2 destination)
        {
            grid.PathClean();

            Cell start = (Cell)grid.GetCellAtPosition(startPoint);
            Cell dest = (Cell)grid.GetCellAtPosition(destination);

            if (start == null || dest == null)
                return null;
			
            openList = new List<Cell>();
            closedList = new List<Cell>();

            Cell currentCell = null;

            openList.Add(start);

            while (openList.Count > 0 && !openList.Contains(dest))
            {
                currentCell = openList[0];
                foreach (Cell cell in openList)
                {
                    if ((cell.G + cell.H) < (currentCell.G + currentCell.H))
                    {
                        currentCell = cell;
                    }
                }
                SwitchList(currentCell);

                Point currentCellLocation = new Point((int)currentCell.Location.X, (int)currentCell.Location.Y);
                for (int i = -1; i < 2; ++i)
                {
                    for (int j = -1; j < 2; ++j)
                    {
                        Vector2 iPos = new Vector2(currentCellLocation.X + j, currentCellLocation.Y + i);
                        if (iPos.X == currentCellLocation.X || iPos.Y == currentCellLocation.Y)
                        {
                            Cell c = (Cell)grid.GetCellAtLocation(iPos);

                            if (c != null && c != currentCell && c.CellState != CellState.WALL && !closedList.Contains(c))
                            {
                                int distance = ((currentCellLocation.Y + i) == currentCellLocation.Y || (currentCellLocation.X + j) == currentCellLocation.X) ? 10 : 14;
                                if (!openList.Contains(c))
                                {
                                    openList.Add(c);
                                    c.Parent = currentCell;
                                    c.G = distance + currentCell.G;
                                    c.H = (int)(Math.Abs(dest.Location.X - c.Location.X) + Math.Abs(dest.Location.Y - c.Location.Y)) * 10;
                                }
                                else
                                {
                                    if (c.G > currentCell.G + distance)
                                    {
                                        c.Parent = currentCell;
                                        c.G = currentCell.G + distance;
                                        c.H = (int)(Math.Abs(dest.Location.X - c.Location.X) + Math.Abs(dest.Location.Y - c.Location.Y)) * 10;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Make a path
            List<Vector2> result = new List<Vector2>();
            result.Add(destination);
            if (currentCell != null)
            {
                while (currentCell.Parent != null)
                {
                    Vector2 cellVec = currentCell.Position;
                    result.Add(cellVec);
                    currentCell = currentCell.Parent;
                }
                //result.Add(startPoint);
                result.Reverse();
            }

            return result;
        }

        private void SwitchList(Cell cell)
        {
            if (openList.Contains(cell))
                openList.Remove(cell);

            if (!closedList.Contains(cell))
                closedList.Add(cell);
        }
    }
}
