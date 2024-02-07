namespace AC22005Assignment1
{
    partial class NameForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NameForm));
            BtnMute = new Button();
            BtnBack = new Button();
            txtBxName = new TextBox();
            BtnChangeName = new Button();
            BtnStart = new Button();
            LblName = new Label();
            LblNameForm = new Label();
            SuspendLayout();
            // 
            // BtnMute
            // 
            BtnMute.Anchor = AnchorStyles.None;
            BtnMute.BackColor = Color.Linen;
            BtnMute.BackgroundImage = (Image)resources.GetObject("BtnMute.BackgroundImage");
            BtnMute.BackgroundImageLayout = ImageLayout.Zoom;
            BtnMute.Location = new Point(1057, 28);
            BtnMute.Name = "BtnMute";
            BtnMute.Size = new Size(58, 62);
            BtnMute.TabIndex = 4;
            BtnMute.UseVisualStyleBackColor = false;
            BtnMute.Click += BtnMute_Click;
            // 
            // BtnBack
            // 
            BtnBack.BackColor = Color.YellowGreen;
            BtnBack.Location = new Point(86, 34);
            BtnBack.Name = "BtnBack";
            BtnBack.Size = new Size(90, 52);
            BtnBack.TabIndex = 5;
            BtnBack.Text = "Back";
            BtnBack.UseVisualStyleBackColor = false;
            BtnBack.Click += BtnBack_Click;
            // 
            // txtBxName
            // 
            txtBxName.Anchor = AnchorStyles.None;
            txtBxName.Cursor = Cursors.IBeam;
            txtBxName.Font = new Font("Times New Roman", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBxName.Location = new Point(502, 128);
            txtBxName.MaxLength = 15;
            txtBxName.Name = "txtBxName";
            txtBxName.Size = new Size(185, 22);
            txtBxName.TabIndex = 5;
            // 
            // BtnChangeName
            // 
            BtnChangeName.Anchor = AnchorStyles.None;
            BtnChangeName.BackColor = Color.YellowGreen;
            BtnChangeName.Font = new Font("Times New Roman", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtnChangeName.Location = new Point(536, 171);
            BtnChangeName.Name = "BtnChangeName";
            BtnChangeName.Size = new Size(127, 34);
            BtnChangeName.TabIndex = 7;
            BtnChangeName.Text = "Change Name";
            BtnChangeName.UseVisualStyleBackColor = false;
            BtnChangeName.Click += BtnChangeName_Click;
            // 
            // BtnStart
            // 
            BtnStart.BackColor = Color.YellowGreen;
            BtnStart.Font = new Font("Times New Roman", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtnStart.ImageAlign = ContentAlignment.MiddleRight;
            BtnStart.Location = new Point(521, 467);
            BtnStart.Name = "BtnStart";
            BtnStart.Size = new Size(169, 83);
            BtnStart.TabIndex = 8;
            BtnStart.Text = "Start";
            BtnStart.UseVisualStyleBackColor = false;
            BtnStart.Click += BtnStart_Click;
            // 
            // LblName
            // 
            LblName.Anchor = AnchorStyles.None;
            LblName.AutoSize = true;
            LblName.BackColor = Color.Transparent;
            LblName.BorderStyle = BorderStyle.Fixed3D;
            LblName.Font = new Font("Stencil", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            LblName.Location = new Point(443, 53);
            LblName.Name = "LblName";
            LblName.Size = new Size(307, 59);
            LblName.TabIndex = 9;
            LblName.Text = "Your Name:";
            // 
            // LblNameForm
            // 
            LblNameForm.AutoSize = true;
            LblNameForm.BackColor = Color.YellowGreen;
            LblNameForm.Font = new Font("Times New Roman", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            LblNameForm.ForeColor = Color.YellowGreen;
            LblNameForm.Location = new Point(536, 264);
            LblNameForm.Name = "LblNameForm";
            LblNameForm.Size = new Size(0, 15);
            LblNameForm.TabIndex = 10;
            // 
            // NameForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.YellowGreen;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1143, 673);
            Controls.Add(LblNameForm);
            Controls.Add(LblName);
            Controls.Add(BtnStart);
            Controls.Add(BtnChangeName);
            Controls.Add(txtBxName);
            Controls.Add(BtnBack);
            Controls.Add(BtnMute);
            Name = "NameForm";
            Text = "NameForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnMute;
        private Button BtnBack;
        private TextBox txtBxName;
        private Button BtnChangeName;
        private Button BtnStart;
        private Label LblName;
        private Label LblNameForm;
    }
}