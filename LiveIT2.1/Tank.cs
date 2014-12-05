﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveIT2._1
{
    public class Tank : Car
    {

        public Tank(Map map, Point startPosition)
            : base(map, startPosition)
        {
            Position = startPosition;
            Texture = CarTexture.Tank;
            Size = new Size(1000, 1000);
            Speed = 20000;
        }

        public void Shoot()
        {

        }

        public override void Draw( Graphics g, Rectangle target, Rectangle viewPort, Rectangle targetMiniMap, Rectangle viewPortMiniMap, Texture texture )
        {
            base.Draw( g, target, viewPort, targetMiniMap, viewPortMiniMap, texture );

        }
    }
}


