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
         BoxGround _favoriteEnvironnment;
         AnimalTexture _texture;
        Rectangle _fieldOfViewRect;
        int _viewDistance;
        int _speed;
        int _defaultSpeed;
        public Animal(Map map, Point position, Size size, AnimalTexture texture)
        {
            _map = map;
            _position = position;
            _size = size;
            _texture = texture;
            _direction = new Point( 0, 0 );
            _favoriteEnvironnment = BoxGround.Grass;
            _viewDistance = 1000;
            _speed = 4;
            
        }
        public Animal( Map map, Point position )
        {
            _map = map;
            _position = position;
        }
        public Animal()
        {

        }

        public Rectangle FieldOfView
        {
            get { return new Rectangle(this.Position.X + (this.Area.Width / 2), this.Position.Y + (this.Area.Height / 2), _viewDistance, _viewDistance); }
            set { _fieldOfViewRect = value; }
        }

        public int ViewDistance
        {
            get { return _viewDistance; }
            set { _viewDistance = value; }
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

        public int Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public int DefaultSpeed
        {
            get { return _defaultSpeed; }
            set { _defaultSpeed = value; }
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

        public BoxGround FavoriteEnvironnment
        {
            get { return _favoriteEnvironnment; }
            set { _favoriteEnvironnment = value; }
        }

        public virtual void Draw( Graphics g, Rectangle target, Rectangle viewPort, Rectangle targetMiniMap, Rectangle viewPortMiniMap, Texture texture )
        {
            Random r = new Random();
            
            for (int i = 0; i < _map.Boxes.Length; i++)
            {
                if (this.Area.IntersectsWith(_map.Boxes[i].Area))
                {
                    _map.Boxes[i].AddAnimal(this);
                    if (_map.Boxes[i].Ground == this.FavoriteEnvironnment)
                    {
                        this.Speed = r.Next(1, 3);
                    }
                    else
                    {
                        this.Speed = this.DefaultSpeed;
                    }
                }
            }
            if (this.FieldOfView.X >= _map.MapSize)
            {
                this.Speed = -Speed;
            }

            this.Direction = new Point(this.Speed, 0);
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

            g.DrawRectangle(Pens.Red, this.FieldOfView);
            g.DrawImage( texture.LoadTexture(this), new Rectangle( newXpos + target.X, newYpos + target.Y, newSize, newHeight ) );
            g.DrawImage( texture.LoadTexture( this ), new Rectangle( newXposMini + targetMiniMap.X, newYposMini + targetMiniMap.Y, newSizeMini, newHeightMini ) );
        }

    }
}
