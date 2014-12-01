using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveIT2._1
{
    [Serializable]
    public class Gazelle : Animal
    {
        public Gazelle(Map map, Point startPosition)
            : base(map, startPosition)
        {
            Texture = AnimalTexture.Gazelle;
            Position = startPosition;
            Size = new Size(180, 180);
            Speed = 20000;
            DefaultSpeed = Speed;
            ViewDistance = 400;
        }

    }
}
