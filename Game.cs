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
        

        public Game()
        {
            snakePart = new snake();
            snakePart.posX = 16;
            snakePart.posY = 19;
            snakePart.currentDirection = direction.UP;
            currentSnakeLength = 1;
            fullSnake = new List<snake> {snakePart};
        }

        public void mainGameLoop()
        {
            while (GameForm.isGameStart)
            {

            }
        }

        public void printGridInts()
        {

            for(int y = 0; y < GameForm.getLevelMapData().GetLength(0); y++)
            {
                for(int x = 0; x < GameForm.getLevelMapData().GetLength(0); x++)
                {
                    Debug.Write(GameForm.getLevelMapData()[x,y]);
                }
                Debug.Write('\n');
            }
        }
    }
}
