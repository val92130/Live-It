using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveIT2._1
{
    [Serializable]
    public class Rabbit : Animal
    {
        
        System.Windows.Forms.Timer t;
        public Rabbit( Map map, Point startPosition )
            : base( map, startPosition )
        {
            Texture = AnimalTexture.Rabbit;
            Position = startPosition;
            Size = new Size( 100, 100 );
            Speed = 20000;
            DefaultSpeed = Speed;
            ViewDistance = 400;
            Hunger = 45;
        }

        public override void Behavior()
        {
            
            base.Behavior();
            if( this.Hunger >= 50 )
            {
                for( int i =0; i < this.BoxList.Count; i++ )
                {
                    if( BoxList[i].Ground == BoxGround.Grass )
                    {
                        this.Speed = 0;
                        _isEating = true;
                        Eat();
                        BoxList[i].Ground = BoxGround.Grass2;
                    }
                    
                }
            }
            else
            {
                this.Speed = DefaultSpeed;
                if( t != null && t.Enabled == true )
                {
                    t.Stop();
                }                
                _isEating = false;
            }
            
        }
        public void Eat()
        {
            t  = new System.Windows.Forms.Timer();
            t.Interval = 2000;
            t.Start();
            t.Tick += new EventHandler( T_eat_tick );
        }

        private void T_eat_tick( object sender, EventArgs e )
        {
            if( _isEating )
            {
                this.Hunger -= 2;
            }           
        }
        
       
    }
}
