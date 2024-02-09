namespace AC22005Assignment1
{
    partial class MenuForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuForm));
            btnStart = new Button();
            btnExit = new Button();
            lblTitle = new Label();
            BtnMute = new Button();
            SuspendLayout();
            // 
            // btnStart
            // 
            btnStart.Anchor = AnchorStyles.None;
            btnStart.Location = new Point(505, 329);
            btnStart.Margin = new Padding(3, 2, 3, 2);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(130, 54);
            btnStart.TabIndex = 0;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnExit
            // 
            btnExit.Anchor = AnchorStyles.None;
            btnExit.Location = new Point(505, 401);
            btnExit.Margin = new Padding(3, 2, 3, 2);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(130, 54);
            btnExit.TabIndex = 1;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.None;
            lblTitle.AutoSize = true;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.BorderStyle = BorderStyle.Fixed3D;
            lblTitle.Font = new Font("Stencil", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(429, 237);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(291, 59);
            lblTitle.TabIndex = 2;
            lblTitle.Text = "Snake-Man";
            // 
            // BtnMute
            // 
            BtnMute.Anchor = AnchorStyles.None;
            BtnMute.BackColor = Color.Linen;
            BtnMute.BackgroundImage = Properties.Resources.unmute;
            BtnMute.BackgroundImageLayout = ImageLayout.Zoom;
            BtnMute.Location = new Point(1057, 28);
            BtnMute.Name = "BtnMute";
            BtnMute.Size = new Size(58, 62);
            BtnMute.TabIndex = 3;
            BtnMute.UseVisualStyleBackColor = false;
            BtnMute.Click += BtnMute_Click;
            // 
            // MenuForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.YellowGreen;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1143, 673);
            Controls.Add(BtnMute);
            Controls.Add(lblTitle);
            Controls.Add(btnExit);
            Controls.Add(btnStart);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            Name = "MenuForm";
            Text = "Menu";
            WindowState = FormWindowState.Maximized;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnStart;
        private Button btnExit;
        private Label lblTitle;
        private Button BtnMute;
    }
}