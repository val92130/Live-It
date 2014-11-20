using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveIT2._1
{
    public class Animal
    {
         Point _position;
         Size _size;
         Map _map;
         Point _direction;
         AnimalTexture _texture;
        public Animal(Map map, Point position, Size size, AnimalTexture texture)
        {
            _map = map;
            _position = position;
            _size = size;
            _texture = texture;
            _direction = new Point( 0, 0 );
        }
        public Animal( Map map, Point position )
        {
            _map = map;
            _position = position;
        }
        public Animal()
        {

        }
         
        public Point Position
        {
            get { return _position; }
            set { _position = value; }
        }
         
        public virtual Size Size
        {
            get { return _size; }
            set { _size = value; }
        }

        public Rectangle Area
        {
            get { return  new Rectangle(_position, _size); }
        }

        public Point Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        public AnimalTexture Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        internal Map Park
        {
            get { return _map; }
            set { _map = value; }
        }

        public void Draw( Graphics g, Rectangle target, Rectangle viewPort, Rectangle targetMiniMap, Rectangle viewPortMiniMap, Texture texture )
        {
            Random r = new Random();
            _position.X += this.Direction.X;
            _position.Y += this.Direction.Y;

            int newSize = (int)(((double)this.Area.Width / (double)viewPort.Width) * target.Width + 1);
            int newHeight = (int)(((double)this.Area.Height / (double)viewPort.Width) * target.Width + 1);
            int newXpos = (int)(this.Area.X / (this.Area.Width / (((double)this.Area.Width / (double)viewPort.Width) * target.Width))) - (int)(viewPort.X / (this.Area.Width / (((double)this.Area.Width / (double)viewPort.Width) * target.Width)));
            int newYpos = (int)(this.Area.Y / (this.Area.Width / (((double)this.Area.Width / (double)viewPort.Width) * target.Width))) - (int)(viewPort.Y / (this.Area.Width / (((double)this.Area.Width / (double)viewPort.Width) * target.Width)));

            int newSizeMini = (int)(((double)this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            int newHeightMini = (int)(((double)this.Area.Height / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            int newXposMini = (int)(this.Area.X / (this.Area.Width / (((double)this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width))) - (int)(viewPortMiniMap.X / (this.Area.Width / (((double)this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));
            int newYposMini = (int)(this.Area.Y / (this.Area.Width / (((double)this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width))) - (int)(viewPortMiniMap.Y / (this.Area.Width / (((double)this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));


            g.DrawImage( texture.LoadTexture(this), new Rectangle( newXpos + target.X, newYpos + target.Y, newSize, newHeight ) );
            g.DrawImage( texture.LoadTexture( this ), new Rectangle( newXposMini + targetMiniMap.X, newYposMini + targetMiniMap.Y, newSizeMini, newHeightMini ) );
        }

    }
}
