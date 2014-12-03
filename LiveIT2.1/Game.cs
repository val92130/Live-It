﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveIT2._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
      
        Graphics g;
        Graphics _screenGraphic;

        bool right;
        bool left;
        bool up;
        bool down;
        bool ctrl;

        Point _playerPosition;

        AnimalTexture _selectedAnimal;

        bool ShowDebugInfo = false;

        BoxGround _selectedTexture;
        VegetationTexture _selectedVegetation;

        System.Windows.Forms.Timer t;

        // Timer to count frames per second 
        System.Windows.Forms.Timer fpst;

        Bitmap _background;
        Map _map;
        long _fpsCount;
        DateTime _lastCheckTime = DateTime.Now;
  
        Size _selectionCursorWidth;
        Rectangle _mouseRect;
        SoundEnvironment _soundEnvironment;

        MainViewPort _viewPort;

        int _boxWidth;
        int _fps;

        private void Form1_Load( object sender, EventArgs e )
        {
            _selectedAnimal = AnimalTexture.Rabbit;
            _fpsCount = 0;
            this.DoubleBuffered = true;
            button1.Text = "Hide Debug";
            _selectedTexture = BoxGround.Grass;
            _background = new Bitmap( this.Width, this.Height );
            
            _map = new Map( 50, 2 );
            
            _boxWidth = _map.BoxSize;

             _viewPort = new MainViewPort( _map);
             _map.ShowDebug = true;
            _mouseRect = new Rectangle( 0, 0, 100, 100 );

            g = this.CreateGraphics();  
            _screenGraphic = Graphics.FromImage( _background );
            _screenGraphic.CompositingQuality = CompositingQuality.HighSpeed;
            _screenGraphic.InterpolationMode = InterpolationMode.Low;
            

            _selectionCursorWidth = new Size(_boxWidth, _boxWidth);
            this.MouseWheel += new MouseEventHandler(T_mouseWheel);

            t = new System.Windows.Forms.Timer();
            fpst = new System.Windows.Forms.Timer();


            fpst.Interval = 200;
            fpst.Tick += new EventHandler(fpst_Tick);

            t.Interval = 10;
            t.Tick += new EventHandler( t_Tick );

            Random r = new Random();
            int _random = r.Next(0, _map.MapSize);
            _playerPosition = new Point(_random, _random);


            fpst.Start();
            t.Start();
            _soundEnvironment = new SoundEnvironment();
            _soundEnvironment.LoadMap( _map );
            _viewPort.SoundEnvironment = _soundEnvironment;
        }

        public void LoadMap(Map map)
        {
            this._map = map;
        }

        private void T_mouseWheel(object sender, MouseEventArgs e)
        {
            int increment = 0;
            if (e.Delta >= 1)
            {
                if (ctrl)
                {
                    if (_viewPort.MouseSelector.Width >= _map.BoxSize)
                    {
                        increment += _map.BoxSize;
                    }
                    _viewPort.MouseSelector = new Rectangle(_viewPort.MouseSelector.X, _viewPort.MouseSelector.Y, _viewPort.MouseSelector.Width - increment, _viewPort.MouseSelector.Height - increment);
                }
                else
                {
                    _viewPort.Zoom(-400);
                }


            }
            else
            {

                if (ctrl)
                {
                    if (_viewPort.MouseSelector.Width <= 600)
                    {
                        increment += _map.BoxSize;
                    }
                    _viewPort.MouseSelector = new Rectangle(_viewPort.MouseSelector.X, _viewPort.MouseSelector.Y, _viewPort.MouseSelector.Width + increment, _viewPort.MouseSelector.Height + increment);
                }
                else
                {
                    _viewPort.Zoom(400);
                }
            }
        }


        private void t_Tick( object sender, EventArgs e )
        {
            if( up ) { MoveRectangle( Direction.Up ); }
            if( left ) { MoveRectangle( Direction.Left ); }
            if( down ) { MoveRectangle( Direction.Down ); }
            if( right ) { MoveRectangle( Direction.Right ); }

            Draw();
            g.DrawImage(_background, new Point(0,0));
            _soundEnvironment.LoadBoxes(_viewPort.BoxList);
            _soundEnvironment.PlayAllSounds();
            _soundEnvironment.PlayerSounds();
            Interlocked.Increment(ref _fpsCount);
                      
        }

        public enum Direction { Up, Down, Right, Left };
        public void MoveRectangle( Direction d )
        {
            int speed = 45;
            if( d == Direction.Down ) { _viewPort.MoveY(speed); }
            if (d == Direction.Up) { _viewPort.MoveY(-speed); }
            if (d == Direction.Right) { _viewPort.MoveX(speed); }
            if (d == Direction.Left) { _viewPort.MoveX(-speed); }
        }
        public void Draw()
        {
            Rectangle _rMouse = new Rectangle( new Point( Cursor.Position.X, Cursor.Position.Y ), _selectionCursorWidth );
            //_screenGraphic.Clear( Color.FromArgb( 255, Color.Black ) );
            _viewPort.Draw( _screenGraphic ); 
            
        }

        private void Form1_KeyDown( object sender, KeyEventArgs e )
        {
            if (e.KeyCode == Keys.Z) { up = true; if (_map.IsPlayer) _viewPort.Player.IsMoving = true; if (_map.IsInCar) _viewPort.Player.Car.IsMoving = true; }
            if (e.KeyCode == Keys.Q) { left = true; if (_map.IsPlayer) _viewPort.Player.IsMoving = true; if (_map.IsInCar) _viewPort.Player.Car.IsMoving = true; }
            if (e.KeyCode == Keys.S) { down = true; if (_map.IsPlayer) _viewPort.Player.IsMoving = true; if (_map.IsInCar) _viewPort.Player.Car.IsMoving = true; }
            if (e.KeyCode == Keys.D) { right = true; if (_map.IsPlayer) _viewPort.Player.IsMoving = true; if (_map.IsInCar) _viewPort.Player.Car.IsMoving = true; }
            if( e.KeyCode == Keys.E ) {
                
                if( _map.IsInCar )
                {
                    _viewPort.TryEnter = false;
                    _map.ExitCar();
                    
                }
                else
                {
                    _viewPort.TryEnter = true;
                }
            }
            if( e.KeyCode == Keys.ControlKey ) { ctrl = true;  }
        }

        private void Form1_KeyUp( object sender, KeyEventArgs e )
        {
            if (e.KeyCode == Keys.Z) { up = false; if (_map.IsPlayer) _viewPort.Player.IsMoving = false; if (_map.IsInCar) _viewPort.Player.Car.IsMoving = false; }
            if (e.KeyCode == Keys.Q) { left = false; if (_map.IsPlayer) _viewPort.Player.IsMoving = false; if (_map.IsInCar) _viewPort.Player.Car.IsMoving = false; }
            if (e.KeyCode == Keys.S) { down = false; if (_map.IsPlayer) _viewPort.Player.IsMoving = false; if (_map.IsInCar) _viewPort.Player.Car.IsMoving = false; }
            if (e.KeyCode == Keys.D) { right = false; if (_map.IsPlayer) _viewPort.Player.IsMoving = false; if (_map.IsInCar) _viewPort.Player.Car.IsMoving = false; }
            if( e.KeyCode == Keys.E ) { _viewPort.TryEnter = false; }
            if( e.KeyCode == Keys.ControlKey ) { ctrl = false; }
            if (e.KeyCode == Keys.R)
            {
                if (_map.IsInCar)
                {
                    _viewPort.Player.Car.ToggleRadio();
                }
            }
        }

        private void Form1_MouseClick( object sender, MouseEventArgs e )
        {
            
            if (_viewPort.IsAnimalSelected)
            {
                _viewPort.CreateAnimal(_selectedAnimal);
            }
            else if(_viewPort.IsChangeTextureSelected)
            {
                _viewPort.ChangeTexture(_selectedTexture);
            }
            else if(_viewPort.IsVegetationSelected)
            {
                _viewPort.CreateVegetation( _selectedVegetation );
            }
            else if( _viewPort.IsFillTextureSelected )
            {
                _viewPort.ChangeTexture( _selectedTexture );
            }
        }

        private void _waterButton_Click( object sender, EventArgs e )
        {
            _selectedTexture = BoxGround.Water;
        }

        private void _dirtButton_Click( object sender, EventArgs e )
        {
            _selectedTexture = BoxGround.Forest;
        }

        private void _snowButton_Click( object sender, EventArgs e )
        {
            _selectedTexture = BoxGround.Snow;
        }

        private void _desertButton_Click( object sender, EventArgs e )
        {
            _selectedTexture = BoxGround.Desert;
        }

        private void _grassButton_Click( object sender, EventArgs e )
        {
            _selectedTexture = BoxGround.Grass;
        }

        private void _changeTextureButton_Click(object sender, EventArgs e)
        {
            _viewPort.IsChangeTextureSelected = true;
        }

        private void _fillTextureButton_Click(object sender, EventArgs e)
        {
            _viewPort.IsFillTextureSelected = true;
        }


        long _averageFps;
        long _totalFps;
        long _count = 0;
        private void fpst_Tick(object sender, EventArgs e)
        {
            _count++;
            _fps = GetFps();
            _totalFps += _fps;
            _fpsTextBox.Text = "FPS : " + _fps.ToString() + "\n Avg :" + _averageFps.ToString() + "\n\nTotal Frames :" + _totalFps.ToString() + "\nSelected Texture :" + _selectedTexture.ToString() ;
            _fpsTextBox.ForeColor = _fps >= 20 ? Color.Green : Color.Red;
            _averageFps = _totalFps / _count;
        }
        int GetFps()
        {
            double secondsElapsed = (DateTime.Now - _lastCheckTime).TotalSeconds;
            long count = Interlocked.Exchange(ref _fpsCount, 0);
            double fps = count / secondsElapsed;
            _lastCheckTime = DateTime.Now;
            return (int)fps;
        }

        private void button1_Click( object sender, EventArgs e )
        {
            if( ShowDebugInfo )
            {
                _fpsTextBox.Show();
                button1.Text = "Hide Debug";
                ShowDebugInfo = false;
                _map.ShowDebug = true;
            }
            else
            {
                _fpsTextBox.Hide();
                button1.Text = "Show Debug";
                ShowDebugInfo = true;
                _map.ShowDebug = false;
            }
        }

        private void _buttonExit_Click(object sender, EventArgs e)
        {

            
        }

        private void AnimalButton_Click( object sender, EventArgs e )
        {
            _viewPort.IsAnimalSelected = true;

        }

        private void catToolStripMenuItem_Click( object sender, EventArgs e )
        {
            _selectedAnimal = AnimalTexture.Cat;
        }

        private void dogToolStripMenuItem_Click( object sender, EventArgs e )
        {
            _selectedAnimal = AnimalTexture.Dog;
        }

        private void rabbitToolStripMenuItem_Click( object sender, EventArgs e )
        {
            _selectedAnimal = AnimalTexture.Rabbit;
        }

        private void lionToolStripMenuItem_Click( object sender, EventArgs e )
        {
            _selectedAnimal = AnimalTexture.Lion;
        }

        private void elephantToolStripMenuItem_Click( object sender, EventArgs e )
        {
            _selectedAnimal = AnimalTexture.Elephant;
        }

        private void cowToolStripMenuItem_Click( object sender, EventArgs e )
        {
            _selectedAnimal = AnimalTexture.Cow;
        }
        private void _buttonFollowAnimal_Click( object sender, EventArgs e )
        {
            _viewPort.IsFollowAnimalSelected = true;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveBox = new SaveFileDialog();
            saveBox.Filter = "Fichier Live It Map File(*.lim)|*.lim";
            if (saveBox.ShowDialog() == DialogResult.OK)
            {
                _map.Save(saveBox.FileName);
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog loadBox = new OpenFileDialog();
            loadBox.Filter = "Fichier Live It Map File(*.lim)|*.lim";
            if (loadBox.ShowDialog() == DialogResult.OK)
            {
                _map.Boxes = _map.Load(loadBox.FileName).Boxes;
                for( int i = 0; i < _map.Boxes.Count(); i++ )
                {
                    _map.Boxes[i].AnimalList = new List<Animal>();
                }
                _map.Vegetation = _map.Load( loadBox.FileName ).Vegetation;
                _map.Animals = new List<Animal>();
            }
        }

        private void _exitButton_Click( object sender, EventArgs e )
        {
            var confirmResult = MessageBox.Show( "Are you sure to exit ? All unsaved work will be deleted",
            "Confirm",
            MessageBoxButtons.YesNo );
            if( confirmResult == DialogResult.Yes )
            {
                t.Stop();
                fpst.Stop();
                Owner.Close();
                this.Close();                
            }
        }
        public MainViewPort ViewPort
        {
            get { return _viewPort; }
        }

        public Map Map
        {
            get { return _map; }
        }
        private void muteToolStripMenuItem_Click( object sender, EventArgs e )
        {
            _soundEnvironment.ToggleMute();
            if( _soundEnvironment.IsStopped )
            {
                muteToolStripMenuItem.Text = "Play";
            }
            else
            {
                muteToolStripMenuItem.Text = "Mute";
            }
            
        }

        private void eagleToolStripMenuItem_Click( object sender, EventArgs e )
        {
            _selectedAnimal = AnimalTexture.Eagle;
        }

        private void gazelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _selectedAnimal = AnimalTexture.Gazelle;
        }

        private void buttonMountain_Click(object sender, EventArgs e)
        {
            _selectedTexture = BoxGround.Mountain;
        }

        private void treeToolStripMenuItem_Click( object sender, EventArgs e )
        {
            _selectedVegetation = VegetationTexture.Tree;
        }

        private void vegetationToolStripMenuItem_Click( object sender, EventArgs e )
        {
            _viewPort.IsVegetationSelected = true;
        }

        private void bushToolStripMenuItem_Click( object sender, EventArgs e )
        {
            _selectedVegetation = VegetationTexture.Bush;
        }

        private void animalsToolStripMenuItem_Click( object sender, EventArgs e )
        {
            _viewPort.IsAnimalSelected = true;
        }

        private void rockToolStripMenuItem_Click( object sender, EventArgs e )
        {
            _selectedVegetation = VegetationTexture.Rock;
        }

        private void _buttonSpawnPlayer_Click( object sender, EventArgs e )
        {
            

            
            if( !_map.IsPlayer )
            {
                _viewPort.SpawnPlayer(_playerPosition);
                _map.IsPlayer = true;
                _buttonSpawnPlayer.Text = "Quit";
                _viewPort.InitSpawn();
            }
            else
            {
                _playerPosition = _map.ViewPort.Player.Position;
                _map.IsPlayer = false;
                _map.IsInCar = false;
                _buttonSpawnPlayer.Text = "Play";
                _map.ViewPort.ViewPort = new Rectangle(_map.ViewPort.ViewPort.Location, new Size(800,800));
            }
        }

        private void carButton_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            int _random = r.Next(_viewPort.ViewPort.X, _viewPort.ViewPort.Right);
            _viewPort.SpawnCar(new Point(_random, _random));
        }
    }
}
