using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveIT2._1.Terrain
{
    [Serializable]
    public class PathFinder
    {
        private List<Node> openList = new List<Node>();
        private List<Node> closedList = new List<Node>();
        private Node checkingNode = null;
        public Node firstNodeInGrid = null;
        public Node startNode = null;
        public Node targetNode = null;
        public bool foundTarget = false;
        public int baseMovementCost = 10;
        private Map map;
        private  bool _stuck;
        private  List<Box> _finalPath;

        public PathFinder( Box startBox, Box targetBox, Map map )
        {
            _finalPath = new List<Box>();
            startNode = new Node(startBox);
            targetNode = new Node(targetBox);
            checkingNode = startNode;
            this.map = map;
        }

        public void Update()
        {
            if( foundTarget == false )
            {
                FindPath();
            }

            if( foundTarget == true )
            {
                TraceBackPath();
            }
        }

        public Node StartBox
        {
            get { return startNode; }
        }

        public Node TargetBox
        {
            get { return targetNode; }
        }

        public bool IsStuck
        {
            get { return _stuck; }
        }
        public bool FoundTarget
        {
            get { return foundTarget; }
        }
        public void FindPath()
        {
            if( foundTarget == false )
            {
                if( checkingNode.TopNode != null )
                {
                    DetermineNodeValues( checkingNode, checkingNode.TopNode );
                }
                if( checkingNode.RightNode != null )
                {
                    DetermineNodeValues( checkingNode, checkingNode.RightNode );
                }
                if( checkingNode.BottomNode != null )
                {
                    DetermineNodeValues( checkingNode, checkingNode.BottomNode );
                }
                if( checkingNode.LeftNode != null )
                {
                    DetermineNodeValues( checkingNode, checkingNode.LeftNode );
                }

                AddToClosedList( checkingNode );
                RemoveFromOpenList( checkingNode );

                checkingNode = GetSmallestFValueNode();
            }
        }

        private void DetermineNodeValues( Node currentNode, Node testing )
        {
            currentNode.HValue = currentNode.Distance( targetNode );
            testing.HValue = testing.Distance( targetNode );

            if( testing == null )
                return;

            if( testing.Box == targetNode.Box )
            {
                targetNode.ParentNode = currentNode;
                foundTarget = true;
                return;
            }

            if( testing.Box.Ground == Enums.EBoxGround.Water || testing.Box.Ground == Enums.EBoxGround.Mountain )
                return;

            if( !closedList.Contains( testing ) )
            {
                if( openList.Contains( testing ) )
                {
                    int newGCost = currentNode.GValue + baseMovementCost;

                    if( newGCost < testing.GValue )
                    {
                        testing.ParentNode = currentNode;
                        testing.GValue = newGCost;
                        testing.CalculateFValue();
                    }
                }
                else
                {
                    testing.ParentNode = currentNode;
                    testing.GValue = currentNode.GValue + baseMovementCost;
                    testing.CalculateFValue();
                    AddToOpenList( testing );
                }
            }
        }

        private void AddToOpenList( Node node )
        {
            openList.Add( node );
        }

        private void AddToClosedList( Node node )
        {
            closedList.Add( node );
        }

        private void RemoveFromOpenList( Node currentNode )
        {
            openList.Remove( currentNode );
        }

        private Node GetSmallestFValueNode()
        {
            if( openList.Count <= 0 )
            {
                _stuck = true;
                return null;
            }
            {
                Node minValueBox = openList[0];
                for( int i = 0; i < openList.Count; i++ )
                {
                    if( openList[i].FValue < minValueBox.FValue )
                    {
                        minValueBox = openList[i];
                    }
                }
                return minValueBox;
            }

        }

        private void TraceBackPath()
        {
            Node node = targetNode;
            do
            {
                //node.Box.Ground = Enums.EBoxGround.Snow;
                _finalPath.Add( node.Box );
                node = node.ParentNode;

            } while( node != null );
        }

        public List<Box> FinalPath
        {
            get
            {
                _finalPath.Reverse();
                return _finalPath;
            }
        }

    }
}
