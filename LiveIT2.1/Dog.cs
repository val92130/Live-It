using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveIT2._1
{
    public class Dog : Animal
    {

        public Dog( Map map, Point starPosition )
        {
            Position = starPosition;
            Texture = AnimalTexture.Dog;
            Size = new Size( 150, 150 );
            Park = map;
            FavoriteEnvironnment = BoxGround.Forest;
            Speed = 7;
        }
        
    }
}