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
            this.lblDirectionIndicator = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblDirectionIndicator
            // 
            this.lblDirectionIndicator.AutoSize = true;
            this.lblDirectionIndicator.Font = new System.Drawing.Font("Ravie", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDirectionIndicator.Location = new System.Drawing.Point(12, 682);
            this.lblDirectionIndicator.Name = "lblDirectionIndicator";
            this.lblDirectionIndicator.Size = new System.Drawing.Size(325, 39);
            this.lblDirectionIndicator.TabIndex = 0;
            this.lblDirectionIndicator.Text = "Direction: None";
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 848);
            this.Controls.Add(this.lblDirectionIndicator);
            this.Name = "GameForm";
            this.Text = "Game";
            this.Load += new System.EventHandler(this.GameForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblDirectionIndicator;
    }
}