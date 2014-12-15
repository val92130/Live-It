// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SoundEnvironment.cs" company="">
//   
// </copyright>
// <summary>
//   The sound environment.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LiveIT2._1.Animation
{
    using System.Collections.Generic;

    using LiveIT2._1.Enums;
    using LiveIT2._1.Terrain;

    using NAudio.Wave;

    /// <summary>
    ///     The sound environment.
    /// </summary>
    public partial class SoundEnvironment
    {
        #region Fields

        /// <summary>
        ///     The _box grounds.
        /// </summary>
        private readonly List<EBoxGround> _boxGrounds;

        /// <summary>
        ///     The _player box grounds.
        /// </summary>
        private readonly List<EBoxGround> _playerBoxGrounds;

        /// <summary>
        ///     The wave out back ground.
        /// </summary>
        private readonly WaveOut waveOutBackGround;

        /// <summary>
        ///     The wave out car idle.
        /// </summary>
        private readonly WaveOut waveOutCarIdle;

        /// <summary>
        ///     The wave out car running.
        /// </summary>
        private readonly WaveOut waveOutCarRunning;

        /// <summary>
        ///     The wave out car starting.
        /// </summary>
        private readonly WaveOut waveOutCarStarting;

        /// <summary>
        ///     The wave out radio music electro.
        /// </summary>
        private readonly WaveOut waveOutRadioMusicElectro;

        /// <summary>
        ///     The wave out radio music rap.
        /// </summary>
        private readonly WaveOut waveOutRadioMusicRap;

        /// <summary>
        ///     The wave out radio music trap.
        /// </summary>
        private readonly WaveOut waveOutRadioMusicTrap;

        /// <summary>
        ///     The wave out rain.
        /// </summary>
        private readonly WaveOut waveOutRain;

        /// <summary>
        ///     The wave out step.
        /// </summary>
        private readonly WaveOut waveOutStep;

        /// <summary>
        ///     The wave out step_water.
        /// </summary>
        private readonly WaveOut waveOutStep_water;

        /// <summary>
        ///     The wave out water.
        /// </summary>
        private readonly WaveOut waveOutWater;

        /// <summary>
        ///     The _boxes.
        /// </summary>
        private List<Box> _boxes;

        /// <summary>
        ///     The _is raining.
        /// </summary>
        private bool _isRaining;

        /// <summary>
        ///     The _is stopped.
        /// </summary>
        private bool _isStopped;

        /// <summary>
        ///     The _is water.
        /// </summary>
        private bool _isWater;

        /// <summary>
        ///     The _map.
        /// </summary>
        private Map _map;

        /// <summary>
        ///     The wave out radio actual.
        /// </summary>
        private WaveOut waveOutRadioActual;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SoundEnvironment" /> class.
        /// </summary>
        public SoundEnvironment()
        {
            var readerBackGround = new WaveFileReader("../../../sounds/background.wav");
            var loopBackGround = new LoopStream(readerBackGround);
            this.waveOutBackGround = new WaveOut();
            this.waveOutBackGround.Init(loopBackGround);
            this.waveOutBackGround.Volume = 0.7f;
            this.waveOutBackGround.Play();

            var readerWater = new WaveFileReader("../../../sounds/river.wav");
            var loopWater = new LoopStream(readerWater);
            this.waveOutWater = new WaveOut();
            this.waveOutWater.Init(loopWater);
            this.waveOutWater.Volume = 0.2f;

            var readerRain = new WaveFileReader("../../../sounds/rain.wav");
            var loopRain = new LoopStream(readerRain);
            this.waveOutRain = new WaveOut();
            this.waveOutRain.Init(loopRain);
            this.waveOutRain.Volume = 0.6f;

            var readerStep = new WaveFileReader("../../../sounds/Player/step.wav");
            var loopStep = new LoopStream(readerStep);
            this.waveOutStep = new WaveOut();
            this.waveOutStep.Init(loopStep);
            this.waveOutStep.Volume = 0.5f;

            var readerStep_water = new WaveFileReader("../../../sounds/Player/step_water.wav");
            var loopStep_water = new LoopStream(readerStep_water);
            this.waveOutStep_water = new WaveOut();
            this.waveOutStep_water.Init(loopStep_water);
            this.waveOutStep_water.Volume = 0.2f;

            var readerCarIdle = new WaveFileReader("../../../sounds/Car/Idle.wav");
            var loopCarIdle = new LoopStream(readerCarIdle);
            this.waveOutCarIdle = new WaveOut();
            this.waveOutCarIdle.Init(loopCarIdle);
            this.waveOutCarIdle.Volume = 0.5f;

            var readerCarRunning = new WaveFileReader("../../../sounds/Car/Running.wav");
            var loopCarRunning = new LoopStream(readerCarRunning);
            this.waveOutCarRunning = new WaveOut();
            this.waveOutCarRunning.Init(loopCarRunning);
            this.waveOutCarRunning.Volume = 0.5f;

            var readerCarStarting = new WaveFileReader("../../../sounds/Car/StartEngine.wav");
            this.waveOutCarStarting = new WaveOut();
            this.waveOutCarStarting.Init(readerCarStarting);
            this.waveOutCarStarting.Volume = 0.5f;

            var readerRadioMusic1 = new WaveFileReader("../../../sounds/Music/Trap.wav");
            var loopRadioMusic1 = new LoopStream(readerRadioMusic1);
            this.waveOutRadioMusicTrap = new WaveOut();
            this.waveOutRadioMusicTrap.Init(loopRadioMusic1);
            this.waveOutRadioMusicTrap.Volume = 0.5f;

            var readerRadioMusicElectro = new WaveFileReader("../../../sounds/Music/Electro.wav");
            var loopRadioMusicElectro = new LoopStream(readerRadioMusicElectro);
            this.waveOutRadioMusicElectro = new WaveOut();
            this.waveOutRadioMusicElectro.Init(loopRadioMusicElectro);
            this.waveOutRadioMusicElectro.Volume = 0.5f;

            var readerRadioMusicRap = new WaveFileReader("../../../sounds/Music/Rap.wav");
            var loopRadioMusicRap = new LoopStream(readerRadioMusicRap);
            this.waveOutRadioMusicRap = new WaveOut();
            this.waveOutRadioMusicRap.Init(loopRadioMusicRap);
            this.waveOutRadioMusicRap.Volume = 0.5f;

            this.waveOutRadioActual = new WaveOut();

            this._boxGrounds = new List<EBoxGround>();
            this._playerBoxGrounds = new List<EBoxGround>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets a value indicating whether is stopped.
        /// </summary>
        public bool IsStopped
        {
            get
            {
                return this._isStopped;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Manage the car sounds
        /// </summary>
        public void CarSounds()
        {
            if (this._map.IsPlayer)
            {
                if (this._map.IsInCar)
                {
                    switch (this._map.ViewPort.Player.Car.ERadioSong)
                    {
                        case ERadioSongs.Electro:
                            this.waveOutRadioActual = this.waveOutRadioMusicElectro;
                            break;
                        case ERadioSongs.Trap:
                            this.waveOutRadioActual = this.waveOutRadioMusicTrap;
                            break;
                        case ERadioSongs.Rap:
                            this.waveOutRadioActual = this.waveOutRadioMusicRap;
                            break;
                        default:
                            this.waveOutRadioActual = this.waveOutRadioMusicTrap;
                            break;
                    }

                    if (this._map.ViewPort.Player.Car.IsRadioPlaying
                        && this.waveOutRadioActual.PlaybackState != PlaybackState.Playing)
                    {
                        this.waveOutRadioActual.Play();
                    }

                    if (!this._map.ViewPort.Player.Car.IsRadioPlaying
                        && this.waveOutRadioActual.PlaybackState == PlaybackState.Playing)
                    {
                        this.waveOutRadioActual.Pause();
                    }

                    if (!this._map.ViewPort.Player.Car.IsMoving
                        && this.waveOutCarIdle.PlaybackState == PlaybackState.Stopped
                        && this.waveOutCarStarting.PlaybackState == PlaybackState.Stopped)
                    {
                        this.waveOutCarIdle.Play();
                        if (this.waveOutCarRunning.PlaybackState == PlaybackState.Playing)
                        {
                            this.waveOutCarRunning.Stop();
                        }
                    }

                    if (this._map.ViewPort.Player.Car.IsMoving
                        && this.waveOutCarStarting.PlaybackState == PlaybackState.Stopped)
                    {
                        this.waveOutCarRunning.Play();
                        if (this.waveOutCarIdle.PlaybackState == PlaybackState.Playing)
                        {
                            this.waveOutCarIdle.Stop();
                        }
                    }
                }
                else
                {
                    if (this.waveOutCarIdle.PlaybackState == PlaybackState.Playing)
                    {
                        this.waveOutCarIdle.Stop();
                    }

                    if (this.waveOutCarRunning.PlaybackState == PlaybackState.Playing)
                    {
                        this.waveOutCarRunning.Stop();
                    }

                    if (this.waveOutRadioActual.PlaybackState == PlaybackState.Playing)
                    {
                        this.waveOutRadioActual.Stop();
                    }
                }
            }
            else
            {
                if (this.waveOutRadioActual.PlaybackState == PlaybackState.Playing)
                {
                    this.waveOutRadioActual.Stop();
                }

                if (this.waveOutCarIdle.PlaybackState == PlaybackState.Playing)
                {
                    this.waveOutCarIdle.Stop();
                }

                if (this.waveOutCarRunning.PlaybackState == PlaybackState.Playing)
                {
                    this.waveOutCarRunning.Stop();
                }
            }
        }

        /// <summary>
        /// The load boxes.
        /// </summary>
        /// <param name="Boxes">
        /// The boxes.
        /// </param>
        public void LoadBoxes(List<Box> Boxes)
        {
            this._boxes = Boxes;
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
        ///     Play all songs of the game
        /// </summary>
        public void PlayAllSounds()
        {
            this._boxGrounds.Clear();
            this.CarSounds();
            foreach (Box box in this._boxes)
            {
                this._boxGrounds.Add(box.Ground);
            }

            if (this._boxGrounds.Contains(EBoxGround.Water))
            {
                this._isWater = true;
            }
            else
            {
                this._isWater = false;
                if (this.waveOutWater.PlaybackState == PlaybackState.Playing)
                {
                    this.waveOutWater.Stop();
                }
            }

            if (this._isWater && this.waveOutWater.PlaybackState == PlaybackState.Stopped && this._isStopped == false)
            {
                this.waveOutWater.Play();
            }

            if (this._map.IsRaining)
            {
                this._isRaining = true;
            }
            else
            {
                this._isRaining = false;
                if (this.waveOutRain.PlaybackState == PlaybackState.Playing)
                {
                    this.waveOutRain.Stop();
                }
            }

            if (this._isRaining && this.waveOutRain.PlaybackState == PlaybackState.Stopped && this._isStopped == false)
            {
                this.waveOutRain.Play();
            }
        }

        /// <summary>
        ///     Manage player sounds
        /// </summary>
        public void PlayerSounds()
        {
            if (this._map.IsPlayer)
            {
                this._playerBoxGrounds.Clear();
                int _count = 0;
                foreach (Box b in this._map.ViewPort.Player.BoxList)
                {
                    if (_count == 0)
                    {
                        this._playerBoxGrounds.Add(b.Ground);
                    }

                    _count++;
                }

                if (this._map.ViewPort.Player.EMovingDirection != EMovingDirection.Idle)
                {
                    if (this._playerBoxGrounds.Contains(EBoxGround.Water))
                    {
                        if (this.waveOutStep_water.PlaybackState == PlaybackState.Stopped)
                        {
                            this.waveOutStep_water.Play();
                        }

                        if (this.waveOutStep.PlaybackState == PlaybackState.Playing)
                        {
                            this.waveOutStep.Stop();
                        }
                    }
                    else
                    {
                        if (this.waveOutStep.PlaybackState == PlaybackState.Stopped)
                        {
                            this.waveOutStep.Play();
                        }

                        if (this.waveOutStep_water.PlaybackState == PlaybackState.Playing)
                        {
                            this.waveOutStep_water.Stop();
                        }
                    }
                }
                else
                {
                    if (this.waveOutStep.PlaybackState == PlaybackState.Playing)
                    {
                        this.waveOutStep.Stop();
                    }

                    if (this.waveOutStep_water.PlaybackState == PlaybackState.Playing)
                    {
                        this.waveOutStep_water.Stop();
                    }
                }
            }
        }

        /// <summary>
        ///     The start engine.
        /// </summary>
        public void StartEngine()
        {
            this.waveOutCarStarting.Play();
        }

        /// <summary>
        ///     The toggle mute.
        /// </summary>
        public void ToggleMute()
        {
            if (this._isStopped == false)
            {
                try
                {
                    this.waveOutBackGround.Stop();
                    if (this.waveOutWater.PlaybackState == PlaybackState.Playing)
                    {
                        this.waveOutWater.Stop();
                    }

                    this._isStopped = true;
                }
                catch
                {
                }
            }
            else
            {
                if (this._isWater && this.waveOutWater.PlaybackState == PlaybackState.Stopped)
                {
                    this.waveOutWater.Play();
                }

                this.waveOutBackGround.Play();
                this._isStopped = false;
            }
        }

        #endregion
    }
}