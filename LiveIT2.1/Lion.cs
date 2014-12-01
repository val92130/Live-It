﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveIT2._1
{
    [Serializable]
    public class Lion : Animal
    {
        List<AnimalTexture> TargetAnimals;

        public Lion( Map map, Point starPosition )
            : base( map, starPosition )
        {
            Position = starPosition;
            Texture = AnimalTexture.Lion;
            Size = new Size(250, 250);
            FavoriteEnvironnment = BoxGround.Forest;
            Speed = 15000;
            DefaultSpeed = Speed;
            ViewDistance = 400;
            TargetAnimals = new List<AnimalTexture>() { AnimalTexture.Rabbit, AnimalTexture.Cow, AnimalTexture.Elephant, AnimalTexture.Gazelle };
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