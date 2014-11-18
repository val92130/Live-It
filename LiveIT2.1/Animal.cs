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
        Size _size;
        Point _position;
        Map _map;

        public Animal( Map map )
        {
            _map = map;
            _size = new Size( 50, 50 );
            Random r = new Random();
            _position = new Point( r.Next( 0, _map.MapSize ), r.Next( 0, _map.MapSize ) );
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


    }
}
