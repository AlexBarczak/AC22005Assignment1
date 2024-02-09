using System.Net.NetworkInformation;

namespace AC22005Assignment1
{
    internal static class Program
    {
        //Player name
        public static string name = "";
        //Music player
        public static System.Media.SoundPlayer player = new(@"../../../LikeADream.wav");
        public static bool musicPlaying = true;






        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MenuForm());
        }
    }
}