namespace LiveIT2._1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && (components != null) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._waterButton = new System.Windows.Forms.Button();
            this._dirtButton = new System.Windows.Forms.Button();
            this._snowButton = new System.Windows.Forms.Button();
            this._desertButton = new System.Windows.Forms.Button();
            this._grassButton = new System.Windows.Forms.Button();
            this._changeTextureButton = new System.Windows.Forms.Button();
            this._fillTextureButton = new System.Windows.Forms.Button();
            this._fpsTextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this._buttonExit = new System.Windows.Forms.Button();
            this.AnimalButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.animalsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.catToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rabbitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.elephantToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._buttonFollowAnimal = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _waterButton
            // 
            this._waterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this._waterButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._waterButton.ForeColor = System.Drawing.Color.Transparent;
            this._waterButton.Location = new System.Drawing.Point(10, 289);
            this._waterButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._waterButton.Name = "_waterButton";
            this._waterButton.Size = new System.Drawing.Size(111, 30);
            this._waterButton.TabIndex = 0;
            this._waterButton.Text = "Water";
            this._waterButton.UseVisualStyleBackColor = false;
            this._waterButton.Click += new System.EventHandler(this._waterButton_Click);
            // 
            // _dirtButton
            // 
            this._dirtButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this._dirtButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._dirtButton.ForeColor = System.Drawing.Color.Transparent;
            this._dirtButton.Location = new System.Drawing.Point(10, 327);
            this._dirtButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._dirtButton.Name = "_dirtButton";
            this._dirtButton.Size = new System.Drawing.Size(111, 30);
            this._dirtButton.TabIndex = 1;
            this._dirtButton.Text = "Forest";
            this._dirtButton.UseVisualStyleBackColor = false;
            this._dirtButton.Click += new System.EventHandler(this._dirtButton_Click);
            // 
            // _snowButton
            // 
            this._snowButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this._snowButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._snowButton.ForeColor = System.Drawing.Color.Transparent;
            this._snowButton.Location = new System.Drawing.Point(10, 365);
            this._snowButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._snowButton.Name = "_snowButton";
            this._snowButton.Size = new System.Drawing.Size(111, 30);
            this._snowButton.TabIndex = 2;
            this._snowButton.Text = "Snow";
            this._snowButton.UseVisualStyleBackColor = false;
            this._snowButton.Click += new System.EventHandler(this._snowButton_Click);
            // 
            // _desertButton
            // 
            this._desertButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(18)))));
            this._desertButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._desertButton.ForeColor = System.Drawing.Color.Transparent;
            this._desertButton.Location = new System.Drawing.Point(12, 403);
            this._desertButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._desertButton.Name = "_desertButton";
            this._desertButton.Size = new System.Drawing.Size(111, 30);
            this._desertButton.TabIndex = 3;
            this._desertButton.Text = "Desert";
            this._desertButton.UseVisualStyleBackColor = false;
            this._desertButton.Click += new System.EventHandler(this._desertButton_Click);
            // 
            // _grassButton
            // 
            this._grassButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this._grassButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._grassButton.ForeColor = System.Drawing.Color.Transparent;
            this._grassButton.Location = new System.Drawing.Point(12, 441);
            this._grassButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._grassButton.Name = "_grassButton";
            this._grassButton.Size = new System.Drawing.Size(111, 30);
            this._grassButton.TabIndex = 4;
            this._grassButton.Text = "Grass";
            this._grassButton.UseVisualStyleBackColor = false;
            this._grassButton.Click += new System.EventHandler(this._grassButton_Click);
            // 
            // _changeTextureButton
            // 
            this._changeTextureButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this._changeTextureButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._changeTextureButton.ForeColor = System.Drawing.Color.Transparent;
            this._changeTextureButton.Location = new System.Drawing.Point(10, 190);
            this._changeTextureButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._changeTextureButton.Name = "_changeTextureButton";
            this._changeTextureButton.Size = new System.Drawing.Size(205, 30);
            this._changeTextureButton.TabIndex = 7;
            this._changeTextureButton.Text = "Change Texture";
            this._changeTextureButton.UseVisualStyleBackColor = false;
            this._changeTextureButton.Click += new System.EventHandler(this._changeTextureButton_Click);
            // 
            // _fillTextureButton
            // 
            this._fillTextureButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this._fillTextureButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._fillTextureButton.ForeColor = System.Drawing.Color.Transparent;
            this._fillTextureButton.Location = new System.Drawing.Point(10, 236);
            this._fillTextureButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._fillTextureButton.Name = "_fillTextureButton";
            this._fillTextureButton.Size = new System.Drawing.Size(205, 30);
            this._fillTextureButton.TabIndex = 8;
            this._fillTextureButton.Text = "Fill Texture";
            this._fillTextureButton.UseVisualStyleBackColor = false;
            this._fillTextureButton.Click += new System.EventHandler(this._fillTextureButton_Click);
            // 
            // _fpsTextBox
            // 
            this._fpsTextBox.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this._fpsTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._fpsTextBox.Dock = System.Windows.Forms.DockStyle.Right;
            this._fpsTextBox.Enabled = false;
            this._fpsTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this._fpsTextBox.ForeColor = System.Drawing.Color.Black;
            this._fpsTextBox.Location = new System.Drawing.Point(833, 34);
            this._fpsTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._fpsTextBox.Multiline = true;
            this._fpsTextBox.Name = "_fpsTextBox";
            this._fpsTextBox.Size = new System.Drawing.Size(187, 685);
            this._fpsTextBox.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.Transparent;
            this.button1.Location = new System.Drawing.Point(252, 94);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 29);
            this.button1.TabIndex = 10;
            this.button1.Text = "Show Debug Info";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // _buttonExit
            // 
            this._buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonExit.BackColor = System.Drawing.Color.Red;
            this._buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._buttonExit.ForeColor = System.Drawing.Color.Transparent;
            this._buttonExit.Location = new System.Drawing.Point(1281, 0);
            this._buttonExit.Name = "_buttonExit";
            this._buttonExit.Size = new System.Drawing.Size(62, 34);
            this._buttonExit.TabIndex = 13;
            this._buttonExit.Text = "Exit";
            this._buttonExit.UseVisualStyleBackColor = false;
            this._buttonExit.Click += new System.EventHandler(this._buttonExit_Click);
            // 
            // AnimalButton
            // 
            this.AnimalButton.BackColor = System.Drawing.Color.DarkOrange;
            this.AnimalButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AnimalButton.ForeColor = System.Drawing.Color.Transparent;
            this.AnimalButton.Location = new System.Drawing.Point(12, 144);
            this.AnimalButton.Name = "AnimalButton";
            this.AnimalButton.Size = new System.Drawing.Size(203, 30);
            this.AnimalButton.TabIndex = 14;
            this.AnimalButton.Text = "Place Animal";
            this.AnimalButton.UseVisualStyleBackColor = false;
            this.AnimalButton.Click += new System.EventHandler(this.AnimalButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(121)))), ((int)(((byte)(53)))));
            this.menuStrip1.Font = new System.Drawing.Font("Forte", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.animalsToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1020, 34);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // animalsToolStripMenuItem
            // 
            this.animalsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.catToolStripMenuItem,
            this.dogToolStripMenuItem,
            this.rabbitToolStripMenuItem,
            this.lionToolStripMenuItem,
            this.elephantToolStripMenuItem,
            this.cowToolStripMenuItem});
            this.animalsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(167)))), ((int)(((byte)(240)))));
            this.animalsToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.animalsToolStripMenuItem.Name = "animalsToolStripMenuItem";
            this.animalsToolStripMenuItem.Size = new System.Drawing.Size(113, 30);
            this.animalsToolStripMenuItem.Text = "Animals";
            // 
            // catToolStripMenuItem
            // 
            this.catToolStripMenuItem.Name = "catToolStripMenuItem";
            this.catToolStripMenuItem.Size = new System.Drawing.Size(178, 30);
            this.catToolStripMenuItem.Text = "Cat";
            this.catToolStripMenuItem.Click += new System.EventHandler(this.catToolStripMenuItem_Click);
            // 
            // dogToolStripMenuItem
            // 
            this.dogToolStripMenuItem.Name = "dogToolStripMenuItem";
            this.dogToolStripMenuItem.Size = new System.Drawing.Size(178, 30);
            this.dogToolStripMenuItem.Text = "Dog";
            this.dogToolStripMenuItem.Click += new System.EventHandler(this.dogToolStripMenuItem_Click);
            // 
            // rabbitToolStripMenuItem
            // 
            this.rabbitToolStripMenuItem.Name = "rabbitToolStripMenuItem";
            this.rabbitToolStripMenuItem.Size = new System.Drawing.Size(178, 30);
            this.rabbitToolStripMenuItem.Text = "Rabbit";
            this.rabbitToolStripMenuItem.Click += new System.EventHandler(this.rabbitToolStripMenuItem_Click);
            // 
            // lionToolStripMenuItem
            // 
            this.lionToolStripMenuItem.Name = "lionToolStripMenuItem";
            this.lionToolStripMenuItem.Size = new System.Drawing.Size(178, 30);
            this.lionToolStripMenuItem.Text = "Lion";
            this.lionToolStripMenuItem.Click += new System.EventHandler(this.lionToolStripMenuItem_Click);
            // 
            // elephantToolStripMenuItem
            // 
            this.elephantToolStripMenuItem.Name = "elephantToolStripMenuItem";
            this.elephantToolStripMenuItem.Size = new System.Drawing.Size(178, 30);
            this.elephantToolStripMenuItem.Text = "Elephant";
            this.elephantToolStripMenuItem.Click += new System.EventHandler(this.elephantToolStripMenuItem_Click);
            // 
            // cowToolStripMenuItem
            // 
            this.cowToolStripMenuItem.Name = "cowToolStripMenuItem";
            this.cowToolStripMenuItem.Size = new System.Drawing.Size(178, 30);
            this.cowToolStripMenuItem.Text = "Cow";
            this.cowToolStripMenuItem.Click += new System.EventHandler(this.cowToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(110)))), ((int)(((byte)(123)))));
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(69, 30);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(110)))), ((int)(((byte)(123)))));
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(73, 30);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // _buttonFollowAnimal
            // 
            this._buttonFollowAnimal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(108)))), ((int)(((byte)(179)))));
            this._buttonFollowAnimal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._buttonFollowAnimal.ForeColor = System.Drawing.Color.Transparent;
            this._buttonFollowAnimal.Location = new System.Drawing.Point(12, 94);
            this._buttonFollowAnimal.Name = "_buttonFollowAnimal";
            this._buttonFollowAnimal.Size = new System.Drawing.Size(203, 32);
            this._buttonFollowAnimal.TabIndex = 17;
            this._buttonFollowAnimal.Text = "Follow Animal";
            this._buttonFollowAnimal.UseVisualStyleBackColor = false;
            this._buttonFollowAnimal.Click += new System.EventHandler(this._buttonFollowAnimal_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1020, 719);
            this.Controls.Add(this._buttonFollowAnimal);
            this.Controls.Add(this.AnimalButton);
            this.Controls.Add(this._buttonExit);
            this.Controls.Add(this.button1);
            this.Controls.Add(this._fpsTextBox);
            this.Controls.Add(this._fillTextureButton);
            this.Controls.Add(this._changeTextureButton);
            this.Controls.Add(this._grassButton);
            this.Controls.Add(this._desertButton);
            this.Controls.Add(this._snowButton);
            this.Controls.Add(this._dirtButton);
            this.Controls.Add(this._waterButton);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Forte", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _waterButton;
        private System.Windows.Forms.Button _dirtButton;
        private System.Windows.Forms.Button _snowButton;
        private System.Windows.Forms.Button _desertButton;
        private System.Windows.Forms.Button _grassButton;
        private System.Windows.Forms.Button _changeTextureButton;
        private System.Windows.Forms.Button _fillTextureButton;
        private System.Windows.Forms.TextBox _fpsTextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button _buttonExit;
        private System.Windows.Forms.Button AnimalButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem animalsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem catToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rabbitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem elephantToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.Button _buttonFollowAnimal;

    }
}

