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

        // bitmap data is currently set to 1 bit per pixel meaning the color values will either be 0 or 255, we'll expand the color values as we add more stuff
        public const int EMPTY_TILE = 255;
        public const int WALL_TILE = 0;
        public const int cellSize = 20;

        Button[,] grid;
        Bitmap levelBitmap;
        int[,] levelMapData;
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


            InitializeComponent();

            int xOffset = (this.ClientSize.Width - cellSize * levelBitmap.Width) / 2;
            int yOffset = (this.ClientSize.Height - cellSize * levelBitmap.Height) / 2;

            for (int x = 0; x < levelBitmap.Width; x++)
            {
                for (int y = 0; y < levelBitmap.Height; y++)
                {
                    grid[x, y] = new Button();
                    grid[x, y].FlatStyle = FlatStyle.Flat;
                    grid[x, y].FlatAppearance.BorderSize = 0;
                    //grid[x, y].SetBounds(cellSize * x, cellSize * y, cellSize , cellSize);
                    grid[x, y].SetBounds(xOffset + cellSize * x, yOffset + cellSize * y, cellSize, cellSize);

                    if (levelMapData[x, y] == EMPTY_TILE) grid[x, y].BackColor = Color.White;
                    else if (levelMapData[x, y] == WALL_TILE) grid[x, y].BackColor = Color.Black;
                    else grid[x, y].BackColor = Color.Blue;

                    grid[x, y].Click += new EventHandler(this.GridButtonClicked);
                    Controls.Add(grid[x, y]);
                }
            }
            Button exitButton = new Button();
            exitButton.Text = "Exit";
            exitButton.Size = new Size(80, 30);
            exitButton.Location = new Point(10, (this.ClientSize.Height - exitButton.Height) / 2);
            exitButton.Click += new EventHandler(this.ExitButtonClicked);
            Controls.Add(exitButton);

            Button helpButton = new Button();
            helpButton.Text = "How to play";
            helpButton.Size = new Size(80, 30);
            helpButton.Location = new Point(10, ((this.ClientSize.Height - helpButton.Height) / 2) - 40);
            helpButton.Click += new EventHandler(this.HelpButtonClicked);
            Controls.Add(helpButton);
        }

        private void GridButtonClicked(object sender, EventArgs e)
        {
            // figure out center of grid
            int centerX = cellSize * levelBitmap.Width / 2;
            int centerY = cellSize * levelBitmap.Height / 2;

            // figure out difference between center and tile pressed
            int diffX = centerX - ((Button)sender).Location.X;
            int diffY = centerY - ((Button)sender).Location.Y;

            Debug.WriteLine("x: " + diffX.ToString() + " y: " + diffY.ToString());

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
    }
}
