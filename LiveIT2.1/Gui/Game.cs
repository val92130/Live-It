namespace LiveIT2._1.Gui
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;

    using LiveIT2._1.Animals;
    using LiveIT2._1.Enums;

    public partial class Form1 : Form
    {
        public Form1()
        {
            this.InitializeComponent();
        }
      
        Graphics g;
        Graphics _screenGraphic;

        bool right;
        bool left;
        bool up;
        bool down;
        bool ctrl;

        Point _playerPosition;

        EAnimalTexture selectedEAnimal;

        bool ShowDebugInfo = false;

        EBoxGround _selectedTexture;
        EVegetationTexture selectedEVegetation;

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
            this.selectedEAnimal = EAnimalTexture.Rabbit;
            this._fpsCount = 0;
            this.DoubleBuffered = true;
            this.button1.Text = "Hide Debug";
            this._selectedTexture = EBoxGround.Grass;
            this._background = new Bitmap( this.Width, this.Height );
            
            this._map = new Map( 50, 2 );
            
            this._boxWidth = this._map.BoxSize;

             this._viewPort = new MainViewPort( this._map);
             this._map.ShowDebug = true;
            this._mouseRect = new Rectangle( 0, 0, 100, 100 );

            this.g = this.CreateGraphics();  
            this._screenGraphic = Graphics.FromImage( this._background );
            this._screenGraphic.CompositingQuality = CompositingQuality.HighSpeed;
            this._screenGraphic.InterpolationMode = InterpolationMode.Low;
            

            this._selectionCursorWidth = new Size(this._boxWidth, this._boxWidth);
            this.MouseWheel += new MouseEventHandler(this.T_mouseWheel);

            this.t = new System.Windows.Forms.Timer();
            this.fpst = new System.Windows.Forms.Timer();


            this.fpst.Interval = 200;
            this.fpst.Tick += new EventHandler(this.fpst_Tick);

            this.t.Interval = 10;
            this.t.Tick += new EventHandler( this.t_Tick );

            Random r = new Random();
            int _random = r.Next(0, this._map.MapSize);
            this._playerPosition = new Point(_random, _random);


            this.fpst.Start();
            this.t.Start();
            this._soundEnvironment = new SoundEnvironment();
            this._soundEnvironment.LoadMap( this._map );
            this._viewPort.SoundEnvironment = this._soundEnvironment;
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
                if (this.ctrl)
                {
                    if (this._viewPort.MouseSelector.Width >= this._map.BoxSize)
                    {
                        increment += this._map.BoxSize;
                    }
                    this._viewPort.MouseSelector = new Rectangle(this._viewPort.MouseSelector.X, this._viewPort.MouseSelector.Y, this._viewPort.MouseSelector.Width - increment, this._viewPort.MouseSelector.Height - increment);
                }
                else
                {
                    this._viewPort.Zoom(-400);
                }


            }
            else
            {

                if (this.ctrl)
                {
                    if (this._viewPort.MouseSelector.Width <= 600)
                    {
                        increment += this._map.BoxSize;
                    }
                    this._viewPort.MouseSelector = new Rectangle(this._viewPort.MouseSelector.X, this._viewPort.MouseSelector.Y, this._viewPort.MouseSelector.Width + increment, this._viewPort.MouseSelector.Height + increment);
                }
                else
                {
                    this._viewPort.Zoom(400);
                }
            }
        }


        private void t_Tick( object sender, EventArgs e )
        {
            if( this.up ) { this.MoveRectangle( Direction.Up ); }
            if( this.left ) { this.MoveRectangle( Direction.Left ); }
            if( this.down ) { this.MoveRectangle( Direction.Down ); }
            if( this.right ) { this.MoveRectangle( Direction.Right ); }

            this.Draw();
            this.g.DrawImage(this._background, new Point(0,0));
            this._soundEnvironment.LoadBoxes(this._viewPort.BoxList);
            this._soundEnvironment.PlayAllSounds();
            this._soundEnvironment.PlayerSounds();
            Interlocked.Increment(ref this._fpsCount);

            if (this._viewPort.IsFollowMode)
            {
                this._buttonFollowAnimal.Text = "Quit following mode";
            }
            else
            {
                this._buttonFollowAnimal.Text = "Follow animal mode";
            }
                      
        }

        public enum Direction { Up, Down, Right, Left };
        public void MoveRectangle( Direction d )
        {
            int speed = 45;
            if( this._map.IsPlayer )
            {
                if( d == Direction.Down ) { this._viewPort.MoveY( this._map.ViewPort.Player.Speed ); }
                if( d == Direction.Up ) { this._viewPort.MoveY( -this._map.ViewPort.Player.Speed ); }
                if( d == Direction.Right ) { this._viewPort.MoveX( this._map.ViewPort.Player.Speed ); }
                if( d == Direction.Left ) { this._viewPort.MoveX( -this._map.ViewPort.Player.Speed ); }
            }
            else
            {
                if( d == Direction.Down ) { this._viewPort.MoveY( speed ); }
                if( d == Direction.Up ) { this._viewPort.MoveY( -speed ); }
                if( d == Direction.Right ) { this._viewPort.MoveX( speed ); }
                if( d == Direction.Left ) { this._viewPort.MoveX( -speed ); }
            }
            
        }
        public void Draw()
        {
            Rectangle _rMouse = new Rectangle( new Point( Cursor.Position.X, Cursor.Position.Y ), this._selectionCursorWidth );
            //_screenGraphic.Clear( Color.FromArgb( 255, Color.Black ) );
            this._viewPort.Draw( this._screenGraphic ); 
            
        }

        private void Form1_KeyDown( object sender, KeyEventArgs e )
        {
            if (e.KeyCode == Keys.Z) { this.up = true; if (this._map.IsPlayer) this._viewPort.Player.IsMoving = true; if (this._map.IsInCar) this._viewPort.Player.Car.IsMoving = true; }
            if (e.KeyCode == Keys.Q) { this.left = true; if (this._map.IsPlayer) this._viewPort.Player.IsMoving = true; if (this._map.IsInCar) this._viewPort.Player.Car.IsMoving = true; }
            if (e.KeyCode == Keys.S) { this.down = true; if (this._map.IsPlayer) this._viewPort.Player.IsMoving = true; if (this._map.IsInCar) this._viewPort.Player.Car.IsMoving = true; }
            if (e.KeyCode == Keys.D) { this.right = true; if (this._map.IsPlayer) this._viewPort.Player.IsMoving = true; if (this._map.IsInCar) this._viewPort.Player.Car.IsMoving = true; }
            if( e.KeyCode == Keys.E ) {
                
                if( this._map.IsInCar )
                {
                    this._viewPort.TryEnter = false;
                    this._map.ExitCar();
                    
                }
                else
                {
                    this._viewPort.TryEnter = true;
                }
            }
            if( e.KeyCode == Keys.ControlKey ) { this.ctrl = true;  }
        }

        private void Form1_KeyUp( object sender, KeyEventArgs e )
        {
            if (e.KeyCode == Keys.Z) { this.up = false; if (this._map.IsPlayer) this._viewPort.Player.IsMoving = false; if (this._map.IsInCar) this._viewPort.Player.Car.IsMoving = false; }
            if (e.KeyCode == Keys.Q) { this.left = false; if (this._map.IsPlayer) this._viewPort.Player.IsMoving = false; if (this._map.IsInCar) this._viewPort.Player.Car.IsMoving = false; }
            if (e.KeyCode == Keys.S) { this.down = false; if (this._map.IsPlayer) this._viewPort.Player.IsMoving = false; if (this._map.IsInCar) this._viewPort.Player.Car.IsMoving = false; }
            if (e.KeyCode == Keys.D) { this.right = false; if (this._map.IsPlayer) this._viewPort.Player.IsMoving = false; if (this._map.IsInCar) this._viewPort.Player.Car.IsMoving = false; }
            if( e.KeyCode == Keys.E ) { this._viewPort.TryEnter = false; }
            if( e.KeyCode == Keys.ControlKey ) { this.ctrl = false; }
            if (e.KeyCode == Keys.R)
            {
                if (this._map.IsInCar)
                {
                    this._viewPort.Player.Car.ToggleRadio();
                }
            }
        }

        private void Form1_MouseClick( object sender, MouseEventArgs e )
        {
            this._viewPort.HasClicked = true;

            if (this._viewPort.IsAnimalSelected)
            {
                this._viewPort.CreateAnimal(this.selectedEAnimal);
            }
            else if(this._viewPort.IsChangeTextureSelected)
            {
                this._viewPort.ChangeTexture(this._selectedTexture);
            }
            else if(this._viewPort.IsVegetationSelected)
            {
                this._viewPort.CreateVegetation( this.selectedEVegetation );
            }
            else if( this._viewPort.IsFillTextureSelected )
            {
                this._viewPort.ChangeTexture( this._selectedTexture );
            }
        }

        private void _waterButton_Click( object sender, EventArgs e )
        {
            this._selectedTexture = EBoxGround.Water;
        }

        private void _dirtButton_Click( object sender, EventArgs e )
        {
            this._selectedTexture = EBoxGround.Forest;
        }

        private void _snowButton_Click( object sender, EventArgs e )
        {
            this._selectedTexture = EBoxGround.Snow;
        }

        private void _desertButton_Click( object sender, EventArgs e )
        {
            this._selectedTexture = EBoxGround.Desert;
        }

        private void _grassButton_Click( object sender, EventArgs e )
        {
            this._selectedTexture = EBoxGround.Grass;
        }

        private void _changeTextureButton_Click(object sender, EventArgs e)
        {
            this._viewPort.IsChangeTextureSelected = true;
        }

        private void _fillTextureButton_Click(object sender, EventArgs e)
        {
            this._viewPort.IsFillTextureSelected = true;
        }


        long _averageFps;
        long _totalFps;
        long _count = 0;
        private void fpst_Tick(object sender, EventArgs e)
        {
            this._count++;
            this._fps = this.GetFps();
            this._totalFps += this._fps;
            this._fpsTextBox.Text = "FPS : " + this._fps.ToString() + "\n Avg :" + this._averageFps.ToString() + "\n\nTotal Frames :" + this._totalFps.ToString() + "\nSelected Texture :" + this._selectedTexture.ToString() ;
            this._fpsTextBox.ForeColor = this._fps >= 20 ? Color.Green : Color.Red;
            this._averageFps = this._totalFps / this._count;
        }
        int GetFps()
        {
            double secondsElapsed = (DateTime.Now - this._lastCheckTime).TotalSeconds;
            long count = Interlocked.Exchange(ref this._fpsCount, 0);
            double fps = count / secondsElapsed;
            this._lastCheckTime = DateTime.Now;
            return (int)fps;
        }

        private void button1_Click( object sender, EventArgs e )
        {
            if( this.ShowDebugInfo )
            {
                this._fpsTextBox.Show();
                this.button1.Text = "Hide Debug";
                this.ShowDebugInfo = false;
                this._map.ShowDebug = true;
            }
            else
            {
                this._fpsTextBox.Hide();
                this.button1.Text = "Show Debug";
                this.ShowDebugInfo = true;
                this._map.ShowDebug = false;
            }

            
        }

        private void _buttonExit_Click(object sender, EventArgs e)
        {

            
        }

        private void AnimalButton_Click( object sender, EventArgs e )
        {
            this._viewPort.IsAnimalSelected = true;

        }

        private void catToolStripMenuItem_Click( object sender, EventArgs e )
        {
            this.selectedEAnimal = EAnimalTexture.Cat;
        }

        private void dogToolStripMenuItem_Click( object sender, EventArgs e )
        {
            this.selectedEAnimal = EAnimalTexture.Dog;
        }

        private void rabbitToolStripMenuItem_Click( object sender, EventArgs e )
        {
            this.selectedEAnimal = EAnimalTexture.Rabbit;
        }

        private void lionToolStripMenuItem_Click( object sender, EventArgs e )
        {
            this.selectedEAnimal = EAnimalTexture.Lion;
        }

        private void elephantToolStripMenuItem_Click( object sender, EventArgs e )
        {
            this.selectedEAnimal = EAnimalTexture.Elephant;
        }

        private void cowToolStripMenuItem_Click( object sender, EventArgs e )
        {
            this.selectedEAnimal = EAnimalTexture.Cow;
        }
        private void _buttonFollowAnimal_Click( object sender, EventArgs e )
        {
            if (this._viewPort.IsFollowAnimalSelected == true)
            {
                this._viewPort.IsFollowAnimalSelected = false;
            }
            else
            {
                this._viewPort.IsFollowAnimalSelected = true;
                
            }
            
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveBox = new SaveFileDialog();
            saveBox.Filter = "Fichier Live It Map File(*.lim)|*.lim";
            if (saveBox.ShowDialog() == DialogResult.OK)
            {
                this._map.Save(saveBox.FileName);
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog loadBox = new OpenFileDialog();
            loadBox.Filter = "Fichier Live It Map File(*.lim)|*.lim";
            if (loadBox.ShowDialog() == DialogResult.OK)
            {
                this._map.Boxes = this._map.Load(loadBox.FileName).Boxes;
                for( int i = 0; i < this._map.Boxes.Count(); i++ )
                {
                    this._map.Boxes[i].AnimalList = new List<Animal>();
                }
                this._map.Vegetation = this._map.Load( loadBox.FileName ).Vegetation;
                this._map.Animals = new List<Animal>();
            }
        }

        private void _exitButton_Click( object sender, EventArgs e )
        {
            var confirmResult = MessageBox.Show( "Are you sure to exit ? All unsaved work will be deleted",
            "Confirm",
            MessageBoxButtons.YesNo );
            if( confirmResult == DialogResult.Yes )
            {
                this.t.Stop();
                this.fpst.Stop();
                this.Owner.Close();
                this.Close();                
            }
        }
        public MainViewPort ViewPort
        {
            get { return this._viewPort; }
        }

        public Map Map
        {
            get { return this._map; }
        }
        private void muteToolStripMenuItem_Click( object sender, EventArgs e )
        {
            this._soundEnvironment.ToggleMute();
            if( this._soundEnvironment.IsStopped )
            {
                this.muteToolStripMenuItem.Text = "Play";
            }
            else
            {
                this.muteToolStripMenuItem.Text = "Mute";
            }
            
        }

        private void eagleToolStripMenuItem_Click( object sender, EventArgs e )
        {
            this.selectedEAnimal = EAnimalTexture.Eagle;
        }

        private void gazelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.selectedEAnimal = EAnimalTexture.Gazelle;
        }

        private void buttonMountain_Click(object sender, EventArgs e)
        {
            this._selectedTexture = EBoxGround.Mountain;
        }

        private void treeToolStripMenuItem_Click( object sender, EventArgs e )
        {
            this.selectedEVegetation = EVegetationTexture.Tree;
        }

        private void vegetationToolStripMenuItem_Click( object sender, EventArgs e )
        {
            this._viewPort.IsVegetationSelected = true;
        }

        private void bushToolStripMenuItem_Click( object sender, EventArgs e )
        {
            this.selectedEVegetation = EVegetationTexture.Bush;
        }

        private void animalsToolStripMenuItem_Click( object sender, EventArgs e )
        {
            this._viewPort.IsAnimalSelected = true;
        }

        private void rockToolStripMenuItem_Click( object sender, EventArgs e )
        {
            this.selectedEVegetation = EVegetationTexture.Rock;
        }

        private void _buttonSpawnPlayer_Click( object sender, EventArgs e )
        {
            

            
            if( !this._map.IsPlayer )
            {
                this._viewPort.SpawnPlayer(this._playerPosition);
                this._map.IsPlayer = true;
                this._buttonSpawnPlayer.Text = "Quit";
                this._viewPort.InitSpawn();
            }
            else
            {
                this._playerPosition = this._map.ViewPort.Player.Position;
                this._map.IsPlayer = false;
                this._map.IsInCar = false;
                this._buttonSpawnPlayer.Text = "Play";
                this._map.ViewPort.ViewPort = new Rectangle(this._map.ViewPort.ViewPort.Location, new Size(800,800));
            }
        }

        private void carButton_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            int _random = r.Next(this._viewPort.ViewPort.X, this._viewPort.ViewPort.Right);
            this._viewPort.SpawnCar(new Point(_random, _random));
        }

        private void tank_button_Click(object sender, EventArgs e)
        {
            Random r2 = new Random();
            int _random2 = r2.Next(this._viewPort.ViewPort.X, this._viewPort.ViewPort.Right);
            this._viewPort.SpawnTank(new Point(_random2, _random2));
        }

        private void sportCarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            int _random = r.Next(this._viewPort.ViewPort.X, this._viewPort.ViewPort.Right);
            this._viewPort.SpawnCar(new Point(_random, _random));
        }

        private void tankToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Random r2 = new Random();
            int _random2 = r2.Next(this._viewPort.ViewPort.X, this._viewPort.ViewPort.Right);
            this._viewPort.SpawnTank(new Point(_random2, _random2));
        }
    }
}
