using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace MazeGen
{
    /*class Program
    {
        const int SIZE_OF_MAZE = 5;
        static void Main(string[] args)
        {
            Maze m = new Maze(SIZE_OF_MAZE);
            m.GenCells();
            m.GenMaze();
        }


    }

    class Maze
    {
        //Properties
        char[] maze;        //Array to hold the maze
        Cell[] cells;
        int dimensions = 3;
        Stack<Cell> stack;
        int[] directions;

        //Constructor - Populate all relevent variables
        public Maze(int dimensions_)
        {
            dimensions = dimensions_;
            maze = new char[dimensions * dimensions];
            for (int i=0;i<maze.Length;i++)
            {
                maze[i] = '_';
            }
            stack = new Stack<Cell>();
            directions = new int[] { -(dimensions), (dimensions), 1 , -1 }; //North, South, East, West
        }

        //Generate all the cells for the stack to hold. 
        public void GenCells()
        {
            cells = new Cell[dimensions * dimensions];
            for (int i = 0; i < (dimensions * dimensions); i++)
            {
                cells[i] = new Cell(i);
            }
        }

        //Algorithm to gen the actual maze. Uses a stack. Looks at available next moves, moves there and continues. Backs up if no available moves. Replace "stepped" tiles with a 0. Records path
        public void GenMaze()
        {
            Random random = new Random();
            int start = random.Next(0, (dimensions));

            List<int> neighbors = new List<int>();

            //Actual Gen
            stack.Push(cells[start]);

            //find "longest path" with the largest stack
            int maxStackSize = 0;
            Cell furthestCell = null;
            while (stack.Count > 0) {
                int tempId = stack.Peek().getId;    //Get the current position
                maze[tempId] = '0';
                cells[tempId].hasVisited = true;

                
                //Look at all the possible neighbor choices
                    for (int i = 0; i < 4; i++)
                    {
                    //Prevent edge wrapping
                        if (i == 3 && tempId % dimensions == 0) { continue; }
                        if (i == 2 && (tempId + 1) % dimensions == 0) { continue; }

                        //Add avaiable neighbors
                        if (tempId + directions[i] >= 0 && tempId + directions[i] < (dimensions*dimensions) && !cells[tempId + directions[i]].hasVisited) { neighbors.Add(tempId + directions[i]); }
                    }
                //No possibilities, backtrack
                if (neighbors.Count == 0) { if (stack.Count > maxStackSize) { maxStackSize = stack.Count; furthestCell = stack.Peek(); } stack.Pop(); }
                else
                {
                    //1 neighbor, go there   
                    if (neighbors.Count == 1) { stack.Push(cells[neighbors[0]]); cells[tempId].AddNeighbor(cells[neighbors[0]]); }

                    //2+ neighbors, random choice here
                    else
                    {
                        int next = random.Next(0, neighbors.Count);
                        stack.Push(cells[neighbors[next]]);
                        cells[tempId].AddNeighbor(cells[neighbors[next]]);
                    }
                }
                //Dump old neighbors
                neighbors.Clear();                 
            }
            //done with the gen, time to create the final export
            maze[start] = 's';
            maze[furthestCell.getId] = 'x';
            MazeExport();
        }

        //Sets up and exports the maze in a way that unity tool can read. Must "double" size and migrate index to the correct spot. Adds in the connections between cells.
        public void MazeExport()
        {
            int newSize = (dimensions * 2) - 1;
            int oldSize =dimensions;
            char[] exportMaze = new char[newSize*newSize];
            for(int i = 0; i < exportMaze.Length; i++) { exportMaze[i] = ' '; }     //populate the maze with empty spaces
            for(int i = 0; i < maze.Length; i++)
            {
                //Work out the new positions for the new matrix.
                int rowNum = (i / oldSize);
                int colNum = (i - (rowNum * oldSize));

                int newPos = ((2 * rowNum) * newSize) + (2 * colNum);

                exportMaze[newPos] = maze[i];

                //Populate the connection cells
                foreach(Cell e in cells[i].connections)
                {
                    if (e.getId - cells[i].getId == -oldSize)     //Connection is up
                    {
                        exportMaze[newPos - newSize] = '0';
                    }
                    else if (e.getId - cells[i].getId == -1)      //Connection is to the left
                    {
                        exportMaze[newPos - 1] = '0';

                    }
                    else if (e.getId - cells[i].getId == 1)       //Connection is to the right
                    {
                        exportMaze[newPos + 1] = '0';

                    }
                    else if (e.getId - cells[i].getId == oldSize) //Connection is down
                    {
                        exportMaze[newPos + newSize] = '0';

                    }
                }
            }

            //Write maze to file.
            string export = "";
            for(int i = 0; i < newSize; i++)
            {
                for (int j = 0; j < newSize; j++)
                {
                    export += exportMaze[(i * newSize) + j];
                }
                export += System.Environment.NewLine;
            }
            File.WriteAllText("test.txt", export);
        }
    }
    */
}

/*

    position (int divid) small distance (3) --- Row num
    mult by small dist (3)
    position - prev --- col num
    double that --- new col number
    double row num --- new row number
    new row * new dist --- how many before
    add new col number

    0   -   1   -   2
    -   -   -   -   -
    3   -   4   -   5
    -   -   -   -   -
    6   -   7   -   8

    0   1   2   3   4
    5   6   7   8   9
    10  11  12  13  14
    15  16  17  18  19
    20  21  22  23  24

 */
