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
            Speed = 30000;
            ViewDistance = 300;
        }

        public override void Behavior()
        {
            base.Behavior();
            if (this.AnimalsAround.Count != 0)
            {
                for (int i = 0; i < AnimalsAround.Count(); i++)
                {
                    if (AnimalsAround[i].Texture == AnimalTexture.Rabbit)
                    {
                        ChangePosition(AnimalsAround[i].Position);
                        if (this.Area.IntersectsWith(AnimalsAround[i].Area))
                        {
                            foreach (Box b in _map.Boxes)
                            {
                                b.RemoveFromList(AnimalsAround[i]);
                            }
                            _map.Animals.Remove(AnimalsAround[i]);
                            AnimalsAround.Remove(AnimalsAround[i]);
                        }
                    }
                }
            }
        }
    }
}