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
    using LiveIT2._1.Animation;
    using LiveIT2._1.Animals;
    using System.Diagnostics;

    [TestFixture]
    public class Class1
    {
        [Test]
        public void Map_Test()
        {
            Map map = new Map(200,20);
            Assert.That( map.MapSize == 200 * 20 * 100 );
            Assert.That( map.BoxSize == 20 * 100 );
        }

        [Test]
        public void Animals_Creation_works_correctly()
        {           
            Map map = new Map( 200, 20 );
            MainViewPort v = new MainViewPort( map );
            SoundEnvironment s;
            s = new SoundEnvironment();
            s.LoadMap(map);
            v.SoundEnvironment = s;

            v.Update();
            v.CreateAnimal( Enums.EAnimalTexture.Cat, new Point( 1, 1 ) );
            Assert.That( map.Animals.Count == 1 );
        }

        [Test]

        public void Animals_kill_Works_Correctly()
        {
            Map map = new Map( 200, 20 );
            MainViewPort v = new MainViewPort( map );
            SoundEnvironment s;
            s = new SoundEnvironment();
            s.LoadMap(map);
            v.SoundEnvironment = s;
            v.CreateAnimal( Enums.EAnimalTexture.Cat, new Point( 1, 1 ) );
            Assert.That( map.Animals.Count == 1 );
            for( int i = 0; i < map.Animals.Count; i++ )
            {
                map.Animals[i].Die();
            }
            Assert.That( map.Animals.Count == 0 );
        }

        [Test]
        public void Car_Creation_Works_Correctly()
        {
            Map map = new Map( 200, 20 );
            MainViewPort v = new MainViewPort( map );
            SoundEnvironment s;
            s = new SoundEnvironment();
            s.LoadMap(map);
            v.SoundEnvironment = s;
            Assert.That(v.Cars.Count == 0);
            v.SpawnCar( new Point( 500, 500 ) );
            Assert.That(v.Cars.Count == 1);
            Assert.That(v.Cars[0].Position == new Point(500, 500));
        }

        [Test]
        public void Box_Creation_works_correctly()
        {
            Map map = new Map(200, 20);
            MainViewPort v = new MainViewPort(map);
            SoundEnvironment s;
            s = new SoundEnvironment();
            s.LoadMap(map);
            v.SoundEnvironment = s;
            Assert.That(map.Boxes.Length == 200 * 200);
        }

        [Test]
        public void Box_Change_Texture_Works_Correctly()
        {
            Map map = new Map(200, 20);
            MainViewPort v = new MainViewPort(map);
            SoundEnvironment s;
            s = new SoundEnvironment();
            s.LoadMap(map);
            v.SoundEnvironment = s;
            map[5, 20].Ground = Enums.EBoxGround.Dirt;
            Assert.That(map[5, 20].Ground == Enums.EBoxGround.Dirt);
        }

        [Test]
        public void Box_Knows_What_Animals_It_Contains()
        {
            Map map = new Map(200, 20);
            MainViewPort v = new MainViewPort(map);
            SoundEnvironment s;
            s = new SoundEnvironment();
            s.LoadMap(map);
            v.SoundEnvironment = s;
            v.Update();
            v.CreateAnimal(Enums.EAnimalTexture.Cat, new Point(500, 500));
            v.Update();
            int j = 0;
            for (int i = 0; i < map.Boxes.Length; i++ )
            {
                if (map.Boxes[i].AnimalList.Count != 0)
                {
                    j += map.Boxes[i].AnimalList.Count;
                }

            }

            Cat c = (Cat)map.Animals[0];
            Assert.That(map[0, 0].AnimalList.Contains(c));

        }

    }
}
