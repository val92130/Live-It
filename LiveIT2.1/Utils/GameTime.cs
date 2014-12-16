using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveIT2._1.Utils
{
    public class GameTime
    {
        float oldTimeSinceStart = 0;
        Stopwatch t;
        float _deltaTime;
        public GameTime()
        {
            t = new Stopwatch();
            t.Start();
        }

        public void Update()
        {
            float timeSinceStart = t.ElapsedMilliseconds;
            _deltaTime = timeSinceStart - oldTimeSinceStart;
            oldTimeSinceStart = timeSinceStart;
        }

        public float deltaTime
        {
            get
            {
                return 1/(_deltaTime/60);
            }
        }
    }

}
