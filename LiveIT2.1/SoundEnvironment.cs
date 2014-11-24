using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace LiveIT2._1
{
    public class SoundEnvironment
    {
        SoundPlayer sound;
        bool _isStopped;
        public SoundEnvironment()
        {
             sound = new SoundPlayer( "../../../sounds/background.wav" );
            sound.PlayLooping();
        }

        public void ToggleMute()
        {
            if( _isStopped == false )
            {
                try
                {
                    sound.Stop();
                    _isStopped = true;
                }
                catch
                {

                }
            }
            else
            {
                sound.PlayLooping();
                _isStopped = false;
            }

        }

        public bool IsStopped
        {
            get { return _isStopped; }
        }

    }
}
