using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace LiveIT2._1
{
    [Serializable]
    public class Map
    {
        Box[] _boxes;
        readonly int _boxCountPerLine;
        // Box size in centimeter.
        readonly int _boxSize;
        [NonSerializedAttribute]
        List<Animal> _animals = new List<Animal>();
        [NonSerializedAttribute]
        MainViewPort _viewPort;
  bool _showDebug;

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

        public List<Animal> Animals
        {
            get { return _animals; }
            set { _animals = value; }
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

        public Box[] Boxes
        {
            get { return _boxes; }
            set { _boxes = value; }
        }

        
        public MainViewPort ViewPort
        {
            get { return _viewPort; }
            set { _viewPort = value; }
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

        public void Save(string filename)
        {
            Stream stream = File.Open(filename, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, this.Boxes);
            stream.Close();
        }

        public Box[] Load(string filename)
        {
            Stream stream = File.Open(filename, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            _boxes = (Box[])bFormatter.Deserialize(stream);
            stream.Close();
            return _boxes;
        }
        public bool ShowDebug
        {
            get { return _showDebug; }
            set { _showDebug = value; }
        }
    }
}
