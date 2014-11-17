using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveIt2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Graphics g;
        Bitmap btm;
        Rectangle r;
        Pen p;
        public void DrawMethod(Brush b)
        {
            
            
            p = new Pen( b);
            r = new Rectangle( 0, 0, 100, 100 );
            btm = new Bitmap( r.Width + 1, r.Height + 1 );
            g = Graphics.FromImage(btm);
            g.DrawRectangle( p, r );

        }

        private void button1_Click( object sender, EventArgs e )
        {
            DrawMethod( Brushes.Black );
            pictureBox1.Image = btm; 
          
        }

        private void pictureBox1_MouseLeave( object sender, EventArgs e )
        {
            DrawMethod( Brushes.Red );
            pictureBox1.Image = btm; 
        }

        private void pictureBox1_MouseEnter( object sender, EventArgs e )
        {
            DrawMethod( Brushes.Blue );
            pictureBox1.Image = btm; 
        }
    }
}
