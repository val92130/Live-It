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
        List<AnimalTexture> TargetAnimals;
        public Cat( Map map, Point starPosition )
            :base(map, starPosition)
        {
            Texture = AnimalTexture.Cat;
            Size = new Size( 120, 120 );
            FavoriteEnvironnment = BoxGround.Grass;
            Speed = 15000;
            DefaultSpeed = Speed;
            ViewDistance = 300;
            TargetAnimals = new List<AnimalTexture>() { AnimalTexture.Rabbit};
        }

        public override void Behavior()
        {
            base.Behavior();
            if (this.AnimalsAround.Count != 0)
            {
                for (int i = 0; i < AnimalsAround.Count(); i++)
                {
                    if (TargetAnimals.Contains(AnimalsAround[i].Texture))
                    {
                        ChangePosition(AnimalsAround[i].Position);
                        this.AnimalsAround[i].Speed = (int)(this.Speed * 2.5); 
                        if (this.Area.IntersectsWith(AnimalsAround[i].Area))
                        {
                            AnimalsAround[i].Die();
                        }
                    }
                }
            }
        }
    }
}