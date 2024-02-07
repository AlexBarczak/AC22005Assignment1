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

        public struct snake
        {
            public int posX;
            public int posY;
        }
        private struct enemy
        {

        }

        int currentSnakeLength = 5;
        public List<snake> fullSnake;

        public Game(GameForm form)
        {
            fullSnake = new List<snake>();

            snake snakeHead = new snake();

            snakeHead.posX = 19;
            snakeHead.posY = 19;

            fullSnake.Add(snakeHead);
            this.form = form;
        }

        public void mainGameLoop()
        {
            Debug.WriteLine("Balling");

            // 1 check snake can move
            // 1.1 get the snake head i.e, the element stored in the first position on the snake, i.e fullsnake[0]
            snake snakeHead = fullSnake[0];
            snake newHead = new snake();

            newHead.posX = (snakeHead.posX + form.directionX + form.levelBitmap.Width) %form.levelBitmap.Width;
            newHead.posY = (snakeHead.posY + form.directionY + form.levelBitmap.Height) %form.levelBitmap.Height;
            fullSnake.Insert(0, newHead);

            if (fullSnake.Count() > currentSnakeLength)
            {
                fullSnake.RemoveAt(currentSnakeLength);
            }


            // move snake
            // check if snake collides with ghosts
            // if eat ghost
            //      grow
            // move ghosts
            // if ghost collide with snake body
            // snake explode
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
