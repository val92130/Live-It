using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using LiveIT2._1;
using System.Drawing;

namespace LiveIT2._1
{
    using LiveIT2._1.Terrain;
    using LiveIT2._1.Viewport;

    [TestFixture]
    public class Class1
    {
        [Test]
        public void Test_Map()
        {
            Map map = new Map(200,20);
            Assert.That( map.MapSize == 200 * 20 * 100 );
            Assert.That( map.BoxSize == 20 * 100 );
        }

        [Test]
        public void Create_animals_works_correctly()
        {
            Map map = new Map( 200, 20 );
            MainViewPort v = new MainViewPort( map );
            Assert.That( map.Animals.Count == 0 );
            v.CreateAnimal( Enums.EAnimalTexture.Cat, new Point( 1, 1 ) );
            Assert.That( map.Animals.Count == 1 );
        }
        [Test]

        public void Kill_Animal_Works_Correctly()
        {
            Map map = new Map( 200, 20 );
            MainViewPort v = new MainViewPort( map );
            v.CreateAnimal( Enums.EAnimalTexture.Cat, new Point( 1, 1 ) );
            Assert.That( map.Animals.Count == 1 );
            for( int i = 0; i < map.Animals.Count; i++ )
            {
                map.Animals[i].Die();
            }
            Assert.That( map.Animals.Count == 0 );
        }

        public void Create_Car_Works_Correctly()
        {
            Map map = new Map( 200, 20 );
            MainViewPort v = new MainViewPort( map );
            v.SpawnCar( new Point( 500, 500 ) );
        }
    }
}
