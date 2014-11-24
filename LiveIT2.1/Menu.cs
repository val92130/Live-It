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
            
            Thread t = new Thread(() =>
            {
                Application.Run(new Form1());
            });
            t.Start();
            this.Close();
        }

        private void pbRestore_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
