using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveIT2._1
{
    [Serializable]
    public class Box
    {
         Map _map;
        int _line;
        int _column;
        BoxGround _ground;
        Rectangle _source;
        Point _relativePosition;
        Size _relativeSize;
        List<Animal> _animalList;

        public Box( int line, int column, Map map )
        {
            _map = map;
            _line = line;
            _column = column;
            _ground = BoxGround.Grass;
            _relativePosition = new Point(line, column);
            _relativeSize = new Size(_map.BoxSize, _map.BoxSize);
            _animalList = new List<Animal>();
        }

        public Point Location
        {
            get { return new Point(_line*_map.BoxSize, _column*_map.BoxSize); }
        }

        public Rectangle Area
        {
            get { return new Rectangle( Location, new Size( _map.BoxSize, _map.BoxSize )); }
        }

        public BoxGround Ground
        {
            get { return _ground; }
            set { _ground = value; }
        }
        public Box Top
        {
            get { return _map[_line, _column - 1]; }
        }

        public Box Bottom
        {
            get { return _map[_line, _column + 1]; }
        }

        public Box Left
        {
            get { return _map[_line - 1, _column]; }
        }

        public Box Right
        {
            get { return _map[_line + 1, _column]; }
        }


        public int Line
        {
            get { return _line; }
          
        }
        public int Column
        {
            get { return _column; }
           
        }

        public Rectangle Source
        {
            get { return _source; }
            set { _source = value; }
        }

        public void LoadMap(Map map)
        {
            _map = map;
        }

        public void AddAnimal(Animal a)
        {
            if (!_animalList.Contains(a))
            {
                _animalList.Add(a);
            }
        }

        public void RemoveFromList(Animal a)
        {
            if (_animalList.Contains(a))
            {
                _animalList.Remove(a);
            }
        }

        public void DrawTransitionTextures()
        {
            if (this.Ground == BoxGround.Water)
            {

                if (this.Top != null && this.Top.Ground != BoxGround.Water )
                {
                    this.Top.Ground = BoxGround.Dirt;
                }

                if (this.Left != null && this.Left.Ground != BoxGround.Water)
                {
                    this.Left.Ground = BoxGround.Dirt;
                }
                if (this.Right != null && this.Right.Ground != BoxGround.Water)
                {
                    this.Right.Ground = BoxGround.Dirt;
                }
                if (this.Bottom != null && this.Bottom.Ground != BoxGround.Water)
                {
                    this.Bottom.Ground = BoxGround.Dirt;
                }                
            }
        }

        /// <summary>
        /// Gets the position of the box in the viewport
        /// </summary>
        public Point RelativePosition
        {
            get { return _relativePosition; }
        }

        /// <summary>
        /// Gets the size of the box in pixels in the viewport
        /// </summary>
        public Size RelativeSize
        {
            get { return _relativeSize; }
        }


        /// <summary>
        /// Draw the box in the targeted Rectangle
        /// </summary>
        /// <param name="g"></param>
        /// <param name="target">Rectangle in pixel in the Graphics.</param>
        /// <param name="textures">Texture object to apply the texture on the box </param>
        internal void Draw( Graphics g, Rectangle target, Texture textures, Rectangle viewPort)
        {        
            int newSize = (int)(((double)this.Source.Width / (double)viewPort.Width) * target.Width + 1);
            int newXpos = (int)(this.Area.X / (_map.BoxSize / (((double)this.Source.Width / (double)viewPort.Width) * target.Width))) - (int)(viewPort.X / (_map.BoxSize / (((double)this.Source.Width / (double)viewPort.Width) * target.Width)));
            int newYpos = (int)(this.Area.Y / (_map.BoxSize / (((double)this.Source.Width / (double)viewPort.Width) * target.Width))) - (int)(viewPort.Y / (_map.BoxSize / (((double)this.Source.Width / (double)viewPort.Width) * target.Width)));
            _relativePosition.X = newXpos;
            _relativePosition.Y = newYpos;
            _relativeSize.Height = newSize;
            _relativeSize.Width = newSize;

            if (viewPort.Width < 5000)
            {
                g.DrawImage(textures.LoadTexture(this), new Rectangle(newXpos, newYpos, newSize, newSize));
            }
            else
            {
                g.FillRectangle(textures.LoadColor(this), new Rectangle(newXpos, newYpos, newSize, newSize));               
            }


            Task CheckAnimalList = new Task(() =>
            {
                for (int i = 0; i < _animalList.Count; i++)
                {
                    if (_animalList[i].Area.IntersectsWith(this.Area))
                    {
                        RemoveFromList(_animalList[i]);
                    }
                }
            });
            CheckAnimalList.Start();
            DrawTransitionTextures();
        }
        internal void DrawMiniMap( Graphics g, Rectangle target, Texture textures, Rectangle viewPort )
        {
            int newSize = (int)(((double)this.Source.Width / (double)viewPort.Width) * target.Width);
            int newXpos = (int)(this.Area.X / (_map.BoxSize / (((double)this.Source.Width / (double)viewPort.Width) * target.Width))) - (int)(viewPort.X / (_map.BoxSize / (((double)this.Source.Width / (double)viewPort.Width) * target.Width)));
            int newYpos = (int)(this.Area.Y / (_map.BoxSize / (((double)this.Source.Width / (double)viewPort.Width) * target.Width))) - (int)(viewPort.Y / (_map.BoxSize / (((double)this.Source.Width / (double)viewPort.Width) * target.Width)));

            g.FillRectangle( textures.LoadColor( this ), new Rectangle( newXpos + target.X, newYpos + target.Y, newSize, newSize ) );
        }
        
    }
}
