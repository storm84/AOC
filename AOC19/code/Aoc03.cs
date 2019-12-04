namespace AOC19
{
    using System;
    using System.Collections.Generic;

    class Aoc03 : AocBase
    {
        public Aoc03(string filepath) : base(filepath)
        {
        }

        public override string PartA(string[] inputs)
        {
            
            List<Point> intersectionPoints = GetIntersectionPoints(inputs);

            int minDistance = int.MaxValue;
            foreach(Point p in intersectionPoints)
            {
                int distance = Math.Abs(p.X) + Math.Abs(p.Y);
                if(distance < minDistance)
                {
                    minDistance = distance;
                }
            }
            return minDistance.ToString();
        }

        public override string PartB(string[] inputs)
        {
            List<Point> intersectionPoints = GetIntersectionPoints(inputs);
            int minDistance = int.MaxValue;
            foreach(var p in intersectionPoints)
            {
                int distance = GetStepsToPoint(p, inputs[0]) + GetStepsToPoint(p, inputs[1]);
                if (distance < minDistance)
                {
                    minDistance = distance;
                }
                
            }
            
            return minDistance.ToString();
        }

        private int  GetStepsToPoint(Point p, string wire)
        {
            int distance = 0;
            var directions = wire.Split(',');
            Point pointA0 = new Point(0,0);
            foreach(var direction in directions)
            {
                Point pointA1= createPoint(pointA0, direction);
                if(pointA0.X == pointA1.X && p.X == pointA0.X 
                    && Math.Min(pointA0.Y,pointA1.Y) <= p.Y && p.Y <= Math.Max(pointA0.Y,pointA1.Y))
                {
                    distance += Math.Abs(p.Y - pointA0.Y);
                    return distance;
                }
                else if(pointA0.Y == pointA1.Y && p.Y == pointA0.Y
                    && Math.Min(pointA0.X,pointA1.X) <= p.X  && p.X <= Math.Max(pointA0.X,pointA1.X))
                {
                    distance += Math.Abs(p.X - pointA0.X);
                    return distance;
                }
                else
                {
                    distance += int.Parse(direction.Substring(1));
                }
                pointA0 = pointA1;
            }
            return 0;
        }

        private List<Point> GetIntersectionPoints(string [] inputs)
        {
            List<Point> intersectionPoints = new List<Point>();

            Point pointA0 = new Point(0,0);
            foreach(var directionA in inputs[0].Split(','))
            {
                Point pointA1 = createPoint(pointA0, directionA);

                Point pointB0 = new Point(0,0); 
                foreach(var directionB in inputs[1].Split(','))
                {
                    Point pointB1 = createPoint(pointB0, directionB);
                    //Check intersection
                    if(pointA0.X == pointA1.X)
                    {
                        if( pointB0.Y == pointB1.Y
                            && Math.Min(pointB0.X, pointB1.X) <= pointA0.X && Math.Max(pointB0.X, pointB1.X) >= pointA0.X
                            && Math.Min(pointA0.Y, pointA1.Y) <= pointB0.Y && Math.Max(pointA0.Y, pointA1.Y) >= pointB0.Y
                            )
                            {
                                //intersection match
                                intersectionPoints.Add(new Point(pointA0.X, pointB0.Y));
                            }
                    }
                    else
                    {
                        if( pointB0.Y != pointB1.Y
                            && Math.Min(pointA0.X, pointA1.X) <= pointB0.X && Math.Max(pointA0.X, pointA1.X) >= pointB0.X
                            && Math.Min(pointB0.Y, pointB1.Y) <= pointA0.Y && Math.Max(pointB0.Y, pointB1.Y) >= pointA0.Y
                            )
                            {
                                //intersection match
                                intersectionPoints.Add(new Point(pointB0.X, pointA0.Y));
                            }
                    }
                
                    pointB0 = pointB1;
                }
                pointA0 = pointA1;
            }
            return intersectionPoints;
        }
        private Point createPoint(Point startpoint, string direction)
        {
            switch(direction[0])
            {
                case 'R': startpoint.X += int.Parse(direction.Substring(1)); break;
                case 'L': startpoint.X -= int.Parse(direction.Substring(1)); break;
                case 'U': startpoint.Y += int.Parse(direction.Substring(1)); break;
                case 'D': startpoint.Y -= int.Parse(direction.Substring(1)); break;
            }
            return startpoint;
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