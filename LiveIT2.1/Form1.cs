using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveIT2._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Bitmap creature;
        Bitmap textureGrass;
        Graphics g;
        Rectangle area;
        bool right;
        bool left;
        bool up;
        bool down;
        int collectedNumber = 0;
        Timer t;
        Bitmap background;
        Graphics scG;

        Rectangle[] rects;
        Rectangle[] terrainRects;

        private void Form1_Load( object sender, EventArgs e )
        {
            creature = new Bitmap( @"C:\Users\Rami\Desktop\LiveIT\ico.png" );
            creature.MakeTransparent( Color.White );
            background = new Bitmap( this.Width, this.Height );
            area = new Rectangle( this.Width / 2 , this.Height / 2, 100, 100 );

            textureGrass = new Bitmap( @"C:\Users\Rami\Desktop\LiveIT\grass.png" );
            
            rects = new Rectangle[10];
            terrainRects = new Rectangle[1000];
            //makerect();
            MakeTerrain();
            g = this.CreateGraphics();
            scG = Graphics.FromImage( background );

            t = new Timer();
            t.Interval = 10;
            t.Tick += new EventHandler( t_Tick );
            t.Start();
        }
        public void makerect()
        {
            Random r = new Random();
            for( int i = 0; i < rects.Length; i++ )
            {
                rects[i] = new Rectangle(r.Next(0, this.Width), r.Next(0, this.Height), 10, 10);
            }
            
        }

        public void MakeTerrain()
        {
            int rectpos = 0;
            for( int x = 0; x < this.Width; x += 60 )
            {
                for( int y = 0; y < this.Width; y += 60 )
                {
                    terrainRects[rectpos++] = new Rectangle( x, y, 60, 60 );
                }
            }
        }


        private void t_Tick( object sender, EventArgs e )
        {
            if( up ) { MoveRectangle(Direction.Up); }
            if( left ) {MoveRectangle(Direction.Left); }
            if( down ) { MoveRectangle(Direction.Down); }
            if( right ) { MoveRectangle( Direction.Right ); }
            g.DrawImage( Draw(), new Point( 0, 0 ) );
            //for( int i = 0; i < rects.Length; i++ )
            //{
            //    if( area.IntersectsWith( rects[i] ) )
            //    {
            //        collectedNumber += 1;
            //        rects[i] = new Rectangle(-100, -100, 1, 1);
            //    }
            //}
            
        }

        public enum Direction { Up, Down, Right, Left };
        public void MoveRectangle(Direction d)
        {
            for( int i = 0; i < rects.Length; i++ )
            {
                if( d == Direction.Down ) { rects[i].Y -= 5; }
                if( d == Direction.Up ) { rects[i].Y += 5; }
                if( d == Direction.Right ) { rects[i].X -= 5; }
                if( d == Direction.Left ) { rects[i].X += 5; }
            }
            for( int i = 0; i < terrainRects.Length; i++ )
            {
                if( d == Direction.Down ) { terrainRects[i].Y -= 5; }
                if( d == Direction.Up ) { terrainRects[i].Y += 5; }
                if( d == Direction.Right ) { terrainRects[i].X -= 5; }
                if( d == Direction.Left ) { terrainRects[i].X += 5; }
            }
        }
        public Bitmap Draw ()
        {
           
            scG.Clear( Color.FromArgb( 255, Color.Blue ) );
            for( int i = 0; i < terrainRects.Length; i++)
            {
                scG.DrawImage( textureGrass, terrainRects[i] );
            }
            scG.FillRectangles( Brushes.Gold, rects );
            //scG.DrawImage( creature, area );
            scG.DrawString( collectedNumber.ToString(), new Font("Arial", 15), Brushes.Gold, new Point( 10, 10 ) );
            return background;
        }

        private void Form1_KeyDown( object sender, KeyEventArgs e )
        {
            if( e.KeyCode == Keys.Z ) { up = true;}
            if( e.KeyCode == Keys.Q ) { left = true; }
            if( e.KeyCode == Keys.S ) { down = true; }
            if( e.KeyCode == Keys.D ) { right = true; }
        }

        private void Form1_KeyUp( object sender, KeyEventArgs e )
        {
            if( e.KeyCode == Keys.Z ) { up = false; }
            if( e.KeyCode == Keys.Q ) { left = false; }
            if( e.KeyCode == Keys.S ) { down = false; }
            if( e.KeyCode == Keys.D ) { right = false; }
        }

    }
}
