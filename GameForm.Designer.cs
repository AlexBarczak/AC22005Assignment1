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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
            lblDirectionIndicator = new Label();
            menuStrip1 = new MenuStrip();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            howToPlayToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            BtnMute = new Button();
            lbl_score = new Label();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // lblDirectionIndicator
            // 
            lblDirectionIndicator.Anchor = AnchorStyles.None;
            lblDirectionIndicator.AutoSize = true;
            lblDirectionIndicator.Font = new Font("Ravie", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDirectionIndicator.Location = new Point(460, 745);
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
            menuStrip1.Size = new Size(1273, 24);
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
            aboutToolStripMenuItem.Click += AboutToolStripMenuItem_Click;
            // 
            // howToPlayToolStripMenuItem
            // 
            howToPlayToolStripMenuItem.Name = "howToPlayToolStripMenuItem";
            howToPlayToolStripMenuItem.Size = new Size(138, 22);
            howToPlayToolStripMenuItem.Text = "How to Play";
            howToPlayToolStripMenuItem.Click += HowToPlayToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(138, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            // 
            // BtnMute
            // 
            BtnMute.Anchor = AnchorStyles.None;
            BtnMute.BackColor = Color.Linen;
            BtnMute.BackgroundImage = Properties.Resources.unmute;
            BtnMute.BackgroundImageLayout = ImageLayout.Zoom;
            BtnMute.Location = new Point(1107, 39);
            BtnMute.Name = "BtnMute";
            BtnMute.Size = new Size(58, 62);
            BtnMute.TabIndex = 4;
            BtnMute.UseVisualStyleBackColor = false;
            BtnMute.Click += BtnMute_Click;
            // 
            // lbl_score
            // 
            lbl_score.Anchor = AnchorStyles.None;
            lbl_score.AutoSize = true;
            lbl_score.Font = new Font("Papyrus", 21.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lbl_score.Location = new Point(460, 800);
            lbl_score.Name = "lbl_score";
            lbl_score.Size = new Size(140, 46);
            lbl_score.TabIndex = 5;
            lbl_score.Text = "Score: 0";
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.YellowGreen;
            ClientSize = new Size(1273, 848);
            Controls.Add(lbl_score);
            Controls.Add(BtnMute);
            Controls.Add(lblDirectionIndicator);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            Name = "GameForm";
            Text = "Game";
            WindowState = FormWindowState.Maximized;
            KeyDown += GameForm_KeyDown;
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
        private Button BtnMute;
        private Label lbl_score;
    }
}