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
        private Box checkingNode = null;
        private Box firstNodeInGrid = null;
        private Box startNode = null;
        private Box targetNode = null;
        private bool foundTarget = false;
        public int baseMovementCost = 10;
        private Map map;
        public List<Box> finalPath = new List<Box>();
        bool _stuck;

        public bool FoundTarget
        {
            get { return foundTarget; }
        }
        public PathFinder(Box startBox, Box targetBox, Map map)
        {
            startNode = startBox;
            targetNode = targetBox;
            checkingNode = startNode;
            this.map = map;
        }

        public void Update()
        {
            if (foundTarget == false)
            {
                FindPath();
            }
        }


        public void FindPath()
        {
            if (foundTarget == false && _stuck == false)
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
                    }
                }
                else
                {
                    if (!finalPath.Contains(currentNode))
                    {
                        finalPath.Add(currentNode);
                    }
                    AddToOpenList(testing);
                }
            }
        }

        private void AddToOpenList(Box box)
        {
            openList.Add(box);
        }

        public bool IsStuck
        {
            get { return _stuck; }
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
            if (openList.Count <= 0)
            {
                _stuck = true;
                return null;
            }
            else
            {
                Box minValueBox = openList[0];
                for (int i = 0; i < openList.Count; i++)
                {
                    if (openList[i].GetFValue(targetNode) < minValueBox.GetFValue(targetNode))
                    {
                        minValueBox = openList[i];
                    }
                }
                return minValueBox;
            }
            
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
                return finalPath;
            }
        }
    }
}
