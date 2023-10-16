using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    internal class Game
    {
        private Grid _grid;
        private Snake _snake;
        private Random _random;
        private int _score;
        private List<Position> _obstacles;
        private bool _isGameOver;
        public Game(int height, int width)
        {
            _grid = new Grid(height, width);
            _grid.Draw();
            _snake = new Snake();
            _snake.Draw();
            _score = 0;
            _random = new Random();
            _obstacles = new List<Position>();
            _isGameOver = false;
        }

        public List<Position> Obstacles => _obstacles;
        public Snake Snake => _snake;
        public bool IsGameLost
        {
            get;
            private set;
        }
        public Grid GameGrid => _grid;

        public int Score { get; private set; }
        
        private void DrawScore()
        {
            Console.SetCursorPosition(0, GameGrid.Height + 1);
            Console.Write(Score);
        }

        


        private bool IsGameOver(Position snakeNewHead)
        {
            if (Snake.GetSnake.Contains(snakeNewHead)) return true;
            else if (snakeNewHead.Col <= 0) return true;
            else if (snakeNewHead.Row <= 0) return true;
            else if (snakeNewHead.Row >= GameGrid.Height - 1) return true;
            else if (snakeNewHead.Col >= GameGrid.Width - 1) return true;
            else
            {
                return false;
            }
        }

      
        
        public void GameLoop()
        {
            int gameSpeed = 100;
            Position food = Food.CreateAndPlace(GameGrid.GeneratePosition());
            Console.CursorVisible = false;
            int direction = (int)Directions.right;
            while (!IsGameLost)
            {
                DrawScore();
                direction = Snake.HandleMovement(direction);
                Position snakeHead = Snake.GetSnake.Last();
                Position snakeNewHead = Snake.UpdateHead(direction);

                //-------------------------
                IsGameLost = IsGameOver(snakeNewHead);
                //-------------------------

                Snake.UpdateBody(snakeHead);

                Snake.DrawNewHead(snakeNewHead, direction);

                if(snakeNewHead.Col == food.Col && snakeNewHead.Row ==  food.Row)
                {
                    food = Food.CreateAndPlace(GameGrid.GeneratePosition());
                    Position increasedSnake = snakeNewHead;
                    Snake.GetSnake.Enqueue(increasedSnake);
                    Score += 100;
                    if (gameSpeed >= 50)
                    {
                        gameSpeed--;
                    }
                    Obstacles.Add(Obstacle.CreateAndPlace(GameGrid.GeneratePosition()));
                }
                if(Obstacles.Contains(snakeNewHead))
                {
                    IsGameLost = !IsGameLost;
                    break;
                }

                Snake.EraseTail();
                Thread.Sleep(gameSpeed);
            }
        }
    }
    public enum Directions
    {
        right,
        left,
        down,
        up,
    }
}
