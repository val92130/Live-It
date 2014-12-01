﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveIT2._1
{
    [Serializable]
    public class Dog : Animal
    {

        public Dog( Map map, Point starPosition )
            : base( map, starPosition )
        {
            Texture = AnimalTexture.Dog;
            Size = new Size( 150, 150 );
            FavoriteEnvironnment = BoxGround.Forest;
            Speed = 15000;
            DefaultSpeed = Speed;
            ViewDistance = 400;
        }

        public override void Behavior()
        {
            base.Behavior();
            if( this.Hunger > 50 )
            {
                if( this.AnimalsAround.Count != 0 )
                {
                    for( int i = 0; i < AnimalsAround.Count(); i++ )
                    {
                        if( AnimalsAround[i].Texture == AnimalTexture.Rabbit || AnimalsAround[i].Texture == AnimalTexture.Cat )
                        {
                            ChangePosition( AnimalsAround[i].Position );
                            if( this.Area.IntersectsWith( AnimalsAround[i].Area ) )
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