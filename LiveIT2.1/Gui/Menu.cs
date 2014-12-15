namespace LiveIT2._1.Gui
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    using LiveIT2._1.Animals;

    public partial class Menu : Form
    {
        public Menu()
        {
            this.InitializeComponent();
        }

        private void newGame_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            this.Hide();
            form.Show(this);
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
