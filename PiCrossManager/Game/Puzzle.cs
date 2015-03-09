using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiCrossManager.Game
{
    public class Puzzle
    {
        private Cell[,] _cells;
        private int width;
        private int height;

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public Cell[,] Cells
        {
            get { return _cells; }
            set { _cells = value; }
        }

        public Puzzle() { }
    }
}
