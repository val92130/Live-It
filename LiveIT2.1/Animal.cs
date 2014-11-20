using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
        Point _relativePosition;
        List<Animal> _animalsAround =  new List<Animal>();
        public Animal(Map map, Point position, Size size, AnimalTexture texture)
        {
            _map = map;
            _position = position;
            _size = size;
            _texture = texture;
            _direction = new Point( 0, 0 );
            _favoriteEnvironnment = BoxGround.Grass;
            _viewDistance = 1000;
            
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
            get { return new Rectangle(this.Position.X - (_viewDistance / 2), this.Position.Y - (_viewDistance / 2), _viewDistance * 2, _viewDistance * 2); }
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

        public double DistanceBetweenAnimal(Animal a)
        {
            return (Math.Pow(this.Area.X - a.Area.X, 2) + Math.Pow(this.Area.Y - a.Area.Y, 2));
        }

        public void GetAnimalsAround()
        {
            for (int i = 0; i < _map.Animals.Count; i++)
            {
                if (this.FieldOfView.IntersectsWith(_map.Animals[i].FieldOfView) && _map.Animals[i] != this)
                {
                    if (!_animalsAround.Contains(_map.Animals[i]))
                    {
                        _animalsAround.Add(_map.Animals[i]);
                    }
                    
                }
                else
                {
                    if (_animalsAround.Contains(_map.Animals[i]) && !_map.Animals[i].FieldOfView.IntersectsWith(this.FieldOfView))
                    {
                        _animalsAround.Remove(_map.Animals[i]);
                    }
                }
            }
        }

        public List<Animal> AnimalsAround
        {
            get { return _animalsAround; }
        }

        public Point RelativePosition
        {
            get { return _relativePosition; }
            set { _relativePosition = value; }
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

            Task CheckIntersect = new Task(() =>
            {
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
            });
            //CheckIntersect.Start();

            this.Direction = new Point(this.Speed, 0);
            _position.X += this.Direction.X;
            _position.Y += this.Direction.Y;


            int newSize = (int)(((double)this.Area.Width / (double)viewPort.Width) * target.Width + 1);
            int newHeight = (int)(((double)this.Area.Height / (double)viewPort.Width) * target.Width + 1);
            int newXpos = (int)(this.Area.X / (this.Area.Width / (((double)this.Area.Width / (double)viewPort.Width) * target.Width))) - (int)(viewPort.X / (this.Area.Width / (((double)this.Area.Width / (double)viewPort.Width) * target.Width)));
            int newYpos = (int)(this.Area.Y / (this.Area.Width / (((double)this.Area.Width / (double)viewPort.Width) * target.Width))) - (int)(viewPort.Y / (this.Area.Width / (((double)this.Area.Width / (double)viewPort.Width) * target.Width)));

            this.RelativePosition = new Point(newXpos, newYpos);


            if (this.AnimalsAround.Count != 0)
            {
                foreach (Animal a in AnimalsAround)
                {
                    g.DrawLine(Pens.Red, this.RelativePosition, a.RelativePosition);
                    g.DrawString("Animals in field of view : " + this.AnimalsAround.Count.ToString(), new Font("Arial", 15f), Brushes.White, this.RelativePosition);
                }

            }

            Task ThreadGetAnimalsAround = new Task(() =>
            {
                GetAnimalsAround();
            });
            ThreadGetAnimalsAround.Start();

            _map.ViewPort.DrawRectangleInViewPort(g, this.FieldOfView, _map.ViewPort.ScreenSize, _map.ViewPort.ViewPort, _map.ViewPort.MiniMap, _map.ViewPort.MiniMapViewPort);
            int newSizeMini = (int)(((double)this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            int newHeightMini = (int)(((double)this.Area.Height / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            int newXposMini = (int)(this.Area.X / (this.Area.Width / (((double)this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width))) - (int)(viewPortMiniMap.X / (this.Area.Width / (((double)this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));
            int newYposMini = (int)(this.Area.Y / (this.Area.Width / (((double)this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width))) - (int)(viewPortMiniMap.Y / (this.Area.Width / (((double)this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));

            g.DrawImage( texture.LoadTexture(this), new Rectangle( newXpos + target.X, newYpos + target.Y, newSize, newHeight ) );
            g.DrawImage( texture.LoadTexture( this ), new Rectangle( newXposMini + targetMiniMap.X, newYposMini + targetMiniMap.Y, newSizeMini, newHeightMini ) );
        }
    }
}
