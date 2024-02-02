namespace AC22005Assignment1
{
    partial class GameForm
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
            if (disposing && (components != null))
            {
                components.Dispose();
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
            lblDirectionIndicator = new Label();
            menuStrip1 = new MenuStrip();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            howToPlayToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // lblDirectionIndicator
            // 
            lblDirectionIndicator.Anchor = AnchorStyles.None;
            lblDirectionIndicator.AutoSize = true;
            lblDirectionIndicator.Font = new Font("Ravie", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDirectionIndicator.Location = new Point(270, 745);
            lblDirectionIndicator.Name = "lblDirectionIndicator";
            lblDirectionIndicator.Size = new Size(325, 39);
            lblDirectionIndicator.TabIndex = 0;
            lblDirectionIndicator.Text = "Direction: None";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { optionsToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(893, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aboutToolStripMenuItem, howToPlayToolStripMenuItem, exitToolStripMenuItem });
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(61, 20);
            optionsToolStripMenuItem.Text = "Options";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(138, 22);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // howToPlayToolStripMenuItem
            // 
            howToPlayToolStripMenuItem.Name = "howToPlayToolStripMenuItem";
            howToPlayToolStripMenuItem.Size = new Size(138, 22);
            howToPlayToolStripMenuItem.Text = "How to Play";
            howToPlayToolStripMenuItem.Click += howToPlayToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(138, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.YellowGreen;
            ClientSize = new Size(893, 848);
            Controls.Add(lblDirectionIndicator);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "GameForm";
            Text = "Game";
            WindowState = FormWindowState.Maximized;
            Load += GameForm_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblDirectionIndicator;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem howToPlayToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
    }
}