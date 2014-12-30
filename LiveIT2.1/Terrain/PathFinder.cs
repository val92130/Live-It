using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveIT2._1.Terrain
{
    public class PathFinder
    {
        private List<Box> openList = new List<Box>();
        private List<Box> closedList = new List<Box>();
        private Box checkingNode = null;
        public Box firstNodeInGrid = null;
        public Box startNode = null;
        public Box targetNode = null;
        public bool foundTarget = false;
        public int baseMovementCost = 10;
        private Map map;

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
                TraceBackPath();
            }
        }

        public Box StartBox
        {
            get { return startNode; }
        }

        public Box TargetBox
        {
            get { return targetNode; }
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
                targetNode.ParentBox = currentNode;
                foundTarget = true;
                return;
            }

            if (testing.Ground == Enums.EBoxGround.Water)
                return;

            if (!closedList.Contains(testing))
            {
                if (openList.Contains(testing))
                {
                    int newGCost = currentNode.GValue + baseMovementCost;

                    if (newGCost < testing.GValue)
                    {
                        testing.ParentBox = currentNode;
                        testing.GValue = newGCost;
                        testing.CalculateFValue();
                    }
                }
                else
                {
                    testing.ParentBox = currentNode;
                    testing.GValue = currentNode.GValue + baseMovementCost;
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
            Box node = targetNode;
            do
            {
                node.Ground = Enums.EBoxGround.Snow;
                node = node.ParentBox;

            } while (node != null);
        }

        public void CalculateDistance()
        {
            foreach (Box b in map.Boxes)
            {
                b.HValue = (int)(Math.Pow(b.Area.X - targetNode.Area.X, 2) + Math.Pow(b.Area.Y - targetNode.Area.Y, 2));
            }
        }
    }
}
