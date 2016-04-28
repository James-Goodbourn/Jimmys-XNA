///JimysXNA Created by James Goodbourn
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JimysXNA.Pathfinding
{
    public class coords
    {
        public Vector2 Coordinates;
        public coords parent;
        public int cost;
        public int mapVal;
        public int totalCost;
        public int heuristicCost;
        public string Direction;
    };

    public class Pathfinding
    {
        private int WIDTH = 100;
        private int HEIGHT = 100;
        private int[,] map;// = new int[WIDTH,HEIGHT];
        private List<coords> OpenList;
        private List<coords> ClosedList;
        private List<coords> PathList;
        private List<string> DirectionList;
        public List<Vector2> CoordsList;

        private coords path;
        private coords current;
        private coords goal;
        private coords root;
        private coords p, q;

        /// <summary>
        /// Initialise pathfinder
        /// </summary>
        /// <param name="Start">start coord</param>
        /// <param name="Goal">end coord</param>
        /// <param name="MapArrray">the map - a 2D array consisting of int values 0 being an obstruction any other value is the cost to move</param>
        /// <param name="width">width of the map (array x)</param>
        /// <param name="height">height of the map (array y)</param>
        public Pathfinding(Vector2 Start, Vector2 Goal, int[,] MapArrray, int width, int height)
        {
            WIDTH = width;
            HEIGHT = height;
            map = new int[width, height];
            root = new coords();
            goal = new coords();
            path = new coords();
            current = new coords();
            OpenList = new List<coords>();
            ClosedList = new List<coords>();
            PathList = new List<coords>();
            DirectionList = new List<string>();
            CoordsList = new List<Vector2>();
            Random r = new Random();

            map = MapArrray;

            root.Coordinates = Start;
            root.parent = new coords() { Coordinates = new Vector2(-1, -1) };
            goal.Coordinates = Goal;
            root.heuristicCost = Math.Abs((int)goal.Coordinates.X - (int)root.Coordinates.X) + Math.Abs((int)goal.Coordinates.Y - (int)root.Coordinates.Y);
            root.mapVal = map[(int)root.Coordinates.X, (int)root.Coordinates.Y];
            OpenList.Add(root);
        }

        /// <summary>
        /// checks to see if the goal coordinate is actually reachable
        /// </summary>
        /// <returns>bool</returns>
        public bool isReachable()
        {
            while (OpenList.Count != 0)
            {
                current = OpenList.First();
                if (current.Coordinates == goal.Coordinates)
                {
                    path = current;
                    while (path.Coordinates.X != -1 && path.Coordinates.Y != -1)
                    {
                        path = path.parent;
                    }
                    return true;
                }
                else
                {
                    NodeGen();
                    OpenList.RemoveAt(0);
                    Closed();
                    OpenList = OpenList.OrderBy(i => i.totalCost).ToList();
                }
            }
            return false;
        }

        /// <summary>
        /// Generates a series of list from  start to goal can get the list with the following
        /// GetPathCoords() -> List of Coord objects
        /// GetPathDirections() -> List of strings compass style (N/E/S/W)
        /// GetPathVector() -> List of Vector2 (the map coordinates)
        /// </summary>
        public void CalculatePath()
        {
            while (OpenList.Count != 0)
            {
                current = OpenList.First();
                if (current.Coordinates == goal.Coordinates)
                {
                    path = current;
                    while (path.Coordinates.X != -1 && path.Coordinates.Y != -1)
                    {
                        PathList.Add(path);
                        DirectionList.Add(path.Direction);
                        CoordsList.Add(path.Coordinates);
                        path = path.parent;
                    }
                    OrderLists();
                    OpenList.RemoveAt(0);
                    break;
                }
                else
                {
                    NodeGen();
                    OpenList.RemoveAt(0);
                    Closed();
                    OpenList = OpenList.OrderBy(i => i.totalCost).ToList();
                }
            }
        }

        private void Closed()
        {
            coords closer = new coords();
            closer = current;
            ClosedList.Add(closer);
        }

        private void NodeGen()
        {
            coords north = new coords() { Coordinates = new Vector2(current.Coordinates.X, current.Coordinates.Y - 1) };  // north
            coords east = new coords() { Coordinates = new Vector2(current.Coordinates.X + 1, current.Coordinates.Y) };  // east
            coords south = new coords() { Coordinates = new Vector2(current.Coordinates.X, current.Coordinates.Y + 1) };  // south 
            coords west = new coords() { Coordinates = new Vector2(current.Coordinates.X -1, current.Coordinates.Y) };  // west

            TestNode(north, "N");
            TestNode(east, "E");
            TestNode(south, "S");
            TestNode(west, "W");
        }

        private void TestNode(coords node, string direction)
        {
            int openCompare = 0; // comparitor for open list
            int closedCompare = 0; // comparitor for closed list

            //=======if in the map bounds=======
            if (node.Coordinates.X > -1 && node.Coordinates.X < WIDTH)
            {
                if (node.Coordinates.Y > -1 && node.Coordinates.Y < HEIGHT)
                {
                    node.heuristicCost = Math.Abs((int)goal.Coordinates.X - (int)node.Coordinates.X) + Math.Abs((int)goal.Coordinates.Y - (int)node.Coordinates.Y); // calculate heuristic
                    node.mapVal = map[(int)node.Coordinates.X, (int)node.Coordinates.Y]; // set map value
                    node.cost = current.cost + node.mapVal; // set cost
                    node.totalCost = node.heuristicCost + node.cost; // calculate total cost
                    node.Direction = direction;
                    if (node.mapVal != 0) // if not a wall
                    {
                        //=======test to see if on openlist=======
                        int index = 0;
                        while (index != OpenList.Count)
                        {

                            p = OpenList.ElementAt(index);
                            index++;
                            if (p.Coordinates.X == node.Coordinates.X && p.Coordinates.Y == node.Coordinates.Y)
                            {
                                // if on open list and cost is greater
                                if (p.totalCost > node.totalCost)
                                {
                                    OpenList.Remove(p);//erase and replace
                                    break;
                                }
                                else
                                {
                                    openCompare = 1;
                                }
                            }
                        }
                        //=======test to see if on closedlist=======	       
                        index = 0;
                        while (index != ClosedList.Count())
                        {
                            q = ClosedList.ElementAt(index);
                            index++;
                            if (q.Coordinates.X == node.Coordinates.X && q.Coordinates.Y == node.Coordinates.Y)
                            {
                                closedCompare = 1;
                                if (q.totalCost > node.totalCost)
                                {
                                    OpenList.Remove(q);
                                    break;
                                }
                                else
                                {
                                    closedCompare = 1;
                                }
                            }
                        }
                        // if not on any list
                        if (openCompare != 1 && closedCompare != 1)
                        {
                            node.parent = current; // set parent
                            OpenList.Add(node); // push onto list
                        }
                    }
                }
            }
        }

        private void OrderLists()
        {
            var templist = new List<Vector2>();
            for (int i = CoordsList.Count - 1; i > -1; i--)
            {
                templist.Add(CoordsList.ElementAt(i));
            }
            CoordsList = templist;

            var temp2 = new List<coords>();
            for (int i = PathList.Count - 1; i > -1; i--)
            {
                temp2.Add(PathList[i]);
            }
            PathList = temp2;

            var temp3 = new List<string>();
            for (int i = DirectionList.Count - 1; i > -1; i--)
            {
                temp3.Add(DirectionList[i]);
            }
            DirectionList = temp3;
        }

        /// <summary>
        /// get path as coord objects
        /// </summary>
        /// <returns></returns>
        public List<coords> GetPathCoords()
        {
            return PathList;
        }

        /// <summary>
        /// get path as directionalstrings (N/E/S/W)
        /// </summary>
        /// <returns></returns>
        public List<string> GetPathDirections()
        {
            return DirectionList;
        }

        /// <summary>
        /// get path a Vector2 objects
        /// </summary>
        /// <returns></returns>
        public List<Vector2> GetPathVector()
        {
            return CoordsList;
        }

    }
}
