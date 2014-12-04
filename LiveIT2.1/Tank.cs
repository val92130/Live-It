using System;
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
            Size = new Size(300, 400);
            Speed = 20000;

        }
    }
}


