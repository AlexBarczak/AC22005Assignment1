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
            Debug.WriteLine("loading");
            // initialise level data variable
            levelBitmap = new Bitmap(@"../../../level.bmp");
            levelMapData = new int[levelBitmap.Width, levelBitmap.Height];
            grid = new Button[levelBitmap.Width, levelBitmap.Height];

            // translate bitmap into the 2D array for level data, though it might not even be necessary
            for (int i = 0; i < levelBitmap.Width; i++)
            {
                for(int j = 0; j < levelBitmap.Height; j++)
                {
                    levelMapData[i,j] = levelBitmap.GetPixel(i, j).R;
                }
            }


            InitializeComponent();
            for (int x = 0; x < levelBitmap.Width; x++)
            {
                for(int y = 0; y < levelBitmap.Height; y++)
                {
                    grid[x, y] = new Button();
                    grid[x, y].FlatStyle = FlatStyle.Flat;
                    grid[x, y].FlatAppearance.BorderSize = 0;
                    grid[x, y].SetBounds(cellSize * x, cellSize * y, cellSize , cellSize);
                    if (levelMapData[x, y] == EMPTY_TILE) grid[x, y].BackColor = Color.White;
                    else if (levelMapData[x, y] == WALL_TILE) grid[x, y].BackColor = Color.Black;
                    else grid[x, y].BackColor = Color.Blue;

                    grid[x, y].Click += new EventHandler(this.GridButtonClicked);
                    Controls.Add(grid[x, y]);
                }
            }
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
            if (Math.Abs(diffX) > Math.Abs(diffY)) {
                if (diffX > 0)
                {
                    lblDirectionIndicator.Text = "Direction: left";
                } else
                {
                    lblDirectionIndicator.Text = "Direction: Right";
                }
            } 
            else {
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

        private void GameForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
