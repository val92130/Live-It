using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveIT2._1
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void pbTransport_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.Show(this);
        }

        private void pbRestore_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void loadGameText_Click( object sender, EventArgs e )
        {
            this.Hide();
            Form1 game = new Form1();
            game.Show();
        }
    }
}
