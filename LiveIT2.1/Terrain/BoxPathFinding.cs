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
        private Dictionary<Box, int> _heuristics = new Dictionary<Box, int>();

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
        }
        public int GValue
        {
            get { return g_movementCost; }
        }

        public void EmptyHeuristic()
        {
            _heuristics.Clear();
        }

        public int FValue
        {
            get { return f_totalCost; }
        }

        public void CalculateFValue()
        {
            f_totalCost = GValue + HValue;
        }

        public int GetFValue(Box b)
        {
            int hvalue;
           _heuristics.TryGetValue(b, out hvalue);
           return hvalue;
        }
        public void CalculateHeuristic()
        {
            foreach (Box b in _map.Boxes)
            {
                int h = (int)(Math.Pow(this.Area.X - b.Area.X, 2) + Math.Pow(this.Area.Y - b.Area.Y, 2));
                _heuristics.Add(b, h);
            }
        }
    }

   
}
