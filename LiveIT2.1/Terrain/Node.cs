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
        public int h_heuristicValue = 0;
        public int g_movementCost = 0;
        public int f_totalCost = 0;
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

        public Node TopNode
        {
            get
            {
                if( _box.Top != null )
                {
                    return new Node( _box.Top );
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
                if( _box.Bottom != null )
                {
                    return new Node( _box.Bottom );
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
                if( _box.Left != null )
                {
                    return new Node( _box.Left );
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
                if( _box.Right != null )
                {
                    return new Node( _box.Right );
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
    }
}
