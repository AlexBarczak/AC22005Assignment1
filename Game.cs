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
        List<enemy> enemies = new List<enemy>();

        public struct snake
        {
            public int posX;
            public int posY;
            public int currentDirX;
            public int currentDirY;
        }
        private struct enemy
        {
            public int posX;
            public int posY;
            public int currentDirX;
            public int currentDirY;
        }

        int currentSnakeLength = 9;
        public List<snake> fullSnake;

        public Game(GameForm form)
        {
            fullSnake = new List<snake>();

            snake snakeHead = new snake();

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

            // check if snake collides with ghosts
            // if eat ghost
            //      grow
            // move ghosts
            // if ghost collide with snake body
            // snake explode
        
            // possibly spawn in an enemy
            
        }

        private void moveTheSnake()
        {
            snake snakeHead = fullSnake[0];
            snake newHead = new snake();

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

        public void printGridInts()
        {

            for(int y = 0; y < form.getLevelMapData().GetLength(0); y++)
            {
                for(int x = 0; x < form.getLevelMapData().GetLength(0); x++)
                {
                    Debug.Write(form.getLevelMapData()[x,y]);
                }
                Debug.Write('\n');
            }
        }
    }
}
