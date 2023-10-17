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
        private List<Obstacle> _obstacles;
        private bool _isGameOver;
        private AbstractCreator[] _creator;
        public Game(int height, int width)
        {
            _grid = new Grid(height, width);
            _grid.Draw();
            _snake = new Snake();
            _snake.Draw();
            _score = 0;
            _random = new Random();
            _obstacles = new List<Obstacle>();
            _isGameOver = false;
            _creator = new AbstractCreator[2];
            _creator[0] = new FoodCreator();
            _creator[1] = new ObstacleCreator();
        }

        public List<Obstacle> Obstacles => _obstacles;

        public AbstractCreator FoodCreator => _creator[0];
        public AbstractCreator ObstacleCreator => _creator[1];
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
            Food food = (Food)FoodCreator.CreatePlacable(GameGrid.GeneratePosition(), "$");
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

                if(snakeNewHead.Col == food.Coordinates.Col && snakeNewHead.Row ==  food.Coordinates.Row)
                {
                    food = (Food)FoodCreator.CreatePlacable(GameGrid.GeneratePosition(), "$");
                    food.Place();
                    Position increasedSnake = snakeNewHead;
                    Snake.GetSnake.Enqueue(increasedSnake);
                    Score += 100;
                    if (gameSpeed >= 50)
                    {
                        gameSpeed--;
                    }
                    Obstacles.Add((Obstacle)ObstacleCreator.CreatePlacable(GameGrid.GeneratePosition(), "*"));
                    Obstacles.Last().Place();
                }
                if(Obstacles.Exists(x => x.Coordinates.Col == snakeNewHead.Col && x.Coordinates.Row == snakeNewHead.Row))
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
