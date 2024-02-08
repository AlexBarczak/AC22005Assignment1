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
using System.Windows.Input;

namespace AC22005Assignment1
{
    public partial class GameForm : Form
    {
        public bool isGameStart;
        Game g;
        private Label timerLabel;
        private System.Windows.Forms.Timer timer;
        private int timeInSeconds;
        Thread gameThread;

        // bitmap data is currently set to 1 bit per pixel meaning the color values will either be 0 or 255, we'll expand the color values as we add more stuff
        public const int EMPTY_TILE = 255;
        public const int SPAWN_TILE = 128;
        public const int WALL_TILE = 0;
        public const int cellSize = 20;

        Button[,] grid;
        public Bitmap levelBitmap;
        int[,] levelMapData;

        public int directionX = 0;
        public int directionY = 0;

        // slightly annoying way of doing this but it's easily enforced in keeping each map
        // having 12 enemy spawns
        public List<vector2> enemySpawns = new List<vector2>();

        public struct vector2
        {
            public int x;
            public int y;
        }

        public GameForm()
        {

            InitializeComponent();
            this.Icon = new Icon(@"../../../Snake.ico");
            // Size Form, load Bitmap
            this.AutoSize = true;
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
                    else if (levelMapData[x, y] == SPAWN_TILE)
                    {
                        vector2 position = new vector2();
                        position.x = x;
                        position.y = y;
                        enemySpawns.Add(position);
                        grid[x, y].BackColor = Color.White;
                    }
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

            g = new Game(this);
            isGameStart = false;
            gameThread = new Thread(this.gameLoop);
        }

        public int[,] getLevelMapData()
        {
            return levelMapData;
        }


    private void GridButtonClicked(object sender, EventArgs e)
        {
            // figure out center of grid
            int centerX = this.ClientSize.Width / 2;
            int centerY = this.ClientSize.Height / 2;

            // figure out difference between center and tile pressed
            if ((Button)sender != null)
            {
                int diffX = centerX - ((Button)sender).Location.X - cellSize / 2;
                int diffY = centerY - ((Button)sender).Location.Y - cellSize / 2;

                //Debug.WriteLine("x: " + diffX.ToString() + " y: " + diffY.ToString());
                //Debug.WriteLine(((Button)sender).Name);

                // right, left or up, down
                if (Math.Abs(diffX) > Math.Abs(diffY))
                {
                    if (diffX > 0)
                    {
                        goLeft();
                    }
                    else
                    {
                        goRight();
                    }
                }
                else
                {
                    if (diffY > 0)
                    {
                        goUp();
                    }
                    else
                    {
                        goDown();
                    }
                }
            }

            if (!isGameStart)
            {
                isGameStart = true;
                Debug.WriteLine("begin the balling");
                gameThread.Start();
            }
            
        }

        private void goUp()
        {
            lblDirectionIndicator.Text = "Direction: Up";
            directionX = 0;
            directionY = -1;
        }

        private void goDown()
        {
            lblDirectionIndicator.Text = "Direction: Down";
            directionX = 0;
            directionY = 1;
        }

        private void goLeft()
        {
            lblDirectionIndicator.Text = "Direction: left";
            directionX = -1;
            directionY = 0;
        }

        private void goRight()
        {
            lblDirectionIndicator.Text = "Direction: Right";
            directionX = 1;
            directionY = 0;
        }

        private void gameLoop()
        {
            while (isGameStart)
            {

                // draw background
                drawBackground();
                // run tick of game logic
                g.mainGameLoop();
                // draw foreground
                drawForeground();
                Thread.Sleep(100);
            }
        }

        private void drawBackground()
        {
            for (int x = 0; x < levelBitmap.Width; x++)
            {
                for (int y = 0; y < levelBitmap.Height; y++)
                {
                    if (levelMapData[x, y] == EMPTY_TILE) grid[x, y].BackColor = Color.White;
                    else if (levelMapData[x, y] == WALL_TILE) grid[x, y].BackColor = Color.Black;
                    else if (levelMapData[x, y] == SPAWN_TILE) grid[x, y].BackColor = Color.White;
                    else grid[x, y].BackColor = Color.Blue;
                }
            }
        }

        private void drawForeground()
        {
            foreach(Game.Snake snakePart in g.fullSnake)
            {
                grid[snakePart.posX, snakePart.posY].BackColor = Color.Red;
            }

            foreach(Game.Enemy enemy in g.enemies)
            {
                grid[enemy.posX, enemy.posY].BackColor = Color.MediumPurple;
            }
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

        private void ExitButtonClicked(object? sender, EventArgs e)
        {
            this.Close(); // Close the current form
        }

        private new void HelpButtonClicked(object? sender, EventArgs e)
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

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.W)
            {
                goUp();
            } 
            else if (e.KeyCode == Keys.S)
            {
                goDown();
            }
            else if (e.KeyCode == Keys.A)
            {
                goLeft();
            }
            else if (e.KeyCode == Keys.D)
            {
                goRight();
            }
        }
    }
}
