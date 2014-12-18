// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Player.cs" company="">
//   
// </copyright>
// <summary>
//   The player.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LiveIT2._1.Player
{
    using System;
using System.Collections.Generic;
using System.Drawing;
using LiveIT2._1.Enums;
using LiveIT2._1.Terrain;
using LiveIT2._1.Textures;
using LiveIT2._1.Vegetation;
using LiveIT2._1.Vehicules;

    /// <summary>
    ///     The player.
    /// </summary>
    [Serializable]
    public class Player
    {
        #region Fields

        /// <summary>
        ///     The _direction.
        /// </summary>
        private SizeF _direction;

        bool leftCollide, rightCollide, upCollide, downCollide;

        /// <summary>
        ///     The _map.
        /// </summary>
        private Map _map;

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

        private bool _isInHouse;

        /// <summary>
        ///     The _size.
        /// </summary>
        private Size _size;
        private  int _acceleration;

        List<EBoxGround> _collisionTextures;
        List<EmapElements> _collisionElements;


        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="map">
        /// The map.
        /// </param>
        /// <param name="startPosition">
        /// The start position.
        /// </param>
        public Player(Map map, Point startPosition)
        {
            this._map = map;
            this._position = startPosition;
            this._size = new Size(100, 210);
            this.Texture = EPlayerTexture.MainPlayer;
            this.Speed = 0;
            MaxSpeed = 30;
            this.Acceleration = 10;
            _collisionTextures = new List<EBoxGround>() { EBoxGround.Wall, EBoxGround.Water };
            _collisionElements = new List<EmapElements>() {EmapElements.Table, EmapElements.Barrel, EmapElements.Chair, EmapElements.Shelf, EmapElements.Chair };
        }

        #endregion

        #region Public Properties

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

        public bool IsInHouse
        {
            get { return _isInHouse; }
            set { _isInHouse = value; }
        }

        public int Acceleration
        {
            get
            {
                return _acceleration;
            }
            set
            {
                if( value < 1 )
                {
                    _acceleration = 1;
                }
                else
                {
                    _acceleration = value;
                }
            }
        }

        /// <summary>
        ///     Gets the area bottom.
        /// </summary>
        public Rectangle AreaBottom
        {
            get
            {
                return new Rectangle(
                    new Point(this._position.X, this._position.Y + (this._size.Height / 3)), 
                    new Size(this._size.Width, this._size.Height / 3));
            }
        }

        /// <summary>
        ///     Gets or sets the box list.
        /// </summary>
        public List<Box> BoxList { get; set; }

        /// <summary>
        ///     Gets or sets the car.
        /// </summary>
        public Car Car { get; set; }

        /// <summary>
        ///     Gets the direction.
        /// </summary>
        public SizeF Direction
        {
            get
            {
                return this._direction;
            }

            internal set
            {
                this._direction = value;
            }
        }

        /// <summary>
        ///     Gets or sets the e moving direction.
        /// </summary>
        public EMovingDirection EMovingDirection { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether is moving.
        /// </summary>
        public bool IsMoving { get; set; }

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
        ///     Gets the size.
        /// </summary>
        public Size Size
        {
            get
            {
                return this._size;
            }

            internal set
            {
                this._size = value;
            }
        }

        public bool LeftCollide
        {
            get { return leftCollide; }
        }
        public bool RightCollide
        {
            get { return rightCollide; }
        }
        public bool UpCollide
        {
            get { return upCollide; }
        }
        public bool DownCollide
        {
            get { return downCollide; }
        }

        /// <summary>
        ///     Gets or sets the speed.
        /// </summary>
        public int Speed { get; set; }


        public int MaxSpeed { get; set; }

        /// <summary>
        ///     Gets the texture.
        /// </summary>
        public EPlayerTexture Texture { get; internal set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The draw.
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

            this.Position = new Point(
                this._position.X + (int)(this.Direction.Width * this.Speed), 
                this._position.Y + (int)(this.Direction.Height * this.Speed));

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
            if (this.Area.IntersectsWith(viewPort))
            {
                g.DrawImage(
                    texture.LoadTexture(this), 
                    new Rectangle(newXpos + target.X, newYpos + target.Y, newWidth, newHeight));
            }
            rightCollide = false;
            upCollide = false;
            downCollide = false;
            leftCollide = false;

            foreach( Box b in BoxList )
            {
                if( b.Left != null )
                {
                    if( _collisionTextures.Contains(b.Left.Ground) )
                    {
                        leftCollide = true;
                    }
                }
                if( b.Right != null )
                {
                    if( _collisionTextures.Contains(b.Right.Ground) )
                    {
                        rightCollide = true;
                    }
                }
                if( b.Top != null )
                {
                    if(_collisionTextures.Contains( b.Top.Ground) )
                    {
                        upCollide = true;
                    }
                }
                if( b.Bottom != null )
                {
                    if( _collisionTextures.Contains(b.Bottom.Ground) )
                    {
                        downCollide = true;
                    }
                }
                if( _map.ShowDebug )
                {
                    _map.ViewPort.DrawRectangleInViewPort( g, b.Area, _map.ViewPort.ScreenSize, _map.ViewPort.ViewPort, _map.ViewPort.MiniMapViewPort, _map.ViewPort.MiniMap );
                }
            }
            g.DrawRectangle(
                Pens.Black, 
                new Rectangle(newXposMini + targetMiniMap.X, newYposMini + targetMiniMap.Y, newSizeMini, newHeightMini));
        }

        /// <summary>
        ///     The not alloud to move.
        /// </summary>
        public void notAlloudToMove()
        {
            this.Position = new Point(this.Area.X - 100, this.Area.Y);
        }

        #endregion
    }
}