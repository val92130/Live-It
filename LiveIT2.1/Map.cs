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
        List<Vegetation> _vegetation = new List<Vegetation>();
        [NonSerializedAttribute]
        MainViewPort _viewPort;
        bool _showDebug, _isRaining;
        bool _isPlayerSpawned;
        bool _isInCar;
        List<Rectangle> _bloodList = new List<Rectangle>();

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

        public List<Vegetation> Vegetation
        {
            get { return _vegetation; }
            set { _vegetation = value; }
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

        public bool IsPlayer
        {
            get { return _isPlayerSpawned; }
            set { _isPlayerSpawned = value; }
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

        public bool IsRaining
        {
            get { return _isRaining; }
            set { _isRaining = value; }
        }

        public List<Rectangle> BloodList
        {
            get { return _bloodList; }
            set { _bloodList = value; }
        }


        public bool IsInCar
        {
            get { return _isInCar; }
            set { _isInCar = value; }
        }

        public void ExitCar()
        {
            if( this.IsInCar )
            {
                this.IsInCar = false;
            }
        }

        public List<Box> GetOverlappedBoxes( Rectangle r )
        {
            List<Box> boxList = new List<Box>();
            int top = r.Top / this.BoxSize;
            int left = r.Left / this.BoxSize;
            int bottom = (r.Bottom - 1) / this.BoxSize;
            int right = (r.Right - 1) / this.BoxSize;
            for( int i = top; i <= bottom; ++i )
            {
                for( int j = left; j <= right; ++j )
                {
                    if (this[i, j] != null)
                    {
                        Box b = this[j, i];
                        b.Source = b.Area;
                        boxList.Add(b);
                    }

                }
            }
            return boxList;
        }

        public List<Animal> GetOverlappedAnimals( Rectangle r )
        {
            List<Animal> animalList = new List<Animal>();
            for( int i =0; i < this.Animals.Count; i++ )
            {
                if( Animals[i].Area.IntersectsWith( r ) )
                {
                    animalList.Add( Animals[i] );
                }
            }
                return animalList;
        }
        public List<Vegetation> GetOverlappedVegetation( Rectangle r )
        {
            List<Vegetation> vegetationList = new List<Vegetation>();
            for( int i =0; i < this.Vegetation.Count; i++ )
            {
                if( Vegetation[i].Area.IntersectsWith( r ) )
                {
                    vegetationList.Add( Vegetation[i] );
                }
            }
            return vegetationList;
        }

        public void Save(string filename)
        {
            Stream stream = File.Open(filename, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, this);
            stream.Close();
        }

        public Map Load(string filename)
        {
            Stream stream = File.Open( filename, FileMode.Open );
            BinaryFormatter bFormatter = new BinaryFormatter();
            //_boxes = (Box[])bFormatter.Deserialize( stream );
            Map _newMap = (Map)bFormatter.Deserialize( stream );
            
            stream.Close();
            return _newMap;
        }
        public bool ShowDebug
        {
            get { return _showDebug; }
            set { _showDebug = value; }
        }
    }
}
