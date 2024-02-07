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

        struct snake
        {
            public int posX;
            public int posY;
            public direction currentDirection;
        }

        enum direction
        {
            UP,
            RIGHT,
            DOWN,
            LEFT
        }

        //static direction currentDirection;

        int currentSnakeLength;
        snake snakePart;
        List<snake> fullSnake;
        

        public Game(GameForm form)
        {
            snakePart = new snake();
            snakePart.posX = 16;
            snakePart.posY = 19;
            snakePart.currentDirection = direction.UP;
            currentSnakeLength = 1;
            fullSnake = new List<snake> {snakePart};
            this.form = form;
        }

        public void mainGameLoop()
        {
            Debug.WriteLine("Balling");
            // check snake can move
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
