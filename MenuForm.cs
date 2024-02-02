using System.Media;



namespace AC22005Assignment1
{
    public partial class MenuForm : Form
    {
        System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"../../../LikeADream.wav");
        public MenuForm()
        {
            InitializeComponent();
            this.Icon = new Icon(@"../../../Snake.ico");
            player.PlayLooping();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            GameForm gameForm = new GameForm();
            gameForm.Show();
            this.Hide();
            gameForm.FormClosed += (s, args) => Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            player.Stop();
            Application.Exit();
        }
    }
}