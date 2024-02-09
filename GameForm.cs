/**
 * Snake-man Grid Game C# Assignment
 * Team members:
 * - Alex Barczak
 * - Flynn Henderson
 * - Ben Houghton
 */

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
using static System.Windows.Forms.AxHost;

namespace AC22005Assignment1
{
    public partial class GameForm : Form
    {
        // necessary game variables
        public bool isGameStart;
        private Game g;
        private readonly Label timerLabel;
        private readonly System.Windows.Forms.Timer timer;
        private int timeInSeconds;
        private Thread gameThread;

        // bitmap data is currently set to 1 bit per pixel meaning the color values will either be 0 or 255, we'll expand the color values as we add more stuff
        public const int EMPTY_TILE = 255;
        public const int SPAWN_TILE = 128;
        public const int WALL_TILE = 0;
        public const int cellSize = 20;

        // grid level data
        private readonly Button[,] grid;
        public Bitmap levelBitmap;
        private readonly int[,] levelMapData;

        // direction variables
        public int directionX = 0;
        public int directionY = 0;

        // slightly annoying way of doing this but it's easily enforced in keeping each map
        // having 12 enemy spawns
        public List<Vector2> enemySpawns = new();

        Form HelpForm = new Form(); // help form
        // Dummy Vector for Enemy spawns
        public struct Vector2
        {
            public int x;
            public int y;
        }

        public GameForm()
        {

            InitializeComponent();
            HelpSetup(); // help form function
            // Size Form, load Bitmap
            this.AutoSize = true;
            // initialise level data variable
            levelBitmap = new Bitmap(@"../../../level.bmp"); //Grid Bitmap
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
                    grid[x, y] = new Button() { Name = x + "," + y,
                                                FlatStyle = FlatStyle.Flat
                    };
                    grid[x, y].FlatAppearance.BorderSize = 0;
                    grid[x, y].SetBounds(xOffset + cellSize * x, yOffset + cellSize * y, cellSize, cellSize);

                    if (levelMapData[x, y] == EMPTY_TILE) grid[x, y].BackColor = Color.White;
                    else if (levelMapData[x, y] == WALL_TILE) grid[x, y].BackColor = Color.Black;
                    else if (levelMapData[x, y] == SPAWN_TILE)
                    {
                        Vector2 position = new() { x = x, y = y };
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
            timerLabel.Text = "Time: 0 seconds";
            timerLabel.AutoSize = true;
            timerLabel.Location = new Point((xOffset), 40);
            timerLabel.TextAlign = ContentAlignment.MiddleCenter;
            timerLabel.Size = new Size(120, 80);
            timerLabel.Font = new Font("Stencil", 36);
            timerLabel.Anchor = AnchorStyles.None;
            Controls.Add(timerLabel);

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000; //Milliseconds
            timer.Tick += TimerTick;

            //Create Exit, Help and New Game buttons
            Button NewGame = new();
            NewGame.Text = "New Game";
            NewGame.Size = new Size(80, 30);
            NewGame.Location = new Point(10, ((this.ClientSize.Height - NewGame.Height) / 2));
            NewGame.Click += new EventHandler(this.NewGameClicked);
            NewGame.Anchor = AnchorStyles.None;
            Controls.Add(NewGame);
            Button exitButton = new();
            exitButton.Text = "Exit";
            exitButton.Size = new Size(80, 30);
            exitButton.Location = new Point(10, (this.ClientSize.Height - exitButton.Height) / 2 - 40);
            exitButton.Click += new EventHandler(this.ExitButtonClicked);
            exitButton.Anchor = AnchorStyles.None;
            Controls.Add(exitButton);

            Button helpButton = new();
            helpButton.Text = "How to play";
            helpButton.Size = new Size(80, 30);
            helpButton.Location = new Point(10, ((this.ClientSize.Height - helpButton.Height) / 2) - 80);
            helpButton.Click += new EventHandler(this.HelpButtonClicked);
            helpButton.Anchor = AnchorStyles.None;
            Controls.Add(helpButton);

            //Start Game Loop
            g = new Game(this);
            isGameStart = false;
            gameThread = new Thread(this.GameLoop);
        }

        //Event handler for New Game Button
        private void NewGameClicked(object? sender, EventArgs e)
        {
            
            g = new Game(this);
            isGameStart = false;
            DrawBackground();
            DrawForeground();
            gameThread = new Thread(this.GameLoop);
            timeInSeconds = 0;
            timer.Stop();
        }

        //Function to Update Score
        internal void UpdateScoreLbl()
        {
            // threading problems resolved using method invoking 
            // details: https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.methodinvoker?view=windowsdesktop-8.0
            if (!this.IsHandleCreated || this.IsDisposed) return;
            if (!lbl_score.IsHandleCreated || lbl_score.IsDisposed) return;
            

            this.lbl_score.Invoke((MethodInvoker) delegate
            {
                this.lbl_score.Text = "Score: " + g.score.ToString();
            });
        }
        //Getter for LevelMapData
        public int[,] GetLevelMapData()
        {
            return levelMapData;
        }

    //Event Handler to handle directional inputs via grid clicking
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

                // right, left or up, down
                if (Math.Abs(diffX) > Math.Abs(diffY))
                {
                    if (diffX > 0)
                    {
                        GoLeft();
                    }
                    else
                    {
                        GoRight();
                    }
                }
                else
                {
                    if (diffY > 0)
                    {
                        GoUp();
                    }
                    else
                    {
                        GoDown();
                    }
                }
            }
            //Start Game
            if (!isGameStart)
            {
                isGameStart = true;
                gameThread.Start();
                timer.Start();
            }
        }

        //Various functions to handle Directional Indicator + Snake Movement
        private void GoUp()
        {
            lblDirectionIndicator.Text = "Direction: Up";
            directionX = 0;
            directionY = -1;
        }

        private void GoDown()
        {
            lblDirectionIndicator.Text = "Direction: Down";
            directionX = 0;
            directionY = 1;
        }

        private void GoLeft()
        {
            lblDirectionIndicator.Text = "Direction: left";
            directionX = -1;
            directionY = 0;
        }

        private void GoRight()
        {
            lblDirectionIndicator.Text = "Direction: Right";
            directionX = 1;
            directionY = 0;
        }

        //Main game loop
        private void GameLoop()
        {
            while (isGameStart)
            {

                // draw background
                DrawBackground();
                // run tick of game logic
                g.MainGameLoop();
                // draw foreground
                DrawForeground();

                //Statement to check for game over
                if(g.health <= 0)
                {
                    g.health = 3;
                    timer.Stop();
                    MessageBox.Show(
                        "Game Over\n" +
                        "You lasted " + timeInSeconds.ToString() + " seconds\n" +
                        "And Ate " + g.score.ToString() + " enemies");
                    break;
                }
                Thread.Sleep(100);
            }
        }

        //Draw bitmap
        private void DrawBackground()
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
        //Draw Snake and Enemies
        private void DrawForeground()
        {
            Color snakeColor;
            if (g.health == 3) snakeColor = Color.Red;
            else if (g.health == 2) snakeColor = Color.DarkRed;
            else if (g.health == 1) snakeColor = Color.Purple;
            else snakeColor = Color.Blue;
            foreach (Game.Snake snakePart in g.fullSnake)
            {
                grid[snakePart.posX, snakePart.posY].BackColor = snakeColor;
            }

            foreach(Game.Enemy enemy in g.enemies)
            {
                grid[enemy.posX, enemy.posY].BackColor = Color.MediumPurple;
            }
        }
        //Function to increment timer
        private void TimerTick(object sender, EventArgs e)
        {
            timeInSeconds++;
            timerLabel.Text = "Time: " + timeInSeconds + " seconds";
            if (timeInSeconds == 999)
            {
                timer.Stop();
            }
        }
        //Function to setup help form 
        void HelpSetup()
        {
            HelpForm.AutoScroll = true;

            HelpForm.Icon = Properties.Resources.Snake;
            HelpForm.Text = "Help";
            HelpForm.BackColor = Color.YellowGreen;
            HelpForm.Anchor = AnchorStyles.None;
            HelpForm.MaximumSize = new System.Drawing.Size(1000, 500);
            HelpForm.MinimumSize = new System.Drawing.Size(1000, 500);
            HelpForm.StartPosition = FormStartPosition.CenterParent;

            Label LblHelpTitle = new Label();
            LblHelpTitle.Font = new System.Drawing.Font("Stencil", 36F, FontStyle.Bold);
            LblHelpTitle.Text = "Snake-man shall aid you!";
            LblHelpTitle.ForeColor = Color.DarkGreen;
            LblHelpTitle.SetBounds(50, 20, 400, 50);
            HelpForm.Controls.Add(LblHelpTitle);

            Label LblGameplay = new Label();
            LblGameplay.Font = new System.Drawing.Font("Papyrus", 12F, FontStyle.Bold);
            LblGameplay.ForeColor = Color.Black;
            LblGameplay.SetBounds(50, 100, 600,100);
            LblGameplay.Text = "Upon initialising Snake-man, you will be met by the maze grid upon which Snake-man must traverse. In order to start the game, one must click on the grid in any part of it to spawn our glorious Snake. He will proceed to move around the maze, directed by clicking on the various sides of the grid that correspond to the desired direction: left, right, up and down. If preferred, one can also use WASD keyboard inputs, however you cannot start the game that way.";
            HelpForm.Controls.Add(LblGameplay);

            Label LblGameplay2 = new Label();
            LblGameplay2.Font = new System.Drawing.Font("Papyrus", 12F, FontStyle.Bold);
            LblGameplay2.ForeColor = Color.Black;
            LblGameplay2.SetBounds(50, 275, 600, 100);
            LblGameplay2.Text = "Be warned, however, as upon the spawning of our beloved snake into the maze, the Snake's blasted enemies shall also begin to spawn randomly around the grid space, intent on squashing the beautiful life of our beloved slithering friend. In order to counteract this, Snake-man must fight back by consuming each enemy with his head, however if Snake-man encounters an enemy on his body, he will lose a life point - out of his total of 3 - and decay in colour!";
            HelpForm.Controls.Add(LblGameplay2);

            Label LblGameplay3 = new Label();
            LblGameplay3.Font = new System.Drawing.Font("Papyrus", 12F, FontStyle.Bold);
            LblGameplay3.ForeColor = Color.Black;
            LblGameplay3.SetBounds(50, 450, 600, 100);
            LblGameplay3.Text = "However, most fortuitously, Snake-man grows stronger through the blood of his enemies, and will receive additional snake segments for every 2 enemies consumed, branching from his minimum of 4 segments to truly infinite possibilities. Alongside bodily growth, the score counter will increase likewise, for every enemy consumed.";
            HelpForm.Controls.Add(LblGameplay3);

            Label LblGameplay4 = new Label();
            LblGameplay4.Font = new System.Drawing.Font("Papyrus", 12F, FontStyle.Bold);
            LblGameplay4.ForeColor = Color.Black;
            LblGameplay4.SetBounds(50, 625, 600, 100);
            LblGameplay4.Text = "Finally, the grid space of Snake-man's domain allows for him to travel across the edges of the grid to the other side, truly groundbreaking. Alongside this, Snake-man should be noted of his tendency to act irrationally and independently of his ever loving protector if left idle for too long.";
            HelpForm.Controls.Add(LblGameplay4);

            Label LblEndHelpTitle = new Label();
            LblEndHelpTitle.Font = new System.Drawing.Font("Stencil", 20F);
            LblEndHelpTitle.Text = "Fortune favours the Snake!";
            LblEndHelpTitle.ForeColor = Color.DarkGreen;
            LblEndHelpTitle.SetBounds(50, 800, 400, 100);
            HelpForm.Controls.Add(LblEndHelpTitle);

            Button BtnHelpClose = new Button();
            BtnHelpClose.Text = "Close";
            BtnHelpClose.SetBounds(100, 900, 130, 50);
            BtnHelpClose.Font = new System.Drawing.Font("Stencil", 20F, FontStyle.Bold);
            BtnHelpClose.BackColor = Color.DarkGreen;
            BtnHelpClose.Click += new EventHandler(this.BtnHelpClose_Click);
            HelpForm.Controls.Add(BtnHelpClose);
        }
        //Button to close help form
        private void BtnHelpClose_Click(object sender, EventArgs e)
        {
            HelpForm.Close();
        }

        private void ExitButtonClicked(object? sender, EventArgs e)
        {
            this.Close(); // Close the current form
        }
        //Eventhandler for Help button
        private new void HelpButtonClicked(object? sender, EventArgs e)
        {
            HelpForm.ShowDialog();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            //Necessary
        }

        //Event Handler for Tool Strip Exit button
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        //Event Handler for Tool Strip "About" button
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Snake-Man developed in C# by Alex Barczak (2497555), Flynn Henderson (2502464) and Ben Houghton (2498662)", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //Event Handler for Tool Strip Help button
        private void HowToPlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpForm.ShowDialog();
        }

        //Mute Button
        private void BtnMute_Click(object sender, EventArgs e)
        {
            if (Program.musicPlaying)
            {
                Program.player.Stop();
                BtnMute.BackgroundImage = Image.FromFile(@"../../../mute.bmp");
                Program.musicPlaying = false;
            }
            else
            {
                Program.musicPlaying = true;
                Program.player.PlayLooping();
                BtnMute.BackgroundImage = Image.FromFile(@"../../../unmute.bmp");
            }
        }
        //Event handler for keyboard inputs
        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.W)
            {
                GoUp();
            } 
            else if (e.KeyCode == Keys.S)
            {
                GoDown();
            }
            else if (e.KeyCode == Keys.A)
            {
                GoLeft();
            }
            else if (e.KeyCode == Keys.D)
            {
                GoRight();
            }
        }
    }
}
