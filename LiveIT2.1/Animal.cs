using System;
using System.Collections.Generic;
using System.Diagnostics;
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
         protected readonly Map _map;
         SizeF _direction;
         BoxGround _favoriteEnvironnment;
         AnimalTexture _texture;
        Rectangle _fieldOfViewRect;
        List<Box> BoxList = new List<Box>();
        List<Box> WalkableBoxes = new List<Box>();
        int _viewDistance;
        int _speed;
        int _defaultSpeed;
        int _health;
        Point _relativePosition;
        Size _relativeSize;
        List<Animal> _animalsAround =  new List<Animal>();
        bool _walking, _isInWater;
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
            _health = 100;
            
        }

        public bool IsInMovement
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

        public int Health
        {
            get { return _health; }
            private set
            {
                _health += value;
                if( _health - value <= 0 )
                {
                    _health = 0;
                    this.Die();
                }
                if( _health + value >= 100 )
                {
                    _health = 100;
                }
            }
        }

        public void Hurt( int HealthPoint )
        {
            if( HealthPoint < 0 ) throw new ArgumentException( "HealthPoints must be negative" );
            this.Health -= HealthPoint;
        }

        public void Drown()
        {
            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
            t.Start();
            t.Interval = 2000;
            t.Tick += new EventHandler( T_drown_tick );
            if( !_isInWater )
            {
                t.Stop();               
            }
        }

        private void T_drown_tick( object sender, EventArgs e )
        {
            this.Hurt( 10 );
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
                if( _animalsAround.Contains( _map.Animals[i] ) && !_map.Animals[i].FieldOfView.IntersectsWith( this.FieldOfView ) )
                {
                    _animalsAround.Remove( _map.Animals[i] );
                }
                for( int j = 0; j < _animalsAround.Count(); j++ )
                {
                    if( !_map.Animals.Contains( _animalsAround[j] ) ) _animalsAround.Remove( _animalsAround[j] );
                }                   
            }
        }

        public void Die()
        {
            foreach( Box b in _map.Boxes )
            {
                b.RemoveFromList( this );
            }
            _map.Animals.Remove( this );
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
        public virtual void Behavior()
        {
            if (this.FieldOfView.IntersectsWith(new Rectangle(this.TargetLocation, this.FieldOfView.Size)))
            {
                this.IsInMovement = false;
            }

            if( _isInWater )
            {
                this.Hurt( 1 );
            }

            if (!this.IsInMovement)
            {
                ChangePosition();
            }
            if (BoxList.Count() != 0)
            {
                foreach (Box b in BoxList)
                {
                    if (b != null)
                    {
                        if (b.Ground == BoxGround.Water)
                        {
                            _isInWater = true;                          
                            for (int i = 0; i < _map.Boxes.Length; i++)
                            {
                                if (_map.Boxes[i].Area.IntersectsWith(this.FieldOfView) && _map.Boxes[i].Ground != BoxGround.Water)
                                {
                                    if (!WalkableBoxes.Contains(_map.Boxes[i])) WalkableBoxes.Add(_map.Boxes[i]);
                                }
                                if (WalkableBoxes.Count != 0)
                                {
                                    for (int j = 0; j < WalkableBoxes.Count; j++)
                                    {
                                        if (!WalkableBoxes[j].Area.IntersectsWith(this.FieldOfView))
                                        {
                                            WalkableBoxes.Remove(WalkableBoxes[j]);
                                        }
                                    }
                                }
                            }
                            if (WalkableBoxes.Count != 0)
                            {
                                Random r = new Random();
                                ChangePosition(WalkableBoxes[r.Next(0,WalkableBoxes.Count)].Location);
                            }
                            else
                            {
                                ChangePosition();
                            }

                        }
                        else
                        {
                            _isInWater = false;
                        }
                    }
                    
                }
            }           
        }

        public virtual void Draw( Graphics g, Rectangle target, Rectangle viewPort, Rectangle targetMiniMap, Rectangle viewPortMiniMap, Texture texture )
        {

            int newWidth = (int)(((double)this.Area.Width / (double)viewPort.Width) * target.Width + 1);
            int newHeight = (int)(((double)this.Area.Height / (double)viewPort.Width) * target.Width + 1);
            int newXpos = (int)(this.Area.X / (this.Area.Width / (((double)this.Area.Width / (double)viewPort.Width) * target.Width))) - (int)(viewPort.X / (this.Area.Width / (((double)this.Area.Width / (double)viewPort.Width) * target.Width)));
            int newYpos = (int)(this.Area.Y / (this.Area.Width / (((double)this.Area.Width / (double)viewPort.Width) * target.Width))) - (int)(viewPort.Y / (this.Area.Width / (((double)this.Area.Width / (double)viewPort.Width) * target.Width)));

            this.RelativePosition = new Point(newXpos, newYpos);
            this.RelativeSize = new Size(newWidth, newHeight);

            

            Position = new Point(_position.X + (int)(this.Direction.Width * this.Speed),
                                  _position.Y + (int)(this.Direction.Height * this.Speed));
            Behavior();


            if (this._map.ShowDebug == true)
            {

                foreach (Animal a in AnimalsAround)
                {
                    if (this.Texture != a.Texture)
                    {
                        g.DrawLine(new Pen(Brushes.Red, 4), this.RelativePosition, a.RelativePosition);
                        g.DrawString("Animals in field of view : " + this.AnimalsAround.Count.ToString(), new Font("Arial", 15f), Brushes.White, this.RelativePosition);
                    }

                }               
                _map.ViewPort.DrawRectangleInViewPort(g, this.FieldOfView, _map.ViewPort.ScreenSize, _map.ViewPort.ViewPort, _map.ViewPort.MiniMap, _map.ViewPort.MiniMapViewPort);

                g.DrawString("Target pos : " + this.TargetLocation.X.ToString() + "\n" + this.TargetLocation.Y.ToString(), new Font("Arial", 20f), Brushes.Black, this.RelativePosition);
                _map.ViewPort.DrawRectangleInViewPort(g, new Rectangle(this.TargetLocation, this.Area.Size), _map.ViewPort.ScreenSize, _map.ViewPort.ViewPort, _map.ViewPort.MiniMap, _map.ViewPort.MiniMapViewPort);
            }           

            GetAnimalsAround();
            int newSizeMini = (int)(((double)this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            int newHeightMini = (int)(((double)this.Area.Height / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            int newXposMini = (int)(this.Area.X / (this.Area.Width / (((double)this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width))) - (int)(viewPortMiniMap.X / (this.Area.Width / (((double)this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));
            int newYposMini = (int)(this.Area.Y / (this.Area.Width / (((double)this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width))) - (int)(viewPortMiniMap.Y / (this.Area.Width / (((double)this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));

            g.DrawImage( texture.LoadTexture(this), new Rectangle( newXpos + target.X, newYpos + target.Y, newWidth, newHeight ) );
            g.DrawRectangle( Pens.Black, new Rectangle( newXposMini + targetMiniMap.X, newYposMini + targetMiniMap.Y, newSizeMini, newHeightMini ) );          
        }

        public void ChangePosition()
        {
            Random r = new Random();
            Point newTarget = new Point( r.Next( 0, _map.MapSize ), r.Next( 0, _map.MapSize ) );
            Debug.Assert(newTarget.X <= _map.MapSize && newTarget.Y <= _map.MapSize);
            this.TargetLocation = newTarget;
            float distance = (float)(Math.Pow( this.Position.X -newTarget.X , 2 ) + Math.Pow(  this.Position.Y - newTarget.Y, 2 ));
            SizeF _dir = new SizeF( (newTarget.X - this.Position.X) / distance, (newTarget.Y - this.Position.Y) / distance );
            this.Direction = _dir;
            this.IsInMovement = true;
        }
        public void ChangePosition(Point target)
        {
            Random r = new Random();
            Point newTarget = target;
            this.TargetLocation = newTarget;
            float distance = (float)(Math.Pow( this.Position.X - newTarget.X, 2 ) + Math.Pow( this.Position.Y - newTarget.Y, 2 ));
            SizeF _dir = new SizeF((newTarget.X - this.Position.X) / distance, (newTarget.Y - this.Position.Y) / distance);
            this.Direction = _dir;
            this.IsInMovement = true;
        }
    }
}
