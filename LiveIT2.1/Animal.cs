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
        internal List<Box> BoxList = new List<Box>();
        List<Box> WalkableBoxes = new List<Box>();
        int _viewDistance;
        int _speed;
        int _defaultSpeed;
        int _health, _hunger, _thirst;
        Point _relativePosition;
        Size _relativeSize;
        List<Animal> _animalsAround =  new List<Animal>();
        internal bool _walking, _isInWater, _isDrinking, _isEating, _isDead;
        private  Point _targetLocation;
        Graphics _graphics;
        Texture _textureGraphics;
        
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

        public bool IsInMovement
        {
            get { return _walking; }
            set { _walking = value; }
        }

        public bool IsDead
        {
            get { return _isDead; }
            internal set { _isDead = value; }
        }
        public Animal( Map map, Point position )
        {
            _map = map;
            _position = position;
            _health = 100;
            _hunger = 0;
            _thirst = 0;
            CheckDrowning();
            HungerTimer();
            ThirstTimer();
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
            internal set
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
                if( _health <= 0 ) this.Die();
            }
        }
        public int Hunger
        {
            get { return _hunger; }
            internal set
            {
                _hunger = value;
                if( _hunger <= 0 )
                {
                    _hunger = 0;
                }
                if( _hunger >= 100 )
                {
                    _hunger = 100;
                }
            }
        }
        public int Thirst
        {
            get { return _thirst; }
            internal set
            {
                _thirst = value;
                if( _thirst <= 0 )
                {
                    _thirst = 0;
                }
                if( _thirst >= 100 )
                {
                    _thirst = 100;
                }
            }
        }

        public void Hurt( int HealthPoint )
        {
            if( HealthPoint < 0 ) throw new ArgumentException( "HealthPoints must be negative" );
            this.Health = - HealthPoint;
        }

        public void CheckDrowning()
        {
            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();       
            t.Interval = 2000;
            t.Tick += new EventHandler( T_drown_tick );
            t.Start();
        }

        public void HungerTimer()
        {
            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
            t.Interval = 30000;
            t.Tick += new EventHandler( T_hunger_tick );
            t.Start();
        }
        public void ThirstTimer()
        {
            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
            t.Interval = 20000;
            t.Tick += new EventHandler( T_thirst_tick );
            t.Start();
        }

        private void T_thirst_tick( object sender, EventArgs e )
        {
            this.Thirst += 5;
        }

        private void T_hunger_tick( object sender, EventArgs e )
        {
            this.Hunger += 5;
        }

        private void T_drown_tick( object sender, EventArgs e )
        {
            if( this._isInWater && _isDrinking == false )
            {
                this.Hurt( 10 );
            }
            
        }

        public MovingDirection MovingDirection
        {
            get
            {
                if( this.Direction.Width > 0 && this.Direction.Width > this.Direction.Height )
                {
                    return MovingDirection.Right;
                }
                if( this.Direction.Width < 0 && this.Direction.Width > this.Direction.Height )
                {
                    return MovingDirection.Left;
                }
                if( this.Direction.Height > 0 && this.Direction.Height > this.Direction.Width )
                {
                    return MovingDirection.Down;
                }
                if( this.Direction.Height < 0 && this.Direction.Height > this.Direction.Width )
                {
                    return MovingDirection.Up;
                }
                return MovingDirection.Up;
            }
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
            //Rectangle r = new Rectangle(this.RelativePosition, this.RelativeSize);
            this.IsDead = true;
            _map.BloodList.Add( this.Area);
            _map.DeadAnimals += 1;
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
            _isDrinking = false;
            if (this.FieldOfView.IntersectsWith(new Rectangle(this.TargetLocation, this.FieldOfView.Size)))
            {
                this.IsInMovement = false;
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
                        if (b.Ground == BoxGround.Water && this.Texture != AnimalTexture.Eagle)
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
                            if( this.Thirst >= 15 )
                            {
                                this._isDrinking = true;
                                this.Speed = 0;
                                this.Thirst -= 5;
                            }
                            else
                            {
                                this.Speed = DefaultSpeed;
                                if( WalkableBoxes.Count != 0 )
                                {
                                    Random r = new Random();
                                    ChangePosition( WalkableBoxes[r.Next( 0, WalkableBoxes.Count )].Location );
                                }
                                else
                                {
                                    ChangePosition();
                                }
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

            _graphics = g;

            _textureGraphics = texture;
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

                g.DrawString( "Health " + this.Health.ToString(), new Font( "Arial", 20f ), Brushes.Black, new Point(this.RelativePosition.X,this.RelativePosition.Y - 20) );
                g.DrawString( "Hunger " + this.Hunger.ToString(), new Font( "Arial", 20f ), Brushes.Black, new Point( this.RelativePosition.X, this.RelativePosition.Y - 40 ) );
                g.DrawString( "Thirst " + this.Thirst.ToString(), new Font( "Arial", 20f ), Brushes.Black, new Point( this.RelativePosition.X + 200, this.RelativePosition.Y - 40 ) );

                g.DrawString("Target pos : " + this.TargetLocation.X.ToString() + "\n" + this.TargetLocation.Y.ToString(), new Font("Arial", 20f), Brushes.Black, this.RelativePosition);
                _map.ViewPort.DrawRectangleInViewPort(g, new Rectangle(this.TargetLocation, this.Area.Size), _map.ViewPort.ScreenSize, _map.ViewPort.ViewPort, _map.ViewPort.MiniMap, _map.ViewPort.MiniMapViewPort);
            }           

            GetAnimalsAround();
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
