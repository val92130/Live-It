﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveIT2._1
{
    public class Map
    {
        readonly Box[] _boxes;
        readonly int _boxCountPerLine;
        // Box size in centimeter.
        readonly int _boxSize;

        public Map( int boxCountPerLine, int boxSizeInMeter )
        {
            _boxCountPerLine = boxCountPerLine;
            _boxes = new Box[boxCountPerLine * boxCountPerLine];
            _boxSize = boxSizeInMeter * 100;
            int count = 0;
            for( int i = 0; i < _boxCountPerLine; i++ )
            {
                for( int j = 0; j < _boxCountPerLine; j++ )
                {
                    _boxes[count++] = new Box( i, j, this );
                }
            }
        }


        /// <summary>
        /// Gets the box size in centimeter.
        /// </summary>
        public int BoxSize
        {
            get { return _boxSize; }
        }

        /// <summary>
        /// Gets the number of lines and number of columns.
        /// </summary>
        public int BoxCountPerLine
        {
            get { return _boxCountPerLine; }
        }

        /// <summary>
        /// Gets the size of the map in centimeters.
        /// </summary>
        public int MapSize
        {
            get { return _boxCountPerLine * _boxSize; }
        }

        /// <summary>
        /// Gets the box at (line,column). line and column must be in [0,<see cref="MapSize"/>[
        /// otherwise null is returned.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="column"></param>
        /// <returns>The box or null.</returns>
        public Box this[ int line, int column ]
        {
            get
            {
                if (line < 0 || line >= _boxCountPerLine
                    || column < 0 || column >= _boxCountPerLine) return null;
                return _boxes[line*_boxCountPerLine + column];
            }
        }

        public List<Box> GetOverlappedBoxes(Rectangle viewPort)
        {
            List<Box> boxList = new List<Box>();
            for( int i = 0; i < _boxes.Length; i++ )
            {
                Box b = _boxes[i];

                Rectangle rIntersect =  b.Area;
                rIntersect.Intersect( viewPort );
                if( rIntersect.IsEmpty ) continue;
                rIntersect.Offset( -b.Area.Left, -b.Area.Top );
                b.Source = b.Area;

                if( _boxes[i].Area.IntersectsWith( viewPort ) )
                {
                    boxList.Add( b );
                }
            }
            return boxList;
        }
    }
}
