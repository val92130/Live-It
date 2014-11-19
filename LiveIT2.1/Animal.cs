using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveIT2._1
{
    public class Animal
    {
         Point _position;
         Size _size;
         Map _map;
         AnimalTexture _texture;
        public Animal(Map map, Point position, Size size, AnimalTexture texture)
        {
            _map = map;
            _position = position;
            _size = size;
            _texture = texture;
        }
         

        public Point Position
        {
            get { return _position; }
            set { _position = value; }
        }
         

        public Size Size
        {
            get { return _size; }
            set { _size = value; }
        }

        public Rectangle Area
        {
            get { return  new Rectangle(_position, _size); }
        }

        public AnimalTexture Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

    }
}
