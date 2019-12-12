using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace AOC19
{
    class PaintingRobot
    {
        private int posX = 0, posY = 0;
        private enum Direction {Up,Down,Left,Right};
        private Direction dir = Direction.Up;
        private IntecodeComputer intecodeComputer;
        private Dictionary<Point,int> grid = new Dictionary<Point, int>();
        
        public int PaintedPanelCount { get{return grid.Count;} }
        
        public PaintingRobot(IntecodeComputer icc)
        {
            intecodeComputer = icc;
        }
        
        public void Run(int startColor)
        {
            //set color of start panel
            intecodeComputer.InputQueue.Enqueue(startColor);
            //Start computer
            var computerTask = Task.Run(() => intecodeComputer.Compute());

            while (!computerTask.IsCompleted)
            {
                //Paint the panel
                Paint(WaitForComputerOutput());
                //Move
                Move(WaitForComputerOutput());

                //read from Camera and insert to computer queueu
                lock(intecodeComputer.InputQueue)
                {
                    intecodeComputer.InputQueue.Enqueue(ReadCamera());
                }
                Thread.Sleep(1);
            }
        }

        private int WaitForComputerOutput()   
        {
            int value = -1;
            bool waitForQueue = true;
            while (waitForQueue)
            {
                lock (intecodeComputer.OutputQueue)
                {
                    if (intecodeComputer.OutputQueue.Any())
                    {
                        value = (int)intecodeComputer.OutputQueue.Dequeue();
                        waitForQueue = false;
                        
                    }
                }
            }
            return value;
        }

        private int ReadCamera()
        {
            var p = new Point(posX,posY);
            if(grid.ContainsKey(p))
            {
                return grid[p];
            }
            return 0; //0 on part A
               
        }
        void Compute()
        {

        }
        private void Move(int m) 
        {
            Turn(m);
            Step();
        }
        private void Turn(int m)//0 or 1
        {
            switch(dir)
            {
                case Direction.Up:      dir = m == 0 ? Direction.Left   : Direction.Right;  break;
                case Direction.Left:    dir = m == 0 ? Direction.Down   : Direction.Up;     break;
                case Direction.Down:    dir = m == 0 ? Direction.Right  : Direction.Left;   break;
                case Direction.Right:   dir = m == 0 ? Direction.Up     : Direction.Down;   break;
                default: throw new System.Exception("Invalid Turn operation");
            }
        }
        private void Step()
        {
            switch(dir)
            {
                case Direction.Up:      posY++;   break;
                case Direction.Left:    posX--;   break;
                case Direction.Down:    posY--;   break;
                case Direction.Right:   posX++;   break;
                default: throw new System.Exception("Invalid Move");
            }
        }
        private void Paint(int color)
        {
            var p = new Point(posX,posY);
            if(grid.ContainsKey(p))
            {
                grid[p] = color;
            }
            else
            {
                grid.Add(p,color);
            }
        }

        internal void Print()
        {
            var points = grid.Keys;
            int xMax = int.MinValue, xMin = int.MaxValue, yMax = int.MinValue , yMin = int.MaxValue;
            foreach(Point p in points)
            {
                if(p.X > xMax)
                    xMax = p.X;
                if(p.X < xMin)
                    xMin = p.X;
                if(p.Y > yMax)
                    yMax = p.Y;
                if(p.Y < yMin)
                    yMin = p.Y;
            }
            
            int[,] arr = new int[(yMax-yMin)+1, (xMax-xMin)+1];
            foreach(var valuePair in grid)
            {
                arr[valuePair.Key.Y - yMin, valuePair.Key.X - xMin] = valuePair.Value; 
            } 
            
            for(int y = (yMax-yMin); y > -1; y--)
            {
                for(int x = 0; x < (xMax-xMin)+1; x++)
                {
                    Console.Write(arr[y,x] == 0 ? ' ' : '#');
                }    
                Console.WriteLine();
            }
        }

        private struct Point
        {
            public int X;
            public int Y; 
            public Point(int x, int y)
            {
                X= x;
                Y= y;
            }
        }
        
    }        
}