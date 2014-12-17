// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Game.cs" company="">
//   
// </copyright>
// <summary>
//   The form 1.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LiveIT2._1.Gui
{
    using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using LiveIT2._1.Animals;
using LiveIT2._1.Animation;
using LiveIT2._1.Enums;
using LiveIT2._1.Terrain;
using LiveIT2._1.Utils;
using LiveIT2._1.Viewport;
using Timer = System.Windows.Forms.Timer;

    /// <summary>
    /// The form 1.
    /// </summary>
    public partial class Form1 : Form
    {
        #region Fields

        /// <summary>
        /// The show debug info.
        /// </summary>
        private bool ShowDebugInfo;

        /// <summary>
        /// The _average fps.
        /// </summary>
        private long _averageFps;

        /// <summary>
        /// The _background.
        /// </summary>
        private Bitmap _background;

        /// <summary>
        /// The _box width.
        /// </summary>
        private int _boxWidth;

        private GameTime _gameTime;

        /// <summary>
        /// Define if the game is playing or not
        /// </summary>
        private bool _isPlaying;

        /// <summary>
        /// The _count.
        /// </summary>
        private long _count;

        /// <summary>
        /// The _fps.
        /// </summary>
        private int _fps;

        /// <summary>
        /// The _fps count.
        /// </summary>
        private long _fpsCount;

        /// <summary>
        /// The _last check time.
        /// </summary>
        private DateTime _lastCheckTime = DateTime.Now;

        /// <summary>
        /// The _map.
        /// </summary>
        private Map _map;

        /// <summary>
        /// The _mouse rect.
        /// </summary>
        private Rectangle _mouseRect;

        /// <summary>
        /// The _player position.
        /// </summary>
        private Point _playerPosition;

        /// <summary>
        /// The _screen graphic.
        /// </summary>
        private Graphics _screenGraphic;

        /// <summary>
        /// The _selected texture.
        /// </summary>
        private EBoxGround _selectedTexture;

        private Stopwatch _countDown;

        /// <summary>
        /// The _selection cursor width.
        /// </summary>
        private Size _selectionCursorWidth;

        /// <summary>
        /// The _sound environment.
        /// </summary>
        private SoundEnvironment _soundEnvironment;

        /// <summary>
        /// The _total fps.
        /// </summary>
        private long _totalFps;

        /// <summary>
        /// The _view port.
        /// </summary>
        private MainViewPort _viewPort;

        /// <summary>
        /// The ctrl.
        /// </summary>
        private bool ctrl;

        /// <summary>
        /// The down.
        /// </summary>
        private bool down;

        /// <summary>
        /// The fpst.
        /// </summary>
        private Timer fpst;

        /// <summary>
        /// The g.
        /// </summary>
        private Graphics g;

        /// <summary>
        /// The left.
        /// </summary>
        private bool left;

        /// <summary>
        /// The right.
        /// </summary>
        private bool right;

        /// <summary>
        /// The selected e animal.
        /// </summary>
        private EAnimalTexture selectedEAnimal;

        /// <summary>
        /// The selected e vegetation.
        /// </summary>
        private EmapElements selectedEVegetation;

        /// <summary>
        /// The t.
        /// </summary>
        private Timer t;

        /// <summary>
        /// The up.
        /// </summary>
        private bool up;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Enums

        /// <summary>
        /// The direction.
        /// </summary>
        public enum Direction
        {
            /// <summary>
            /// The up.
            /// </summary>
            Up, 

            /// <summary>
            /// The down.
            /// </summary>
            Down, 

            /// <summary>
            /// The right.
            /// </summary>
            Right, 

            /// <summary>
            /// The left.
            /// </summary>
            Left
        };

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the map.
        /// </summary>
        public Map Map
        {
            get
            {
                return this._map;
            }
        }

        /// <summary>
        /// Gets the view port.
        /// </summary>
        public MainViewPort ViewPort
        {
            get
            {
                return this._viewPort;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The draw.
        /// </summary>
        public void Draw()
        {
            var _rMouse = new Rectangle(new Point(Cursor.Position.X, Cursor.Position.Y), this._selectionCursorWidth);

            // _screenGraphic.Clear( Color.FromArgb( 255, Color.Black ) );
            this._viewPort.Draw(this._screenGraphic);
        }

        /// <summary>
        /// The load map.
        /// </summary>
        /// <param name="map">
        /// The map.
        /// </param>
        public void LoadMap(Map map)
        {
            this._map = map;
        }

        /// <summary>
        /// The move rectangle.
        /// </summary>
        /// <param name="d">
        /// The d.
        /// </param>
        public void MoveRectangle(Direction d)
        {
            int speed = 45;
            if (this._map.IsPlayer)
            {
                if (d == Direction.Down)
                {
                    this._viewPort.MoveY(this._map.ViewPort.Player.Speed + 1);
                }

                if (d == Direction.Up)
                {
                    this._viewPort.MoveY(-this._map.ViewPort.Player.Speed);
                }

                if (d == Direction.Right)
                {
                    
                    this._viewPort.MoveX(this._map.ViewPort.Player.Speed + 1);
                }

                if (d == Direction.Left)
                {
                    this._viewPort.MoveX(-this._map.ViewPort.Player.Speed);
                }
                if( _map.IsInCar )
                {
                    _viewPort.Player.Car.Speed = Lerp( _viewPort.Player.Car.MaxSpeed, _viewPort.Player.Car.Speed, _viewPort.Player.Car.Acceleration );
                }
                _viewPort.Player.Speed = Lerp( _viewPort.Player.MaxSpeed, _viewPort.Player.Speed, _viewPort.Player.Acceleration );
            }
            else
            {
                if (d == Direction.Down)
                {
                    this._viewPort.MoveY(speed);
                }

                if (d == Direction.Up)
                {
                    this._viewPort.MoveY(-speed);
                }

                if (d == Direction.Right)
                {
                    this._viewPort.MoveX(speed);
                }

                if (d == Direction.Left)
                {
                    this._viewPort.MoveX(-speed);
                }
            }
        }

        #endregion

        #region Methods


        /// <summary>
        /// The form 1_ load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Form1_Load(object sender, EventArgs e)
        {
            _gameTime = new GameTime();
            _isPlaying = true;
            this.selectedEAnimal = EAnimalTexture.Rabbit;
            this._fpsCount = 0;
            this.DoubleBuffered = true;
            this.button1.Text = "Hide Debug";
            this._selectedTexture = EBoxGround.Grass;
            this._background = new Bitmap(this.Width, this.Height);

            this._map = new Map(50, 2);

            this._boxWidth = this._map.BoxSize;

            this._viewPort = new MainViewPort(this._map);
            this._map.ShowDebug = true;
            this._mouseRect = new Rectangle(0, 0, 100, 100);

            this.g = this.CreateGraphics();
            this._screenGraphic = Graphics.FromImage(this._background);
            this._screenGraphic.CompositingQuality = CompositingQuality.HighSpeed;
            this._screenGraphic.InterpolationMode = InterpolationMode.Low;

            this._selectionCursorWidth = new Size(this._boxWidth, this._boxWidth);
            this.MouseWheel += this.T_mouseWheel;

            this.t = new Timer();
            this.fpst = new Timer();

            this.fpst.Interval = 200;
            this.fpst.Tick += this.fpst_Tick;

            this.t.Interval = 10;
            this.t.Tick += this.t_Tick;

            this._gameMenu.Width = 600;
            this._gameMenu.Height = 600;
            this._gameMenu.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - _gameMenu.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2 - _gameMenu.Height / 2);

            var r = new Random();
            int _random = r.Next(0, this._map.MapSize);
            this._playerPosition = new Point(_random, _random);

            this.fpst.Start();
            this.t.Start();
            this._soundEnvironment = new SoundEnvironment();
            this._soundEnvironment.LoadMap(this._map);
            this._viewPort.SoundEnvironment = this._soundEnvironment;

            _countDown = new Stopwatch();
            _countDown.Start();

            _gameMenu.Draggable(true);
        }

        /// <summary>
        /// Update and draw method, add game logic here
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void t_Tick(object sender, EventArgs e)
        {
            _gameTime.Update();

            _viewPort.CameraSmoothness = trackBar1.Value * 10;

            _cameraSmoothnessLabel.Text = _viewPort.CameraSmoothness.ToString();
            if (!_map.IsPaused)
            {
                if (this.up)
                {
                    this.MoveRectangle(Direction.Up);
                }

                if (this.left)
                {
                    this.MoveRectangle(Direction.Left);
                }

                if (this.down)
                {
                    this.MoveRectangle(Direction.Down);
                }

                if (this.right)
                {
                    this.MoveRectangle(Direction.Right);
                }

                if (_map.IsPlayer)
                {
                    if (_viewPort.Player != null)
                    {
                        if (!up && !down && !left && !right)
                        {
                            if (_map.IsInCar)
                            {
                                _viewPort.Player.Car.Speed = 0;
                            }
                            _viewPort.Player.Speed = 0;
                        }
                        else
                        {
                            _viewPort.Player.IsMoving = true;
                        }
                    }

                }


                this.Draw();
                this.g.DrawImage(this._background, new Point(0, 0));
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

                ShowStatsInfo();

                _map.Update();

                if (!_map.IsPlayer)
                {
                    _playMedicButton.Hide();
                }
                else
                {
                    _playMedicButton.Show();
                }
            }

        }


        /// <summary>
        /// The animal button_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void AnimalButton_Click(object sender, EventArgs e)
        {
            this._viewPort.IsAnimalSelected = true;
        }

        public int Lerp( int flGoal, int flCurrent, int dt )
        {
            int flDifference = flGoal - flCurrent;

            if( flDifference > dt )
            {
                return flCurrent + dt;
            }
            if( flDifference < -dt )
            {
                return flCurrent - dt;
            }
            return flGoal;
        }

        /// <summary>
        /// The form 1_ key down.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Z)
            {
                this.up = true;
                if (this._map.IsPlayer)
                {
                    this._viewPort.Player.IsMoving = true;
                }

                if (this._map.IsInCar)
                {
                    this._viewPort.Player.Car.IsMoving = true;
                }
            }

            if (e.KeyCode == Keys.Q)
            {
                this.left = true;
                if (this._map.IsPlayer)
                {
                    this._viewPort.Player.IsMoving = true;
                }

                if (this._map.IsInCar)
                {
                    this._viewPort.Player.Car.IsMoving = true;
                }
            }

            if (e.KeyCode == Keys.S)
            {
                this.down = true;
                if (this._map.IsPlayer)
                {
                    this._viewPort.Player.IsMoving = true;
                }

                if (this._map.IsInCar)
                {
                    this._viewPort.Player.Car.IsMoving = true;
                }
            }

            if (e.KeyCode == Keys.D)
            {
                this.right = true;
                if (this._map.IsPlayer)
                {
                    this._viewPort.Player.IsMoving = true;
                }

                if (this._map.IsInCar)
                {
                    this._viewPort.Player.Car.IsMoving = true;
                }
            }

            if (e.KeyCode == Keys.E)
            {
                if (this._map.IsInCar)
                {
                    this._viewPort.TryEnter = false;
                    this._map.ExitCar();
                }
                else
                {
                    this._viewPort.TryEnter = true;
                }
            }

            if( e.KeyCode == Keys.F )
            {
                this._viewPort.Shoot = true;
            }

            if (e.KeyCode == Keys.ControlKey)
            {
                this.ctrl = true;
            }
        }

        /// <summary>
        /// The form 1_ key up.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Z)
            {
                this.up = false;
                if (this._map.IsPlayer)
                {
                    this._viewPort.Player.IsMoving = false;
                }

                if (this._map.IsInCar)
                {
                    this._viewPort.Player.Car.IsMoving = false;
                }
            }

            if (e.KeyCode == Keys.Q)
            {
                this.left = false;
                if (this._map.IsPlayer)
                {
                    this._viewPort.Player.IsMoving = false;
                }

                if (this._map.IsInCar)
                {
                    this._viewPort.Player.Car.IsMoving = false;
                }
            }

            if (e.KeyCode == Keys.S)
            {
                this.down = false;
                if (this._map.IsPlayer)
                {
                    this._viewPort.Player.IsMoving = false;
                }

                if (this._map.IsInCar)
                {
                    this._viewPort.Player.Car.IsMoving = false;
                }
            }

            if (e.KeyCode == Keys.D)
            {
                this.right = false;
                if (this._map.IsPlayer)
                {
                    this._viewPort.Player.IsMoving = false;
                }

                if (this._map.IsInCar)
                {
                    this._viewPort.Player.Car.IsMoving = false;
                }
            }

            if (e.KeyCode == Keys.E)
            {
                this._viewPort.TryEnter = false;
            }

            if (e.KeyCode == Keys.ControlKey)
            {
                this.ctrl = false;
            }

            if (e.KeyCode == Keys.R)
            {
                if (this._map.IsInCar)
                {
                    this._viewPort.Player.Car.ToggleRadio();
                }
            }

            if( e.KeyCode == Keys.F )
            {
                this._viewPort.Shoot = false;
            }

            //if( e.KeyCode != Keys.Z && e.KeyCode != Keys.S && e.KeyCode != Keys.Q && e.KeyCode != Keys.D )
            //{
            //    _viewPort.Player.Speed = 0;
            //}
            
        }


        /// <summary>
        /// The form 1_ mouse click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            this._viewPort.HasClicked = true;

            if (this._viewPort.IsAnimalSelected)
            {
                this._viewPort.CreateAnimal(this.selectedEAnimal, _viewPort.AnimalCursor);
            }
            else if (this._viewPort.IsChangeTextureSelected)
            {
                this._viewPort.ChangeTexture(this._selectedTexture);
            }
            else if (this._viewPort.IsMapElementSelected)
            {
                this._viewPort.CreateMapElement(this.selectedEVegetation);
            }
            else if (this._viewPort.IsFillTextureSelected)
            {
                this._viewPort.ChangeTexture(this._selectedTexture);
            }
        }

        /// <summary>
        /// The get fps.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private int GetFps()
        {
            double secondsElapsed = (DateTime.Now - this._lastCheckTime).TotalSeconds;
            long count = Interlocked.Exchange(ref this._fpsCount, 0);
            double fps = count / secondsElapsed;
            this._lastCheckTime = DateTime.Now;
            return (int)fps;
        }

        /// <summary>
        /// The t_mouse wheel.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
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

                    this._viewPort.MouseSelector = new Rectangle(
                        this._viewPort.MouseSelector.X, 
                        this._viewPort.MouseSelector.Y, 
                        this._viewPort.MouseSelector.Width - increment, 
                        this._viewPort.MouseSelector.Height - increment);
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

                    this._viewPort.MouseSelector = new Rectangle(
                        this._viewPort.MouseSelector.X, 
                        this._viewPort.MouseSelector.Y, 
                        this._viewPort.MouseSelector.Width + increment, 
                        this._viewPort.MouseSelector.Height + increment);
                }
                else
                {
                    this._viewPort.Zoom(400);
                }
            }
        }

        /// <summary>
        /// The _button exit_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void _buttonExit_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// The _button follow animal_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void _buttonFollowAnimal_Click(object sender, EventArgs e)
        {
            if (this._viewPort.IsFollowAnimalSelected)
            {
                this._viewPort.IsFollowAnimalSelected = false;
            }
            else
            {
                this._viewPort.IsFollowAnimalSelected = true;
            }
        }

        /// <summary>
        /// The _button spawn player_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void _buttonSpawnPlayer_Click(object sender, EventArgs e)
        {
            if (!this._map.IsPlayer)
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
                this._buttonSpawnPlayer.Text = "Free Mode";
                this._map.ViewPort.ViewPort = new Rectangle(this._map.ViewPort.ViewPort.Location, new Size(800, 800));
            }
        }

        /// <summary>
        /// The _change texture button_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void _changeTextureButton_Click(object sender, EventArgs e)
        {
            this._viewPort.IsChangeTextureSelected = true;
        }

        /// <summary>
        /// The _desert button_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void _desertButton_Click(object sender, EventArgs e)
        {
            this._selectedTexture = EBoxGround.Desert;
        }

        /// <summary>
        /// The _dirt button_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void _dirtButton_Click(object sender, EventArgs e)
        {
            this._selectedTexture = EBoxGround.Forest;
        }

        /// <summary>
        /// The _exit button_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void _exitButton_Click(object sender, EventArgs e)
        {
            DialogResult confirmResult = MessageBox.Show(
                "Are you sure to exit ? All unsaved work will be deleted", 
                "Confirm", 
                MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                this.t.Stop();
                this.fpst.Stop();
                this.Owner.Close();
                this.Close();
            }
        }

        /// <summary>
        /// The _fill texture button_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void _fillTextureButton_Click(object sender, EventArgs e)
        {
            this._viewPort.IsFillTextureSelected = true;
        }

        /// <summary>
        /// The _grass button_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void _grassButton_Click(object sender, EventArgs e)
        {
            this._selectedTexture = EBoxGround.Grass;
        }

        /// <summary>
        /// The _snow button_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void _snowButton_Click(object sender, EventArgs e)
        {
            this._selectedTexture = EBoxGround.Snow;
        }

        /// <summary>
        /// The _water button_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void _waterButton_Click(object sender, EventArgs e)
        {
            this._selectedTexture = EBoxGround.Water;
        }

        /// <summary>
        /// The animals tool strip menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void animalsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._viewPort.IsAnimalSelected = true;
        }

        /// <summary>
        /// The bush tool strip menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void bushToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.selectedEVegetation = EmapElements.Bush;
        }

        /// <summary>
        /// The button 1_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.ShowDebugInfo)
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

        /// <summary>
        /// The button mountain_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void buttonMountain_Click(object sender, EventArgs e)
        {
            this._selectedTexture = EBoxGround.Mountain;
        }

        /// <summary>
        /// The car button_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void carButton_Click(object sender, EventArgs e)
        {
            var r = new Random();
            int _random = r.Next(this._viewPort.ViewPort.X, this._viewPort.ViewPort.Right);
            this._viewPort.SpawnCar(new Point(_random, _random));
        }

        /// <summary>
        /// The cat tool strip menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void catToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.selectedEAnimal = EAnimalTexture.Cat;
        }

        /// <summary>
        /// The cow tool strip menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void cowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.selectedEAnimal = EAnimalTexture.Cow;
        }

        /// <summary>
        /// The dog tool strip menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void dogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.selectedEAnimal = EAnimalTexture.Dog;
        }

        /// <summary>
        /// The eagle tool strip menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void eagleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.selectedEAnimal = EAnimalTexture.Eagle;
        }

        /// <summary>
        /// The elephant tool strip menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void elephantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.selectedEAnimal = EAnimalTexture.Elephant;
        }

        /// <summary>
        /// The fpst_ tick.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void fpst_Tick(object sender, EventArgs e)
        {
            this._count++;
            this._fps = this.GetFps();
            this._totalFps += this._fps;
            this._fpsTextBox.Text = "FPS : " + this._fps + "\n Avg :" + this._averageFps + "\n\nTotal Frames :"
                                    + this._totalFps + "\nSelected Texture :" + this._selectedTexture;
            this._fpsTextBox.ForeColor = this._fps >= 20 ? Color.Green : Color.Red;
            this._averageFps = this._totalFps / this._count;
        }

        /// <summary>
        /// The gazelle tool strip menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void gazelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.selectedEAnimal = EAnimalTexture.Gazelle;
        }

        /// <summary>
        /// The lion tool strip menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void lionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.selectedEAnimal = EAnimalTexture.Lion;
        }

        /// <summary>
        /// The load tool strip menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var loadBox = new OpenFileDialog();
            loadBox.Filter = "Fichier Live It Map File(*.lim)|*.lim";
            if (loadBox.ShowDialog() == DialogResult.OK)
            {
                LoadMap(loadBox.FileName);
            }
        }

        public void LoadMap(String mapPath)
        {
            this._map = this._map.Load(mapPath);
            _viewPort = new MainViewPort(_map);
            _viewPort.BoxList = _map.BoxList;
            _viewPort.SoundEnvironment = this._soundEnvironment;
            this._soundEnvironment.LoadMap(_map);
        }

        /// <summary>
        /// The mute tool strip menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void muteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._soundEnvironment.ToggleMute();
            if (this._soundEnvironment.IsStopped)
            {
                this.muteToolStripMenuItem.Text = "Play";
            }
            else
            {
                this.muteToolStripMenuItem.Text = "Mute";
            }
        }

        /// <summary>
        /// The rabbit tool strip menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void rabbitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.selectedEAnimal = EAnimalTexture.Rabbit;
        }

        /// <summary>
        /// The rock tool strip menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void rockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.selectedEVegetation = EmapElements.Rock;
        }

        /// <summary>
        /// The save tool strip menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var saveBox = new SaveFileDialog();
            saveBox.Filter = "Fichier Live It Map File(*.lim)|*.lim";
            if (saveBox.ShowDialog() == DialogResult.OK)
            {
                this._map.Save(saveBox.FileName);
            }
        }

        /// <summary>
        /// The sport car tool strip menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void sportCarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var r = new Random();
            int _random = r.Next(this._viewPort.ViewPort.X, this._viewPort.ViewPort.Right);
            this._viewPort.SpawnCar(new Point(_random, _random));
        }


        /// <summary>
        /// The tank tool strip menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void tankToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var r2 = new Random();
            int _random2 = r2.Next(this._viewPort.ViewPort.X, this._viewPort.ViewPort.Right);
            this._viewPort.SpawnTank(new Point(_random2, _random2));
        }

        /// <summary>
        /// The tank_button_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void tank_button_Click(object sender, EventArgs e)
        {
            var r2 = new Random();
            int _random2 = r2.Next(this._viewPort.ViewPort.X, this._viewPort.ViewPort.Right);
            this._viewPort.SpawnTank(new Point(_random2, _random2));
        }

        /// <summary>
        /// The tree tool strip menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void treeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.selectedEVegetation = EmapElements.Tree;
        }

        /// <summary>
        /// The vegetation tool strip menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void vegetationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._viewPort.IsMapElementSelected = true;
        }

        #endregion

        private void desertToolStripMenuItem_Click( object sender, EventArgs e )
        {
            _selectedTexture = EBoxGround.Desert;
        }

        private void forestToolStripMenuItem_Click( object sender, EventArgs e )
        {
            _selectedTexture = EBoxGround.Forest;
        }

        private void grassToolStripMenuItem_Click( object sender, EventArgs e )
        {
            _selectedTexture = EBoxGround.Grass;
        }

        private void mountainToolStripMenuItem_Click( object sender, EventArgs e )
        {
            _selectedTexture = EBoxGround.Mountain;
        }

        private void snowToolStripMenuItem_Click( object sender, EventArgs e )
        {
            _selectedTexture = EBoxGround.Snow;
        }

        private void waterToolStripMenuItem_Click( object sender, EventArgs e )
        {
            _selectedTexture = EBoxGround.Water;
        }

        private void texturesToolStripMenuItem_Click( object sender, EventArgs e )
        {
            _viewPort.IsChangeTextureSelected = true;
        }

        private void _playMedicButton_Click( object sender, EventArgs e )
        {
            if( _map.IsPlayer )
            {
                if( _map.IsMedic )
                {
                    _map.IsMedic = false;
                    _playMedicButton.Text = "Play Medic";
                }
                else
                {
                    _map.IsMedic = true;
                    _playMedicButton.Text = "Quit Medic mode";
                }
            }

        }

        private void pauseToolStripMenuItem_Click( object sender, EventArgs e )
        {
            if( !_map.IsPaused )
            {
                _map.IsPaused = true;
                pauseToolStripMenuItem.Text = "Play";
            }
            else
            {
                _map.IsPaused = false;
                pauseToolStripMenuItem.Text = "Pause";
            }
        }

        private void menuToolStripMenuItem_Click( object sender, EventArgs e )
        {
            if( _gameMenu.Visible == true )
            {
                trackBar1.Enabled = false;
                _gameMenu.Hide();
            }
            else
            {
                trackBar1.Enabled = true;
                _gameMenu.Show();
            }
        }

        private void ShowStatsInfo()
        {
            _labelAnimalsAlive.Text = _map.GetLivingAnimals.ToString() ;
            _labelDeadAnimals.Text = _map.DeadAnimals.ToString();
            _labelTimePlayed.Text = _countDown.Elapsed.ToString();
            _labelSavedAnimals.Text = _map.SavedAnimals.ToString() ;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            
        }

        private void houseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.selectedEVegetation = EmapElements.House;
        }

        private void floorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _selectedTexture = EBoxGround.Floor;
        }

        private void wallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _selectedTexture = EBoxGround.Wall;
        }

        private void tileFloorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _selectedTexture = EBoxGround.Floor2;
        }

    }
}