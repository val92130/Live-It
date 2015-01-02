namespace LiveIT2._1.Gui
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    using LiveIT2._1.Animals;
    using LiveIT2._1.Terrain;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Drawing;

    public partial class Menu : Form
    {
        Form1 form;
        System.Windows.Forms.Timer t;
        public Menu()
        {
            this.InitializeComponent();
        }

        private void newGame_Click(object sender, EventArgs e)
        {
            newGameButton.Visible = false;
            loadGameButton.Visible = false;
            settingsButton.Visible = false;
            exitButton.Visible = false;
            progressBar1.Visible = true;
            ProgressBar p = progressBar1;
            p.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - p.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2 - p.Height / 2);
            t = new System.Windows.Forms.Timer();
            t.Tick += new EventHandler(T_tick);
            t.Start();
            t.Interval = 200;
        }

        private void T_tick(object sender, EventArgs e)
        {
            if (progressBar1.Value <= 90)
            {
                progressBar1.Value += 10;
            }
            if (progressBar1.Value == 100)
            {
                if (form == null)
                {
                    form = new Form1();
                }
                if (form.Map.IsLoaded)
                {
                    this.Hide();
                    form.Show(this);
                    t.Stop();
                }
            }

        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void loadGameText_Click( object sender, EventArgs e )
        {
            
            Form1 game = new Form1();
            OpenFileDialog loadBox = new OpenFileDialog();
            loadBox.Filter = "Fichier Live It Map File(*.lim)|*.lim";
            if (loadBox.ShowDialog() == DialogResult.OK)
            {
                this.Hide();
                game.Show(this);

                game.LoadMap(loadBox.FileName);

            }

        }
    }
}
