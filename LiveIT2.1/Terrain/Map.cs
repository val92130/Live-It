// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Map.cs" company="">
//   
// </copyright>
// <summary>
//   The map.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LiveIT2._1.Terrain
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;
    using LiveIT2._1.Animals;
    using LiveIT2._1.Vegetation;
    using LiveIT2._1.Viewport;
    using System.Windows.Forms;

    /// <summary>
    ///     The map.
    /// </summary>
    [Serializable]
    public class Map
    {
        #region Fields

        /// <summary>
        ///     The _box count per line.
        /// </summary>
        private readonly int _boxCountPerLine;

        private Animal _animalToSave;


        [NonSerialized]
        private Stopwatch countDown;

        // Box size in centimeter.
        /// <summary>
        ///     The _box size.
        /// </summary>
        private readonly int _boxSize;

        /// <summary>
        ///     The _animals.
        /// </summary>
        private List<Animal> _animals = new List<Animal>();

        /// <summary>
        ///     The _blood list.
        /// </summary>
        private List<Rectangle> _bloodList = new List<Rectangle>();

        [NonSerialized]
        Timer _delayMedic;


        /// <summary>
        ///     The _boxes.
        /// </summary>
        private Box[] _boxes;

        /// <summary>
        ///     The _dead animals.
        /// </summary>
        private int _deadAnimals;

        private int _savedAnimals;

        /// <summary>
        ///     The _is in car.
        /// </summary>
        private bool _isInCar;

        private bool _isMedic;

        /// <summary>
        ///     The _is player spawned.
        /// </summary>
        private bool _isPlayerSpawned;

        /// <summary>
        ///     The _is raining.
        /// </summary>
        private bool _isRaining;

        /// <summary>
        /// Is the game paused or not
        /// </summary>
        private bool _isPaused;

        /// <summary>
        ///     The _show debug.
        /// </summary>
        private bool _showDebug;

        /// <summary>
        ///     The _vegetation.
        /// </summary>
        private List<MapElement> _vegetation = new List<MapElement>();

        /// <summary>
        ///     The _view port.
        /// </summary>
        [NonSerialized]
        private MainViewPort _viewPort;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Map"/> class.
        /// </summary>
        /// <param name="boxCountPerLine">
        /// The box count per line.
        /// </param>
        /// <param name="boxSizeInMeter">
        /// The box size in meter.
        /// </param>
        public Map(int boxCountPerLine, int boxSizeInMeter)
        {
            this._boxCountPerLine = boxCountPerLine;
            this._boxes = new Box[boxCountPerLine * boxCountPerLine];
            this._boxSize = boxSizeInMeter * 100;
            int count = 0;
            this._deadAnimals = 0;
            for (int i = 0; i < this._boxCountPerLine; i++)
            {
                for (int j = 0; j < this._boxCountPerLine; j++)
                {
                    this._boxes[count++] = new Box(i, j, this);
                }
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the animals.
        /// </summary>
        public List<Animal> Animals
        {
            get
            {
                return this._animals;
            }

            set
            {
                this._animals = value;
            }
        }

        public List<Box> BoxList
        {
            get { return _viewPort.BoxList; }
        }

        public bool IsPaused
        {
            get { return _isPaused; }
            set { _isPaused = value; }
        }

        /// <summary>
        ///     Gets or sets the blood list.
        /// </summary>
        public List<Rectangle> BloodList
        {
            get
            {
                return this._bloodList;
            }

            set
            {
                this._bloodList = value;
            }
        }

        /// <summary>
        ///     Gets the number of lines and number of columns.
        /// </summary>
        public int BoxCountPerLine
        {
            get
            {
                return this._boxCountPerLine;
            }
        }

        /// <summary>
        ///     Gets the box size in centimeter.
        /// </summary>
        public int BoxSize
        {
            get
            {
                return this._boxSize;
            }
        }

        /// <summary>
        ///     Gets or sets the boxes.
        /// </summary>
        public Box[] Boxes
        {
            get
            {
                return this._boxes;
            }

            set
            {
                this._boxes = value;
            }
        }

        /// <summary>
        /// Know if the player is playing medic mode or not
        /// </summary>
        public bool IsMedic
        {
            get { return _isMedic; }
            set { _isMedic = value; }
        }

        /// <summary>
        ///     Gets or sets the dead animals.
        /// </summary>
        public int DeadAnimals
        {
            get
            {
                return this._deadAnimals;
            }

            set
            {
                this._deadAnimals = value;
            }
        }

        /// <summary>
        ///     Gets the get living animals.
        /// </summary>
        public int GetLivingAnimals
        {
            get
            {
                return this._animals.Count();
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether is in car.
        /// </summary>
        public bool IsInCar
        {
            get
            {
                return this._isInCar;
            }

            set
            {
                this._isInCar = value;
            }
        }

        public int SavedAnimals
        {
            get { return _savedAnimals; }

        }

        /// <summary>
        ///     Gets or sets a value indicating whether is player.
        /// </summary>
        public bool IsPlayer
        {
            get
            {
                return this._isPlayerSpawned;
            }

            set
            {
                this._isPlayerSpawned = value;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether is raining.
        /// </summary>
        public bool IsRaining
        {
            get
            {
                return this._isRaining;
            }

            set
            {
                this._isRaining = value;
            }
        }

        /// <summary>
        ///     Gets the size of the map in centimeters.
        /// </summary>
        public int MapSize
        {
            get
            {
                return this._boxCountPerLine * this._boxSize;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether show debug.
        /// </summary>
        public bool ShowDebug
        {
            get
            {
                return this._showDebug;
            }

            set
            {
                this._showDebug = value;
            }
        }

        /// <summary>
        ///     Gets or sets the vegetation.
        /// </summary>
        public List<MapElement> Vegetation
        {
            get
            {
                return this._vegetation;
            }

            set
            {
                this._vegetation = value;
            }
        }

        /// <summary>
        ///     Gets or sets the view port.
        /// </summary>
        public MainViewPort ViewPort
        {
            get
            {
                return this._viewPort;
            }

            set
            {
                this._viewPort = value;
            }
        }

        #endregion

        #region Public Indexers

        /// <summary>
        /// Gets the box at (line,column). line and column must be in [0,<see cref="MapSize"/>[
        ///     otherwise null is returned.
        /// </summary>
        /// <param name="line">
        /// </param>
        /// <param name="column">
        /// </param>
        /// <returns>
        /// The box or null.
        /// </returns>
        public Box this[int line, int column]
        {
            get
            {
                if (line < 0 || line >= this._boxCountPerLine || column < 0 || column >= this._boxCountPerLine)
                {
                    return null;
                }

                return this._boxes[line * this._boxCountPerLine + column];
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The exit car.
        /// </summary>
        public void ExitCar()
        {
            if (this.IsInCar)
            {
                this.IsInCar = false;
            }
        }

        /// <summary>
        /// The get overlapped animals.
        /// </summary>
        /// <param name="r">
        /// The r.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<Animal> GetOverlappedAnimals(Rectangle r)
        {
            var animalList = new List<Animal>();
            for (int i = 0; i < this.Animals.Count; i++)
            {
                if (this.Animals[i].Area.IntersectsWith(r))
                {
                    animalList.Add(this.Animals[i]);
                }
            }

            return animalList;
        }

        /// <summary>
        /// The get overlapped boxes.
        /// </summary>
        /// <param name="r">
        /// The r.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<Box> GetOverlappedBoxes(Rectangle r)
        {
            var boxList = new List<Box>();
            int top = r.Top / this.BoxSize;
            int left = r.Left / this.BoxSize;
            int bottom = (r.Bottom - 1) / this.BoxSize;
            int right = (r.Right - 1) / this.BoxSize;
            for (int i = top; i <= bottom; ++i)
            {
                for (int j = left; j <= right; ++j)
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

        /// <summary>
        /// The get overlapped vegetation.
        /// </summary>
        /// <param name="r">
        /// The r.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<MapElement> GetOverlappedVegetation(Rectangle r)
        {
            var vegetationList = new List<MapElement>();
            for (int i = 0; i < this.Vegetation.Count; i++)
            {
                if (this.Vegetation[i].Area.IntersectsWith(r))
                {
                    vegetationList.Add(this.Vegetation[i]);
                }
            }

            return vegetationList;
        }

        /// <summary>
        /// The load.
        /// </summary>
        /// <param name="filename">
        /// The filename.
        /// </param>
        /// <returns>
        /// The <see cref="Map"/>.
        /// </returns>
        public Map Load(string filename)
        {
            Stream stream = File.Open(filename, FileMode.Open);
            var bFormatter = new BinaryFormatter();

            // _boxes = (Box[])bFormatter.Deserialize( stream );
            var _newMap = (Map)bFormatter.Deserialize(stream);
            stream.Close();
            return _newMap;
        }

        /// <summary>
        /// The save.
        /// </summary>
        /// <param name="filename">
        /// The filename.
        /// </param>
        public void Save(string filename)
        {
            Stream stream = File.Open(filename, FileMode.Create);
            var bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, this);
            stream.Close();
        }


        /// <summary>
        /// Add aditional game logic here
        /// </summary>
        public void Update()
        {
            if (this.IsMedic)
            {
                Random rCountDown = new Random();

                if (_delayMedic == null)
                {
                    _delayMedic = new System.Windows.Forms.Timer();
                    _delayMedic.Interval = rCountDown.Next(10000,30000);
                    _delayMedic.Tick += new EventHandler(T_delay_medic_tick);
                    _delayMedic.Start();
                }

                if (countDown == null)
                {
                    countDown = new Stopwatch();
                    countDown.Start();
                }
                if (_animals.Count > 0)
                {
                    if (_animalToSave == null)
                    {
                        Random r = new Random();
                        _animalToSave = _animals[r.Next(0, _animals.Count)];
                        _animalToSave.IsHurt = true;
                    }

                    if (_viewPort.Player.Area.IntersectsWith(_animalToSave.Area))
                    {
                        if (_animalToSave.IsHurt)
                        {
                            _savedAnimals++;
                        }
                        _animalToSave.IsHurt = false;
                        
                    }
                    
                }
            }

            
        }

        private void T_delay_medic_tick(object sender, EventArgs e)
        {
            if (_animals.Count > 0)
            {
                if (_animalToSave.IsHurt == true)
                {
                    _animalToSave.IsHurt = false;
                    _animalToSave.Die();
                }
                 Random r = new Random();
                 int indice = r.Next(0, _animals.Count);
                 if (_animals[indice] != null)
                 {
                     _animalToSave = _animals[indice];
                     _animalToSave.IsHurt = true;
                 }

            }
            Random rCountDown = new Random();
            _delayMedic.Stop();
            _delayMedic.Interval = rCountDown.Next(10000, 30000);
            _delayMedic.Start();
        }

        #endregion
    }
}