// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainViewPort.cs" company="">
//   
// </copyright>
// <summary>
//   The main view port.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LiveIT2._1.Viewport
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;

    using LiveIT2._1.Animals;
    using LiveIT2._1.Animation;
    using LiveIT2._1.Enums;
    using LiveIT2._1.Player;
    using LiveIT2._1.Terrain;
    using LiveIT2._1.Textures;
    using LiveIT2._1.Vegetation;
    using LiveIT2._1.Vehicules;

    /// <summary>
    ///     The main view port.
    /// </summary>
    [Serializable]
    public partial class MainViewPort
    {
        #region Fields

        private int _cameraSmoothness;

        /// <summary>
        ///     The _car list.
        /// </summary>
        private readonly List<Car> carList = new List<Car>();

        /// <summary>
        ///     The _mini map view port.
        /// </summary>
        private readonly Rectangle miniMapViewPort;

        /// <summary>
        ///     The _screen bottom.
        /// </summary>
        private readonly Rectangle screenBottom;

        /// <summary>
        ///     The _screen left.
        /// </summary>
        private readonly Rectangle screenLeft;

        /// <summary>
        ///     Has player shooted.
        /// </summary>
        private bool _shoot;

        /// <summary>
        ///     The _screen right.
        /// </summary>
        private readonly Rectangle screenRight;

        /// <summary>
        ///     The _screen top.
        /// </summary>
        private readonly Rectangle screenTop;

        /// <summary>
        ///     The _selected boxes.
        /// </summary>
        private readonly List<Box> selectedBoxes;

        /// <summary>
        ///     The _tank list.
        /// </summary>
        private readonly List<Tank> tankList = new List<Tank>();

        /// <summary>
        ///     The _texture.
        /// </summary>
        private readonly Texture texture;

        /// <summary>
        ///     The _follow animal.
        /// </summary>
        private bool followAnimal;

        /// <summary>
        ///     The _followed animal.
        /// </summary>
        private Animal followedAnimal;

        /// <summary>
        ///     The _is following an animal.
        /// </summary>
        private bool isFollowingAnAnimal;

        /// <summary>
        ///     The _is raining.
        /// </summary>
        private bool isRaining;

        private bool _exitHouse;

        /// <summary>
        ///     The _map.
        /// </summary>
        private Map map;

        /// <summary>
        ///     The _mini map.
        /// </summary>
        private Rectangle miniMap;

        /// <summary>
        ///     The _put animal.
        /// </summary>
        private bool putAnimal;

        /// <summary>
        ///     The _put vegetation.
        /// </summary>
        private bool putMapElement;

        /// <summary>
        ///     The _screen.
        /// </summary>
        private Rectangle screen;

        /// <summary>
        ///     The _sounds.
        /// </summary>
        private SoundEnvironment sounds;

        /// <summary>
        ///     The _try enter.
        /// </summary>
        private bool tryEnter;

        /// <summary>
        ///     The element selector cursor.
        /// </summary>
        private Point elementSelectorCursor;

        /// <summary>
        ///     The _view port.
        /// </summary>
        private Rectangle viewPort;

        /// <summary>
        ///     The _animal selector cursor.
        /// </summary>
        private Point animalSelectorCursor;

        /// <summary>
        ///     The _box list.
        /// </summary>
        private List<Box> boxList;

        /// <summary>
        ///     The _box list mini.
        /// </summary>
        private List<Box> boxListMini;

        /// <summary>
        ///     The _change texture.
        /// </summary>
        private bool changeTexture;

        /// <summary>
        ///     The _fill texture.
        /// </summary>
        private bool fillTexture;

        /// <summary>
        ///     The _mouse rect.
        /// </summary>
        private Rectangle mouseRect = new Rectangle(new Point(Cursor.Position.X, Cursor.Position.Y), new Size(0, 0));

        /// <summary>
        ///     The _player.
        /// </summary>
        private Player player;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewPort"/> class.
        /// </summary>
        /// <param name="map">
        /// The map.
        /// </param>
        public MainViewPort(Map map)
        {
            this.map = map;
            this.texture = new Texture();
            this.selectedBoxes = new List<Box>();
            this.screen = new Rectangle(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.viewPort = new Rectangle(0, 0, 800, 800);
            this.miniMap = new Rectangle(0, 0, 250, 250);
            this.miniMap.Y = this.screen.Bottom - this.miniMap.Height;
            this.miniMapViewPort = new Rectangle(0, 0, this.map.MapSize, this.map.MapSize);
            this.animalSelectorCursor = new Point(0, 0);
            this.map.ViewPort = this;
            this.isRaining = false;
            this.screenTop = new Rectangle((this.screen.Width / 2) - 400, this.screen.Top + 40, 800, 150);
            this.screenBottom = new Rectangle(this.screen.Width / 2 - 400, this.screen.Bottom - 100, 800, 150);
            this.screenLeft = new Rectangle(0, this.screen.Height / 2 - 400, 10, 800);
            this.screenRight = new Rectangle(this.screen.Right - 10, this.screen.Height / 2 - 400, 10, 800);
            _cameraSmoothness = 50;
            player = new Player( this.map, new Point(0, 0) );

            // this._car = new Car(this._map, new Point(600, 600));
            // this._tank = new Tank(this._map, new Point(700, 700));
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the box list.
        /// </summary>
        public List<Box> BoxList
        {
            get
            {
                return this.boxList;
            }
            set
            {
                boxList = value;
            }
        }

        public int CameraSmoothness
        {
            get { return _cameraSmoothness; }
            set
            {
                if (value <= 0)
                {
                    _cameraSmoothness = 1;
                }
                else
                {
                    _cameraSmoothness = value;
                }
            }
        }

        /// <summary>
        /// Check if the player has pressed the shoot key
        /// </summary>
        public bool Shoot
        {
            get { return _shoot; }
            set { _shoot = value; }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether has clicked.
        /// </summary>
        public bool HasClicked { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether is animal selected.
        /// </summary>
        public bool IsAnimalSelected
        {
            get
            {
                return this.putAnimal;
            }

            set
            {
                this.putAnimal = value;
                this.fillTexture = false;
                this.changeTexture = false;
                this.followAnimal = false;
                this.putMapElement = false;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether is change texture selected.
        /// </summary>
        public bool IsChangeTextureSelected
        {
            get
            {
                return this.changeTexture;
            }

            set
            {
                this.changeTexture = value;
                this.fillTexture = false;
                this.putAnimal = false;
                this.followAnimal = false;
                this.putMapElement = false;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether is fill texture selected.
        /// </summary>
        public bool IsFillTextureSelected
        {
            get
            {
                return this.fillTexture;
            }

            set
            {
                this.fillTexture = value;
                this.changeTexture = false;
                this.putAnimal = false;
                this.followAnimal = false;
                this.putMapElement = false;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether is follow animal selected.
        /// </summary>
        public bool IsFollowAnimalSelected
        {
            get
            {
                return this.followAnimal;
            }

            set
            {
                this.followAnimal = value;
                this.changeTexture = false;
                this.putAnimal = false;
                this.fillTexture = false;
                this.putMapElement = false;
            }
        }

        /// <summary>
        ///     Gets a value indicating if the player is following an animal.
        /// </summary>
        public bool IsFollowMode
        {
            get
            {
                return this.followAnimal;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether is following an animal.
        /// </summary>
        public bool IsFollowingAnAnimal
        {
            get
            {
                return this.isFollowingAnAnimal;
            }

            private set
            {
                this.isFollowingAnAnimal = value;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether is vegetation selected.
        /// </summary>
        public bool IsMapElementSelected
        {
            get
            {
                return this.putMapElement;
            }

            set
            {
                this.putMapElement = value;
                this.fillTexture = false;
                this.changeTexture = false;
                this.followAnimal = false;
                this.putAnimal = false;
            }
        }

        /// <summary>
        ///     Gets the mini map.
        /// </summary>
        public Rectangle MiniMap
        {
            get
            {
                return this.miniMap;
            }
        }

        /// <summary>
        ///     Gets the mini map view port.
        /// </summary>
        public Rectangle MiniMapViewPort
        {
            get
            {
                return this.miniMapViewPort;
            }
        }

        /// <summary>
        ///     Gets or sets the mouse selector.
        /// </summary>
        public Rectangle MouseSelector
        {
            get
            {
                return this.mouseRect;
            }

            set
            {
                this.mouseRect = value;
            }
        }

        /// <summary>
        ///     Gets the player.
        /// </summary>
        public Player Player
        {
            get
            {
                return this.player;
            }
        }

        /// <summary>
        ///     Gets the screen size.
        /// </summary>
        public Rectangle ScreenSize
        {
            get
            {
                return this.screen;
            }
        }

        /// <summary>
        ///     Gets the selected box.
        /// </summary>
        public List<Box> SelectedBox
        {
            get
            {
                return this.selectedBoxes;
            }
        }

        /// <summary>
        ///     Gets or sets the sound environment.
        /// </summary>
        public SoundEnvironment SoundEnvironment
        {
            get
            {
                return this.sounds;
            }

            set
            {
                this.sounds = value;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether try enter.
        /// </summary>
        public bool TryEnter
        {
            get
            {
                return this.tryEnter;
            }

            set
            {
                this.tryEnter = value;
            }
        }

        /// <summary>
        ///     Gets or sets the view port.
        /// </summary>
        public Rectangle ViewPort
        {
            get
            {
                return this.viewPort;
            }

            set
            {
                this.viewPort = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The change texture.
        /// </summary>
        /// <param name="SelectedTexture">
        /// The selected texture.
        /// </param>
        public void ChangeTexture(EBoxGround SelectedTexture)
        {
            if (this.IsChangeTextureSelected)
            {
                foreach (Box box in this.SelectedBox)
                {
                    box.Ground = SelectedTexture;
                }
            }

            if (this.IsFillTextureSelected)
            {
                foreach (Box box in this.SelectedBox)
                {
                    this.FillBox(box, box.Ground, SelectedTexture);
                }
            }
        }

        /// <summary>
        /// The create animal.
        /// </summary>
        /// <param name="eAnimalType">
        /// The e animal type.
        /// </param>
        /// <exception cref="NotSupportedException">
        /// </exception>
        public void CreateAnimal(EAnimalTexture eAnimalType, Point StartPosition)
        {
            Animal a;
            switch (eAnimalType.ToString())
            {
                case "Dog":
                    a = new Dog( this.map, StartPosition );
                    break;
                case "Cat":
                    a = new Cat(this.map, StartPosition);
                    break;
                case "Lion":
                    a = new Lion(this.map, StartPosition);
                    break;
                case "Rabbit":
                    a = new Rabbit(this.map, StartPosition);
                    break;
                case "Elephant":
                    a = new Elephant(this.map, StartPosition);
                    break;
                case "Cow":
                    a = new Cow(this.map, StartPosition);
                    break;
                case "Eagle":
                    a = new Eagle(this.map, StartPosition);
                    break;
                case "Gazelle":
                    a = new Gazelle(this.map, StartPosition);
                    break;
                default:
                    throw new NotSupportedException("Unknown animal type");
            }

            sounds.PlaySpawnSound(a);
            this.map.Animals.Add(a);
        }

        public void CreateAnimal(EAnimalTexture eAnimalType, Point StartPosition, bool IsNewBorn, Animal mother)
        {
            Animal a;
            switch (eAnimalType.ToString())
            {
                case "Dog":
                    a = new Dog(this.map, StartPosition, true);
                    break;
                case "Cat":
                    a = new Cat(this.map, StartPosition, true);
                    break;
                case "Lion":
                    a = new Lion(this.map, StartPosition, true);
                    break;
                case "Rabbit":
                    a = new Rabbit(this.map, StartPosition, true);
                    break;
                case "Elephant":
                    a = new Elephant(this.map, StartPosition, true);
                    break;
                case "Cow":
                    a = new Cow(this.map, StartPosition, true);
                    break;
                case "Eagle":
                    a = new Eagle(this.map, StartPosition, true);
                    break;
                case "Gazelle":
                    a = new Gazelle(this.map, StartPosition, true);
                    break;
                default:
                    throw new NotSupportedException("Unknown animal type");
            }
            a.Mother = mother;
            sounds.PlaySpawnSound(a);
            this.map.Animals.Add(a);
        }

        public Point AnimalCursor
        {
            get { return animalSelectorCursor; }
        }

        /// <summary>
        /// The create vegetation.
        /// </summary>
        /// <param name="texture">
        /// The texture.
        /// </param>
        /// <exception cref="NotSupportedException">
        /// </exception>
        public void CreateMapElement(EmapElements texture)
        {
            MapElement v;
            switch (texture)
            {
                case EmapElements.Tree:
                    v = new Tree(this.map, this.elementSelectorCursor);
                    break;
                case EmapElements.Bush:
                    v = new Bush(this.map, this.elementSelectorCursor);
                    break;
                case EmapElements.Rock:
                    v = new Rock(this.map, this.elementSelectorCursor);
                    break;
                case EmapElements.House:
                    v = new House(this.map, this.elementSelectorCursor);
                    break;
                case EmapElements.FlowerPot:
                    v = new Furniture( this.map, this.elementSelectorCursor, EmapElements.FlowerPot );
                    break;
                case EmapElements.Table:
                    v = new Furniture( this.map, this.elementSelectorCursor, EmapElements.Table );
                    break;
                case EmapElements.Barrel:
                    v = new Furniture( this.map, this.elementSelectorCursor, EmapElements.Barrel );
                    break;
                case EmapElements.Chair:
                    v = new Furniture( this.map, this.elementSelectorCursor, EmapElements.Chair );
                    break;
                case EmapElements.Shelf:
                    v = new Furniture( this.map, this.elementSelectorCursor, EmapElements.Shelf );
                    break;
                case EmapElements.Panier:
                    v = new Furniture( this.map, this.elementSelectorCursor, EmapElements.Panier );
                    break;
                case EmapElements.Spi:
                    v = new Spi(this.map, this.elementSelectorCursor);
                    break;
                default:
                    throw new NotSupportedException("Unknown element type");
            }
            foreach( Box b in BoxList )
            {
                if( v.Area.IntersectsWith( b.Area ) )
                {
                    b.AddElement( v.Texture );
                }
            }
            this.map.Vegetation.Add(v);
        }

      

        /// <summary>
        ///     The init spawn.
        /// </summary>
        public void InitSpawn()
        {
            this.viewPort.Size = new Size(this.screen.Width * 2, this.screen.Height * 2);
            this.viewPort.X = this.player.Area.X - (this.viewPort.Size.Width / 2) + (this.player.Area.Width / 2);
            this.viewPort.Y = this.player.Area.Y - (this.viewPort.Size.Height / 2) + (this.player.Area.Height / 2);
        }

        /// <summary>
        /// The load map.
        /// </summary>
        /// <param name="map">
        /// The map.
        /// </param>
        public void LoadMap(Map map)
        {
            this.map = map;
        }


        /// <summary>
        /// The spawn car.
        /// </summary>
        /// <param name="position">
        /// The position.
        /// </param>
        public void SpawnCar(Point position)
        {
            if (this.carList.Count >= 0)
            {
                this.carList.Add(new Car(this.map, position));
            }
        }

        /// <summary>
        /// The spawn player.
        /// </summary>
        /// <param name="position">
        /// The position.
        /// </param>
        public void SpawnPlayer(Point position)
        {
            this.player = new Player(this.map, position);
        }

        /// <summary>
        /// The spawn tank.
        /// </summary>
        /// <param name="position">
        /// The position.
        /// </param>
        public void SpawnTank(Point position)
        {
            if (this.carList.Count >= 0)
            {
                this.carList.Add(new Tank(this.map, position));
            }
        }



        #endregion

        #region Methods

        /// <summary>
        ///     The adjust view port.
        /// </summary>
        private void AdjustViewPort()
        {
            if (this.viewPort.Left < 0)
            {
                this.viewPort.X = 0;
            }

            if (this.viewPort.Top < 0)
            {
                this.viewPort.Y = 0;
            }

            if (this.viewPort.Bottom > this.map.MapSize)
            {
                this.viewPort.Y = this.map.MapSize - this.viewPort.Height;
            }

            if (this.viewPort.Right > this.map.MapSize)
            {
                this.viewPort.X = this.map.MapSize - this.viewPort.Width;
            }
        }

        /// <summary>
        /// The adjust view port.
        /// </summary>
        /// <param name="a">
        /// The a.
        /// </param>
        private void AdjustViewPort(Animal a)
        {
            this.viewPort.Size = new Size(Lerp(this.screen.Width * 2, viewPort.Size.Width, this.CameraSmoothness), Lerp(this.screen.Height * 2, viewPort.Size.Height, this.CameraSmoothness));
            this.viewPort.X = Lerp(this.followedAnimal.Area.X - (this.viewPort.Size.Width / 2)
                               + (this.followedAnimal.Area.Width / 2), this.viewPort.X, this.CameraSmoothness);
            this.viewPort.Y = Lerp(this.followedAnimal.Area.Y - (this.viewPort.Size.Height / 2)
                               + (this.followedAnimal.Area.Height / 2), this.viewPort.Y, this.CameraSmoothness);

            if (this.viewPort.Left < 0)
            {
                this.viewPort.X = 0;
            }

            if (this.viewPort.Top < 0)
            {
                this.viewPort.Y = 0;
            }

            if (this.viewPort.Bottom > this.map.MapSize)
            {
                this.viewPort.Y = this.map.MapSize - this.viewPort.Height;
            }

            if (this.viewPort.Right > this.map.MapSize)
            {
                this.viewPort.X = this.map.MapSize - this.viewPort.Width;
            }
        }

        /// <summary>
        /// The animal following.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        private void AnimalFollowing(Graphics g)
        {
            if (this.followAnimal && this.HasClicked)
            {
                foreach (Animal a in this.map.Animals)
                {
                    if (this.mouseRect.IntersectsWith(new Rectangle(a.RelativePosition, a.RelativeSize)))
                    {
                        if (this.followedAnimal != null)
                        {
                            if (this.followedAnimal == a || this.followedAnimal.IsDead)
                            {
                                this.followedAnimal = null;
                            }
                        }
                        else
                        {
                            this.followedAnimal = a;
                            this.isFollowingAnAnimal = true;
                        }
                    }
                    else
                    {
                        if (this.followedAnimal == null)
                        {
                            this.isFollowingAnAnimal = false;
                        }
                    }
                }
            }

            if (this.followedAnimal != null)
            {
                if (this.followedAnimal.IsDead)
                {
                    this.isFollowingAnAnimal = false;
                }

                if (this.followAnimal)
                {
                    g.DrawRectangle(
                        Pens.Red, 
                        new Rectangle(this.followedAnimal.RelativePosition, this.followedAnimal.RelativeSize));
                }
            }
        }

        /// <summary>
        ///     The check if player has entered a car.
        /// </summary>
        private void CheckIfPlayerHasEnteredACar()
        {
            if (this.TryEnter && !this.map.IsInCar)
            {
                foreach (Car car in this.carList)
                {
                    if (this.player.Area.IntersectsWith(car.Area) && this.map.IsInCar == false)
                    {
                        this.sounds.StartEngine();
                        this.map.IsInCar = true;
                        this.player.Car = car;
                    }
                }
            }
        }

        public bool IsExitingHouse
        {
            get { return _exitHouse; }
            set { _exitHouse = value; }
        }


        /// <summary>
        /// The player behavior.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        private void PlayerBehavior(Graphics g)
        {
            if( this.map.IsPlayer )
            {
                for( int i = 0; i < this.map.Vegetation.Count; i++ )
                {
                    if( this.player.Area.IntersectsWith( map.Vegetation[i].Area ) )
                    {
                        if( map.Vegetation[i].Texture == EmapElements.House )
                        {
                            this.player.IsInHouse = true;
                        }
                    }
                }

                if( player.BoxList != null )
                {
                    for( int j = 0; j < player.BoxList.Count; j++ )
                    {
                        if( player.BoxList[j].Ground == EBoxGround.Exit )
                        {
                            _exitHouse = true;
                        }
                    }
                }


                if( this.player.Position.X < 0 )
                {
                    this.player.Position = new Point( 0, this.player.Position.Y );
                }

                if (this.player.Position.Y < 0)
                {
                    this.player.Position = new Point(this.player.Position.X, 0);
                }

                if (this.player.Position.X > this.map.MapSize - this.player.Area.Width)
                {
                    this.player.Position = new Point(this.map.MapSize - this.player.Area.Width, this.player.Position.Y);
                }

                if (this.player.Position.Y > this.map.MapSize - this.player.Area.Width)
                {
                    this.player.Position = new Point(this.player.Position.X, this.map.MapSize - this.player.Area.Width);
                }

                this.player.BoxList = this.map.GetOverlappedBoxes(this.player.AreaBottom);

                if (!this.player.IsMoving)
                {
                    this.player.EMovingDirection = EMovingDirection.Idle;
                }

                if (!this.map.IsInCar)
                {
                    this.player.Draw(g, this.screen, this.viewPort, this.miniMap, this.miniMapViewPort, this.texture);
                }

                if (this.player.IsMoving)
                {
                    foreach (MapElement vegetation in this.map.Vegetation)
                    {
                        if (vegetation.Area.IntersectsWith(this.Player.Area))
                        {
                            {
                                if (vegetation.Texture == EmapElements.Rock
                                    || vegetation.Texture == EmapElements.Rock2
                                    || vegetation.Texture == EmapElements.Rock3)
                                {
                                    this.Player.notAlloudToMove();
                                }
                                if (vegetation.Texture == EmapElements.Spi)
                                {
                                    this.sounds.SpiVoice();
                                }
                                else
                                {
                                    this.sounds.SpiStop();
                                }
                            }
                        }
                    }
                }

                if (this.map.IsInCar)
                {
                    if (this.player.Car.Position.X < 0)
                    {
                        this.player.Car.Position = new Point(0, this.player.Car.Position.Y);
                    }

                    if (this.player.Car.Position.Y < 0)
                    {
                        this.player.Car.Position = new Point(this.player.Car.Position.X, 0);
                    }

                    if (this.player.Car.Position.X > this.map.MapSize - this.player.Car.Area.Width)
                    {
                        this.player.Car.Position = new Point(
                            this.map.MapSize - this.player.Car.Area.Width, 
                            this.player.Car.Position.Y);
                    }

                    if (this.player.Car.Position.Y > this.map.MapSize - this.player.Car.Area.Width)
                    {
                        this.player.Car.Position = new Point(
                            this.player.Car.Position.X, 
                            this.map.MapSize - this.player.Car.Area.Width);
                    }

                    this.player.Position = this.player.Car.Position;
                }
            }
        }

        /// <summary>
        ///     The rain.
        /// </summary>
        private void Rain()
        {
            var t = new Timer();
            t.Interval = 10000;

            t.Tick += this.T_rain_tick;
            t.Start();
        }

        /// <summary>
        /// The t_rain_tick.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void T_rain_tick(object sender, EventArgs e)
        {
            if (this.map.IsRaining)
            {
                var r = new Random();
                var target = new Point(r.Next(0, this.map.MapSize), r.Next(0, this.map.MapSize));
                var targetRect = new Rectangle(target, new Size(r.Next(0, 800), r.Next(0, 800)));
                int top = targetRect.Top / this.map.BoxSize;
                int left = targetRect.Left / this.map.BoxSize;
                int bottom = (targetRect.Bottom - 1) / this.map.BoxSize;
                int right = (targetRect.Right - 1) / this.map.BoxSize;
                for (int i = top; i <= bottom; ++i)
                {
                    for (int j = left; j <= right; ++j)
                    {
                        if (this.map[i, j] != null)
                        {
                            Box b = this.map[j, i];
                            if (b.Ground == EBoxGround.Grass || b.Ground == EBoxGround.Grass2
                                || b.Ground == EBoxGround.Dirt)
                            {
                                b.Ground = EBoxGround.Water;
                            }

                            b.DrawTransitionTextures();
                        }
                    }
                }
            }
        }

        #endregion
        public int Lerp(int flGoal, int flCurrent, int dt)
        {
            int flDifference = flGoal - flCurrent;

            if (flDifference > dt)
            {
                return flCurrent + dt;
            }
            if (flDifference < -dt)
            {
                return flCurrent - dt;
            }
            return flGoal;
        }


    }


}