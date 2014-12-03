using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveIT2._1
{
    [Serializable]
    public class Eagle : Animal
    {
 List<AnimalTexture> TargetAnimals;

        public Eagle( Map map, Point starPosition )
            : base( map, starPosition )
        {
            Position = starPosition;
            Random r = new Random();
            int _random = r.Next(150,350);
            Texture = AnimalTexture.Eagle;
            Size = new Size( _random, _random );
            FavoriteEnvironnment = BoxGround.Forest;
            Speed = 150000;
            DefaultSpeed = Speed;
            ViewDistance = 400;
            TargetAnimals = new List<AnimalTexture>() { AnimalTexture.Rabbit, AnimalTexture.Cow, AnimalTexture.Gazelle };
        }
        public override void Behavior()
        {
            base.Behavior();
            if (this.Hunger > 50)
            {
                if (this.AnimalsAround.Count != 0)
                {
                    for (int i = 0; i < AnimalsAround.Count(); i++)
                    {
                        if (TargetAnimals.Contains(AnimalsAround[i].Texture))
                        {
                            ChangePosition(AnimalsAround[i].Position);
                            if (this.Area.IntersectsWith(AnimalsAround[i].Area))
                            {
                                AnimalsAround[i].Die();
                                this.Hunger -= 50;
                            }
                        }
                    }
                }
            }

        }
    }
}