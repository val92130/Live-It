using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LiveIT2._1
{
    [Serializable]
    public class Animal
    {
         Point _position;
         Size _size;
         readonly Map _map;
         SizeF _direction;
         BoxGround _favoriteEnvironnment;
         AnimalTexture _texture;
        Rectangle _fieldOfViewRect;
        List<Box> BoxList = new List<Box>();
        int _viewDistance;
        int _speed;
        int _defaultSpeed;
        Point _relativePosition;
        Size _relativeSize;
        List<Animal> _animalsAround =  new List<Animal>();
        bool _walking;
        private  Point _targetLocation;
        
        public Animal(Map map, Point position, Size size, AnimalTexture texture)
        {
            _map = map;
            _position = position;
            _size = size;
            _texture = texture;
            _direction = new SizeF( 0, 0 );
            _favoriteEnvironnment = BoxGround.Grass;
            _viewDistance = 1000;
            
        }

        public bool IsWalking
        {
            get { return _walking; }
            set { _walking = value; }
        }
        public Animal( Map map, Point position )
        {
            _map = map;
            _position = position;
        }

        public Rectangle FieldOfView
        {
            get { return new Rectangle(this.Position.X - (_viewDistance / 2), this.Position.Y - (_viewDistance / 2), _viewDistance * 2, _viewDistance * 2); }
        }

        public int ViewDistance
        {
            get { return _viewDistance; }
            set { _viewDistance = value; }
        }

        public void AddToList( Box b )
        {
            if( !BoxList.Contains( b ) )
            {
                BoxList.Add( b );
            }          
        }
        public void RemoveFromList( Box b )
        {
            if( BoxList.Contains( b ) )
            {
                BoxList.Remove( b );
            }
        }
         
        public Point Position
        {
            get { return _position; }
            set
            {
                _position = value;
            }
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

        public SizeF Direction
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

        public Size RelativeSize
        {
            get { return _relativeSize; }
            set { _relativeSize = value; }
        }

        public AnimalTexture Texture
        {
            get { return _texture; }
            protected set { _texture = value; }
        }

        internal Map Park
        {
            get { return _map; }
        }

        public Point TargetLocation
        {
            get { return _targetLocation; }
            set { _targetLocation = value; }
        }

        public BoxGround FavoriteEnvironnment
        {
            get { return _favoriteEnvironnment; }
            set { _favoriteEnvironnment = value; }
        }

        public virtual void Draw( Graphics g, Rectangle target, Rectangle viewPort, Rectangle targetMiniMap, Rectangle viewPortMiniMap, Texture texture )
        {
            if (this.Position.X + this.Area.Width >= _map.MapSize)
            {
                this.Position = new Point(0, this.Position.Y);
            }
            if (this.Position.Y + this.Area.Height >= _map.MapSize)
            {
                this.Position = new Point(this.Position.X, 0);
            }

            if( !this.IsWalking )
            {
                ChangePosition();
            }
            foreach( Box b in BoxList )
            {
                if( b.Ground == BoxGround.Water && b != null )
                {
                    ChangePosition();
                }
            }

            if( this.Area.IntersectsWith( new Rectangle(this.TargetLocation, this.Area.Size) ))
            {

                this.IsWalking = false;
                
            }


            Position = new Point( _position.X + (int) (this.Direction.Width*this.Speed),
                                  _position.Y + (int)(this.Direction.Height * this.Speed) );
          
            int newWidth = (int)(((double)this.Area.Width / (double)viewPort.Width) * target.Width + 1);
            int newHeight = (int)(((double)this.Area.Height / (double)viewPort.Width) * target.Width + 1);
            int newXpos = (int)(this.Area.X / (this.Area.Width / (((double)this.Area.Width / (double)viewPort.Width) * target.Width))) - (int)(viewPort.X / (this.Area.Width / (((double)this.Area.Width / (double)viewPort.Width) * target.Width)));
            int newYpos = (int)(this.Area.Y / (this.Area.Width / (((double)this.Area.Width / (double)viewPort.Width) * target.Width))) - (int)(viewPort.Y / (this.Area.Width / (((double)this.Area.Width / (double)viewPort.Width) * target.Width)));

            this.RelativePosition = new Point(newXpos, newYpos);
            this.RelativeSize = new Size(newWidth, newHeight);


            if (this.AnimalsAround.Count != 0)
            {
                for( int i = 0; i < AnimalsAround.Count();i++ )
                {
                    if( this.Texture == AnimalTexture.Elephant )
                    {
                        if( AnimalsAround[i].Texture == AnimalTexture.Rabbit )
                        {
                            ChangePosition( AnimalsAround[i].Position );
                            if( this.Area.IntersectsWith( AnimalsAround[i].Area ) )
                            {
                                foreach( Box b in _map.Boxes )
                                {
                                    b.RemoveFromList( AnimalsAround[i] );
                                }
                                _map.Animals.Remove( AnimalsAround[i] );
                                AnimalsAround.Remove( AnimalsAround[i] );

                            }
                        }

                    }

                }

                if( this._map.ShowDebug == true )
                {

                    foreach( Animal a in AnimalsAround )
                    {
                        if( this.Texture != a.Texture )
                        {
                            g.DrawLine( new Pen( Brushes.Red, 4 ), this.RelativePosition, a.RelativePosition );
                            g.DrawString( "Animals in field of view : " + this.AnimalsAround.Count.ToString(), new Font( "Arial", 15f ), Brushes.White, this.RelativePosition );
                        }

                    }
                    for( int i = 0; i < _map.Boxes.Length; i++ )
                    {
                        if( _map.Boxes[i].Area.IntersectsWith( this.FieldOfView ) && this.FavoriteEnvironnment == _map.Boxes[i].Ground )
                        {
                            g.DrawLine( new Pen( Brushes.Blue, 5 ), this.RelativePosition, _map.Boxes[i].RelativePosition );
                        }
                    }
                    _map.ViewPort.DrawRectangleInViewPort( g, this.FieldOfView, _map.ViewPort.ScreenSize, _map.ViewPort.ViewPort, _map.ViewPort.MiniMap, _map.ViewPort.MiniMapViewPort );
                    
                }
            }
            


            Task ThreadGetAnimalsAround = new Task(() =>
            {
                GetAnimalsAround();
            });
            ThreadGetAnimalsAround.Start();

            int newSizeMini = (int)(((double)this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            int newHeightMini = (int)(((double)this.Area.Height / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            int newXposMini = (int)(this.Area.X / (this.Area.Width / (((double)this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width))) - (int)(viewPortMiniMap.X / (this.Area.Width / (((double)this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));
            int newYposMini = (int)(this.Area.Y / (this.Area.Width / (((double)this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width))) - (int)(viewPortMiniMap.Y / (this.Area.Width / (((double)this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));

            g.DrawImage( texture.LoadTexture(this), new Rectangle( newXpos + target.X, newYpos + target.Y, newWidth, newHeight ) );
            g.DrawImage( texture.LoadTexture( this ), new Rectangle( newXposMini + targetMiniMap.X, newYposMini + targetMiniMap.Y, newSizeMini, newHeightMini ) );
        }

        public void ChangePosition()
        {
            Random r = new Random();
            Point newTarget = new Point( r.Next( 0, _map.MapSize ), r.Next( 0, _map.MapSize ) );
            this.TargetLocation = newTarget;
            float distance = (float)(Math.Pow( newTarget.X - this.Position.X, 2 ) + Math.Pow( newTarget.Y - this.Position.Y, 2 ));
            SizeF _dir = new SizeF( (newTarget.X - this.Position.X) / distance, (newTarget.Y - this.Position.Y) / distance );
            this.Direction = _dir;
            this.IsWalking = true;
        }
        public void ChangePosition(Point target)
        {
            Random r = new Random();
            Point newTarget = target;
            this.TargetLocation = newTarget;
            float distance = (float)(Math.Pow(newTarget.X - this.Position.X, 2) + Math.Pow(newTarget.Y - this.Position.Y, 2));
            SizeF _dir = new SizeF((newTarget.X - this.Position.X) / distance, (newTarget.Y - this.Position.Y) / distance);
            this.Direction = _dir;
            this.IsWalking = true;
        }
    }
}
