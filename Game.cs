using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC22005Assignment1
{
    internal class Game
    {
        GameForm form;
        Random rand = new Random();

        public int score = 0;

        public struct Snake
        {
            public int posX;
            public int posY;
            public int currentDirX;
            public int currentDirY;
            public int timeAtWall;
        }
        public struct Enemy
        {
            public int posX;
            public int posY;
            public int currentDirX;
            public int currentDirY;
            public bool movingThisTurn;
        }

        // the snake will grow in length by 1 for every 3 enemies it eats
        int currentSnakeLength = 6;
        int snakeHunger = 3;
        public List<Enemy> enemies;
        public List<Snake> fullSnake;

        public Game(GameForm form)
        {
            fullSnake = new List<Snake>();
            enemies = new List<Enemy>();

            Snake snakeHead = new Snake();

            snakeHead.posX = 16;
            snakeHead.posY = 16;
            snakeHead.currentDirX = 0;
            snakeHead.currentDirY = 0;

            fullSnake.Add(snakeHead);
            this.form = form;
        }

        public void mainGameLoop()
        {
            moveTheSnake();
            eatGhosts();
            moveEnemies();
            eatGhosts();
            biteSnake();
            spawnEnemies();
        }

        private void spawnEnemies()
        {
            // possibly spawn in an enemy
            // ticks happen ten times per second,
            // 5 seconds per enemy spawning in will be about 1/50 chance per tick, this chance is further reduced by the restrictions on
            // available positions by proximity to snake head 
            if (rand.Next(15) == 0)
            {
                int spawnNum = rand.Next(0, form.enemySpawns.Count());
                if (Math.Max(form.enemySpawns[spawnNum].x - fullSnake[0].posX, form.enemySpawns[spawnNum].y - fullSnake[0].posY) > 4)
                {
                    Enemy newEnemy = new Enemy();
                    newEnemy.posX = form.enemySpawns[spawnNum].x;
                    newEnemy.posY = form.enemySpawns[spawnNum].y;
                    newEnemy.movingThisTurn = false;

                    // prevent the ghost from spawning in the snake if it gets that long
                    foreach(Snake bodypart in fullSnake)
                    {
                        if(bodypart.posY == newEnemy.posY && bodypart.posX == newEnemy.posX)
                        {
                            return;
                        }
                    }

                    // provide it a starting direction at random
                    int startDir = rand.Next(4);
                    switch (startDir)
                    {
                        // up, down, left, right
                        case 0:
                            newEnemy.currentDirX = 0;
                            newEnemy.currentDirY = 1;
                            break;
                        case 1:
                            newEnemy.currentDirX = 0;
                            newEnemy.currentDirY = -1;
                            break;
                        case 2:
                            newEnemy.currentDirX = 1;
                            newEnemy.currentDirY = 0;
                            break;
                        case 3:
                            newEnemy.currentDirX = -1;
                            newEnemy.currentDirY = 0;
                            break;
                    }

                    enemies.Add(newEnemy);
                }
            }
        }

        private void biteSnake()
        {
            // Say the snake is poisonous to the enemies and it kill them wen they bite it
            List<Enemy> survivingEnemies = new List<Enemy>();
            foreach(Enemy ghost in enemies)
            {
                survivingEnemies.Add(ghost);
                for (int i = 1; i < fullSnake.Count(); i++)
                {
                    if (ghost.posX == fullSnake[i].posX && ghost.posY == fullSnake[i].posY)
                    {
                        fullSnake.RemoveRange(i, fullSnake.Count() - i);
                        fullSnake.TrimExcess();
                        currentSnakeLength = Math.Max(fullSnake.Count(), 4);
                        survivingEnemies.Remove(ghost);
                        break;
                    }
                }
            }
            enemies = survivingEnemies;
        }

        private void eatGhosts()
        {
            List<Enemy> ghostsToRemove = new List<Enemy>();
            foreach (Enemy ghost in enemies)
            {
                if (fullSnake[0].posX == ghost.posX && fullSnake[0].posY == ghost.posY)
                {
                    snakeHunger -= 1;
                    if (snakeHunger <= 0)
                    {
                        snakeHunger = 3;
                        currentSnakeLength += 1;
                    }
                    ghostsToRemove.Add(ghost);
                    score += 1;
                    form.UpdateScoreLbl();
                }
            }

            foreach (Enemy ghost in ghostsToRemove)
            {
                enemies.Remove(ghost);
            }

            // aside from checking if the snake is biting the ghosts, we must also check it is not biting itself
            for (int i = 1; i < fullSnake.Count(); i++)
            {
                if (fullSnake[0].posX == fullSnake[i].posX && fullSnake[0].posY == fullSnake[i].posY)
                {
                    fullSnake.RemoveRange(i, fullSnake.Count() - i);
                    fullSnake.TrimExcess();
                    currentSnakeLength = Math.Max(fullSnake.Count(), 4);
                    break;
                }
            }
        }

        private void moveEnemies()
        {
            // enemies will move in a near identical manner to the snake functionally speaking
            // they figure out the direction they wanna go and then they go for it whenever possible
            // one difference between the snake and enemies will be that the enemies move at half the speed
            
            // creating new list and swapping lists due to foreach breaking if you alter contents
            List<Enemy> newEnemyPositions = new List<Enemy>(enemies.Count());
            foreach(Enemy ghost in enemies)
            {
                newEnemyPositions.Add(moveEnemy(ghost));
            }

            enemies = newEnemyPositions;
        }

        private Enemy moveEnemy(Enemy ghost)
        {
            if (!ghost.movingThisTurn)
            {
                ghost.movingThisTurn = true;
                return ghost;
            }
            
            ghost.movingThisTurn = false;

            // first determine the direction to reach the snake's centermost section,
            // using the center to make sure that the enemies don't just trail behind the snake's tail

            int snakePiece = fullSnake.Count() / 2;

            int dirToSnakeX = fullSnake[snakePiece].posX - ghost.posX;
            int dirToSnakeY = fullSnake[snakePiece].posY - ghost.posY;

            if (Math.Abs(dirToSnakeX) > Math.Abs(dirToSnakeY))
            {
                dirToSnakeY = 0;
                dirToSnakeX = dirToSnakeX / Math.Abs(dirToSnakeX);
            }
            else
            {
                dirToSnakeY = dirToSnakeY / Math.Abs(dirToSnakeY);
                dirToSnakeX = 0;
            }

            if (form.getLevelMapData()[(ghost.posX + dirToSnakeX + form.levelBitmap.Width) % form.levelBitmap.Width,
                                       (ghost.posY + dirToSnakeY + form.levelBitmap.Height) % form.levelBitmap.Height] != 0
                                       && !(ghost.currentDirX + dirToSnakeX == 0 && ghost.currentDirY + dirToSnakeY == 0))
            {
                ghost.posX = (ghost.posX + dirToSnakeX + form.levelBitmap.Width) % form.levelBitmap.Width;
                ghost.posY = (ghost.posY + dirToSnakeY + form.levelBitmap.Height) % form.levelBitmap.Height;
                ghost.currentDirX = dirToSnakeX;
                ghost.currentDirY = dirToSnakeY;
            }
            else if (form.getLevelMapData()[(ghost.posX + ghost.currentDirX + form.levelBitmap.Width) % form.levelBitmap.Width,
                                            (ghost.posY + ghost.currentDirY + form.levelBitmap.Height) % form.levelBitmap.Height] != 0)
            {
                ghost.posX = (ghost.posX + ghost.currentDirX + form.levelBitmap.Width) % form.levelBitmap.Width;
                ghost.posY = (ghost.posY + ghost.currentDirY + form.levelBitmap.Height) % form.levelBitmap.Height;
            }
            else
            {
                int temp = ghost.currentDirX;
                ghost.currentDirX = ghost.currentDirY;
                ghost.currentDirY = temp;

                // randomize between left and right
                if (rand.Next(0, 2) == 1)
                {
                    ghost.currentDirX = -ghost.currentDirX;
                    ghost.currentDirY = -ghost.currentDirY;
                }

                if (form.getLevelMapData()[(ghost.posX + ghost.currentDirX + form.levelBitmap.Width) % form.levelBitmap.Width,
                                           (ghost.posY + ghost.currentDirY + form.levelBitmap.Height) % form.levelBitmap.Height] != 0)
                {
                    ghost.posX = (ghost.posX + ghost.currentDirX + form.levelBitmap.Width) % form.levelBitmap.Width;
                    ghost.posY = (ghost.posY + ghost.currentDirY + form.levelBitmap.Height) % form.levelBitmap.Height;
                }
                else
                {
                    ghost.currentDirX = -ghost.currentDirX;
                    ghost.currentDirY = -ghost.currentDirY;

                    ghost.posX = (ghost.posX + ghost.currentDirX + form.levelBitmap.Width) % form.levelBitmap.Width;
                    ghost.posY = (ghost.posY + ghost.currentDirY + form.levelBitmap.Height) % form.levelBitmap.Height;
                }
            }

            return ghost;
        }

        private void moveTheSnake()
        {
            Snake snakeHead = fullSnake[0];
            Snake newHead = new Snake();
            newHead.timeAtWall = 0;

            // the reason we add the map width and then take it's modulo is to ensure that the modulo function never returns a negative value
            // If it did, we would have a outOfRange Exception as soon as the snake travels into the left or top sides of the map
            if (form.getLevelMapData()[(snakeHead.posX + form.directionX + form.levelBitmap.Width) % form.levelBitmap.Width,
                                       (snakeHead.posY + form.directionY + form.levelBitmap.Height) % form.levelBitmap.Height] != 0

                                       // check that if the direction the snake is headed is opposite to the direction the player wants to go are opposite,
                                       // if that is the case, do not run this segment

                                       && !(snakeHead.currentDirX + form.directionX == 0 && snakeHead.currentDirY + form.directionY == 0))
            {
                newHead.posX = (snakeHead.posX + form.directionX + form.levelBitmap.Width) % form.levelBitmap.Width;
                newHead.posY = (snakeHead.posY + form.directionY + form.levelBitmap.Height) % form.levelBitmap.Height;
                newHead.currentDirX = form.directionX;
                newHead.currentDirY = form.directionY;
                fullSnake.Insert(0, newHead);
            }

            // if the snake cannot move the direction player wants to go, check if the snake can move in it's current direction
            else if (form.getLevelMapData()[(snakeHead.posX + snakeHead.currentDirX + form.levelBitmap.Width) % form.levelBitmap.Width,
                                            (snakeHead.posY + snakeHead.currentDirY + form.levelBitmap.Height) % form.levelBitmap.Height] != 0)
            {
                newHead.posX = (snakeHead.posX + snakeHead.currentDirX + form.levelBitmap.Width) % form.levelBitmap.Width;
                newHead.posY = (snakeHead.posY + snakeHead.currentDirY + form.levelBitmap.Height) % form.levelBitmap.Height;
                newHead.currentDirX = snakeHead.currentDirX;
                newHead.currentDirY = snakeHead.currentDirY;
                fullSnake.Insert(0, newHead);
            }

            // if the snake cannot move where the player wants him to go, and the snake cannot keep moving straight ahead, have the snake attempt to move left or right
            else
            {
                // if the snake has hit a wall then the current direction is not viable, we do not want the snake to turn around either
                // we will swap the value of the snakes current x direction and current y direction D it finds a suitable path

                if (snakeHead.timeAtWall < 2)
                {

                    // must do this due to c# being a prick about using references
                    snakeHead.timeAtWall += 1;
                    fullSnake[0] = snakeHead;
                    return;
                }

                int temp = snakeHead.currentDirX;
                snakeHead.currentDirX = snakeHead.currentDirY;
                snakeHead.currentDirY = temp;

                // randomize between left and right
                if (rand.Next(0, 2) == 1)
                {
                    snakeHead.currentDirX = -snakeHead.currentDirX;
                    snakeHead.currentDirY = -snakeHead.currentDirY;
                }

                if (form.getLevelMapData()[(snakeHead.posX + snakeHead.currentDirX + form.levelBitmap.Width) % form.levelBitmap.Width,
                                           (snakeHead.posY + snakeHead.currentDirY + form.levelBitmap.Height) % form.levelBitmap.Height] != 0)
                {
                    newHead.posX = (snakeHead.posX + snakeHead.currentDirX + form.levelBitmap.Width) % form.levelBitmap.Width;
                    newHead.posY = (snakeHead.posY + snakeHead.currentDirY + form.levelBitmap.Height) % form.levelBitmap.Height;
                    newHead.currentDirX = snakeHead.currentDirX;
                    newHead.currentDirY = snakeHead.currentDirY;
                    fullSnake.Insert(0, newHead);
                }
                else
                {
                    snakeHead.currentDirX = -snakeHead.currentDirX;
                    snakeHead.currentDirY = -snakeHead.currentDirY;

                    newHead.posX = (snakeHead.posX + snakeHead.currentDirX + form.levelBitmap.Width) % form.levelBitmap.Width;
                    newHead.posY = (snakeHead.posY + snakeHead.currentDirY + form.levelBitmap.Height) % form.levelBitmap.Height;
                    newHead.currentDirX = snakeHead.currentDirX;
                    newHead.currentDirY = snakeHead.currentDirY;
                    fullSnake.Insert(0, newHead);
                }
            }


            // If the snake has not grown, cut off the oldest tailpiece
            if (fullSnake.Count() > currentSnakeLength)
            {
                fullSnake.RemoveAt(currentSnakeLength);
            }
        }
    }
}
