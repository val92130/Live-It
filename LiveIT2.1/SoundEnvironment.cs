﻿using System;
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
        bool _isStopped, _isWater, _isRaining;
        List<Box> _boxes;
        List<BoxGround> _boxGrounds;
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
            waveOutRain.Volume = 1f;

            _boxGrounds = new List<BoxGround>();
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
