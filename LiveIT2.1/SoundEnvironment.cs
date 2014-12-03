using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using NAudio;

namespace LiveIT2._1
{
    public class SoundEnvironment
    {
        private WaveOut waveOutBackGround;
        private WaveOut waveOutWater;
        private WaveOut waveOutRain;
        private WaveOut waveOutStep;
        private WaveOut waveOutStep_water;
        private WaveOut waveOutCarIdle;
        private WaveOut waveOutCarStarting;
        private WaveOut waveOutCarRunning;
        private WaveOut waveOutRadioMusicTrap;
        private WaveOut waveOutRadioMusicElectro;
        private WaveOut waveOutRadioMusicRap;

        private WaveOut waveOutRadioActual;

        bool _isStopped, _isWater, _isRaining;
        List<Box> _boxes;
        List<BoxGround> _boxGrounds;
        List<BoxGround> _playerBoxGrounds;
        Map _map;
        public SoundEnvironment()
        {
            WaveFileReader readerBackGround = new WaveFileReader("../../../sounds/background.wav");
            LoopStream loopBackGround = new LoopStream(readerBackGround);
            waveOutBackGround = new WaveOut();
            waveOutBackGround.Init(loopBackGround);
            waveOutBackGround.Volume = 0.7f;
            waveOutBackGround.Play();

            WaveFileReader readerWater = new WaveFileReader("../../../sounds/river.wav");
            LoopStream loopWater = new LoopStream(readerWater);
            waveOutWater = new WaveOut();
            waveOutWater.Init(loopWater);
            waveOutWater.Volume = 0.2f;

            WaveFileReader readerRain = new WaveFileReader( "../../../sounds/rain.wav" );
            LoopStream loopRain = new LoopStream( readerRain );
            waveOutRain = new WaveOut();
            waveOutRain.Init( loopRain );
            waveOutRain.Volume = 0.6f;

            WaveFileReader readerStep = new WaveFileReader("../../../sounds/Player/step.wav");
            LoopStream loopStep = new LoopStream(readerStep);
            waveOutStep = new WaveOut();
            waveOutStep.Init(loopStep);
            waveOutStep.Volume = 0.5f;

            WaveFileReader readerStep_water = new WaveFileReader("../../../sounds/Player/step_water.wav");
            LoopStream loopStep_water = new LoopStream(readerStep_water);
            waveOutStep_water = new WaveOut();
            waveOutStep_water.Init(loopStep_water);
            waveOutStep_water.Volume = 0.2f;

            WaveFileReader readerCarIdle = new WaveFileReader("../../../sounds/Car/Idle.wav");
            LoopStream loopCarIdle = new LoopStream(readerCarIdle);
            waveOutCarIdle = new WaveOut();
            waveOutCarIdle.Init(loopCarIdle);
            waveOutCarIdle.Volume = 0.5f;

            WaveFileReader readerCarRunning = new WaveFileReader("../../../sounds/Car/Running.wav");
            LoopStream loopCarRunning = new LoopStream(readerCarRunning);
            waveOutCarRunning = new WaveOut();
            waveOutCarRunning.Init(loopCarRunning);
            waveOutCarRunning.Volume = 0.5f;

            WaveFileReader readerCarStarting = new WaveFileReader("../../../sounds/Car/StartEngine.wav");
            waveOutCarStarting = new WaveOut();
            waveOutCarStarting.Init(readerCarStarting);
            waveOutCarStarting.Volume = 0.5f;

            WaveFileReader readerRadioMusic1 = new WaveFileReader("../../../sounds/Music/Trap.wav");
            LoopStream loopRadioMusic1 = new LoopStream(readerRadioMusic1);
            waveOutRadioMusicTrap = new WaveOut();
            waveOutRadioMusicTrap.Init(loopRadioMusic1);
            waveOutRadioMusicTrap.Volume = 0.5f;

            WaveFileReader readerRadioMusicElectro = new WaveFileReader("../../../sounds/Music/Electro.wav");
            LoopStream loopRadioMusicElectro = new LoopStream(readerRadioMusicElectro);
            waveOutRadioMusicElectro = new WaveOut();
            waveOutRadioMusicElectro.Init(loopRadioMusicElectro);
            waveOutRadioMusicElectro.Volume = 0.5f;

            WaveFileReader readerRadioMusicRap = new WaveFileReader("../../../sounds/Music/Rap.wav");
            LoopStream loopRadioMusicRap = new LoopStream(readerRadioMusicRap);
            waveOutRadioMusicRap = new WaveOut();
            waveOutRadioMusicRap.Init(loopRadioMusicRap);
            waveOutRadioMusicRap.Volume = 0.5f;

            waveOutRadioActual = new WaveOut();

            _boxGrounds = new List<BoxGround>();
            _playerBoxGrounds = new List<BoxGround>();
        }

        public void LoadBoxes(List<Box> Boxes)
        {
            _boxes = Boxes;
        }

        public void LoadMap( Map map )
        {
            _map = map;
        }

        public void PlayAllSounds()
        {
            _boxGrounds.Clear();
            CarSounds();
            foreach (Box box in _boxes)
            {
                _boxGrounds.Add(box.Ground);
            }
            if (_boxGrounds.Contains(BoxGround.Water))
            {
                _isWater = true;
            }
            else
            {
                _isWater = false;
                if (waveOutWater.PlaybackState == PlaybackState.Playing)
                {
                    waveOutWater.Stop();
                }
            }
            if (_isWater && waveOutWater.PlaybackState == PlaybackState.Stopped && _isStopped == false)
            {
                waveOutWater.Play();
            }

            if( _map.IsRaining )
            {
                _isRaining = true;
            }
            else
            {
                _isRaining = false;
                if( waveOutRain.PlaybackState == PlaybackState.Playing )
                {
                    waveOutRain.Stop();
                }
            }
            if( _isRaining && waveOutRain.PlaybackState == PlaybackState.Stopped && _isStopped == false )
            {
                waveOutRain.Play();
            }
        }

        public void PlayerSounds()
        {
            if (_map.IsPlayer)
            {
                _playerBoxGrounds.Clear();
                int _count = 0;
                foreach (Box b in _map.ViewPort.Player.BoxList)
                {
                    if (_count == 0)
                    {
                        _playerBoxGrounds.Add(b.Ground);
                    }
                    _count++;
                    
                }
                if (_map.ViewPort.Player.MovingDirection != MovingDirection.Idle)
                {

                    if (_playerBoxGrounds.Contains(BoxGround.Water))
                    {
                        if (waveOutStep_water.PlaybackState == PlaybackState.Stopped)
                        {
                            waveOutStep_water.Play();
                        }
                        if (waveOutStep.PlaybackState == PlaybackState.Playing)
                        {
                            waveOutStep.Stop();
                        }
                    }
                    else
                    {
                        if (waveOutStep.PlaybackState == PlaybackState.Stopped)
                        {
                            waveOutStep.Play();
                        }
                        if (waveOutStep_water.PlaybackState == PlaybackState.Playing)
                        {
                            waveOutStep_water.Stop();
                        }
                    }
                    
                }
                else
                {
                    if (waveOutStep.PlaybackState == PlaybackState.Playing)
                    {
                        waveOutStep.Stop();
                    }
                    if (waveOutStep_water.PlaybackState == PlaybackState.Playing)
                    {
                        waveOutStep_water.Stop();
                    }
                }
            }
        }

        public void StartEngine()
        {
            waveOutCarStarting.Play();
        }

        public void CarSounds()
        {
            if (_map.IsPlayer)
            {
                if (_map.IsInCar)
                {
                    switch (_map.ViewPort.Player.Car.RadioSong)
                    {
                        case RadioSongs.Electro :
                            waveOutRadioActual = waveOutRadioMusicElectro;
                            break;
                        case RadioSongs.Trap :
                            waveOutRadioActual = waveOutRadioMusicTrap;
                            break;
                        case RadioSongs.Rap:
                            waveOutRadioActual = waveOutRadioMusicRap;
                            break;
                        default :
                            waveOutRadioActual = waveOutRadioMusicTrap;
                            break;
                    }

                    if (_map.ViewPort.Player.Car.IsRadioPlaying && waveOutRadioActual.PlaybackState != PlaybackState.Playing)
                    {
                        waveOutRadioActual.Play();
                    }

                    if (!_map.ViewPort.Player.Car.IsRadioPlaying && waveOutRadioActual.PlaybackState == PlaybackState.Playing)
                    {
                        waveOutRadioActual.Pause();
                    }
                    if (!_map.ViewPort.Player.Car.IsMoving && waveOutCarIdle.PlaybackState == PlaybackState.Stopped && waveOutCarStarting.PlaybackState == PlaybackState.Stopped)
                    {
                        waveOutCarIdle.Play();
                        if (waveOutCarRunning.PlaybackState == PlaybackState.Playing)
                        {
                            waveOutCarRunning.Stop();
                        }
                    }
                    if (_map.ViewPort.Player.Car.IsMoving && waveOutCarStarting.PlaybackState == PlaybackState.Stopped)
                    {
                        waveOutCarRunning.Play();
                        if (waveOutCarIdle.PlaybackState == PlaybackState.Playing)
                        {
                            waveOutCarIdle.Stop();
                        }
                    }
                }
                else
                {
                    if (waveOutCarIdle.PlaybackState == PlaybackState.Playing)
                    {
                        waveOutCarIdle.Stop();
                    }
                    if (waveOutCarRunning.PlaybackState == PlaybackState.Playing)
                    {
                        waveOutCarRunning.Stop();
                    }
                    if (waveOutRadioActual.PlaybackState == PlaybackState.Playing)
                    {
                        waveOutRadioActual.Stop();
                    }
                }
            }
            else
            {
                if (waveOutRadioActual.PlaybackState == PlaybackState.Playing)
                {
                    waveOutRadioActual.Stop();
                }
                if (waveOutCarIdle.PlaybackState == PlaybackState.Playing)
                {
                    waveOutCarIdle.Stop();
                }
                if (waveOutCarRunning.PlaybackState == PlaybackState.Playing)
                {
                    waveOutCarRunning.Stop();
                }
            }
        }

        public void ToggleMute()
        {
            if( _isStopped == false )
            {
                try
                {
                    waveOutBackGround.Stop();
                    if (waveOutWater.PlaybackState == PlaybackState.Playing)
                    {
                        waveOutWater.Stop();
                    }
                    _isStopped = true;
                }
                catch
                {

                }
            }
            else
            {
                if (_isWater && waveOutWater.PlaybackState == PlaybackState.Stopped)
                {
                    waveOutWater.Play();
                }
                waveOutBackGround.Play();
                _isStopped = false;
            }

        }

        public bool IsStopped
        {
            get { return _isStopped; }
        }

    }
}
