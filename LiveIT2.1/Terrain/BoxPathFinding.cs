using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveIT2._1.Terrain
{
    public partial class Box
    {
        public int h_heuristicValue = 0;
        public int g_movementCost = 0;
        public int f_totalCost = 0;
        Box _parentBox;

        public Box ParentBox
        {
            get { return _parentBox; }
            internal set
            {
                _parentBox = value;
            }
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
    }

   
}
