// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Box.cs" company="">
//   
// </copyright>
// <summary>
//   The box.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LiveIT2._1.Terrain
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    using LiveIT2._1.Animals;
    using LiveIT2._1.Enums;
    using LiveIT2._1.Textures;
    using LiveIT2._1.Vegetation;

    /// <summary>
    ///     The box.
    /// </summary>
    [Serializable]
    public class Box
    {
        #region Fields

        /// <summary>
        ///     The _column.
        /// </summary>
        private readonly int _column;

        /// <summary>
        ///     The _line.
        /// </summary>
        private readonly int _line;

        /// <summary>
        ///     The _animal list.
        /// </summary>
        private List<Animal> _animalList;
        private List<EmapElements> _elements;

        /// <summary>
        ///     The _ground.
        /// </summary>
        private EBoxGround _ground;

        
        /// <summary>
        ///     The _map.
        /// </summary>
        private Map _map;

        /// <summary>
        ///     The _relative position.
        /// </summary>
        private Point _relativePosition;

        /// <summary>
        ///     The _relative size.
        /// </summary>
        private Size _relativeSize;

        /// <summary>
        ///     The _source.
        /// </summary>
        private Rectangle _source;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Box"/> class.
        /// </summary>
        /// <param name="line">
        /// The line.
        /// </param>
        /// <param name="column">
        /// The column.
        /// </param>
        /// <param name="map">
        /// The map.
        /// </param>
        public Box(int line, int column, Map map)
        {
            this._map = map;
            this._line = line;
            this._column = column;
            this._ground = EBoxGround.Grass;
            this._relativePosition = new Point(line, column);
            this._relativeSize = new Size(this._map.BoxSize, this._map.BoxSize);
            this._animalList = new List<Animal>();
            this._elements = new List<EmapElements>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the animal list.
        /// </summary>
        public List<Animal> AnimalList
        {
            get
            {
                return this._animalList;
            }

            set
            {
                this._animalList = value;
            }
        }

        /// <summary>
        ///     Gets the area.
        /// </summary>
        public Rectangle Area
        {
            get
            {
                return new Rectangle(this.Location, new Size(this._map.BoxSize, this._map.BoxSize));
            }
        }

        /// <summary>
        ///     Gets the bottom.
        /// </summary>
        public Box Bottom
        {
            get
            {
                return this._map[this._line, this._column + 1];
            }
        }

        /// <summary>
        ///     Gets the column.
        /// </summary>
        public int Column
        {
            get
            {
                return this._column;
            }
        }

        /// <summary>
        ///     Gets or sets the ground.
        /// </summary>
        public EBoxGround Ground
        {
            get
            {
                return this._ground;
            }

            set
            {
                this._ground = value;
            }
        }

        /// <summary>
        ///     Gets the left.
        /// </summary>
        public Box Left
        {
            get
            {
                return this._map[this._line - 1, this._column];
            }
        }

        /// <summary>
        ///     Gets the line.
        /// </summary>
        public int Line
        {
            get
            {
                return this._line;
            }
        }

        /// <summary>
        ///     Gets the location.
        /// </summary>
        public Point Location
        {
            get
            {
                return new Point(this._line * this._map.BoxSize, this._column * this._map.BoxSize);
            }
        }

        /// <summary>
        ///     Gets the position of the box in the viewport
        /// </summary>
        public Point RelativePosition
        {
            get
            {
                return this._relativePosition;
            }
        }

        /// <summary>
        ///     Gets the size of the box in pixels in the viewport
        /// </summary>
        public Size RelativeSize
        {
            get
            {
                return this._relativeSize;
            }
        }

        /// <summary>
        ///     Gets the right.
        /// </summary>
        public Box Right
        {
            get
            {
                return this._map[this._line + 1, this._column];
            }
        }

        /// <summary>
        ///     Gets or sets the source.
        /// </summary>
        public Rectangle Source
        {
            get
            {
                return this._source;
            }

            set
            {
                this._source = value;
            }
        }

        /// <summary>
        ///     Gets the top.
        /// </summary>
        public Box Top
        {
            get
            {
                return this._map[this._line, this._column - 1];
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add animal.
        /// </summary>
        /// <param name="a">
        /// The a.
        /// </param>
        public void AddAnimal(Animal a)
        {
            if (!this._animalList.Contains(a))
            {
                a.AddToList(this);
                this._animalList.Add(a);
            }
        }
        public void AddElement(EmapElements e)
        {
            if (!this._elements.Contains(e))
            {
               
                this._elements.Add(e);
            }
        }

        /// <summary>
        ///     The draw transition textures.
        /// </summary>
        public void DrawTransitionTextures()
        {
            if (this.Ground == EBoxGround.Water)
            {
                if (this.Top != null && this.Top.Ground != EBoxGround.Water)
                {
                    this.Top.Ground = EBoxGround.Dirt;
                }

                if (this.Left != null && this.Left.Ground != EBoxGround.Water)
                {
                    this.Left.Ground = EBoxGround.Dirt;
                }

                if (this.Right != null && this.Right.Ground != EBoxGround.Water)
                {
                    this.Right.Ground = EBoxGround.Dirt;
                }

                if (this.Bottom != null && this.Bottom.Ground != EBoxGround.Water)
                {
                    this.Bottom.Ground = EBoxGround.Dirt;
                }
            }
        }

        /// <summary>
        /// The load map.
        /// </summary>
        /// <param name="map">
        /// The map.
        /// </param>
        public void LoadMap(Map map)
        {
            this._map = map;
        }

        /// <summary>
        /// The remove from list.
        /// </summary>
        /// <param name="a">
        /// The a.
        /// </param>
        public void RemoveFromList(Animal a)
        {
            if (this._animalList.Contains(a))
            {
                a.RemoveFromList(this);
                this._animalList.Remove(a);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Draw the box in the targeted Rectangle
        /// </summary>
        /// <param name="g">
        /// </param>
        /// <param name="target">
        /// Rectangle in pixel in the Graphics.
        /// </param>
        /// <param name="textures">
        /// Texture object to apply the texture on the box
        /// </param>
        /// <param name="viewPort">
        /// The view Port.
        /// </param>
        internal void Draw(Graphics g, Rectangle target, Texture textures, Rectangle viewPort)
        {
            var newSize = (int)((this.Source.Width / (double)viewPort.Width) * target.Width + 2);
            int newXpos =
                (int)(this.Area.X / (this._map.BoxSize / ((this.Source.Width / (double)viewPort.Width) * target.Width)))
                - (int)
                  (viewPort.X / (this._map.BoxSize / ((this.Source.Width / (double)viewPort.Width) * target.Width)));
            int newYpos =
                (int)(this.Area.Y / (this._map.BoxSize / ((this.Source.Width / (double)viewPort.Width) * target.Width)))
                - (int)
                  (viewPort.Y / (this._map.BoxSize / ((this.Source.Width / (double)viewPort.Width) * target.Width)));
            this._relativePosition.X = newXpos;
            this._relativePosition.Y = newYpos;
            this._relativeSize.Height = newSize;
            this._relativeSize.Width = newSize;

            if (viewPort.Width < 5000)
            {
                g.DrawImage(textures.GetTexture(this), new Rectangle(newXpos, newYpos, newSize, newSize));
            }
            else
            {
                g.FillRectangle(textures.GetColor(this), new Rectangle(newXpos, newYpos, newSize, newSize));
            }

            for (int i = 0; i < this._animalList.Count; i++)
            {
                if (!this._animalList[i].Area.IntersectsWith(this.Area))
                {
                    this.RemoveFromList(this._animalList[i]);
                }
            }

            this.DrawTransitionTextures();
        }

        /// <summary>
        /// The draw mini map.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <param name="textures">
        /// The textures.
        /// </param>
        /// <param name="viewPort">
        /// The view port.
        /// </param>
        internal void DrawMiniMap(Graphics g, Rectangle target, Texture textures, Rectangle viewPort)
        {
            var newSize = (int)((this.Source.Width / (double)viewPort.Width) * target.Width);
            int newXpos =
                (int)(this.Area.X / (this._map.BoxSize / ((this.Source.Width / (double)viewPort.Width) * target.Width)))
                - (int)
                  (viewPort.X / (this._map.BoxSize / ((this.Source.Width / (double)viewPort.Width) * target.Width)));
            int newYpos =
                (int)(this.Area.Y / (this._map.BoxSize / ((this.Source.Width / (double)viewPort.Width) * target.Width)))
                - (int)
                  (viewPort.Y / (this._map.BoxSize / ((this.Source.Width / (double)viewPort.Width) * target.Width)));

            g.FillRectangle(
                textures.GetColor(this), 
                new Rectangle(newXpos + target.X, newYpos + target.Y, newSize, newSize));
        }

        #endregion

    }
}