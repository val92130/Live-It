﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveIT2._1
{
    public class Eagle : Animal
    {

        public Eagle( Map map, Point starPosition )
            : base( map, starPosition )
        {
            Position = starPosition;
            Texture = AnimalTexture.Elephant;
            Size = new Size( 200, 200 );
        }
    }
}