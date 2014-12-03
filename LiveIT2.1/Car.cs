using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveIT2._1
{
    public class Car
    {
        Map _map;
        Point _position, _relativePosition;
        Size _size, _relativeSize;
        CarTexture _texture;
        SizeF _direction;
        private  int _speed;
        MovingDirection _movingDirection;
        

        public Car( Map map, Point startPosition )
        {
            _map = map;
            _position = startPosition;
            _size = new Size( 200, 300 );
            _texture = CarTexture.MainPlayerCar;
        }

        public Rectangle Area
        {
            get { return new Rectangle( _position, _size ); }
        }

        public Point Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Size Size
        {
            get { return _size; }
            internal set { _size = value; }
        }

        public Point RelativePosition
        {
            get { return _relativePosition; }
            set { _relativePosition = value; }
        }

        public Size RelativeSize
        {
            get { return _relativeSize; }
            set { _relativeSize = value; }
        }

        public CarTexture Texture
        {
            get { return _texture; }
            internal set { _texture = value; }
        }

        public SizeF Direction
        {
            get { return _direction; }
            internal set { _direction = value; }
        }
        public MovingDirection MovingDirection
        {
            get { return _movingDirection; }
            set { _movingDirection = value; }
        }
        public int Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public virtual void Draw( Graphics g, Rectangle target, Rectangle viewPort, Rectangle targetMiniMap, Rectangle viewPortMiniMap, Texture texture )
        {

            int newWidth = (int)(((double)this.Area.Width / (double)viewPort.Width) * target.Width + 1);
            int newHeight = (int)(((double)this.Area.Height / (double)viewPort.Width) * target.Width + 1);
            int newXpos = (int)(this.Area.X / (this.Area.Width / (((double)this.Area.Width / (double)viewPort.Width) * target.Width))) - (int)(viewPort.X / (this.Area.Width / (((double)this.Area.Width / (double)viewPort.Width) * target.Width)));
            int newYpos = (int)(this.Area.Y / (this.Area.Width / (((double)this.Area.Width / (double)viewPort.Width) * target.Width))) - (int)(viewPort.Y / (this.Area.Width / (((double)this.Area.Width / (double)viewPort.Width) * target.Width)));

            this.RelativePosition = new Point( newXpos, newYpos );
            this.RelativeSize = new Size( newWidth, newHeight );



            Position = new Point( _position.X + (int)(this.Direction.Width * this.Speed),
                                  _position.Y + (int)(this.Direction.Height * this.Speed) );
 
            int newSizeMini = (int)(((double)this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            int newHeightMini = (int)(((double)this.Area.Height / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            int newXposMini = (int)(this.Area.X / (this.Area.Width / (((double)this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width))) - (int)(viewPortMiniMap.X / (this.Area.Width / (((double)this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));
            int newYposMini = (int)(this.Area.Y / (this.Area.Width / (((double)this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width))) - (int)(viewPortMiniMap.Y / (this.Area.Width / (((double)this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));
            if( this.Area.IntersectsWith( viewPort ) )
            {
                g.DrawImage( texture.LoadTexture( this ), new Rectangle( newXpos + target.X, newYpos + target.Y, newWidth, newHeight ) );
            }
            g.DrawRectangle( Pens.Black, new Rectangle( newXposMini + targetMiniMap.X, newYposMini + targetMiniMap.Y, newSizeMini, newHeightMini ) );
        }
    }
}
