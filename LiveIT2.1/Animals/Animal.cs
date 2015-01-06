// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Animal.cs" company="">
//   
// </copyright>
// <summary>
//   The animal.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LiveIT2._1.Animals
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;

    using LiveIT2._1.Enums;
    using LiveIT2._1.Terrain;
    using LiveIT2._1.Textures;
    using LiveIT2._1.Vegetation;

    /// <summary>
    ///     The animal.
    ///     The base class for all the animals.
    /// </summary>
    [Serializable]
    public abstract partial class Animal
    {
        #region Fields

        PathFinder _pathFinder;

        EMovingDirection _animalDirection;
        List<Box> _pathBoxList;

        /// <summary>
        ///     Boxes that contains the animal
        /// </summary>
        internal List<Box> BoxList = new List<Box>();

        /// <summary>
        ///     If the animal is dead
        /// </summary>
        internal bool _isDead;

        /// <summary>
        ///     If the animal is currently drinking
        /// </summary>
        internal bool _isDrinking;

        /// <summary>
        ///     If the animal is currently eating
        /// </summary>
        internal bool _isEating;

        /// <summary>
        ///     If the animal is in water
        /// </summary>
        internal bool _isInWater;

        /// <summary>
        ///     If the animal is currently walking
        /// </summary>
        internal bool _walking;

        /// <summary>
        ///     Map attached to the animal
        /// </summary>
        protected readonly Map _map;

        /// <summary>
        /// If the animal is hurt ( medic mode )
        /// </summary>
        private bool _isHurt;

        /// <summary>
        ///     The walkable boxes of the animal.
        /// </summary>
        private readonly List<Box> WalkableBoxes = new List<Box>();

        /// <summary>
        ///     Animals contained in the field of view of this animal
        /// </summary>
        private readonly List<Animal> _animalsAround = new List<Animal>();

        /// <summary>
        ///     The _default speed.
        /// </summary>
        private int _defaultSpeed;

        /// <summary>
        ///     The _direction.
        /// </summary>
        private SizeF _direction;

        /// <summary>
        ///     The _favorite environnment of the animal.
        /// </summary>
        private EBoxGround _favoriteEnvironnment;

        [NonSerialized]
        /// <summary>
        ///     The _graphics.
        /// </summary>
        private Graphics _graphics;

        /// <summary>
        ///     The _health.
        /// </summary>
        private int _health;

        /// <summary>
        ///     The _hunger.
        /// </summary>
        private int _hunger;

        internal Size _finalSize;

        /// <summary>
        ///     The _position.
        /// </summary>
        private Point _position;

        /// <summary>
        ///     The _relative position.
        /// </summary>
        private Point _relativePosition;

        /// <summary>
        ///     The _relative size.
        /// </summary>
        private Size _relativeSize;

        /// <summary>
        ///     The _sex type.
        /// </summary>
        private ESex eSex;

        /// <summary>
        ///     The _size.
        /// </summary>
        private Size _size;

        /// <summary>
        ///     The _speed.
        /// </summary>
        private int _speed;

        /// <summary>
        /// Grow speed of the newBorn animal, in millisecond
        /// </summary>
        private int _growSpeed;

        /// <summary>
        ///     The _target location.
        /// </summary>
        private Point _targetLocation;

        private Animal _mother;

        /// <summary>
        ///     The _texture.
        /// </summary>
        private EAnimalTexture _texture;

        [NonSerialized]
        /// <summary>
        ///     The _texture graphics.
        /// </summary>
        private Texture _textureGraphics;

        [NonSerialized]
        Timer _growTimer;

        /// <summary>
        ///     The _thirst.
        /// </summary>
        private int _thirst;

        /// <summary>
        ///     The _view distance.
        /// </summary>
        private int _viewDistance;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Animal"/> class.
        /// </summary>
        /// <param name="map">
        /// The map.
        /// </param>
        /// <param name="position">
        /// The position.
        /// </param>
        protected Animal(Map map, Point position)
        {
            // TODO : add some tests here
            this._map = map;
            this._position = position;
            this._health = 100;
            this._hunger = 0;
            this._thirst = 0;
            this.CheckDrowning();
            this.HungerTimer();
            this.ThirstTimer();

            Array values = Enum.GetValues(typeof(ESex));
            var random = new Random();
            var randomSex = (ESex)values.GetValue(random.Next(values.Length));
            this.eSex = randomSex;
        }

        protected Animal(Map map, Point position, bool IsNewBorn) 
            :this(map,position)
        {
            _growSpeed = 20000;
            _growTimer = new Timer();
            _growTimer.Interval = _growSpeed;
            _growTimer.Tick += new EventHandler(T_Grow_Animal);
            _growTimer.Start();
        }

        private void T_Grow_Animal(object sender, EventArgs e)
        {
            if (this.Size.Height < _finalSize.Height)
            {
                this.Size += new Size(10, 10);
            }
            
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the animals around.
        /// </summary>
        public List<Animal> AnimalsAround
        {
            get
            {
                return this._animalsAround;
            }
        }

        internal Animal Mother
        {
            get { return _mother; }
            set { _mother = value; }
        }

        public PathFinder PathFinder
        {
            get { return _pathFinder; }
        }

        public bool IsHurt
        {
            get { return _isHurt; }
            set { _isHurt = value; }
        }

        public int GrowSpeed
        {
            get { return _growSpeed; }
            set
            {
                if (value >= 0)
                {
                    _growSpeed = value;
                }
            }
        }

        /// <summary>
        ///     Gets the area.
        /// </summary>
        public Rectangle Area
        {
            get
            {
                return new Rectangle(this._position, this._size);
            }
        }

        public Box OverlappedBox
        {
            get { return _map.GetOverlappedBox(this); }
        }

        public Rectangle CenteredArea
        {
            get
            {
                return new Rectangle(new Point(this.Position.X + (this.Size.Width/2), this.Position.Y + (this.Size.Height/2)), new Size(5,5));
            }
        }

        /// <summary>
        ///     Gets or sets the default speed.
        /// </summary>
        public int DefaultSpeed
        {
            get
            {
                return this._defaultSpeed;
            }

            set
            {
                this._defaultSpeed = value;
            }
        }

        /// <summary>
        ///     Gets or sets the direction.
        /// </summary>
        public SizeF Direction
        {
            get
            {
                return this._direction;
            }

            set
            {
                this._direction = value;
            }
        }

        /// <summary>
        ///     Gets or sets the favorite environnment.
        /// </summary>
        public EBoxGround FavoriteEnvironnment
        {
            get
            {
                return this._favoriteEnvironnment;
            }

            set
            {
                this._favoriteEnvironnment = value;
            }
        }

        /// <summary>
        ///     Gets the field of view.
        /// </summary>
        public Rectangle FieldOfView
        {
            get
            {
                return new Rectangle(
                    this.Position.X - (this._viewDistance / 2), 
                    this.Position.Y - (this._viewDistance / 2), 
                    this._viewDistance * 2, 
                    this._viewDistance * 2);
            }
        }

        /// <summary>
        ///     Gets the health.
        /// </summary>
        public int Health
        {
            get
            {
                return this._health;
            }

            internal set
            {
                this._health += value;
                if (this._health - value <= 0)
                {
                    this._health = 0;
                    this.Die();
                }

                if (this._health + value >= 100)
                {
                    this._health = 100;
                }

                if (this._health <= 0)
                {
                    this.Die();
                }
            }
        }

        /// <summary>
        ///     Gets the hunger.
        /// </summary>
        public int Hunger
        {
            get
            {
                return this._hunger;
            }

            internal set
            {
                this._hunger = value;
                if (this._hunger <= 0)
                {
                    this._hunger = 0;
                }

                if (this._hunger >= 100)
                {
                    this._hunger = 100;
                }
            }
        }

        /// <summary>
        ///     Gets a value indicating whether is dead.
        /// </summary>
        public bool IsDead
        {
            get
            {
                return this._isDead;
            }

            internal set
            {
                this._isDead = value;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether is in movement.
        /// </summary>
        public bool IsInMovement
        {
            get
            {
                return this._walking;
            }

            set
            {
                this._walking = value;
            }
        }

        /// <summary>
        /// Gets the max hunger.
        /// </summary>
        public virtual int MaxHunger
        {
            get
            {
                return 50;
            }
        }

        /// <summary>
        ///     Gets the moving direction.
        /// </summary>
        public EMovingDirection EMovingDirection
        {
            get
            {
                if( this.Direction.Width > 0 && this.Direction.Width > this.Direction.Height )
                {
                    return EMovingDirection.Right;
                }

                if( this.Direction.Width < 0 && this.Direction.Width > this.Direction.Height )
                {
                    return EMovingDirection.Left;
                }

                if( this.Direction.Height > 0 && this.Direction.Height > this.Direction.Width )
                {
                    return EMovingDirection.Down;
                }

                if( this.Direction.Height < 0 && this.Direction.Height > this.Direction.Width )
                {
                    return EMovingDirection.Up;
                }

                return EMovingDirection.Up;
            }
        }

        /// <summary>
        ///     Gets or sets the position.
        /// </summary>
        public Point Position
        {
            get
            {
                return this._position;
            }

            set
            {
                this._position = value;
            }
        }

        /// <summary>
        ///     Gets or sets the relative position.
        /// </summary>
        public Point RelativePosition
        {
            get
            {
                return this._relativePosition;
            }

            set
            {
                this._relativePosition = value;
            }
        }

        /// <summary>
        ///     Gets or sets the relative size.
        /// </summary>
        public Size RelativeSize
        {
            get
            {
                return this._relativeSize;
            }

            set
            {
                this._relativeSize = value;
            }
        }

        /// <summary>
        ///     Gets the sex.
        /// </summary>
        public ESex ESex
        {
            get
            {
                return this.eSex;
            }

            internal set
            {
                this.eSex = value;
            }
        }

        /// <summary>
        ///     Gets or sets the size.
        /// </summary>
        public virtual Size Size
        {
            get
            {
                return this._size;
            }

            set
            {
                this._size = value;
            }
        }

        /// <summary>
        ///     Gets or sets the speed.
        /// </summary>
        public int Speed
        {
            get
            {
                return this._speed;
            }

            set
            {
                this._speed = value;
            }
        }

        /// <summary>
        ///     Gets or sets the target location.
        /// </summary>
        public Point TargetLocation
        {
            get
            {
                return this._targetLocation;
            }

            set
            {
                this._targetLocation = value;
            }
        }

        /// <summary>
        ///     Gets or sets the texture.
        /// </summary>
        public EAnimalTexture Texture
        {
            get
            {
                return this._texture;
            }

            protected set
            {
                this._texture = value;
            }
        }

        /// <summary>
        ///     Gets the thirst.
        /// </summary>
        public int Thirst
        {
            get
            {
                return this._thirst;
            }

            internal set
            {
                this._thirst = value;
                if (this._thirst <= 0)
                {
                    this._thirst = 0;
                }

                if (this._thirst >= 100)
                {
                    this._thirst = 100;
                }
            }
        }

        /// <summary>
        ///     Gets or sets the view distance.
        /// </summary>
        public int ViewDistance
        {
            get
            {
                return this._viewDistance;
            }

            set
            {
                this._viewDistance = value;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the park.
        /// </summary>
        internal Map Park
        {
            get
            {
                return this._map;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add to list.
        /// </summary>
        /// <param name="b">
        /// The box to add in the list.
        /// </param>
        public void AddToList(Box b)
        {
            if (!this.BoxList.Contains(b))
            {
                this.BoxList.Add(b);
            }
        }
        int _pathCount = 1;
        /// <summary>
        ///     The behavior of the animal, add here the interactions with other animals.
        /// </summary>
        public virtual void Behavior()
        {

            if( _pathFinder == null )
            {
                this.FindNewPath();
            }

            this._isDrinking = false;

            
            // if the animal isn't in movement, find a new path
            if (!this.IsInMovement)
            {
                //this.ChangePosition();
                this.FindNewPath();
                IsInMovement = true;
            }

            // if the path has reached his target, add the path to the pathboxlist
            if (_pathFinder.FoundTarget)
            {
                this.Speed = this.DefaultSpeed;
                _pathBoxList = _pathFinder.FinalPath;
            }

            // if the animal hasn't reached all the paths, change position to the next paths
            if (_pathBoxList != null)
            {
                ChangePosition(_pathBoxList[_pathCount].Location);

                if( _pathBoxList[_pathCount].Area.X > this.Area.X )
                {
                    this._animalDirection = Enums.EMovingDirection.Right;
                }
                if( _pathBoxList[_pathCount].Area.X < this.Area.X )
                {
                    this._animalDirection = Enums.EMovingDirection.Left;
                }
                if( _pathBoxList[_pathCount].Area.Y > this.Area.Y )
                {
                    this._animalDirection = Enums.EMovingDirection.Down;
                }
                if( _pathBoxList[_pathCount].Area.Y < this.Area.Y )
                {
                    this._animalDirection = Enums.EMovingDirection.Up;
                }
                
                if (_pathBoxList.Count == 1)
                {
                    ChangePosition(_pathBoxList[0].Location);
                }
                else
                {
                    ChangePosition(_pathBoxList[_pathCount].Location);
                }
            }

            // if animal interesect with the next path, increment the path count to go to the next path
            if (this.Area.IntersectsWith(new Rectangle(this.TargetLocation, this.Area.Size)))
            {
                if (_pathBoxList != null)
                {
                    if (_pathCount != _pathBoxList.Count - 1) _pathCount++;
                }

                if (_pathCount == _pathBoxList.Count - 1)
                {
                    _pathCount = 1;
                    this.Speed = 0;
                    this.IsInMovement = false;
                }
            }

            while (!_pathFinder.FoundTarget)
            {
                _pathFinder.Update();
            }
            

            // BREEDING
            Breeding();

            if (this.BoxList.Count() != 0)
            {
                foreach (Box b in this.BoxList)
                {
                    foreach (MapElement vegetation in this._map.Vegetation)
                    {
                        if (vegetation.Area.IntersectsWith(this.Area))
                        {
                            {
                                if (vegetation.Texture == EmapElements.Rock
                                    || vegetation.Texture == EmapElements.Rock2
                                    || vegetation.Texture == EmapElements.Rock3)
                                {
                                    this.ChangePosition();
                                }
                            }
                        }
                    }

                    if( b.Area.IntersectsWith( this.FieldOfView ) && this.Thirst >= 60 && b.Ground == EBoxGround.Water )
                    {
                        this.ChangePosition( b.Area.Location );
                    }

                    if (b != null)
                    {
                        if (b.Ground == EBoxGround.Water && this.Texture != EAnimalTexture.Eagle)
                        {
                            this._isInWater = true;

                            for (int i = 0; i < this._map.Boxes.Length; i++)
                            {
                                if (this._map.Boxes[i].Area.IntersectsWith(this.FieldOfView)
                                    && this._map.Boxes[i].Ground != EBoxGround.Water)
                                {
                                    if (!this.WalkableBoxes.Contains(this._map.Boxes[i]))
                                    {
                                        this.WalkableBoxes.Add(this._map.Boxes[i]);
                                        this.ChangePosition();
                                    }
                                }

                                if (this.WalkableBoxes.Count != 0)
                                {
                                    for (int j = 0; j < this.WalkableBoxes.Count; j++)
                                    {
                                        if (!this.WalkableBoxes[j].Area.IntersectsWith(this.FieldOfView))
                                        {
                                            this.WalkableBoxes.Remove(this.WalkableBoxes[j]);
                                        }
                                    }
                                }
                            }

                            if (this.Thirst >= 15)
                            {
                                this._isDrinking = true;
                                this.Speed = 0;
                                this.Thirst -= 5;
                            }
                            else
                            {
                                this.Speed = this.DefaultSpeed;
                                if (this.WalkableBoxes.Count != 0)
                                {
                                    var r = new Random();
                                    this.ChangePosition(
                                        this.WalkableBoxes[r.Next(0, this.WalkableBoxes.Count)].Location);
                                }
                                else
                                {
                                    this.ChangePosition();
                                }
                            }
                        }
                        else
                        {
                            this._isInWater = false;
                        }
                    }
                }
            }
        }

        private void Breeding()
        {
            for (int i = 0; i < this._animalsAround.Count; i++)
            {
                if (this._animalsAround[i].Texture == this.Texture)
                {
                    if (this.ESex != this._animalsAround[i].ESex)
                    {
                        if (this.Hunger < 20 && this._animalsAround[i].Hunger < 20 && this.Health > 70
                            && this._animalsAround[i].Health > 70 && this.Size.Width >= this._finalSize.Width)
                        {
                            this.ChangePosition(this._animalsAround[i].Position);
                            if (this.Area.IntersectsWith(this._animalsAround[i].Area))
                            {
                                this.Hunger += 30;
                                this._animalsAround[i].Hunger += 30;
                                if (this.ESex == Enums.ESex.Female)
                                {
                                    this._map.ViewPort.CreateAnimal(this.Texture, this.Position, true, this);
                                }
                                else
                                {
                                    this._map.ViewPort.CreateAnimal(this.Texture, this.Position, true, _animalsAround[i]);
                                }

                            }
                        }
                    }
                }
            }
        }

        private void FindNewPath()
        {
            if (this.OverlappedBox != null)
            {
                Random r = new Random();
                Box targetBox = _map[r.Next(0, _map.BoxCountPerLine), r.Next(0, _map.BoxCountPerLine)];
                if (targetBox != null && targetBox.Ground != EBoxGround.Water && targetBox.Ground != EBoxGround.Mountain)
                {
                    _pathFinder = new PathFinder(this.OverlappedBox, targetBox, _map);
                }
                else
                {
                    FindNewPath();
                }
            }
            else
            {
                Random r = new Random();
                if (this.Mother != null)
                {
                    Box targetBox = _map[r.Next(0, _map.BoxCountPerLine), r.Next(0, _map.BoxCountPerLine)];
                    if (targetBox != null && targetBox.Ground != EBoxGround.Water && targetBox.Ground != EBoxGround.Mountain)
                    {
                        _pathFinder = new PathFinder(this.Mother.OverlappedBox, targetBox, _map);
                    }
                    else
                    {
                        FindNewPath();
                    }
                    
                }
                else
                {
                    _pathFinder = new PathFinder(_map[r.Next(0, _map.BoxCountPerLine), r.Next(0, _map.BoxCountPerLine)], _map[r.Next(0, _map.BoxCountPerLine), r.Next(0, _map.BoxCountPerLine)], _map);
                }
            }
        }

        /// <summary>
        ///     Change the position of the animal to a random position
        /// </summary>
        public void ChangePosition()
        {
            var r = new Random();
            var newTarget = new Point(r.Next(0, this._map.MapSize), r.Next(0, this._map.MapSize));
            Debug.Assert(newTarget.X <= this._map.MapSize && newTarget.Y <= this._map.MapSize);
            this.TargetLocation = newTarget;
            var distance =
                (float)(Math.Pow(this.Position.X - newTarget.X, 2) + Math.Pow(this.Position.Y - newTarget.Y, 2));
            var _dir = new SizeF((newTarget.X - this.Position.X) / distance, (newTarget.Y - this.Position.Y) / distance);
            this.Direction = _dir;
            this.IsInMovement = true;
        }

        /// <summary>
        /// Change position to another point defined in the argument
        /// </summary>
        /// <param name="target">
        /// The target location.
        /// </param>
        public void ChangePosition(Point target)
        {
            var r = new Random();
            Point newTarget = target;
            this.TargetLocation = newTarget;
            var distance =
                (float)(Math.Pow(this.Position.X - newTarget.X, 2) + Math.Pow(this.Position.Y - newTarget.Y, 2));
            var _dir = new SizeF((newTarget.X - this.Position.X) / distance, (newTarget.Y - this.Position.Y) / distance);
            this.Direction = _dir;
            this.IsInMovement = true;
        }

        /// <summary>
        ///     The check drowning.
        /// </summary>
        public void CheckDrowning()
        {
            var t = new Timer();
            t.Interval = 2000;
            t.Tick += this.T_drown_tick;
            t.Start();
        }

        /// <summary>
        ///     Make the animal die
        /// </summary>
        public void Die()
        {
            // Rectangle r = new Rectangle(this.RelativePosition, this.RelativeSize);
            this.IsDead = true;
            this._map.BloodList.Add(this.Area);
            this._map.DeadAnimals += 1;
            foreach (Box b in this._map.Boxes)
            {
                b.RemoveFromList(this);
            }

            this._map.Animals.Remove(this);
        }

        /// <summary>
        /// The distance between animal.
        /// </summary>
        /// <param name="a">
        /// The a.
        /// </param>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        public double DistanceBetweenAnimal(Animal a)
        {
            return Math.Pow(this.Area.X - a.Area.X, 2) + Math.Pow(this.Area.Y - a.Area.Y, 2);
        }

        /// <summary>
        /// Drawing method
        /// </summary>
        /// <param name="g">
        /// The g.
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
        /// <param name="texture">
        /// The texture.
        /// </param>
        public virtual void Draw(

            Graphics g, 
            Rectangle target, 
            Rectangle viewPort, 
            Rectangle targetMiniMap, 
            Rectangle viewPortMiniMap, 
            Texture texture)
        {

            this._graphics = g;

            this._textureGraphics = texture;
            var newWidth = (int)((this.Area.Width / (double)viewPort.Width) * target.Width + 1);
            var newHeight = (int)((this.Area.Height / (double)viewPort.Width) * target.Width + 1);
            int newXpos =
                (int)(this.Area.X / (this.Area.Width / ((this.Area.Width / (double)viewPort.Width) * target.Width)))
                - (int)(viewPort.X / (this.Area.Width / ((this.Area.Width / (double)viewPort.Width) * target.Width)));
            int newYpos =
                (int)(this.Area.Y / (this.Area.Width / ((this.Area.Width / (double)viewPort.Width) * target.Width)))
                - (int)(viewPort.Y / (this.Area.Width / ((this.Area.Width / (double)viewPort.Width) * target.Width)));

            this.RelativePosition = new Point(newXpos, newYpos);
            this.RelativeSize = new Size(newWidth, newHeight);



            if (this._map.ShowDebug)
            {
                foreach (Animal a in this.AnimalsAround)
                {
                    if (this.Texture != a.Texture)
                    {
                        g.DrawLine(new Pen(Brushes.Red, 4), this.RelativePosition, a.RelativePosition);
                        g.DrawString(
                            "Animals in field of view : " + this.AnimalsAround.Count, 
                            new Font("Arial", 15f), 
                            Brushes.White, 
                            this.RelativePosition);
                    }
                }

                this._map.ViewPort.DrawRectangleInViewPort(
                    g, 
                    this.FieldOfView, 
                    this._map.ViewPort.ScreenSize, 
                    this._map.ViewPort.ViewPort, 
                    this._map.ViewPort.MiniMap, 
                    this._map.ViewPort.MiniMapViewPort);

                g.DrawString(
                    "Health " + this.Health, 
                    new Font("Arial", 20f), 
                    Brushes.Black, 
                    new Point(this.RelativePosition.X, this.RelativePosition.Y - 20));
                g.DrawString(
                    "Hunger " + this.Hunger, 
                    new Font("Arial", 20f), 
                    Brushes.Black, 
                    new Point(this.RelativePosition.X, this.RelativePosition.Y - 40));
                g.DrawString(
                    "Thirst " + this.Thirst, 
                    new Font("Arial", 20f), 
                    Brushes.Black, 
                    new Point(this.RelativePosition.X + 200, this.RelativePosition.Y - 40));
                g.DrawString(
                    "ESex " + this.ESex, 
                    new Font("Arial", 20f), 
                    Brushes.Black, 
                    new Point(this.RelativePosition.X + 200, this.RelativePosition.Y + 200));

                g.DrawString(
                    "Target pos : " + this.TargetLocation.X + "\n" + this.TargetLocation.Y, 
                    new Font("Arial", 20f), 
                    Brushes.Black, 
                    this.RelativePosition);
                this._map.ViewPort.DrawRectangleInViewPort(
                    g, 
                    new Rectangle(this.TargetLocation, this.Area.Size), 
                    this._map.ViewPort.ScreenSize, 
                    this._map.ViewPort.ViewPort, 
                    this._map.ViewPort.MiniMap, 
                    this._map.ViewPort.MiniMapViewPort);
            }



            if (_map.IsMedic)
            {
                if (this.IsHurt)
                {
                    g.DrawRectangle(Pens.Blue, new Rectangle(this.RelativePosition, this.RelativeSize));
                    g.DrawImage(texture.GetBlood(), new Rectangle(this.RelativePosition, this.RelativeSize));
                }
            }

            if( this._map.ShowDebug )
            {
                if( this.PathFinder != null )
                {
                    if( this.PathFinder.FoundTarget )
                    {
                        foreach( Box b in this.PathFinder.FinalPath )
                        {
                            g.DrawRectangle( Pens.Red, new Rectangle( b.RelativePosition, b.RelativeSize ) );
                        }
                    }

                }
            }

            if (this.Area.IntersectsWith(viewPort))
            {
                g.DrawImage(
                    texture.LoadTexture(this), 
                    new Rectangle(newXpos + target.X, newYpos + target.Y, newWidth, newHeight));
            }


        }

        public void DrawMiniMap(Graphics g, 
            Rectangle target, 
            Rectangle viewPort, 
            Rectangle targetMiniMap, 
            Rectangle viewPortMiniMap, 
            Texture texture)
        {
            var newSizeMini = (int)((this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            var newHeightMini = (int)((this.Area.Height / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            int newXposMini =
                (int)
                (this.Area.X
                 / (this.Area.Width / ((this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)))
                - (int)
                  (viewPortMiniMap.X
                   / (this.Area.Width / ((this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));
            int newYposMini =
                (int)
                (this.Area.Y
                 / (this.Area.Width / ((this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)))
                - (int)
                  (viewPortMiniMap.Y
                   / (this.Area.Width / ((this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));
            if (_map.IsMedic)
            {
                if (this.IsHurt)
                {
                    g.FillRectangle(Brushes.Red, new Rectangle(newXposMini + targetMiniMap.X, newYposMini + targetMiniMap.Y, newSizeMini, newHeightMini));
                }
            }

            g.DrawRectangle(
    Pens.Black,
    new Rectangle(newXposMini + targetMiniMap.X, newYposMini + targetMiniMap.Y, newSizeMini, newHeightMini));

        }

        public void Update()
        {

            if (_growTimer == null)
            {
                if (_growSpeed <= 0)
                {
                    _growSpeed = 20000;
                }
                _growTimer = new Timer();
                _growTimer.Interval = _growSpeed;
                _growTimer.Tick += new EventHandler(T_Grow_Animal);
                _growTimer.Start();
            }

            if (this.Size.Width < this._finalSize.Width)
            {
                if (this.Mother != null)
                {
                    this.Direction = this.Mother.Direction;
                }
            }

            if (!this.IsHurt)
            {
                this.Position = new Point(
    this._position.X + (int)(this.Direction.Width * Speed),
    this._position.Y + (int)(this.Direction.Height * Speed));
            }

            this.Behavior();

            this.GetAnimalsAround();
        }

        /// <summary>
        ///     The get animals around.
        /// </summary>
        public void GetAnimalsAround()
        {
            for (int i = 0; i < this._map.Animals.Count; i++)
            {
                if (this.FieldOfView.IntersectsWith(this._map.Animals[i].FieldOfView) && this._map.Animals[i] != this)
                {
                    if (!this._animalsAround.Contains(this._map.Animals[i]))
                    {
                        this._animalsAround.Add(this._map.Animals[i]);
                    }
                }

                if (this._animalsAround.Contains(this._map.Animals[i])
                    && !this._map.Animals[i].FieldOfView.IntersectsWith(this.FieldOfView))
                {
                    this._animalsAround.Remove(this._map.Animals[i]);
                }

                for (int j = 0; j < this._animalsAround.Count(); j++)
                {
                    if (!this._map.Animals.Contains(this._animalsAround[j]))
                    {
                        this._animalsAround.Remove(this._animalsAround[j]);
                    }
                }
            }
        }

        /// <summary>
        ///     The hunger timer.
        /// </summary>
        public void HungerTimer()
        {
            var t = new Timer();
            t.Interval = 30000;
            t.Tick += this.T_hunger_tick;
            t.Start();
        }

        /// <summary>
        /// The hurt.
        /// </summary>
        /// <param name="HealthPoint">
        /// The health point.
        /// </param>
        /// <exception cref="ArgumentException">
        /// </exception>
        public void Hurt(int HealthPoint)
        {
            if (HealthPoint < 0)
            {
                throw new ArgumentException("HealthPoints must be negative");
            }

            this.Health = -HealthPoint;
        }

        /// <summary>
        /// The remove from list.
        /// </summary>
        /// <param name="b">
        /// The b.
        /// </param>
        public void RemoveFromList(Box b)
        {
            if (this.BoxList.Contains(b))
            {
                this.BoxList.Remove(b);
            }
        }

        /// <summary>
        ///     The thirst timer.
        /// </summary>
        public void ThirstTimer()
        {
            var t = new Timer();
            t.Interval = 20000;
            t.Tick += this.T_thirst_tick;
            t.Start();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The t_drown_tick.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void T_drown_tick(object sender, EventArgs e)
        {
            if (this._isInWater && this._isDrinking == false)
            {
                this.Hurt(10);
            }
        }

        /// <summary>
        /// The t_hunger_tick.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void T_hunger_tick(object sender, EventArgs e)
        {
            this.Hunger += 5;
        }

        /// <summary>
        /// The t_thirst_tick.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void T_thirst_tick(object sender, EventArgs e)
        {
            this.Thirst += 5;
        }

        #endregion
    }
}