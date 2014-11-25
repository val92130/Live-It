using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveIT2._1
{
    [Serializable]
    public class Cat : Animal
    {

        public Cat( Map map, Point starPosition )
            :base(map, starPosition)
        {
            Texture = AnimalTexture.Cat;
            Size = new Size( 120, 120 );
            FavoriteEnvironnment = BoxGround.Grass;
            Speed = 15;
            ViewDistance = 300;
        }
    }
}