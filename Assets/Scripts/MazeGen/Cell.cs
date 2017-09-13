using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazeGen
{
    class Cell
    {
        private int id;
        public List<Cell> connections;
        public int getId { get { return id; } }

        public bool hasVisited = false;
        public Cell()
        {

        }
        public Cell(int id_)
        {
            id = id_;
            connections = new List<Cell>();
        }
        public void AddNeighbor(Cell n)
        {
            connections.Add(n);
        }
    }
}
