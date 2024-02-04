using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace AC22005Assignment1
{
    public partial class GameForm : Form
    {
        public static bool isGameStart;
        Game g;
        private Label timerLabel;
        private System.Windows.Forms.Timer timer;
        private int timeInSeconds;

        // bitmap data is currently set to 1 bit per pixel meaning the color values will either be 0 or 255, we'll expand the color values as we add more stuff
        public const int EMPTY_TILE = 255;
        public const int WALL_TILE = 0;
        public const int cellSize = 20;

        Button[,] grid;
        Bitmap levelBitmap;
        static int[,] levelMapData;
        //levelMapData = new int[,]
        //{
        //    {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
        //    {1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1 },
        //    {1,0,1,1,1,1,0,1,1,1,1,1,0,1,1,0,1,1,1,1,1,0,1,1,1,1,0,1 },
        //    {1,0,1,1,1,1,0,1,1,1,1,1,0,1,1,0,1,1,1,1,1,0,1,1,1,1,0,1 },
        //    {1,0,1,1,1,1,0,1,1,1,1,1,0,1,1,0,1,1,1,1,1,0,1,1,1,1,0,1 },
        //    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
        //    {1,0,1,1,1,1,0,1,1,0,1,1,1,1,1,1,1,1,0,1,1,0,1,1,1,1,0,1 },
        //    {1,0,1,1,1,1,0,1,1,0,1,1,1,1,1,1,1,1,0,1,1,0,1,1,1,1,0,1 },
        //    {1,0,0,0,0,0,0,1,1,0,0,0,0,1,1,0,0,0,0,1,1,0,0,0,0,0,0,1 },
        //    {1,1,1,1,1,1,0,1,1,1,1,1,0,1,1,0,1,1,1,1,1,0,1,1,1,1,1,1 },
        //    {2,2,2,2,2,1,0,1,1,1,1,1,0,1,1,0,1,1,1,1,1,0,1,2,2,2,2,2 },
        //    {2,2,2,2,2,1,0,1,1,0,0,0,0,0,0,0,0,0,0,1,1,0,1,2,2,2,2,2 },
        //    {2,2,2,2,2,1,0,1,1,0,0,0,0,0,0,0,0,0,0,1,1,0,1,2,2,2,2,2 },
        //    {1,1,1,1,1,1,0,1,1,0,0,0,0,0,0,0,0,0,0,1,1,0,1,1,1,1,1,1 },
        //    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        //    {1,1,1,1,1,1,0,1,1,0,0,0,0,0,0,0,0,0,0,1,1,0,1,1,1,1,1,1 },
        //    {2,2,2,2,2,1,0,1,1,0,0,0,0,0,0,0,0,0,0,1,1,0,1,2,2,2,2,2 },
        //    {2,2,2,2,2,1,0,1,1,0,0,0,0,0,0,0,0,0,0,1,1,0,1,2,2,2,2,2 },
        //    {2,2,2,2,2,1,0,1,1,0,1,1,1,1,1,1,1,1,0,1,1,0,1,2,2,2,2,2 },
        //    {1,1,1,1,1,1,0,1,1,0,1,1,1,1,1,1,1,1,0,1,1,0,1,1,1,1,1,1 },
        //    {1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1 },
        //    {1,0,1,1,1,1,0,1,1,1,1,1,0,1,1,0,1,1,1,1,1,0,1,1,1,1,0,1 },
        //    {1,0,1,1,1,1,0,1,1,1,1,1,0,1,1,0,1,1,1,1,1,0,1,1,1,1,0,1 },
        //    {1,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,1 },
        //    {1,1,1,0,1,1,0,1,1,0,1,1,1,1,1,1,1,1,0,1,1,0,1,1,0,1,1,1 },
        //    {1,1,1,0,1,1,0,1,1,0,1,1,1,1,1,1,1,1,0,1,1,0,1,1,0,1,1,1 },
        //    {1,0,0,0,0,0,0,1,1,0,0,0,0,1,1,0,0,0,0,1,1,0,0,0,0,0,0,1 },
        //    {1,0,1,1,1,1,1,1,1,1,1,1,0,1,1,0,1,1,1,1,1,1,1,1,1,1,0,1 },
        //    {1,0,1,1,1,1,1,1,1,1,1,1,0,1,1,0,1,1,1,1,1,1,1,1,1,1,0,1 },
        //    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
        //    {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }

        //};

        public GameForm()
        {

            InitializeComponent();
            trBar.Value = 5;
            this.Icon = new Icon(@"../../../Snake.ico");
            //Size Form, load Bitmap
            this.AutoSize = true;
            Debug.WriteLine("loading");
            // initialise level data variable
            levelBitmap = new Bitmap(@"../../../level.bmp");
            levelMapData = new int[levelBitmap.Width, levelBitmap.Height];
            grid = new Button[levelBitmap.Width, levelBitmap.Height];

            // translate bitmap into the 2D array for level data, though it might not even be necessary
            for (int i = 0; i < levelBitmap.Width; i++)
            {
                for (int j = 0; j < levelBitmap.Height; j++)
                {
                    levelMapData[i, j] = levelBitmap.GetPixel(i, j).R;
                }
            }

            //Create bitmap
            int xOffset = (this.ClientSize.Width - cellSize * levelBitmap.Width) / 2;
            int yOffset = (this.ClientSize.Height - cellSize * levelBitmap.Height) / 2;

            for (int x = 0; x < levelBitmap.Width; x++)
            {
                for (int y = 0; y < levelBitmap.Height; y++)
                {
                    grid[x, y] = new Button();
                    grid[x, y].Name = x + "," + y;
                    grid[x, y].FlatStyle = FlatStyle.Flat;
                    grid[x, y].FlatAppearance.BorderSize = 0;
                    grid[x, y].SetBounds(xOffset + cellSize * x, yOffset + cellSize * y, cellSize, cellSize);

                    if (levelMapData[x, y] == EMPTY_TILE) grid[x, y].BackColor = Color.White;
                    else if (levelMapData[x, y] == WALL_TILE) grid[x, y].BackColor = Color.Black;
                    else grid[x, y].BackColor = Color.Blue;

                    grid[x, y].Click += new EventHandler(this.GridButtonClicked);
                    grid[x, y].Anchor = AnchorStyles.None;
                    Controls.Add(grid[x, y]);
                }
            }

            //Create timer
            timerLabel = new Label();
            timerLabel.Text = "Time: -2 seconds";
            timerLabel.AutoSize = true;
            timerLabel.Location = new Point((xOffset) * 2, 40);
            timerLabel.TextAlign = ContentAlignment.MiddleCenter;
            timerLabel.Size = new Size(120, 80);
            timerLabel.Font = new Font("Stencil", 36);
            timerLabel.Anchor = AnchorStyles.None;
            Controls.Add(timerLabel);

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000; //Milliseconds
            timer.Tick += TimerTick;
            timer.Start();

            //Create Exit and Help buttons
            Button exitButton = new Button();
            exitButton.Text = "Exit";
            exitButton.Size = new Size(80, 30);
            exitButton.Location = new Point(10, (this.ClientSize.Height - exitButton.Height) / 2);
            exitButton.Click += new EventHandler(this.ExitButtonClicked);
            exitButton.Anchor = AnchorStyles.None;
            Controls.Add(exitButton);

            Button helpButton = new Button();
            helpButton.Text = "How to play";
            helpButton.Size = new Size(80, 30);
            helpButton.Location = new Point(10, ((this.ClientSize.Height - helpButton.Height) / 2) - 40);
            helpButton.Click += new EventHandler(this.HelpButtonClicked);
            helpButton.Anchor = AnchorStyles.None;
            Controls.Add(helpButton);

            g = new Game();
            isGameStart = false;
            //g.printGridInts();
        }

        public static void updateGraphics()
        {

        }

        public static int[,] getLevelMapData()
        {
            return levelMapData;
        }

        private void GridButtonClicked(object sender, EventArgs e)
        {


            // figure out center of grid
            int centerX = this.ClientSize.Width / 2;
            int centerY = this.ClientSize.Height / 2;

            // figure out difference between center and tile pressed
            int diffX = centerX - ((Button)sender).Location.X - cellSize / 2;
            int diffY = centerY - ((Button)sender).Location.Y - cellSize / 2;

            Debug.WriteLine("x: " + diffX.ToString() + " y: " + diffY.ToString());
            Debug.WriteLine(((Button)sender).Name);

            // right, left or up, down
            if (Math.Abs(diffX) > Math.Abs(diffY))
            {
                if (diffX > 0)
                {
                    lblDirectionIndicator.Text = "Direction: left";
                }
                else
                {
                    lblDirectionIndicator.Text = "Direction: Right";
                }
            }
            else
            {
                if (diffY > 0)
                {
                    lblDirectionIndicator.Text = "Direction: Up";
                }
                else
                {
                    lblDirectionIndicator.Text = "Direction: Down";
                }
            }

            if(!isGameStart) isGameStart = true;
            g.mainGameLoop();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            timeInSeconds++;
            timerLabel.Text = "Time: " + timeInSeconds + " seconds";
            if (timeInSeconds == 999)
            {
                timer.Stop();
            }
        }

        private void ExitButtonClicked(object sender, EventArgs e)
        {
            this.Close(); // Close the current form
        }

        private void HelpButtonClicked(object sender, EventArgs e)
        {
            MessageBox.Show("I'VE GOT NO CLUE LMAOOOOO", "How to Play", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void GameForm_Load(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult aboutResult;
            aboutResult = MessageBox.Show("Snake-Man developed in C# by Alex Barczak (2497555), Flynn Henderson (2502464) and Ben Houghton (2498662)", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void howToPlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("I'VE GOT NO CLUE LMAOOOOO", "How to Play", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void trBar_Scroll(object sender, EventArgs e)
        {
            lblVolume.Text = "" + trBar.Value;
            
        }

        private void setVolume(int sliderVal)
        {
            //MenuForm.player.
        }
    }
}
