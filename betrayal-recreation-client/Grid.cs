using System;

namespace betrayal_recreation_shared
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
            _ids = new int[_columns, _rows];

            for (int y = 0; y < _rows; y++)
            {
                for (int x = 0; x < _columns; x++)
                {
                    SetCell(x, y, Room.Empty);
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
                    var i = _ids[x, y];
                    borderV += i == -1 ? " " : i.ToString();
                    borderV += " ";
                }
                borderV += "|\n";
                Console.Write(borderV);
            }
            Console.Write(borderH);
        }
        public bool Find (Room r, out int x, out int y)
        {
            x = 0; y = 0;

            for (int gY = 0; gY < _rows; gY++)
            {
                for (int gX = 0; gX < _columns; gX++)
                {
                    if (_ids[gX, gY] == r.ID)
                    {
                        x = gX; y = gY;
                        return true;
                    }
                }
            }

            return false;
        }
        public bool NullOrEmptyAt (int x, int y)
        {
            var r = GetCell(x, y);
            if (r == null || r.Equals(Room.Empty))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
