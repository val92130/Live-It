using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveIT2._1
{
    public class Cow : Animal
    {

        public Cow ( Map map, Point starPosition )
        {
            Position = starPosition;
            Texture = AnimalTexture.Cow;
            Size = new Size( 300, 300 );
            Park = map;
            FavoriteEnvironnment = BoxGround.Grass;
        }
    }
}