// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Vegetation.cs" company="">
//   
// </copyright>
// <summary>
//   The vegetation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LiveIT2._1.Vegetation
{
    using System;
    using System.Drawing;

    using LiveIT2._1.Enums;
    using LiveIT2._1.Terrain;
    using LiveIT2._1.Textures;

    /// <summary>
    ///     The vegetation.
    /// </summary>
    [Serializable]
    public class MapElement
    {
        #region Fields

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

        /// <summary>
        ///     The _size.
        /// </summary>
        private Size _size;

        /// <summary>
        ///     The _texture.
        /// </summary>
        private EmapElements _texture;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MapElement"/> class.
        /// </summary>
        /// <param name="map">
        /// The map.
        /// </param>
        /// <param name="StartPosition">
        /// The start position.
        /// </param>
        public MapElement(Map map, Point StartPosition)
        {
            this._map = map;
            this._position = StartPosition;
            this._texture = EmapElements.Tree;
            this._size = new Size(400, 400);
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
                return new Rectangle(this.Position, this.Size);
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
        ///     Gets or sets the size.
        /// </summary>
        public Size Size
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
        ///     Gets or sets the texture.
        /// </summary>
        public EmapElements Texture
        {
            get
            {
                return this._texture;
            }

            set
            {
                this._texture = value;
            }
        }

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
                    texture.GetTexture(this), 
                    new Rectangle(newXpos + target.X, newYpos + target.Y, newWidth, newHeight));
            }

            g.DrawRectangle(
                Pens.Brown, 
                new Rectangle(newXposMini + targetMiniMap.X, newYposMini + targetMiniMap.Y, newSizeMini, newHeightMini));
        }

        #endregion
    }
}