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
    public partial class SplashScreen : Form
    {
        Timer t;
        public SplashScreen()
        {
            t = new Timer();
            t.Interval = 3000;
            t.Start();
            t.Tick += new EventHandler(T_tick);
            InitializeComponent();
            
        }

        private void T_tick(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
