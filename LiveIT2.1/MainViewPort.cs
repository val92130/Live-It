using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveIT2._1
{
    [Serializable]
    public class MainViewPort
    {
        const int _minimalWidthInCentimeter = 600;
        List<Box> _boxList, _boxListMini;
        Point _animalSelectorCursor;
        Point _vegetationSelectorCursor;
        private Rectangle _viewPort, _screen, _miniMap, _miniMapViewPort;
        Rectangle _mouseRect = new Rectangle(new Point(Cursor.Position.X, Cursor.Position.Y), new Size(0,0));
        Map _map;
        List<Box> _selectedBoxes;
        Texture _texture;
        bool _changeTexture, _fillTexture,_putAnimal, _followAnimal, _isRaining, _putVegetation;
        bool _tryEnter;
        Rectangle _screenTop, _screenBottom, _screenLeft, _screenRight;
        Player _player;
        Car _car ;
        Tank _tank;
        List<Car> _carList = new List<Car>();
        List<Tank> _tankList = new List<Tank>();
        SoundEnvironment _sounds;
        bool _hasClicked;
        bool _isFollowingAnAnimal;
        Animal _followedAnimal;
        public MainViewPort( Map map)
        {
            _map = map;
            _texture = new Texture();
            _selectedBoxes = new List<Box>();
            _screen = new Rectangle(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            _viewPort = new Rectangle(0, 0, 800, 800);
            _miniMap = new Rectangle( 0, 0,250, 250  );
            _miniMap.Y = _screen.Bottom - _miniMap.Height;
            _miniMapViewPort = new Rectangle( 0, 0, _map.MapSize, _map.MapSize );
            _animalSelectorCursor = new Point( 0, 0 );
            _map.ViewPort = this;
            _isRaining = false;
            _screenTop = new Rectangle(_screen.Width/2 - 400 , _screen.Top + 10, 800, 150);
            _screenBottom = new Rectangle(_screen.Width / 2 - 400, _screen.Bottom - 100, 800, 150);
            _screenLeft = new Rectangle(0, _screen.Height / 2 - 400, 10, 800);
            _screenRight = new Rectangle(_screen.Right - 10, _screen.Height / 2 - 400, 10, 800);
            _car = new Car(_map, new Point(600,600));
            _tank = new Tank(_map, new Point(700, 700));

        }


        public void Draw( Graphics g )
        {
            MoveWithMouse();

            AdjustViewPort();

            if (!_followAnimal)
            {
                _followedAnimal = null;
            }

            if (this.IsFollowingAnAnimal)
            {
                if (_followedAnimal != null)
                {
                    AdjustViewPort(_followedAnimal);
                }
                else
                {
                    _isFollowingAnAnimal = false;
                }
            }
            Random t = new Random();
            if( t.Next( 0, 50000 ) == 30 ) _map.IsRaining = true;
            if( t.Next( 0, 20000 ) == 40 && _map.IsRaining )
            {
                _map.IsRaining = false;
            }
            _boxList = _map.GetOverlappedBoxes(_viewPort);
            _boxListMini = _map.GetOverlappedBoxes( _miniMapViewPort );
            _mouseRect.X = Cursor.Position.X - (_mouseRect.Width / 2);
            _mouseRect.Y = Cursor.Position.Y - (_mouseRect.Height / 2);


            DrawBoxes(g);


            foreach( Rectangle r in _map.BloodList )
            {
                DrawBloodInViewPort( g, r, _screen, _viewPort, _miniMap, _miniMapViewPort );
            }


            g.DrawRectangle( Pens.White, new Rectangle(_miniMap.X, _miniMap.Y, _miniMap.Width, _miniMap.Height + 20) );

            DrawLandAnimals(g);

            PlayerBehavior(g);

            DrawCars(g);

            DrawVegetation(g);

            DrawFlyingAnimals(g);

            if( _changeTexture ) DrawMouseSelector( g );
            if (_fillTexture) FillMouseSelector(g);
            if(_putAnimal)PutAnimalSelector(g);
            if( _putVegetation ) PutVegetationSelector( g );

            AnimalFollowing(g);

            MakeRain(g);
          
            DrawViewPortMiniMap( g, _viewPort, _miniMap, _miniMapViewPort );

            CheckIfPlayerHasEnteredACar();

            _hasClicked = false;
        }

        private void CheckIfPlayerHasEnteredACar()
        {
            if (TryEnter && !_map.IsInCar)
            {
                foreach (Car car in _carList)
                {
                    if (_player.Area.IntersectsWith(car.Area) && _map.IsInCar == false)
                    {
                        _sounds.StartEngine();
                        _map.IsInCar = true;
                        _player.Car = car;

                    }
                }

            }
        }

        private void MakeRain(Graphics g)
        {

            if (_map.IsRaining)
            {

                if (this._isRaining == false)
                {
                    Rain();
                    this._isRaining = true;

                }
                //g.DrawImage(_texture.GetThunder(), _screen);
                g.DrawImage(_texture.GetRain(), _screen);
            }
        }

        private void AnimalFollowing(Graphics g)
        {
            if (_followAnimal && _hasClicked)
            {
                foreach (Animal a in _map.Animals)
                {
                    if (_mouseRect.IntersectsWith(new Rectangle(a.RelativePosition, a.RelativeSize)))
                    {
                        if (_followedAnimal != null )
                        {
                            if (_followedAnimal == a || _followedAnimal.IsDead)
                            {
                                _followedAnimal = null;
                            }
                        }
                        else
                        {
                            _followedAnimal = a;
                            _isFollowingAnAnimal = true;
                        }
                        
                    }
                    else
                    {
                        if (_followedAnimal == null)
                        {
                            _isFollowingAnAnimal = false;
                        }
                        
                    }
                }
            }

            if (_followedAnimal != null)
            {
                if (_followedAnimal.IsDead)
                {
                    _isFollowingAnAnimal = false;
                }

                if (_followAnimal && _map.ShowDebug)
                {
                    g.DrawRectangle(Pens.Red, new Rectangle(_followedAnimal.RelativePosition, _followedAnimal.RelativeSize));
                }

            }
        }

        private void DrawFlyingAnimals(Graphics g)
        {

            for (int i = 0; i < _map.Animals.Count; i++)
            {
                if (_map.Animals[i].Texture == AnimalTexture.Eagle)
                {
                    _map.Animals[i].Draw(g, _screen, _viewPort, _miniMap, _miniMapViewPort, _texture);
                }

            }
        }

        private void DrawVegetation(Graphics g)
        {
            for (int i = 0; i < _map.Vegetation.Count; i++)
            {
                _map.Vegetation[i].Draw(g, _screen, _viewPort, _miniMap, _miniMapViewPort, _texture);
            }
        }

        private void DrawLandAnimals(Graphics g)
        {
            for (int i = 0; i < _map.Animals.Count; i++)
            {
                if (_map.Animals[i].Texture != AnimalTexture.Eagle)
                {
                    _map.Animals[i].Draw(g, _screen, _viewPort, _miniMap, _miniMapViewPort, _texture);
                }

            }
        }

        private void DrawBoxes(Graphics g)
        {
            for (int i = 0; i < _boxList.Count; i++)
            {
                for (int j = 0; j < _map.Animals.Count(); j++)
                {
                    if (_map.Animals[j].Area.IntersectsWith(_boxList[i].Area))
                    {
                        _boxList[i].AddAnimal(_map.Animals[j]);
                    }
                }
                _boxList[i].Draw(g, _screen, _texture, _viewPort);
            }

            for (int i = 0; i < _boxListMini.Count; i++)
            {
                _boxListMini[i].DrawMiniMap(g, _miniMap, _texture, _miniMapViewPort);
            }
        }

        private void AdjustViewPort()
        {
            if (_viewPort.Left < 0)
            {
                _viewPort.X = 0;
            }

            if (_viewPort.Top < 0)
            {
                _viewPort.Y = 0;
            }
            if (_viewPort.Bottom > _map.MapSize)
            {
                _viewPort.Y = _map.MapSize - _viewPort.Height;
            }
            if (_viewPort.Right > _map.MapSize)
            {
                _viewPort.X = _map.MapSize - _viewPort.Width;
            }
        }

        private void AdjustViewPort(Animal a)
        {
            _viewPort.Size = new Size(_screen.Width * 2, _screen.Height * 2);
            _viewPort.X = _followedAnimal.Area.X - (_viewPort.Size.Width / 2) + (_followedAnimal.Area.Width / 2);
            _viewPort.Y = _followedAnimal.Area.Y - (_viewPort.Size.Height / 2) + (_followedAnimal.Area.Height / 2);

            if (_viewPort.Left < 0)
            {
                _viewPort.X = 0;
            }

            if (_viewPort.Top < 0)
            {
                _viewPort.Y = 0;
            }
            if (_viewPort.Bottom > _map.MapSize)
            {
                _viewPort.Y = _map.MapSize - _viewPort.Height;
            }
            if (_viewPort.Right > _map.MapSize)
            {
                _viewPort.X = _map.MapSize - _viewPort.Width;
            }


        }

        private void DrawCars(Graphics g)
        {
            foreach (Car car in _carList)
            {
                car.Draw(g, _screen, _viewPort, _miniMap, _miniMapViewPort, _texture);
            }
            foreach (Tank tank in _tankList)
            {
                tank.Draw(g, _screen, _viewPort, _miniMap, _miniMapViewPort, _texture);
            }
        }

        private void PlayerBehavior(Graphics g)
        {
            if (_map.IsPlayer)
            {

                if (_player.Position.X < 0)
                {
                    _player.Position = new Point(0, _player.Position.Y);
                }
                if (_player.Position.Y < 0)
                {
                    _player.Position = new Point(_player.Position.X, 0);
                }

                if (_player.Position.X > _map.MapSize - _player.Area.Width)
                {
                    _player.Position = new Point(_map.MapSize - _player.Area.Width, _player.Position.Y);
                }
                if (_player.Position.Y > _map.MapSize - _player.Area.Width)
                {
                    _player.Position = new Point(_player.Position.X, _map.MapSize - _player.Area.Width);
                }


                _player.BoxList = _map.GetOverlappedBoxes(_player.AreaBottom);


                if (!_player.IsMoving)
                {
                    _player.MovingDirection = MovingDirection.Idle;
                }
                if (!_map.IsInCar)
                {
                    _player.Draw(g, _screen, _viewPort, _miniMap, _miniMapViewPort, _texture);
                }

                if (_map.IsInCar)
                {
                    if (_player.Car.Position.X < 0)
                    {
                        _player.Car.Position = new Point(0, _player.Car.Position.Y);
                    }
                    if (_player.Car.Position.Y < 0)
                    {
                        _player.Car.Position = new Point(_player.Car.Position.X, 0);
                    }

                    if (_player.Car.Position.X > _map.MapSize - _player.Car.Area.Width)
                    {
                        _player.Car.Position = new Point(_map.MapSize - _player.Car.Area.Width, _player.Car.Position.Y);
                    }
                    if (_player.Car.Position.Y > _map.MapSize - _player.Car.Area.Width)
                    {
                        _player.Car.Position = new Point(_player.Car.Position.X, _map.MapSize - _player.Car.Area.Width);
                    }

                    _player.Position = _player.Car.Position;
                }
            }
        }

        private void Rain()
        {
            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
            t.Interval = 10000;
           
            t.Tick += new EventHandler( T_rain_tick );
            t.Start();
            
        }
       

        private void T_rain_tick( object sender, EventArgs e )
        {
            if( _map.IsRaining )
            {
                Random r = new Random();
                Point target = new Point( r.Next( 0, _map.MapSize ), r.Next( 0, _map.MapSize ) );
                Rectangle targetRect = new Rectangle( target, new Size( r.Next( 0, 800 ), r.Next( 0, 800 ) ) );             
                int top = targetRect.Top / _map.BoxSize;
                int left = targetRect.Left / _map.BoxSize;
                int bottom = (targetRect.Bottom - 1) / _map.BoxSize;
                int right = (targetRect.Right - 1) / _map.BoxSize;
                for( int i = top; i <= bottom; ++i )
                {
                    for( int j = left; j <= right; ++j )
                    {
                        if( _map[i, j] != null )
                        {
                            Box b = _map[j, i];
                            if( b.Ground == BoxGround.Grass || b.Ground == BoxGround.Grass2 || b.Ground == BoxGround.Dirt )
                            {
                                b.Ground = BoxGround.Water;
                            }
                            b.DrawTransitionTextures();
                        }

                    }
                }
            }
            
        }


        public void CreateAnimal(AnimalTexture animalType)
        {
            Animal a;
            switch( animalType.ToString() )
            {
                
                case "Dog" :
                    a = new Dog( _map, _animalSelectorCursor );
                    break;
                case "Cat":
                    a = new Cat( _map, _animalSelectorCursor );
                    break;
                case "Lion":
                    a = new Lion( _map, _animalSelectorCursor );
                    break;
                case "Rabbit":
                    a = new Rabbit( _map, _animalSelectorCursor );
                    break;
                case "Elephant":
                    a = new Elephant( _map, _animalSelectorCursor );
                    break;
                case "Cow":
                    a = new Cow( _map, _animalSelectorCursor );
                    break;
                case "Eagle":
                    a = new Eagle( _map, _animalSelectorCursor );
                    break;
                case "Gazelle":
                    a = new Gazelle( _map, _animalSelectorCursor );
                    break;
                default :
                    throw new NotSupportedException( "Unknown animal type" );

            }
            _map.Animals.Add( a );
        }

        public void SpawnPlayer( Point position )
        {
            _player = new Player( _map, position );
        }
        public void SpawnCar(Point position)
        {
            if( _carList.Count >= 0 )
            {
                _carList.Add( new Car( _map, position ) );

            }   
        }
        public void SpawnTank(Point position)
        {
            if (_carList.Count >= 0)
            {
                _carList.Add(new Tank(_map, position));
            }
        }

        public void CreateVegetation( VegetationTexture texture )
        {
            Vegetation v;
            switch( texture )
            {
                case VegetationTexture.Tree :
                    v = new Tree( this._map, _vegetationSelectorCursor );
                    break;
                case VegetationTexture.Bush:
                    v = new Bush( this._map, _vegetationSelectorCursor );
                    break;
                case VegetationTexture.Rock:
                    v = new Rock( this._map, _vegetationSelectorCursor );
                    break;
                default :
                    throw new NotSupportedException( "Unknown vegetation type" );
            }
            _map.Vegetation.Add( v );
        }

        public void MoveWithMouse()
        {
            Rectangle cursorPos = new Rectangle( Cursor.Position, new Size( 10, 10 ) );
            int speed = 45;
            if( !_map.IsPlayer )
            {
                if( cursorPos.IntersectsWith( _screenTop ) )
                {
                    this.MoveY( -speed );
                }
                if( cursorPos.IntersectsWith( _screenBottom ) )
                {
                    this.MoveY( speed );
                }
                if( cursorPos.IntersectsWith( _screenLeft ) )
                {
                    this.MoveX( -speed );
                }
                if( cursorPos.IntersectsWith( _screenRight ) )
                {
                    this.MoveX( speed );
                }
            }

        }

        public void Zoom( int meters )
        {
            if( !_map.IsPlayer )
            {
                _viewPort.Width += meters;
                _viewPort.Height += meters;
                if( _viewPort.Width < _minimalWidthInCentimeter && _viewPort.Height < _minimalWidthInCentimeter )
                {
                    _viewPort.Width = _minimalWidthInCentimeter;
                    _viewPort.Height = _minimalWidthInCentimeter;
                }

                if( _viewPort.Width > _map.MapSize )
                {
                    _viewPort.Height = _map.MapSize;
                    _viewPort.Width = _map.MapSize;
                }
            }
            
                       
        }

        public void Offset( Point delta )
        {
            if( !_map.IsPlayer )
            {
                _viewPort.Offset( delta );
                if( _viewPort.X < 0 ) _viewPort.X = 0;
                if( _viewPort.Y < 0 ) _viewPort.Y = 0;
                if( _viewPort.Right > _map.MapSize ) _viewPort.X = _map.MapSize - _viewPort.Width;
                if( _viewPort.Bottom > _map.MapSize ) _viewPort.Y = _map.MapSize - _viewPort.Height;
            } 
            
        }

        public Rectangle ScreenSize
        {
            get { return _screen; }
        }

        public Rectangle ViewPort
        {
            get { return _viewPort; }
            set { _viewPort = value; }
        }

        public Rectangle MiniMap
        {
            get { return _miniMap; }
        }

        public Rectangle MiniMapViewPort
        {
            get { return _miniMapViewPort; }
        }

        public void LoadMap(Map map)
        {
            _map = map;
        }

        /// <summary>
        /// Draw the selection rectangle to change the textures
        /// </summary>
        /// <param name="g"></param>
        /// <param name="mouseRect"></param>
        public void DrawMouseSelector(Graphics g)
        {
            _selectedBoxes.Clear();
            for (int i = 0; i < _boxList.Count; i++)
            {
                if (!_mouseRect.IntersectsWith(_miniMap))
                {
                    if (_mouseRect.IntersectsWith(new Rectangle(_boxList[i].RelativePosition, _boxList[i].RelativeSize)))
                    {
                        if( _boxList[i].AnimalList.Count != 0 )
                        {
                            g.DrawRectangle( Pens.Red, new Rectangle( _boxList[i].RelativePosition, _boxList[i].RelativeSize ) );
                        }
                        else
                        {
                            g.DrawRectangle( Pens.White, new Rectangle( _boxList[i].RelativePosition, _boxList[i].RelativeSize ) );
                            _selectedBoxes.Add( _map[_boxList[i].Line, _boxList[i].Column] );  
                        }                                            
                        g.DrawString("Box X :" + (_boxList[i].Area.X).ToString() + "\nBox Y :" + (_boxList[i].Area.Y).ToString() + "\nBox Texture : \n" + _boxList[i].Ground.ToString(), new Font("Arial", 10f), Brushes.Aqua, _boxList[i].RelativePosition);
                    }
                }
            }
        }

        public Rectangle MouseSelector
        {
            get { return _mouseRect; }
            set { _mouseRect = value; }
        }

        /// <summary>
        /// Draw the selection rectangle to fill textures
        /// </summary>
        /// <param name="g"></param>
        /// <param name="mouseRect"></param>
        public void FillMouseSelector(Graphics g)
        {
            int count = 0;
            _selectedBoxes.Clear();
            for (int i = 0; i < _boxList.Count; i++)
            {
                _mouseRect.Width = _boxList[i].RelativeSize.Width / 4;
                _mouseRect.Height = _boxList[i].RelativeSize.Height / 4;
                if (_mouseRect.IntersectsWith(new Rectangle(_boxList[i].RelativePosition, _boxList[i].RelativeSize)) && count != 1)
                {
                    count++;
                    _selectedBoxes.Add(_map[_boxList[i].Line, _boxList[i].Column]);
                    g.FillEllipse( new SolidBrush( Color.FromArgb( 52, 152, 219 ) ), new Rectangle( _mouseRect.X, _mouseRect.Y, _mouseRect.Width, _mouseRect.Height ) );
                    g.DrawString("Box X :" + (_boxList[i].Area.X).ToString() + "\nBox Y :" + (_boxList[i].Area.Y).ToString() + "\nBox Texture : \n" + _boxList[i].Ground.ToString(), new Font("Arial", 10f), Brushes.Aqua, _boxList[i].RelativePosition);
                }
            }
        }
        public void PutAnimalSelector( Graphics g )
        {
            int count = 0;
            _selectedBoxes.Clear();
            for( int i = 0; i < _boxList.Count; i++ )
            {
                _mouseRect.Width = _boxList[i].RelativeSize.Width / 4;
                _mouseRect.Height = _boxList[i].RelativeSize.Height / 4;
                if( _mouseRect.IntersectsWith( new Rectangle( _boxList[i].RelativePosition, _boxList[i].RelativeSize ) ) && count != 1 )
                {
                    count++;
                    _selectedBoxes.Add( _map[_boxList[i].Line, _boxList[i].Column] );
                    _animalSelectorCursor.X = _map[_boxList[i].Line, _boxList[i].Column].Area.X;
                    _animalSelectorCursor.Y = _map[_boxList[i].Line, _boxList[i].Column].Area.Y;
                    g.FillEllipse( new SolidBrush( Color.Brown ), new Rectangle( _mouseRect.X, _mouseRect.Y , _mouseRect.Width, _mouseRect.Height ) );
                    g.DrawString( "Box X :" + (_boxList[i].Area.X).ToString() + "\nBox Y :" + (_boxList[i].Area.Y).ToString() + "\nBox Texture : \n" + _boxList[i].Ground.ToString(), new Font( "Arial", 10f ), Brushes.Aqua, _boxList[i].RelativePosition );
                }
            }
        }
        public void PutVegetationSelector( Graphics g )
        {
            int count = 0;
            _selectedBoxes.Clear();
            for( int i = 0; i < _boxList.Count; i++ )
            {
                _mouseRect.Width = _boxList[i].RelativeSize.Width / 4;
                _mouseRect.Height = _boxList[i].RelativeSize.Height / 4;
                if( _mouseRect.IntersectsWith( new Rectangle( _boxList[i].RelativePosition, _boxList[i].RelativeSize ) ) && count != 1 )
                {
                    count++;
                    _selectedBoxes.Add( _map[_boxList[i].Line, _boxList[i].Column] );
                    _vegetationSelectorCursor.X = _map[_boxList[i].Line, _boxList[i].Column].Area.X;
                    _vegetationSelectorCursor.Y = _map[_boxList[i].Line, _boxList[i].Column].Area.Y;
                    g.FillEllipse( new SolidBrush( Color.Brown ), new Rectangle( _mouseRect.X, _mouseRect.Y, _mouseRect.Width, _mouseRect.Height ) );
                    g.DrawString( "Box X :" + (_boxList[i].Area.X).ToString() + "\nBox Y :" + (_boxList[i].Area.Y).ToString() + "\nBox Texture : \n" + _boxList[i].Ground.ToString(), new Font( "Arial", 10f ), Brushes.Aqua, _boxList[i].RelativePosition );
                }
            }
        }

        public List<Box> BoxList
        {
            get { return _boxList; }
        }

        public bool TryEnter
        {
            get { return _tryEnter; }
            set { _tryEnter = value; }
        }

        public bool HasClicked
        {
            get { return _hasClicked; }
            set { _hasClicked = value; }
        }

        public void MoveX(int centimeters) 
        {
            if( !_map.IsPlayer )
            {
                Offset( new Point( centimeters, 0 ) );
            }
            else if( _map.IsPlayer && _map.IsInCar == false)
            {
                if( centimeters > 0 )
                {
                    _player.MovingDirection = MovingDirection.Right;
                }
                else
                {
                    _player.MovingDirection = MovingDirection.Left;
                }
                    _player.Position = new Point(_player.Position.X + (centimeters / 2), _player.Position.Y);
                    _viewPort.Size = new Size(_screen.Width * 2, _screen.Height * 2);
                    _viewPort.X = _player.Area.X - (_viewPort.Size.Width / 2) + (_player.Area.Width / 2);
                    _viewPort.Y = _player.Area.Y - (_viewPort.Size.Height / 2) + (_player.Area.Height / 2);
                
                


                
            }
            else if( _map.IsPlayer && _map.IsInCar )
            {
                if( centimeters > 0 )
                {
                    _player.Car.MovingDirection = MovingDirection.Right;
                }
                else
                {
                    _player.Car.MovingDirection = MovingDirection.Left;
                }
                    _player.Car.Position = new Point(_player.Car.Position.X + (centimeters * 2), _player.Car.Position.Y);
                    _viewPort.Size = new Size(_screen.Width * 2, _screen.Height * 2);
                    _viewPort.X = _player.Car.Area.X - (_viewPort.Size.Width / 2) + (_player.Car.Area.Width / 2);
                    _viewPort.Y = _player.Car.Area.Y - (_viewPort.Size.Height / 2) + (_player.Car.Area.Height / 2);
                
                
            }
            
        }
        public void MoveY( int centimeters )
        {
            if( !_map.IsPlayer )
            {
                Offset( new Point( 0, centimeters ) );
            }
            else if( _map.IsPlayer && _map.IsInCar == false )
            {
                if( centimeters > 0 )
                {
                    _player.MovingDirection = MovingDirection.Down;
                }
                else
                {
                    _player.MovingDirection = MovingDirection.Up;
                }
                    _player.Position = new Point(_player.Position.X, _player.Position.Y + (centimeters / 2));
                    _viewPort.Size = new Size(_screen.Width * 2, _screen.Height * 2);
                    _viewPort.X = _player.Area.X - (_viewPort.Size.Width / 2) + (_player.Area.Width / 2);
                    _viewPort.Y = _player.Area.Y - (_viewPort.Size.Height / 2) + (_player.Area.Height / 2);
                

            }
            else if( _map.IsPlayer && _map.IsInCar )
            {
                if( centimeters > 0 )
                {
                    _player.Car.MovingDirection = MovingDirection.Down;
                }
                else
                {
                    _player.Car.MovingDirection = MovingDirection.Up;
                }
                    _player.Car.Position = new Point(_player.Car.Position.X, _player.Car.Position.Y + (centimeters * 2));
                    _viewPort.Size = new Size(_screen.Width * 2, _screen.Height * 2);
                    _viewPort.X = _player.Car.Area.X - (_viewPort.Size.Width / 2) + (_player.Car.Area.Width / 2);
                    _viewPort.Y = _player.Car.Area.Y - (_viewPort.Size.Height / 2) + (_player.Car.Area.Height / 2);
                

            }
            
        }

        public void InitSpawn()
        {
            _viewPort.Size = new Size( _screen.Width * 2, _screen.Height * 2 );
            _viewPort.X = _player.Area.X - (_viewPort.Size.Width / 2) + (_player.Area.Width / 2);
            _viewPort.Y = _player.Area.Y - (_viewPort.Size.Height / 2) + (_player.Area.Height / 2);
        }

        /// <summary>
        /// Select the boxes with the targetedColor texture, and remplace them with the desired Color
        /// </summary>
        /// <param name="target"></param>
        /// <param name="targetColor">Texture you wish to change</param>
        /// <param name="Color">Remplacement color</param>
        public void FillBox(Box target, BoxGround targetColor, BoxGround Color)
        {

            if (target.Ground == targetColor && Color != target.Ground)
            {
                target.Ground = Color;
                if (target.Top != null)
                {
                    FillBox(target.Top, targetColor, Color);
                }
                if (target.Bottom != null)
                {
                    FillBox(target.Bottom, targetColor, Color);
                }
                if (target.Left != null)
                {
                    FillBox(target.Left, targetColor, Color);
                }
                if (target.Right != null)
                {
                    FillBox(target.Right, targetColor, Color);
                }
            }
        }

        public List<Box> SelectedBox
        {
            get { return _selectedBoxes; }
        }

        public void ChangeTexture(BoxGround SelectedTexture)
        {
            if (this.IsChangeTextureSelected)
            {
                foreach (Box box in this.SelectedBox)
                {
                    box.Ground = SelectedTexture;
                }

            }
            if (this.IsFillTextureSelected)
            {
                    foreach (Box box in this.SelectedBox)
                    {
                        this.FillBox(box, box.Ground, SelectedTexture);
                    }
            }
        }
        public bool IsAnimalSelected
        {
            get { return _putAnimal; }
            set { _putAnimal = value; _fillTexture = false; _changeTexture = false; _followAnimal = false; _putVegetation = false; }
        }

        public bool IsFollowingAnAnimal
        {
            get { return _isFollowingAnAnimal; }
            private set { _isFollowingAnAnimal = value; }
        }

        public bool IsFollowMode
        {
            get { return _followAnimal; }
        }

        public bool IsVegetationSelected
        {
            get { return _putVegetation; }
            set { _putVegetation = value; _fillTexture = false; _changeTexture = false; _followAnimal = false; _putAnimal = false; }
        }
        public bool IsChangeTextureSelected
        {
            get { return _changeTexture; }
            set
            {
                _changeTexture = value; _fillTexture = false; _putAnimal = false; _followAnimal = false; _putVegetation = false;
                
            }
        }

        public bool IsFillTextureSelected
        {
            get { return _fillTexture; }
            set { _fillTexture = value; _changeTexture = false; _putAnimal = false; _followAnimal = false; _putVegetation = false; }
        }

        public bool IsFollowAnimalSelected
        {
            get { return _followAnimal; }
            set { _followAnimal = value; _changeTexture = false; _putAnimal = false; _fillTexture = false; _putVegetation = false; }
        }

        public Player Player
        {
            get { return _player; }
        }

        public SoundEnvironment SoundEnvironment
        {
            get { return _sounds; }
            set { _sounds = value; }
        }


        public void DrawRectangleInViewPort( Graphics g,Rectangle source, Rectangle target, Rectangle viewPort, Rectangle targetMiniMap, Rectangle viewPortMiniMap )
        {
            int newSize = (int)(((double)source.Width / (double)viewPort.Width) * target.Width + 1);
            int newHeight = (int)(((double)source.Height / (double)viewPort.Width) * target.Width + 1);
            int newXpos = (int)(source.X / (source.Width / (((double)source.Width / (double)viewPort.Width) * target.Width))) - (int)(viewPort.X / (source.Width / (((double)source.Width / (double)viewPort.Width) * target.Width)));
            int newYpos = (int)(source.Y / (source.Width / (((double)source.Width / (double)viewPort.Width) * target.Width))) - (int)(viewPort.Y / (source.Width / (((double)source.Width / (double)viewPort.Width) * target.Width)));

            int newSizeMini = (int)(((double)source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            int newHeightMini = (int)(((double)source.Height / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            int newXposMini = (int)(source.X / (source.Width / (((double)source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width))) - (int)(viewPortMiniMap.X / (source.Width / (((double)source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));
            int newYposMini = (int)(source.Y / (source.Width / (((double)source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width))) - (int)(viewPortMiniMap.Y / (source.Width / (((double)source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));

            g.DrawRectangle( Pens.Blue, new Rectangle( newXpos + target.X, newYpos + target.Y, newSize, newHeight ) );
            g.DrawRectangle(Pens.Blue, new Rectangle(newXposMini + targetMiniMap.X, newYposMini + targetMiniMap.Y, newSizeMini, newHeightMini));
        }

        public void DrawBloodInViewPort( Graphics g, Rectangle source, Rectangle target, Rectangle viewPort, Rectangle targetMiniMap, Rectangle viewPortMiniMap )
        {
            int newSize = (int)(((double)source.Width / (double)viewPort.Width) * target.Width + 1);
            int newHeight = (int)(((double)source.Height / (double)viewPort.Width) * target.Width + 1);
            int newXpos = (int)(source.X / (source.Width / (((double)source.Width / (double)viewPort.Width) * target.Width))) - (int)(viewPort.X / (source.Width / (((double)source.Width / (double)viewPort.Width) * target.Width)));
            int newYpos = (int)(source.Y / (source.Width / (((double)source.Width / (double)viewPort.Width) * target.Width))) - (int)(viewPort.Y / (source.Width / (((double)source.Width / (double)viewPort.Width) * target.Width)));

            int newSizeMini = (int)(((double)source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            int newHeightMini = (int)(((double)source.Height / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            int newXposMini = (int)(source.X / (source.Width / (((double)source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width))) - (int)(viewPortMiniMap.X / (source.Width / (((double)source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));
            int newYposMini = (int)(source.Y / (source.Width / (((double)source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width))) - (int)(viewPortMiniMap.Y / (source.Width / (((double)source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));

            g.DrawImage( _texture.GetBlood(), new Rectangle( newXpos + target.X, newYpos + target.Y, newSize, newHeight ) );
            g.DrawRectangle( Pens.Red, new Rectangle( newXposMini + targetMiniMap.X, newYposMini + targetMiniMap.Y, newSizeMini, newHeightMini ) );
        }
        public void DrawRectangleInViewPort( Graphics g, Rectangle source, Rectangle target, Rectangle viewPort, Rectangle targetMiniMap, Rectangle viewPortMiniMap, Animal animal, Texture t )
        {
            int newSize = (int)(((double)source.Width / (double)viewPort.Width) * target.Width + 1);
            int newHeight = (int)(((double)source.Height / (double)viewPort.Width) * target.Width + 1);
            int newXpos = (int)(source.X / (source.Width / (((double)source.Width / (double)viewPort.Width) * target.Width))) - (int)(viewPort.X / (source.Width / (((double)source.Width / (double)viewPort.Width) * target.Width)));
            int newYpos = (int)(source.Y / (source.Width / (((double)source.Width / (double)viewPort.Width) * target.Width))) - (int)(viewPort.Y / (source.Width / (((double)source.Width / (double)viewPort.Width) * target.Width)));

            int newSizeMini = (int)(((double)source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            int newHeightMini = (int)(((double)source.Height / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            int newXposMini = (int)(source.X / (source.Width / (((double)source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width))) - (int)(viewPortMiniMap.X / (source.Width / (((double)source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));
            int newYposMini = (int)(source.Y / (source.Width / (((double)source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width))) - (int)(viewPortMiniMap.Y / (source.Width / (((double)source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));

            g.DrawImage( t.LoadTexture( animal ), new Rectangle( newXpos + target.X, newYpos + target.Y, newSize, newHeight ) );
            g.DrawImage( t.LoadTexture(animal), new Rectangle( newXposMini + targetMiniMap.X, newYposMini + targetMiniMap.Y, newSizeMini, newHeightMini ) );
        }

        public void DrawViewPortMiniMap( Graphics g, Rectangle source, Rectangle targetMiniMap, Rectangle viewPortMiniMap )
        {
            int newSizeMini = (int)(((double)source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            int newHeightMini = (int)(((double)source.Height / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            int newXposMini = (int)(source.X / (source.Width / (((double)source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width))) - (int)(viewPortMiniMap.X / (source.Width / (((double)source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));
            int newYposMini = (int)(source.Y / (source.Width / (((double)source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width))) - (int)(viewPortMiniMap.Y / (source.Width / (((double)source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));
            g.DrawRectangle( Pens.White, new Rectangle( newXposMini + targetMiniMap.X, newYposMini + targetMiniMap.Y, newSizeMini, newHeightMini ) );
        }
       
    }

}
