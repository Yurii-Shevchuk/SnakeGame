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
        private List<Obstacle> _obstacles;
        private bool _isGameOver;
        private AbstractCreator<Food> _foodCreator;
        private AbstractCreator<Obstacle> _obstacleCreator;
        public Game(int height, int width)
        {
            _grid = new Grid(height, width);
            _grid.Draw();
            _snake = new Snake();
            _snake.Draw();
            _obstacles = new List<Obstacle>();
            _isGameOver = false;
            _foodCreator = new FoodCreator();
            _obstacleCreator = new ObstacleCreator();
        }

        public List<Obstacle> Obstacles => _obstacles;

        public AbstractCreator<Food> FoodCreator => _foodCreator;
        public AbstractCreator<Obstacle> ObstacleCreator => _obstacleCreator;
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
            Food food = FoodCreator.CreatePlacable(GameGrid.GeneratePosition(), "$");
            food.Place();
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

                if(snakeNewHead == food.Coordinates)
                {
                    food = FoodCreator.CreatePlacable(GameGrid.GeneratePosition(), "$");
                    food.Place();
                    Position increasedSnake = snakeNewHead;
                    Snake.GetSnake.Enqueue(increasedSnake);
                    Score += 100;
                    if (gameSpeed >= 50)
                    {
                        gameSpeed--;
                    }
                    Obstacles.Add(ObstacleCreator.CreatePlacable(GameGrid.GeneratePosition(), "*"));
                    Obstacles.Last().Place();
                }
                if(Obstacles.Exists(x => x.Coordinates == snakeNewHead))
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
