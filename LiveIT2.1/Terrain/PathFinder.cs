using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveIT2._1.Terrain
{
    [Serializable]
    public class PathFinder
    {
        public List<Node> _openList = new List<Node>();
        public List<Node> _closedList = new List<Node>();
        private Node _checkingNode = null;
        private Node _startNode = null;
        private Node _targetNode = null;
        private bool _foundTarget = false;
        private int _baseMovementCost = 10;
        private Map _map;
        private  bool _stuck;
        private  List<Box> _finalPath;
        public  int count = 0;
        public  int countNotNull = 0;
        public  Node _testingNode;
        public  Node minBox;

        public PathFinder( Box startBox, Box targetBox, Map map )
        {
            _finalPath = new List<Box>();
            _startNode = new Node(startBox);
            _targetNode = new Node(targetBox);
            _checkingNode = _startNode;
            this._map = map;
        }

        public void Update()
        {
            
            if( _foundTarget == false )
            {
                FindPath();
            }

            if( _foundTarget == true )
            {
                TraceBackPath();
            }
        }
        public Node CheckingNode
        {
            get { return _checkingNode; }
        }
        public Node StartBox
        {
            get { return _startNode; }
        }

        public Node TargetBox
        {
            get { return _targetNode; }
        }

        public bool IsStuck
        {
            get { return _stuck; }
        }
        public bool FoundTarget
        {
            get { return _foundTarget; }
        }
        public void FindPath()
        {
            if( _foundTarget == false )
            {
                if( _checkingNode.TopNode != null )
                {
                    DetermineNodeValues( _checkingNode, _checkingNode.TopNode );
                }
                if( _checkingNode.RightNode != null )
                {
                    DetermineNodeValues( _checkingNode, _checkingNode.RightNode );
                }
                if( _checkingNode.BottomNode != null )
                {
                    DetermineNodeValues( _checkingNode, _checkingNode.BottomNode );
                }
                if( _checkingNode.LeftNode != null )
                {
                    DetermineNodeValues( _checkingNode, _checkingNode.LeftNode );
                }

                AddToClosedList( _checkingNode );
                RemoveFromOpenList( _checkingNode );

                count = _openList.Count();

                _checkingNode = GetSmallestFValueNode();
            }
        }

        private void DetermineNodeValues( Node currentNode, Node testing )
        {
            _testingNode = testing;
            if( testing == null )
                return;

            if( testing.Box == _targetNode.Box )
            {
                _targetNode.ParentNode = currentNode;
                _foundTarget = true;
                return;
            }

            if( testing.Box.Ground == Enums.EBoxGround.Water || testing.Box.Ground == Enums.EBoxGround.Mountain )
            {
                return;
            }

            // if the node hasn't been checked yet
            if( !_closedList.Contains( testing ) )
            {
                if( _openList.Contains( testing ) )
                {
                    int newGCost = currentNode.GValue + _baseMovementCost;

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
                    testing.HValue = testing.Distance( _targetNode );
                    testing.GValue = currentNode.GValue + _baseMovementCost;
                    testing.CalculateFValue();
                    AddToOpenList( testing );
                }
            }
        }

        private void AddToOpenList( Node node )
        {
            _openList.Add( node );
        }

        private void AddToClosedList( Node node )
        {
            _closedList.Add( node );
        }

        private void RemoveFromOpenList( Node currentNode )
        {
            if( _openList.Contains( currentNode ) )
            {
                _openList.Remove( currentNode );
            }

            
        }

        public List<Node> ClosedList
        {
            get { return _closedList; }
        }

        public List<Node> OpenList
        {
            get { return _openList;}
        }

        private Node GetSmallestFValueNode()
        {
            if( _openList.Count <= 0 )
            {
                _stuck = true;
                return null;
            }
            {
                Node minValueBox = _openList[0];
                for( int i = 0; i < _openList.Count; i++ )
                {
                    if( _openList[i].FValue < minValueBox.FValue )
                    {
                        minValueBox = _openList[i];
                    }
                }
                minBox = minValueBox;
                return minValueBox;
            }

        }

        private void TraceBackPath()
        {
            Node node = _targetNode;
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
