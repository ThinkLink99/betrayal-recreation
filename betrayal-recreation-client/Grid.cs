using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace betrayal_recreation_client
{
    public class Grid
    {
        private float _cellSize;
        private int _columns = 0;
        private int _rows = 0;

        private Room[,] _grid;

        public Grid(float cellSize, int columns, int rows)
        {
            _cellSize = cellSize;
            _columns = columns;
            _rows = rows;

            _grid = new Room[_columns, _rows];

            for (int y = 0; y < _rows; y++)
            {
                for (int x = 0; x < _columns; x++)
                {
                    SetCell(x, y, new Room(0, "", "", null, false));
                }
            }
        }

        public Room GetCell (int x, int y)
        {
            return _grid[x, y];
        }
        public void SetCell (int x, int y, Room value)
        {
            _grid[x, y] = value;
        }

        public void Print ()
        {
            string borderH = "| ";
            string borderV = "";
            for (int i = 0; i < _columns; i++) borderH += "- ";
            borderH += "|\n";
            Console.Write(borderH);
            for (int y = 0; y < _rows; y++)
            {
                borderV = "| ";
                for (int x = 0; x < _columns; x++)
                {
                    borderV += _grid[x, y].ID;
                    borderV += " ";
                }
                borderV += "|\n";
                Console.Write(borderV);
            }
            Console.Write(borderH);
        }
    }
}
