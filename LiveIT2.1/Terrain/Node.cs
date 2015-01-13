using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveIT2._1.Terrain
{
    public class Node
    {
        Box _box;
        Node _parent;
        private int h_heuristicValue = 0;
        private int g_movementCost = 0;
        private int f_totalCost = 0;
        private Node _topNode, _bottomNode, _leftNode, _rightNode;
        public Node( Box box )
        {
            _box = box;
        }

        public int HValue
        {
            get { return h_heuristicValue; }
            set { h_heuristicValue = value; }
        }
        public int GValue
        {
            get { return g_movementCost; }
            set { g_movementCost = value; }
        }

        public int FValue
        {
            get { return f_totalCost; }
        }
        public void CalculateFValue()
        {
            f_totalCost = GValue + HValue;
        }
        public Box Box
        {
            get { return _box; }
        }
        public Node TopNode
        {
            get
            {
                if( _topNode != null )
                {
                    return _topNode;
                }
                if( _box.Top != null )
                {
                    _topNode = new Node( _box.Top );
                    return _topNode;
                }
                else
                {
                    return null;
                }

            }
        }

        public Node BottomNode
        {
            get
            {
                if( _bottomNode != null )
                {
                    return _bottomNode;
                }
                if( _box.Bottom != null )
                {
                    _bottomNode = new Node( _box.Bottom );
                    return _bottomNode;
                }
                else
                {
                    return null;
                }

            }
        }

        public Node LeftNode
        {
            get
            {
                if( _leftNode != null )
                {
                    return _leftNode;
                }
                if( _box.Left != null )
                {
                    _leftNode = new Node( _box.Left );
                    return _leftNode;
                }
                else
                {
                    return null;
                }

            }
        }
        public Node RightNode
        {
            get
            {
                if( _rightNode != null )
                {
                    return _rightNode;
                }
                if( _box.Right != null )
                {
                    _rightNode = new Node( _box.Right );
                    return _rightNode;
                }
                else
                {
                    return null;
                }

            }
        }

        public Node ParentNode
        {
            get { return _parent; }
            set { _parent = value; }
        }

        public int Distance( Node n )
        {
            return (int)Math.Sqrt( (Math.Pow( this.Box.Area.X - n.Box.Area.X, 2 ) + Math.Pow( this.Box.Area.Y - n.Box.Area.Y, 2 )) );
        }
    }
}
