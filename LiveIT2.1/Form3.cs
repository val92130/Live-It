﻿using System;
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
    public partial class Form3 : Form
    {
        int i =0;
        public Form3()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            i++;

            if (i == 100 )
            {
                timer1.Stop();
            }
        }
    }
}
