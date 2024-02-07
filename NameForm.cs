using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AC22005Assignment1
{
    public partial class NameForm : Form
    {
        public NameForm()
        {
            LblNameForm.Size = new Size(74, 25);
            InitializeComponent();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (Program.name != "")
            {
                GameForm gameForm = new GameForm();
                gameForm.Show();
                this.Hide();
                gameForm.FormClosed += (s, args) => Show();
            }
            else if (Program.name == "")
            {
                MessageBox.Show("You need to enter a name to play", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Mute Button
        private void BtnMute_Click(object sender, EventArgs e)
        {
            if (Program.musicPlaying)
            {
                Program.player.Stop();
                BtnMute.BackgroundImage = Image.FromFile(@"../../../unmute.bmp");
                Program.musicPlaying = false;
            }
            else
            {
                Program.musicPlaying = true;
                Program.player.PlayLooping();
                BtnMute.BackgroundImage = Image.FromFile(@"../../../mute.bmp");
            }
        }

        private void BtnChangeName_Click(object sender, EventArgs e)
        {
            if (txtBxName.Text != "")
            {
                Program.name = txtBxName.Text;
                LblNameForm.Text = "Name: " + (Program.name);
            }
            else
            {
                MessageBox.Show("You need to enter a name to play", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
