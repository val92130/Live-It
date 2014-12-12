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
        private bool putVegetation;

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
        ///     The _vegetation selector cursor.
        /// </summary>
        private Point vegetationSelectorCursor;

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
            this.screenTop = new Rectangle((this.screen.Width / 2) - 400, this.screen.Top + 10, 800, 150);
            this.screenBottom = new Rectangle(this.screen.Width / 2 - 400, this.screen.Bottom - 100, 800, 150);
            this.screenLeft = new Rectangle(0, this.screen.Height / 2 - 400, 10, 800);
            this.screenRight = new Rectangle(this.screen.Right - 10, this.screen.Height / 2 - 400, 10, 800);

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
                this.putVegetation = false;
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
                this.putVegetation = false;
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
                this.putVegetation = false;
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
                this.putVegetation = false;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether is follow mode.
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
        public bool IsVegetationSelected
        {
            get
            {
                return this.putVegetation;
            }

            set
            {
                this.putVegetation = value;
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
        public void CreateAnimal(EAnimalTexture eAnimalType)
        {
            Animal a;
            switch (eAnimalType.ToString())
            {
                case "Dog":
                    a = new Dog(this.map, this.animalSelectorCursor);
                    break;
                case "Cat":
                    a = new Cat(this.map, this.animalSelectorCursor);
                    break;
                case "Lion":
                    a = new Lion(this.map, this.animalSelectorCursor);
                    break;
                case "Rabbit":
                    a = new Rabbit(this.map, this.animalSelectorCursor);
                    break;
                case "Elephant":
                    a = new Elephant(this.map, this.animalSelectorCursor);
                    break;
                case "Cow":
                    a = new Cow(this.map, this.animalSelectorCursor);
                    break;
                case "Eagle":
                    a = new Eagle(this.map, this.animalSelectorCursor);
                    break;
                case "Gazelle":
                    a = new Gazelle(this.map, this.animalSelectorCursor);
                    break;
                default:
                    throw new NotSupportedException("Unknown animal type");
            }

            this.map.Animals.Add(a);
        }

        /// <summary>
        /// The create vegetation.
        /// </summary>
        /// <param name="texture">
        /// The texture.
        /// </param>
        /// <exception cref="NotSupportedException">
        /// </exception>
        public void CreateVegetation(EVegetationTexture texture)
        {
            Vegetation v;
            switch (texture)
            {
                case EVegetationTexture.Tree:
                    v = new Tree(this.map, this.vegetationSelectorCursor);
                    break;
                case EVegetationTexture.Bush:
                    v = new Bush(this.map, this.vegetationSelectorCursor);
                    break;
                case EVegetationTexture.Rock:
                    v = new Rock(this.map, this.vegetationSelectorCursor);
                    break;
                default:
                    throw new NotSupportedException("Unknown vegetation type");
            }

            this.map.Vegetation.Add(v);
        }

        /// <summary>
        /// The draw.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        public void Draw(Graphics g)
        {
            this.MoveWithMouse();

            this.AdjustViewPort();

            if (!this.followAnimal)
            {
                this.followedAnimal = null;
            }

            if (this.IsFollowingAnAnimal)
            {
                if (this.followedAnimal != null)
                {
                    this.AdjustViewPort(this.followedAnimal);
                }
                else
                {
                    this.isFollowingAnAnimal = false;
                }
            }

            // Create the rain
            var t = new Random();
            if (t.Next(MinStartRain, MaxStartRain) == 30)
            {
                this.map.IsRaining = true;
            }

            if (t.Next(MinStopRain, MaxStopRain) == 40 && this.map.IsRaining)
            {
                this.map.IsRaining = false;
            }

            this.boxList = this.map.GetOverlappedBoxes(this.viewPort);
            this.boxListMini = this.map.GetOverlappedBoxes(this.miniMapViewPort);
            this.mouseRect.X = Cursor.Position.X - (this.mouseRect.Width / 2);
            this.mouseRect.Y = Cursor.Position.Y - (this.mouseRect.Height / 2);

            this.DrawBoxes(g);

            foreach (Rectangle r in this.map.BloodList)
            {
                this.DrawBloodInViewPort(g, r, this.screen, this.viewPort, this.miniMap, this.miniMapViewPort);
            }

            g.DrawRectangle(
                Pens.White, 
                new Rectangle(this.miniMap.X, this.miniMap.Y, this.miniMap.Width, this.miniMap.Height + 20));

            this.DrawLandAnimals(g);

            this.PlayerBehavior(g);

            this.DrawCars(g);

            this.DrawVegetation(g);

            this.DrawFlyingAnimals(g);

            if (this.changeTexture)
            {
                this.DrawMouseSelector(g);
            }

            if (this.fillTexture)
            {
                this.FillMouseSelector(g);
            }

            if (this.putAnimal)
            {
                this.PutAnimalSelector(g);
            }

            if (this.putVegetation)
            {
                this.PutVegetationSelector(g);
            }

            this.AnimalFollowing(g);

            this.MakeRain(g);

            this.DrawViewPortMiniMap(g, this.viewPort, this.miniMap, this.miniMapViewPort);

            this.CheckIfPlayerHasEnteredACar();

            this.HasClicked = false;
        }

        /// <summary>
        /// The draw blood in view port.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <param name="viewPort">
        /// The view port.
        /// </param>
        /// <param name="targetMiniMap">
        /// The target mini map.
        /// </param>
        /// <param name="viewPortMiniMap">
        /// The view port mini map.
        /// </param>
        public void DrawBloodInViewPort(
            Graphics g, 
            Rectangle source, 
            Rectangle target, 
            Rectangle viewPort, 
            Rectangle targetMiniMap, 
            Rectangle viewPortMiniMap)
        {
            var newSize = (int)((source.Width / (double)viewPort.Width) * target.Width + 1);
            var newHeight = (int)((source.Height / (double)viewPort.Width) * target.Width + 1);
            int newXpos = (int)(source.X / (source.Width / ((source.Width / (double)viewPort.Width) * target.Width)))
                          - (int)
                            (viewPort.X / (source.Width / ((source.Width / (double)viewPort.Width) * target.Width)));
            int newYpos = (int)(source.Y / (source.Width / ((source.Width / (double)viewPort.Width) * target.Width)))
                          - (int)
                            (viewPort.Y / (source.Width / ((source.Width / (double)viewPort.Width) * target.Width)));

            var newSizeMini = (int)((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            var newHeightMini = (int)((source.Height / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            int newXposMini =
                (int)
                (source.X / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)))
                - (int)
                  (viewPortMiniMap.X
                   / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));
            int newYposMini =
                (int)
                (source.Y / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)))
                - (int)
                  (viewPortMiniMap.Y
                   / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));

            g.DrawImage(
                this.texture.GetBlood(), 
                new Rectangle(newXpos + target.X, newYpos + target.Y, newSize, newHeight));
            g.DrawRectangle(
                Pens.Red, 
                new Rectangle(newXposMini + targetMiniMap.X, newYposMini + targetMiniMap.Y, newSizeMini, newHeightMini));
        }

        /// <summary>
        /// Draw the selection rectangle to change the textures
        /// </summary>
        /// <param name="g">
        /// </param>
        public void DrawMouseSelector(Graphics g)
        {
            this.selectedBoxes.Clear();
            for (int i = 0; i < this.boxList.Count; i++)
            {
                if (!this.mouseRect.IntersectsWith(this.miniMap))
                {
                    if (
                        this.mouseRect.IntersectsWith(
                            new Rectangle(this.boxList[i].RelativePosition, this.boxList[i].RelativeSize)))
                    {
                        if (this.boxList[i].AnimalList.Count != 0)
                        {
                            g.DrawRectangle(
                                Pens.Red, 
                                new Rectangle(this.boxList[i].RelativePosition, this.boxList[i].RelativeSize));
                        }
                        else
                        {
                            g.DrawRectangle(
                                Pens.White, 
                                new Rectangle(this.boxList[i].RelativePosition, this.boxList[i].RelativeSize));
                            this.selectedBoxes.Add(this.map[this.boxList[i].Line, this.boxList[i].Column]);
                        }

                        g.DrawString(
                            "Box X :" + this.boxList[i].Area.X + "\nBox Y :" + this.boxList[i].Area.Y
                            + "\nBox Texture : \n" + this.boxList[i].Ground, 
                            new Font("Arial", 10f), 
                            Brushes.Aqua, 
                            this.boxList[i].RelativePosition);
                    }
                }
            }
        }

        /// <summary>
        /// The draw rectangle in view port.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <param name="viewPort">
        /// The view port.
        /// </param>
        /// <param name="targetMiniMap">
        /// The target mini map.
        /// </param>
        /// <param name="viewPortMiniMap">
        /// The view port mini map.
        /// </param>
        public void DrawRectangleInViewPort(
            Graphics g, 
            Rectangle source, 
            Rectangle target, 
            Rectangle viewPort, 
            Rectangle targetMiniMap, 
            Rectangle viewPortMiniMap)
        {
            var newSize = (int)((source.Width / (double)viewPort.Width) * target.Width + 1);
            var newHeight = (int)((source.Height / (double)viewPort.Width) * target.Width + 1);
            int newXpos = (int)(source.X / (source.Width / ((source.Width / (double)viewPort.Width) * target.Width)))
                          - (int)
                            (viewPort.X / (source.Width / ((source.Width / (double)viewPort.Width) * target.Width)));
            int newYpos = (int)(source.Y / (source.Width / ((source.Width / (double)viewPort.Width) * target.Width)))
                          - (int)
                            (viewPort.Y / (source.Width / ((source.Width / (double)viewPort.Width) * target.Width)));

            var newSizeMini = (int)((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            var newHeightMini = (int)((source.Height / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            int newXposMini =
                (int)
                (source.X / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)))
                - (int)
                  (viewPortMiniMap.X
                   / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));
            int newYposMini =
                (int)
                (source.Y / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)))
                - (int)
                  (viewPortMiniMap.Y
                   / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));

            g.DrawRectangle(Pens.Blue, new Rectangle(newXpos + target.X, newYpos + target.Y, newSize, newHeight));
            g.DrawRectangle(
                Pens.Blue, 
                new Rectangle(newXposMini + targetMiniMap.X, newYposMini + targetMiniMap.Y, newSizeMini, newHeightMini));
        }

        /// <summary>
        /// The draw rectangle in view port.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <param name="viewPort">
        /// The view port.
        /// </param>
        /// <param name="targetMiniMap">
        /// The target mini map.
        /// </param>
        /// <param name="viewPortMiniMap">
        /// The view port mini map.
        /// </param>
        /// <param name="animal">
        /// The animal.
        /// </param>
        /// <param name="t">
        /// The t.
        /// </param>
        public void DrawRectangleInViewPort(
            Graphics g, 
            Rectangle source, 
            Rectangle target, 
            Rectangle viewPort, 
            Rectangle targetMiniMap, 
            Rectangle viewPortMiniMap, 
            Animal animal, 
            Texture t)
        {
            var newSize = (int)((source.Width / (double)viewPort.Width) * target.Width + 1);
            var newHeight = (int)((source.Height / (double)viewPort.Width) * target.Width + 1);
            int newXpos = (int)(source.X / (source.Width / ((source.Width / (double)viewPort.Width) * target.Width)))
                          - (int)
                            (viewPort.X / (source.Width / ((source.Width / (double)viewPort.Width) * target.Width)));
            int newYpos = (int)(source.Y / (source.Width / ((source.Width / (double)viewPort.Width) * target.Width)))
                          - (int)
                            (viewPort.Y / (source.Width / ((source.Width / (double)viewPort.Width) * target.Width)));

            var newSizeMini = (int)((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            var newHeightMini = (int)((source.Height / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            int newXposMini =
                (int)
                (source.X / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)))
                - (int)
                  (viewPortMiniMap.X
                   / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));
            int newYposMini =
                (int)
                (source.Y / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)))
                - (int)
                  (viewPortMiniMap.Y
                   / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));

            g.DrawImage(
                t.LoadTexture(animal), 
                new Rectangle(newXpos + target.X, newYpos + target.Y, newSize, newHeight));
            g.DrawImage(
                t.LoadTexture(animal), 
                new Rectangle(newXposMini + targetMiniMap.X, newYposMini + targetMiniMap.Y, newSizeMini, newHeightMini));
        }

        /// <summary>
        /// The draw view port mini map.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="targetMiniMap">
        /// The target mini map.
        /// </param>
        /// <param name="viewPortMiniMap">
        /// The view port mini map.
        /// </param>
        public void DrawViewPortMiniMap(
            Graphics g, 
            Rectangle source, 
            Rectangle targetMiniMap, 
            Rectangle viewPortMiniMap)
        {
            var newSizeMini = (int)((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            var newHeightMini = (int)((source.Height / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            int newXposMini =
                (int)
                (source.X / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)))
                - (int)
                  (viewPortMiniMap.X
                   / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));
            int newYposMini =
                (int)
                (source.Y / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)))
                - (int)
                  (viewPortMiniMap.Y
                   / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));
            g.DrawRectangle(
                Pens.White, 
                new Rectangle(newXposMini + targetMiniMap.X, newYposMini + targetMiniMap.Y, newSizeMini, newHeightMini));
        }

        /// <summary>
        /// Select the boxes with the targetedColor texture, and remplace them with the desired Color
        /// </summary>
        /// <param name="target">
        /// </param>
        /// <param name="targetColor">
        /// Texture you wish to change
        /// </param>
        /// <param name="Color">
        /// Remplacement color
        /// </param>
        public void FillBox(Box target, EBoxGround targetColor, EBoxGround Color)
        {
            if (target.Ground == targetColor && Color != target.Ground)
            {
                target.Ground = Color;
                if (target.Top != null)
                {
                    this.FillBox(target.Top, targetColor, Color);
                }

                if (target.Bottom != null)
                {
                    this.FillBox(target.Bottom, targetColor, Color);
                }

                if (target.Left != null)
                {
                    this.FillBox(target.Left, targetColor, Color);
                }

                if (target.Right != null)
                {
                    this.FillBox(target.Right, targetColor, Color);
                }
            }
        }

        /// <summary>
        /// Draw the selection rectangle to fill textures
        /// </summary>
        /// <param name="g">
        /// </param>
        public void FillMouseSelector(Graphics g)
        {
            int count = 0;
            this.selectedBoxes.Clear();
            for (int i = 0; i < this.boxList.Count; i++)
            {
                this.mouseRect.Width = this.boxList[i].RelativeSize.Width / 4;
                this.mouseRect.Height = this.boxList[i].RelativeSize.Height / 4;
                if (
                    this.mouseRect.IntersectsWith(
                        new Rectangle(this.boxList[i].RelativePosition, this.boxList[i].RelativeSize)) && count != 1)
                {
                    count++;
                    this.selectedBoxes.Add(this.map[this.boxList[i].Line, this.boxList[i].Column]);
                    g.FillEllipse(
                        new SolidBrush(Color.FromArgb(52, 152, 219)), 
                        new Rectangle(this.mouseRect.X, this.mouseRect.Y, this.mouseRect.Width, this.mouseRect.Height));
                    g.DrawString(
                        "Box X :" + this.boxList[i].Area.X + "\nBox Y :" + this.boxList[i].Area.Y + "\nBox Texture : \n"
                        + this.boxList[i].Ground, 
                        new Font("Arial", 10f), 
                        Brushes.Aqua, 
                        this.boxList[i].RelativePosition);
                }
            }
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
        ///     The move with mouse.
        /// </summary>
        public void MoveWithMouse()
        {
            var cursorPos = new Rectangle(Cursor.Position, new Size(10, 10));
            const int Speed = 45;
            if (!this.map.IsPlayer)
            {
                if (cursorPos.IntersectsWith(this.screenTop))
                {
                    this.MoveY(-Speed);
                }

                if (cursorPos.IntersectsWith(this.screenBottom))
                {
                    this.MoveY(Speed);
                }

                if (cursorPos.IntersectsWith(this.screenLeft))
                {
                    this.MoveX(-Speed);
                }

                if (cursorPos.IntersectsWith(this.screenRight))
                {
                    this.MoveX(Speed);
                }
            }
        }

        /// <summary>
        /// The move x.
        /// </summary>
        /// <param name="centimeters">
        /// The centimeters.
        /// </param>
        public void MoveX(int centimeters)
        {
            if (!this.map.IsPlayer)
            {
                this.Offset(new Point(centimeters, 0));
            }
            else
            {
                if (this.map.IsPlayer && this.map.IsInCar == false)
                {
                    if (centimeters <= 0)
                    {
                        this.player.EMovingDirection = EMovingDirection.Left;
                    }
                    else
                    {
                        this.player.EMovingDirection = EMovingDirection.Right;
                    }

                    this.player.Position = new Point(this.player.Position.X + (centimeters / 2), this.player.Position.Y);
                    this.viewPort.Size = new Size(this.screen.Width * 2, this.screen.Height * 2);
                    this.viewPort.X = this.player.Area.X - (this.viewPort.Size.Width / 2) + (this.player.Area.Width / 2);
                    this.viewPort.Y = this.player.Area.Y - (this.viewPort.Size.Height / 2)
                                      + (this.player.Area.Height / 2);
                }
                else if (this.map.IsPlayer && this.map.IsInCar)
                {
                    if (centimeters > 0)
                    {
                        this.player.Car.EMovingDirection = EMovingDirection.Right;
                    }
                    else
                    {
                        this.player.Car.EMovingDirection = EMovingDirection.Left;
                    }

                    this.player.Car.Position = new Point(
                        this.player.Car.Position.X + (centimeters * 2),
                        this.player.Car.Position.Y);
                    this.viewPort.Size = new Size(this.screen.Width * 2, this.screen.Height * 2);
                    this.viewPort.X = this.player.Car.Area.X - (this.viewPort.Size.Width / 2)
                                      + (this.player.Car.Area.Width / 2);
                    this.viewPort.Y = this.player.Car.Area.Y - (this.viewPort.Size.Height / 2)
                                      + (this.player.Car.Area.Height / 2);
                }
            }
        }

        /// <summary>
        /// The move y.
        /// </summary>
        /// <param name="centimeters">
        /// The centimeters.
        /// </param>
        public void MoveY(int centimeters)
        {
            if (!this.map.IsPlayer)
            {
                this.Offset(new Point(0, centimeters));
            }
            else if (this.map.IsPlayer && this.map.IsInCar == false)
            {
                if (centimeters > 0)
                {
                    this.player.EMovingDirection = EMovingDirection.Down;
                }
                else
                {
                    this.player.EMovingDirection = EMovingDirection.Up;
                }

                this.player.Position = new Point(this.player.Position.X, this.player.Position.Y + (centimeters / 2));
                this.viewPort.Size = new Size(this.screen.Width * 2, this.screen.Height * 2);
                this.viewPort.X = this.player.Area.X - (this.viewPort.Size.Width / 2) + (this.player.Area.Width / 2);
                this.viewPort.Y = this.player.Area.Y - (this.viewPort.Size.Height / 2) + (this.player.Area.Height / 2);
            }
            else if (this.map.IsPlayer && this.map.IsInCar)
            {
                if (centimeters > 0)
                {
                    this.player.Car.EMovingDirection = EMovingDirection.Down;
                }
                else
                {
                    this.player.Car.EMovingDirection = EMovingDirection.Up;
                }

                this.player.Car.Position = new Point(
                    this.player.Car.Position.X, 
                    this.player.Car.Position.Y + (centimeters * 2));
                this.viewPort.Size = new Size(this.screen.Width * 2, this.screen.Height * 2);
                this.viewPort.X = this.player.Car.Area.X - (this.viewPort.Size.Width / 2)
                                   + (this.player.Car.Area.Width / 2);
                this.viewPort.Y = this.player.Car.Area.Y - (this.viewPort.Size.Height / 2)
                                   + (this.player.Car.Area.Height / 2);
            }
        }

        /// <summary>
        /// The offset.
        /// </summary>
        /// <param name="delta">
        /// The delta.
        /// </param>
        public void Offset(Point delta)
        {
            if (!this.map.IsPlayer)
            {
                this.viewPort.Offset(delta);
                if (this.viewPort.X < 0)
                {
                    this.viewPort.X = 0;
                }

                if (this.viewPort.Y < 0)
                {
                    this.viewPort.Y = 0;
                }

                if (this.viewPort.Right > this.map.MapSize)
                {
                    this.viewPort.X = this.map.MapSize - this.viewPort.Width;
                }

                if (this.viewPort.Bottom > this.map.MapSize)
                {
                    this.viewPort.Y = this.map.MapSize - this.viewPort.Height;
                }
            }
        }

        /// <summary>
        /// The put animal selector.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        public void PutAnimalSelector(Graphics g)
        {
            int count = 0;
            this.selectedBoxes.Clear();
            for (int i = 0; i < this.boxList.Count; i++)
            {
                this.mouseRect.Width = this.boxList[i].RelativeSize.Width / 4;
                this.mouseRect.Height = this.boxList[i].RelativeSize.Height / 4;
                if (
                    this.mouseRect.IntersectsWith(
                        new Rectangle(this.boxList[i].RelativePosition, this.boxList[i].RelativeSize)) && count != 1)
                {
                    count++;
                    this.selectedBoxes.Add(this.map[this.boxList[i].Line, this.boxList[i].Column]);
                    this.animalSelectorCursor.X = this.map[this.boxList[i].Line, this.boxList[i].Column].Area.X;
                    this.animalSelectorCursor.Y = this.map[this.boxList[i].Line, this.boxList[i].Column].Area.Y;
                    g.FillEllipse(
                        new SolidBrush(Color.Brown), 
                        new Rectangle(this.mouseRect.X, this.mouseRect.Y, this.mouseRect.Width, this.mouseRect.Height));
                    g.DrawString(
                        "Box X :" + this.boxList[i].Area.X + "\nBox Y :" + this.boxList[i].Area.Y + "\nBox Texture : \n"
                        + this.boxList[i].Ground, 
                        new Font("Arial", 10f), 
                        Brushes.Aqua, 
                        this.boxList[i].RelativePosition);
                }
            }
        }

        /// <summary>
        /// The put vegetation selector.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        public void PutVegetationSelector(Graphics g)
        {
            int count = 0;
            this.selectedBoxes.Clear();
            for (int i = 0; i < this.boxList.Count; i++)
            {
                this.mouseRect.Width = this.boxList[i].RelativeSize.Width / 4;
                this.mouseRect.Height = this.boxList[i].RelativeSize.Height / 4;
                if (
                    this.mouseRect.IntersectsWith(
                        new Rectangle(this.boxList[i].RelativePosition, this.boxList[i].RelativeSize)) && count != 1)
                {
                    count++;
                    this.selectedBoxes.Add(this.map[this.boxList[i].Line, this.boxList[i].Column]);
                    this.vegetationSelectorCursor.X = this.map[this.boxList[i].Line, this.boxList[i].Column].Area.X;
                    this.vegetationSelectorCursor.Y = this.map[this.boxList[i].Line, this.boxList[i].Column].Area.Y;
                    g.FillEllipse(
                        new SolidBrush(Color.Brown), 
                        new Rectangle(this.mouseRect.X, this.mouseRect.Y, this.mouseRect.Width, this.mouseRect.Height));
                    g.DrawString(
                        "Box X :" + this.boxList[i].Area.X + "\nBox Y :" + this.boxList[i].Area.Y + "\nBox Texture : \n"
                        + this.boxList[i].Ground, 
                        new Font("Arial", 10f), 
                        Brushes.Aqua, 
                        this.boxList[i].RelativePosition);
                }
            }
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

        /// <summary>
        /// The zoom.
        /// </summary>
        /// <param name="meters">
        /// The meters.
        /// </param>
        public void Zoom(int meters)
        {
            if (!this.map.IsPlayer)
            {
                this.viewPort.Width += meters;
                this.viewPort.Height += meters;
                if (this.viewPort.Width < MinimalWidthInCentimeter && this.viewPort.Height < MinimalWidthInCentimeter)
                {
                    this.viewPort.Width = MinimalWidthInCentimeter;
                    this.viewPort.Height = MinimalWidthInCentimeter;
                }

                if (this.viewPort.Width > this.map.MapSize)
                {
                    this.viewPort.Height = this.map.MapSize;
                    this.viewPort.Width = this.map.MapSize;
                }
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
            this.viewPort.Size = new Size(this.screen.Width * 2, this.screen.Height * 2);
            this.viewPort.X = this.followedAnimal.Area.X - (this.viewPort.Size.Width / 2)
                               + (this.followedAnimal.Area.Width / 2);
            this.viewPort.Y = this.followedAnimal.Area.Y - (this.viewPort.Size.Height / 2)
                               + (this.followedAnimal.Area.Height / 2);

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

                if (this.followAnimal && this.map.ShowDebug)
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

        /// <summary>
        /// The draw boxes.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        private void DrawBoxes(Graphics g)
        {
            for (int i = 0; i < this.boxList.Count; i++)
            {
                for (int j = 0; j < this.map.Animals.Count(); j++)
                {
                    if (this.map.Animals[j].Area.IntersectsWith(this.boxList[i].Area))
                    {
                        this.boxList[i].AddAnimal(this.map.Animals[j]);
                    }
                }

                this.boxList[i].Draw(g, this.screen, this.texture, this.viewPort);
            }

            for (int i = 0; i < this.boxListMini.Count; i++)
            {
                this.boxListMini[i].DrawMiniMap(g, this.miniMap, this.texture, this.miniMapViewPort);
            }
        }

        /// <summary>
        /// The draw cars.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        private void DrawCars(Graphics g)
        {
            foreach (Car car in this.carList)
            {
                car.Draw(g, this.screen, this.viewPort, this.miniMap, this.miniMapViewPort, this.texture);
            }

            foreach (Tank tank in this.tankList)
            {
                tank.Draw(g, this.screen, this.viewPort, this.miniMap, this.miniMapViewPort, this.texture);
            }
        }

        /// <summary>
        /// The draw flying animals.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        private void DrawFlyingAnimals(Graphics g)
        {
            for (int i = 0; i < this.map.Animals.Count; i++)
            {
                if (this.map.Animals[i].Texture == EAnimalTexture.Eagle)
                {
                    this.map.Animals[i].Draw(
                        g, 
                        this.screen, 
                        this.viewPort, 
                        this.miniMap, 
                        this.miniMapViewPort, 
                        this.texture);
                }
            }
        }

        /// <summary>
        /// The draw land animals.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        private void DrawLandAnimals(Graphics g)
        {
            for (int i = 0; i < this.map.Animals.Count; i++)
            {
                if (this.map.Animals[i].Texture != EAnimalTexture.Eagle)
                {
                    this.map.Animals[i].Draw(
                        g, 
                        this.screen, 
                        this.viewPort, 
                        this.miniMap, 
                        this.miniMapViewPort, 
                        this.texture);
                }
            }
        }

        /// <summary>
        /// The draw vegetation.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        private void DrawVegetation(Graphics g)
        {
            for (int i = 0; i < this.map.Vegetation.Count; i++)
            {
                this.map.Vegetation[i].Draw(
                    g, 
                    this.screen, 
                    this.viewPort, 
                    this.miniMap, 
                    this.miniMapViewPort, 
                    this.texture);
            }
        }

        /// <summary>
        /// The make rain.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        private void MakeRain(Graphics g)
        {
            if (this.map.IsRaining)
            {
                if (this.isRaining == false)
                {
                    this.Rain();
                    this.isRaining = true;
                }

                // g.DrawImage(_texture.GetThunder(), _screen);
                g.DrawImage(this.texture.GetRain(), this.screen);
            }
        }

        /// <summary>
        /// The player behavior.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        private void PlayerBehavior(Graphics g)
        {
            if (this.map.IsPlayer)
            {
                if (this.player.Position.X < 0)
                {
                    this.player.Position = new Point(0, this.player.Position.Y);
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
                    foreach (Vegetation vegetation in this.map.Vegetation)
                    {
                        if (vegetation.Area.IntersectsWith(this.Player.Area))
                        {
                            {
                                if (vegetation.Texture == EVegetationTexture.Rock
                                    || vegetation.Texture == EVegetationTexture.Rock2
                                    || vegetation.Texture == EVegetationTexture.Rock3)
                                {
                                    this.Player.notAlloudToMove();
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
    }
}