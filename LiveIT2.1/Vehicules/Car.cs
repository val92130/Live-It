// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Car.cs" company="">
//   
// </copyright>
// <summary>
//   The car.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LiveIT2._1.Vehicules
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    using LiveIT2._1.Enums;

    /// <summary>
    /// The car.
    /// </summary>
    public class Car : Vehicule
    {
        #region Fields

        /// <summary>
        /// The _map.
        /// </summary>
        private readonly Map _map;

        /// <summary>
        /// The box list.
        /// </summary>
        private List<Box> BoxList;

        /// <summary>
        /// The _direction.
        /// </summary>
        private SizeF _direction;

        /// <summary>
        /// The _is radio playing.
        /// </summary>
        private bool _isRadioPlaying;

        /// <summary>
        /// The _position.
        /// </summary>
        private Point _position;

        /// <summary>
        /// The _relative position.
        /// </summary>
        private Point _relativePosition;

        /// <summary>
        /// The _relative size.
        /// </summary>
        private Size _relativeSize;

        /// <summary>
        /// The _size.
        /// </summary>
        private Size _size;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Car"/> class.
        /// </summary>
        /// <param name="map">
        /// The map.
        /// </param>
        /// <param name="startPosition">
        /// The start position.
        /// </param>
        public Car(Map map, Point startPosition)
        {
            this._map = map;
            this._position = startPosition;
            this._size = new Size(400, 400);
            this.Texture = ECarTexture.MainPlayerCar;

            Array values = Enum.GetValues(typeof(ERadioSongs));
            var random = new Random();
            var randomRadio = (ERadioSongs)values.GetValue(random.Next(values.Length));
            this.ERadioSong = randomRadio;
            this.BoxList = new List<Box>();
            this.Speed = 200;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the area.
        /// </summary>
        public Rectangle Area
        {
            get
            {
                return new Rectangle(this._position, this._size);
            }
        }

        /// <summary>
        /// Gets the direction.
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
        /// Gets or sets a value indicating whether is moving.
        /// </summary>
        public bool IsMoving { get; set; }

        /// <summary>
        /// Gets a value indicating whether is radio playing.
        /// </summary>
        public bool IsRadioPlaying
        {
            get
            {
                return this._isRadioPlaying;
            }
        }

        /// <summary>
        /// Gets or sets the moving direction.
        /// </summary>
        public EMovingDirection EMovingDirection { get; set; }

        /// <summary>
        /// Gets or sets the position.
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
        /// Gets the radio song.
        /// </summary>
        public ERadioSongs ERadioSong { get; internal set; }

        /// <summary>
        /// Gets or sets the relative position.
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
        /// Gets or sets the relative size.
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
        /// Gets the size.
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

        /// <summary>
        /// Gets or sets the speed.
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// Gets the texture.
        /// </summary>
        public ECarTexture Texture { get; internal set; }

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
            if (this.IsMoving)
            {
                for (int i = 0; i < this.BoxList.Count; i++)
                {
                    for (int j = 0; j < this._map.Animals.Count; j++)
                    {
                        if (this._map.Animals[j].Area.IntersectsWith(this.Area)
                            && this._map.Animals[j].Texture != EAnimalTexture.Eagle)
                        {
                            this._map.Animals[j].Die();
                        }
                    }
                }
            }

            this.BoxList = this._map.GetOverlappedBoxes(this.Area);

            if (this._map.ShowDebug)
            {
                foreach (Box b in this.BoxList)
                {
                    g.DrawRectangle(Pens.Red, new Rectangle(b.RelativePosition, b.RelativeSize));
                }
            }

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

            g.DrawRectangle(
                Pens.Black, 
                new Rectangle(newXposMini + targetMiniMap.X, newYposMini + targetMiniMap.Y, newSizeMini, newHeightMini));
        }

        /// <summary>
        /// The toggle radio.
        /// </summary>
        public void ToggleRadio()
        {
            if (this._isRadioPlaying)
            {
                this._isRadioPlaying = false;
            }
            else
            {
                this._isRadioPlaying = true;
            }
        }

        #endregion
    }
}