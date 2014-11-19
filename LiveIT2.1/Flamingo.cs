using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveIT2._1
{
    public class Flamingo : Animal
    {

        public Flamingo( Map map, Point starPosition )
        {
            Position = starPosition;
            Texture = AnimalTexture.Elephant;
            Size = new Size( 250, 250 );
            Park = map;
        }
    }
}