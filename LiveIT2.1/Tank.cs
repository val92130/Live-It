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
        List<Missile> _missiles;
        Map _map;
        public Tank(Map map, Point startPosition)
            : base(map, startPosition)
        {
            Position = startPosition;
            _map = map;
            Texture = CarTexture.Tank;
            Size = new Size(1000, 1000);
            Speed = 20000;
            _missiles = new List<Missile>();
        }

        public void Shoot()
        {
            

            if( this.MovingDirection == MovingDirection.Right )
            {
                Missile m = new Missile( new Point(this.Position.X + this.Area.Width, this.Position.Y + (this.Area.Width/2)), _map );
                _missiles.Add( m );

                m.Shoot( new Point( this.Position.X + this.Area.Width + 800, this.Position.Y + (this.Area.Width / 2) ) );
            }
            if( this.MovingDirection == MovingDirection.Left )
            {
                Missile m = new Missile( new Point( this.Position.X, this.Position.Y + (this.Area.Height / 2) ), _map );
                _missiles.Add( m );

                m.Shoot( new Point( this.Position.X - 800, this.Position.Y + (this.Area.Height / 2) ) );
            }
            if( this.MovingDirection == MovingDirection.Up )
            {
                Missile m = new Missile( new Point( this.Position.X + (this.Area.Width / 2), this.Position.Y ), _map );
                _missiles.Add( m );
                m.Shoot( new Point( this.Position.X + (this.Area.Width / 2), this.Position.Y - 800 ) );
            }
            if( this.MovingDirection == MovingDirection.Down )
            {
                Missile m = new Missile( new Point( this.Position.X + (this.Area.Width/2), this.Position.Y + (this.Area.Height) ), _map );
                _missiles.Add( m );

                m.Shoot( new Point( this.Position.X + (this.Area.Width / 2), this.Position.Y + 800 + (this.Area.Height) ) );
            }
            if( this.MovingDirection == MovingDirection.Idle )
            {
                Missile m = new Missile( new Point( this.Position.X + (this.Area.Width / 2), this.Position.Y ), _map );
                _missiles.Add( m );
                m.Shoot( new Point( this.Position.X + (this.Area.Width / 2), this.Position.Y - 800 ) );
            }
            
            
        }

        public override void Draw( Graphics g, Rectangle target, Rectangle viewPort, Rectangle targetMiniMap, Rectangle viewPortMiniMap, Texture texture )
        {

            if( _map.ViewPort.HasClicked )
            {
                Shoot();
            }
            base.Draw( g, target, viewPort, targetMiniMap, viewPortMiniMap, texture );


            for( int i = 0; i < _missiles.Count; i++ )
            {
                if( _missiles[i].Area.X > _map.MapSize || _missiles[i].Area.X < 0 || _missiles[i].Area.Y > _map.MapSize || _missiles[i].Area.Y < 0 )
                {
                    _missiles.Remove( _missiles[i] );
                }
                else
                {
                    _missiles[i].Draw( g, target, viewPort, targetMiniMap, viewPortMiniMap, texture );
                }
            }
        }
    }
}


