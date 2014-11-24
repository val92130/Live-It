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
            : base( map, starPosition )
        {
            Texture = AnimalTexture.Dog;
            Size = new Size( 150, 150 );
            FavoriteEnvironnment = BoxGround.Forest;
            Speed = 7;
            ViewDistance = 600;
        }
        
    }
}