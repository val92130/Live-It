using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveIT2._1.Terrain
{
    public class PathFinder
    {
        public List<Box> openList = new List<Box>();
        public List<Box> closedList = new List<Box>();
        public Box checkingNode = null;
        public Box firstNodeInGrid = null;
        public Box startNode = null;
        public Box targetNode = null;
        public bool foundTarget = false;
        public int baseMovementCost = 10;
        private Map map;
        public List<Box> finalPath = new List<Box>();

        public PathFinder(Box startBox, Box targetBox, Map map)
        {
            startNode = startBox;
            targetNode = targetBox;
            checkingNode = startNode;
            this.map = map;
            CalculateDistance();
        }

        public void Update()
        {
            if (foundTarget == false)
            {
                FindPath();
            }

            if (foundTarget == true)
            {
                //TraceBackPath();
            }
        }


        public void FindPath()
        {
            if (foundTarget == false)
            {
                if (checkingNode.Top != null)
                {
                    DetermineNodeValues(checkingNode, checkingNode.Top);
                }
                if (checkingNode.Right != null)
                {
                    DetermineNodeValues(checkingNode, checkingNode.Right);
                }
                if (checkingNode.Bottom != null)
                {
                    DetermineNodeValues(checkingNode, checkingNode.Bottom);
                }
                if (checkingNode.Left != null)
                {
                    DetermineNodeValues(checkingNode, checkingNode.Left);
                }

                AddToClosedList(checkingNode);
                RemoveFromOpenList(checkingNode);

                checkingNode = GetSmallestFValueNode();
            }
        }

        private void DetermineNodeValues(Box currentNode, Box testing)
        {
            if (testing == null)
                return;

            if (testing == targetNode)
            {
                if (!finalPath.Contains(currentNode))
                {
                    finalPath.Add(currentNode);
                }
                //targetNode.ParentBox = currentNode;
                foundTarget = true;
                return;
            }

            if (testing.Ground == Enums.EBoxGround.Mountain || testing.Ground == Enums.EBoxGround.Water)
                return;

            if (!closedList.Contains(testing))
            {
                if (openList.Contains(testing))
                {
                    int newGCost = currentNode.GValue + baseMovementCost;

                    if (newGCost < testing.GValue)
                    {
                        if (!finalPath.Contains(currentNode))
                        {
                            finalPath.Add(currentNode);
                        }
                        
                        //testing.ParentBox = currentNode;
                        //testing.GValue = newGCost;
                        testing.CalculateFValue();
                    }
                }
                else
                {
                    //testing.ParentBox = currentNode;
                    if (!finalPath.Contains(currentNode))
                    {
                        finalPath.Add(currentNode);
                    }
                    //testing.GValue = currentNode.GValue + baseMovementCost;
                    testing.CalculateFValue();
                    AddToOpenList(testing);
                }
            }
        }

        private void AddToOpenList(Box box)
        {
            openList.Add(box);
        }

        private void AddToClosedList(Box box)
        {
            closedList.Add(box);
        }

        private void RemoveFromOpenList(Box currentNode)
        {
            openList.Remove(currentNode);
        }

        private Box GetSmallestFValueNode()
        {
            Box minValueBox = openList[0];
            for (int i = 0; i < openList.Count; i++)
            {
                if (openList[i].FValue < minValueBox.FValue)
                {
                    minValueBox = openList[i];
                }
            }
            return minValueBox;
        }

        private void TraceBackPath()
        {
            finalPath.Clear();
            Box node = targetNode;
            while (node != null)
            {
                if (node != null)
                {
                    finalPath.Add(node);
                    node = node.ParentBox;
                }
            }
        }

        public List<Box> FinalPath
        {
            get
            {
                //finalPath.Reverse();
                return finalPath;
            }
        }
        public void CalculateDistance()
        {
            for (int i = 0; i < map.Boxes.Length; i++ )
            {
                map.Boxes[i].HValue = (int)(Math.Pow(map.Boxes[i].Area.X - targetNode.Area.X, 2) + Math.Pow(map.Boxes[i].Area.Y - targetNode.Area.Y, 2));
            }
        }
    }
}
