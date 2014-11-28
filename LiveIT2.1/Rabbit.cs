using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveIT2._1
{
    [Serializable]
    public class Rabbit : Animal
    {
        public Rabbit( Map map, Point startPosition )
            : base( map, startPosition )
        {
            Texture = AnimalTexture.Rabbit;
            Position = startPosition;
            Size = new Size( 100, 100 );
            Speed = 40000;
            ViewDistance = 400;
        }
    }
}
