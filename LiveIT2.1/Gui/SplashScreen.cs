namespace LiveIT2._1.Gui
{
    using System;
    using System.Windows.Forms;

    public partial class SplashScreen : Form
    {
        Timer t;
        public SplashScreen()
        {
            this.t = new Timer();
            this.t.Interval = 1000;
            this.t.Start();
            this.t.Tick += new EventHandler(this.T_tick);
            this.InitializeComponent();
            
        }

        private void T_tick(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
