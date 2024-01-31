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
            SuspendLayout();
            // 
            // lblDirectionIndicator
            // 
            lblDirectionIndicator.AutoSize = true;
            lblDirectionIndicator.Font = new Font("Ravie", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDirectionIndicator.Location = new Point(270, 745);
            lblDirectionIndicator.Name = "lblDirectionIndicator";
            lblDirectionIndicator.Size = new Size(325, 39);
            lblDirectionIndicator.TabIndex = 0;
            lblDirectionIndicator.Text = "Direction: None";
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.YellowGreen;
            ClientSize = new Size(893, 848);
            Controls.Add(lblDirectionIndicator);
            Name = "GameForm";
            Text = "Game";
            Load += GameForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblDirectionIndicator;
    }
}