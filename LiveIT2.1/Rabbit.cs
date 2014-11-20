using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveIT2._1
{
    public class Rabbit : Animal
    {
        public Rabbit( Map map, Point StartPosition )
        {
            Texture = AnimalTexture.Rabbit;
            Position = StartPosition;
            Size = new Size( 100, 100 );
            Park = map;
            Speed = 10;
            ViewDistance = 200;
        }
    }
}
