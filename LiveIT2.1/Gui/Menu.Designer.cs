namespace LiveIT2._1.Gui
{
    partial class Menu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            this.exitButton = new System.Windows.Forms.Panel();
            this.exitLabel = new System.Windows.Forms.PictureBox();
            this.btnExit = new System.Windows.Forms.Label();
            this.settingsButton = new System.Windows.Forms.Panel();
            this.settingsLabel = new System.Windows.Forms.PictureBox();
            this.btnSettings = new System.Windows.Forms.Label();
            this.loadGameButton = new System.Windows.Forms.Panel();
            this.loadGameText = new System.Windows.Forms.PictureBox();
            this.btnLoadGame = new System.Windows.Forms.Label();
            this.newGameButton = new System.Windows.Forms.Panel();
            this.newGameText = new System.Windows.Forms.PictureBox();
            this.btnNewGame = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.exitButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.exitLabel)).BeginInit();
            this.settingsButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.settingsLabel)).BeginInit();
            this.loadGameButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadGameText)).BeginInit();
            this.newGameButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.newGameText)).BeginInit();
            this.SuspendLayout();
            // 
            // exitButton
            // 
            this.exitButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.exitButton.BackColor = System.Drawing.Color.LightSeaGreen;
            this.exitButton.Controls.Add(this.exitLabel);
            this.exitButton.Controls.Add(this.btnExit);
            this.exitButton.Location = new System.Drawing.Point(607, 398);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(311, 125);
            this.exitButton.TabIndex = 19;
            // 
            // exitLabel
            // 
            this.exitLabel.BackColor = System.Drawing.Color.Transparent;
            this.exitLabel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("exitLabel.BackgroundImage")));
            this.exitLabel.Location = new System.Drawing.Point(55, 24);
            this.exitLabel.Name = "exitLabel";
            this.exitLabel.Size = new System.Drawing.Size(76, 76);
            this.exitLabel.TabIndex = 2;
            this.exitLabel.TabStop = false;
            this.exitLabel.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // btnExit
            // 
            this.btnExit.AutoSize = true;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(3, 95);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(50, 30);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "Exit";
            this.btnExit.Visible = false;
            // 
            // settingsButton
            // 
            this.settingsButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.settingsButton.BackColor = System.Drawing.Color.DeepPink;
            this.settingsButton.Controls.Add(this.settingsLabel);
            this.settingsButton.Controls.Add(this.btnSettings);
            this.settingsButton.Location = new System.Drawing.Point(476, 398);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(311, 125);
            this.settingsButton.TabIndex = 18;
            // 
            // settingsLabel
            // 
            this.settingsLabel.BackColor = System.Drawing.Color.Transparent;
            this.settingsLabel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("settingsLabel.BackgroundImage")));
            this.settingsLabel.Location = new System.Drawing.Point(24, 24);
            this.settingsLabel.Name = "settingsLabel";
            this.settingsLabel.Size = new System.Drawing.Size(76, 76);
            this.settingsLabel.TabIndex = 2;
            this.settingsLabel.TabStop = false;
            // 
            // btnSettings
            // 
            this.btnSettings.AutoSize = true;
            this.btnSettings.BackColor = System.Drawing.Color.Transparent;
            this.btnSettings.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSettings.Location = new System.Drawing.Point(3, 95);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(93, 30);
            this.btnSettings.TabIndex = 0;
            this.btnSettings.Text = "Settings";
            this.btnSettings.Visible = false;
            // 
            // loadGameButton
            // 
            this.loadGameButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.loadGameButton.BackColor = System.Drawing.Color.DarkMagenta;
            this.loadGameButton.Controls.Add(this.loadGameText);
            this.loadGameButton.Controls.Add(this.btnLoadGame);
            this.loadGameButton.Location = new System.Drawing.Point(476, 267);
            this.loadGameButton.Name = "loadGameButton";
            this.loadGameButton.Size = new System.Drawing.Size(442, 125);
            this.loadGameButton.TabIndex = 16;
            this.loadGameButton.MouseEnter += new System.EventHandler(this.loadGameButton_MouseEnter);
            this.loadGameButton.MouseLeave += new System.EventHandler(this.loadGameButton_MouseLeave);
            // 
            // loadGameText
            // 
            this.loadGameText.BackColor = System.Drawing.Color.Transparent;
            this.loadGameText.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("loadGameText.BackgroundImage")));
            this.loadGameText.Location = new System.Drawing.Point(186, 22);
            this.loadGameText.Name = "loadGameText";
            this.loadGameText.Size = new System.Drawing.Size(76, 76);
            this.loadGameText.TabIndex = 2;
            this.loadGameText.TabStop = false;
            this.loadGameText.Click += new System.EventHandler(this.loadGameText_Click);
            // 
            // btnLoadGame
            // 
            this.btnLoadGame.AutoSize = true;
            this.btnLoadGame.BackColor = System.Drawing.Color.Transparent;
            this.btnLoadGame.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadGame.Location = new System.Drawing.Point(3, 95);
            this.btnLoadGame.Name = "btnLoadGame";
            this.btnLoadGame.Size = new System.Drawing.Size(123, 30);
            this.btnLoadGame.TabIndex = 0;
            this.btnLoadGame.Text = "Load Game";
            this.btnLoadGame.Visible = false;
            // 
            // newGameButton
            // 
            this.newGameButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.newGameButton.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.newGameButton.Controls.Add(this.newGameText);
            this.newGameButton.Controls.Add(this.btnNewGame);
            this.newGameButton.Location = new System.Drawing.Point(476, 136);
            this.newGameButton.Name = "newGameButton";
            this.newGameButton.Size = new System.Drawing.Size(442, 125);
            this.newGameButton.TabIndex = 15;
            this.newGameButton.MouseEnter += new System.EventHandler(this.newGameButton_MouseEnter);
            this.newGameButton.MouseLeave += new System.EventHandler(this.newGameButton_MouseLeave);
            this.newGameButton.MouseHover += new System.EventHandler(this.newGameButton_MouseHover);
            // 
            // newGameText
            // 
            this.newGameText.BackColor = System.Drawing.Color.Transparent;
            this.newGameText.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("newGameText.BackgroundImage")));
            this.newGameText.InitialImage = ((System.Drawing.Image)(resources.GetObject("newGameText.InitialImage")));
            this.newGameText.Location = new System.Drawing.Point(186, 26);
            this.newGameText.Name = "newGameText";
            this.newGameText.Size = new System.Drawing.Size(76, 76);
            this.newGameText.TabIndex = 2;
            this.newGameText.TabStop = false;
            this.newGameText.Click += new System.EventHandler(this.newGame_Click);
            // 
            // btnNewGame
            // 
            this.btnNewGame.AutoSize = true;
            this.btnNewGame.BackColor = System.Drawing.Color.Transparent;
            this.btnNewGame.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewGame.Location = new System.Drawing.Point(2, 95);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(120, 30);
            this.btnNewGame.TabIndex = 0;
            this.btnNewGame.Text = "New Game";
            this.btnNewGame.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(472, 87);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.TabIndex = 20;
            this.progressBar1.Visible = false;
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(945, 798);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.settingsButton);
            this.Controls.Add(this.loadGameButton);
            this.Controls.Add(this.newGameButton);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Menu";
            this.Text = "Form4";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.exitButton.ResumeLayout(false);
            this.exitButton.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.exitLabel)).EndInit();
            this.settingsButton.ResumeLayout(false);
            this.settingsButton.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.settingsLabel)).EndInit();
            this.loadGameButton.ResumeLayout(false);
            this.loadGameButton.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadGameText)).EndInit();
            this.newGameButton.ResumeLayout(false);
            this.newGameButton.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.newGameText)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel exitButton;
        private System.Windows.Forms.PictureBox exitLabel;
        private System.Windows.Forms.Label btnExit;
        private System.Windows.Forms.Panel settingsButton;
        private System.Windows.Forms.PictureBox settingsLabel;
        private System.Windows.Forms.Label btnSettings;
        private System.Windows.Forms.Panel loadGameButton;
        private System.Windows.Forms.PictureBox loadGameText;
        private System.Windows.Forms.Label btnLoadGame;
        private System.Windows.Forms.Panel newGameButton;
        private System.Windows.Forms.PictureBox newGameText;
        private System.Windows.Forms.Label btnNewGame;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}