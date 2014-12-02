using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveIT2._1
{
    [Serializable]
    public class Elephant: Animal
    {
         
        public Elephant(Map map, Point starPosition)
            :base(map, starPosition)
        {
            Texture = AnimalTexture.Elephant;
            Size = new Size(500,500);
            Speed = 7000;
            ViewDistance = 800;
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
                            AnimalsAround[i].Die();
                        }
                    }
                }
            }
        }
    }
}
