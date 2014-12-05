using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveIT2._1
{
    public class Missile
    {
        Point _position, _targetPosition;
        Rectangle _area;
        Size _size;
        Point _relativePosition;
        Size _relativeSize;
        SizeF _direction;
        int _speed;
        Map _map;
        public Missile( Point StartPosition, Map map )
        {
            _position = StartPosition;
            _size = new Size( 80, 80 );
            _speed = 90000;
            _map = map;
        }

        public Rectangle Area
        {
            get { return new Rectangle(_position, _size); }
            internal set { _area = value; }
        }

        public Point Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Size Size
        {
            get { return _size; }
            internal set { _size = value; }
        }

        public Point RelativePosition
        {
            get { return _relativePosition; }
            set { _relativePosition = value; }
        }

        public Size RelativeSize
        {
            get { return _relativeSize; }
            set { _relativeSize = value; }
        }

        public SizeF Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        public int Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public Point TargetPosition
        {
            get { return _targetPosition; }
            set { _targetPosition = value; }
        }

        public void Shoot(Point Target)
        {
            ChangePosition( Target );
            
        }

        public void Draw( Graphics g, Rectangle target, Rectangle viewPort, Rectangle targetMiniMap, Rectangle viewPortMiniMap, Texture texture )
        {
            int newWidth = (int)(((double)this.Area.Width / (double)viewPort.Width) * target.Width + 1);
            int newHeight = (int)(((double)this.Area.Height / (double)viewPort.Width) * target.Width + 1);
            int newXpos = (int)(this.Area.X / (this.Area.Width / (((double)this.Area.Width / (double)viewPort.Width) * target.Width))) - (int)(viewPort.X / (this.Area.Width / (((double)this.Area.Width / (double)viewPort.Width) * target.Width)));
            int newYpos = (int)(this.Area.Y / (this.Area.Width / (((double)this.Area.Width / (double)viewPort.Width) * target.Width))) - (int)(viewPort.Y / (this.Area.Width / (((double)this.Area.Width / (double)viewPort.Width) * target.Width)));

            this.RelativePosition = new Point( newXpos, newYpos );
            this.RelativeSize = new Size( newWidth, newHeight );



            Position = new Point( _position.X + (int)(this.Direction.Width * this.Speed),
                                  _position.Y + (int)(this.Direction.Height * this.Speed) );

            g.FillRectangle( Brushes.Black, new Rectangle( newXpos + target.X, newYpos + target.Y, newWidth, newHeight ) );

            for( int i = 0; i < _map.Animals.Count; i++ )
            {
                if( _map.Animals[i].Area.IntersectsWith( this.Area ) )
                {
                    _map.Animals[i].Die();
                }
            }

        }

        public void ChangePosition( Point target )
        {
            Random r = new Random();
            Point newTarget = target;
            this.TargetPosition = newTarget;
            float distance = (float)(Math.Pow( this.Position.X - newTarget.X, 2 ) + Math.Pow( this.Position.Y - newTarget.Y, 2 ));
            SizeF _dir = new SizeF( (newTarget.X - this.Position.X) / distance, (newTarget.Y - this.Position.Y) / distance );
            this.Direction = _dir;
        }
    }

}
