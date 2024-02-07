using System.Media;



namespace AC22005Assignment1
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
            this.Icon = new Icon(@"../../../Snake.ico");
            Program.player.PlayLooping();
        }
        //Start Button
        private void btnStart_Click(object sender, EventArgs e)
        {
            GameForm gameForm = new GameForm();
            gameForm.Show();
            this.Hide();
            gameForm.FormClosed += (s, args) => Show();
        }
        //Exit Button
        private void btnExit_Click(object sender, EventArgs e)
        {
            Program.player.Stop();
            Application.Exit();
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
    }
}