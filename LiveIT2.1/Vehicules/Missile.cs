// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Missile.cs" company="">
//   
// </copyright>
// <summary>
//   The missile.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LiveIT2._1.Vehicules
{
    using System;
    using System.Drawing;

    using LiveIT2._1.Terrain;
    using LiveIT2._1.Textures;

    /// <summary>
    /// The tank.
    /// </summary>
    public partial class Tank
    {
        /// <summary>
        ///     The missile.
        /// </summary>
        public class Missile
        {
            #region Fields

            /// <summary>
            ///     The _map.
            /// </summary>
            private readonly Map _map;

            /// <summary>
            ///     The _area.
            /// </summary>
            private Rectangle _area;

            /// <summary>
            ///     The _direction.
            /// </summary>
            private SizeF _direction;

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
            ///     The _size.
            /// </summary>
            private Size _size;

            /// <summary>
            ///     The _target position.
            /// </summary>
            private Point _targetPosition;

            #endregion

            #region Constructors and Destructors

            /// <summary>
            /// Initializes a new instance of the <see cref="Missile"/> class.
            /// </summary>
            /// <param name="StartPosition">
            /// The start position.
            /// </param>
            /// <param name="map">
            /// The map.
            /// </param>
            public Missile(Point StartPosition, Map map)
            {
                this._position = StartPosition;
                this._size = new Size(80, 80);
                this.Speed = 90000;
                this._map = map;
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

                internal set
                {
                    this._area = value;
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

            /// <summary>
            ///     Gets or sets the speed.
            /// </summary>
            public int Speed { get; set; }

            /// <summary>
            ///     Gets or sets the target position.
            /// </summary>
            public Point TargetPosition
            {
                get
                {
                    return this._targetPosition;
                }

                set
                {
                    this._targetPosition = value;
                }
            }

            #endregion

            #region Public Methods and Operators

            /// <summary>
            /// The change position.
            /// </summary>
            /// <param name="target">
            /// The target.
            /// </param>
            public void ChangePosition(Point target)
            {
                var r = new Random();
                Point newTarget = target;
                this.TargetPosition = newTarget;
                var distance =
                    (float)(Math.Pow(this.Position.X - newTarget.X, 2) + Math.Pow(this.Position.Y - newTarget.Y, 2));
                var _dir = new SizeF(
                    (newTarget.X - this.Position.X) / distance, 
                    (newTarget.Y - this.Position.Y) / distance);
                this.Direction = _dir;
            }

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
            public void Draw(
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
                    - (int)
                      (viewPort.X / (this.Area.Width / ((this.Area.Width / (double)viewPort.Width) * target.Width)));
                int newYpos =
                    (int)(this.Area.Y / (this.Area.Width / ((this.Area.Width / (double)viewPort.Width) * target.Width)))
                    - (int)
                      (viewPort.Y / (this.Area.Width / ((this.Area.Width / (double)viewPort.Width) * target.Width)));

                this.RelativePosition = new Point(newXpos, newYpos);
                this.RelativeSize = new Size(newWidth, newHeight);

                this.Position = new Point(
                    this._position.X + (int)(this.Direction.Width * this.Speed), 
                    this._position.Y + (int)(this.Direction.Height * this.Speed));

                g.FillRectangle(
                    Brushes.Black, 
                    new Rectangle(newXpos + target.X, newYpos + target.Y, newWidth, newHeight));

                for (int i = 0; i < this._map.Animals.Count; i++)
                {
                    if (this._map.Animals[i].Area.IntersectsWith(this.Area))
                    {
                        this._map.Animals[i].Die();
                    }
                }
            }

            /// <summary>
            /// The shoot.
            /// </summary>
            /// <param name="Target">
            /// The target.
            /// </param>
            public void Shoot(Point Target)
            {
                this.ChangePosition(Target);
            }

            #endregion
        }
    }
}