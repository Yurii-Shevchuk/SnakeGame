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
        public Game(int height, int width)
        {
            _grid = new Grid(height, width);
            _grid.Draw();
            _snake = new Snake();
            _snake.Draw();
            _score = 0;
            _random = new Random();
            _obstacles = new List<Position>();
        }

        public List<Position> Obstacles => _obstacles;
        public Snake Snake => _snake;

        public Grid GameGrid => _grid;

        public int Score { get; private set; }
        
        private Position CreateFood()
        {
            Position food = GeneratePosition();
            return food;
        }

        private Position GeneratePosition()
        {
            Position position;
            do
            {
                position = new Position(_random.Next(1, GameGrid.Height - 1), _random.Next(1, GameGrid.Width - 1));
            } while (GameGrid.GetElementAt(position.Col, position.Row) != " ");
            return position;
        }

        private void PlaceFood(Position food)
        {
            Console.SetCursorPosition(food.Col, food.Row);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("$");
        }

        private void DrawScore()
        {
            Console.SetCursorPosition(0, GameGrid.Height + 1);
            Console.Write(Score);
        }

        private int HandleInput(int direction)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo userInput = Console.ReadKey(true);
                ConsoleKey key = userInput.Key;
                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        if (direction != (int)Directions.right) direction = (int)Directions.left;
                        break;
                    case ConsoleKey.RightArrow:
                        if (direction != (int)Directions.left) direction = (int)Directions.right;
                        break;
                    case ConsoleKey.UpArrow:
                        if (direction != (int)Directions.down) direction = (int)Directions.up;
                        break;
                    case ConsoleKey.DownArrow:
                        if (direction != (int)Directions.up) direction = (int)Directions.down;
                        break;
                }
            }
            return direction;
        }


        private bool IsGameOver(Position snakeNewHead)
        {
            if (Snake.IsObstacle(snakeNewHead)) return true;
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
            Position food = CreateFood();
            PlaceFood(food);
            Console.CursorVisible = false;
            bool isGameOver = false;


            Position[] directions = new Position[]
            {
                new Position(0, 1), 
                new Position(0, -1), 
                new Position(1, 0), 
                new Position(-1, 0), 
            };
            int direction = (int)Directions.right;
            while (!isGameOver)
            {
                DrawScore();
                direction = HandleInput(direction);
                Position snakeHead = Snake.GetSnake.Last();
                Position nextDirection = directions[direction];
                Position snakeNewHead = new Position(snakeHead.Row + nextDirection.Row,
                    snakeHead.Col + nextDirection.Col);

                //-------------------------
                isGameOver = IsGameOver(snakeNewHead);
                //-------------------------

                Console.SetCursorPosition(snakeHead.Col, snakeHead.Row);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("*");
                Console.ResetColor();

                Snake.GetSnake.Enqueue(snakeNewHead);
                Console.SetCursorPosition(snakeNewHead.Col, snakeNewHead.Row);
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                if (direction == (int)Directions.right) Console.Write(">");
                if (direction == (int)Directions.left) Console.Write("<");
                if (direction == (int)Directions.up) Console.Write("^");
                if (direction == (int)Directions.down) Console.Write("v");

                if(snakeNewHead.Col == food.Col && snakeNewHead.Row ==  food.Row)
                {
                    food = CreateFood();
                    PlaceFood(food);
                    Position increasedSnake = snakeNewHead;
                    Snake.GetSnake.Enqueue(increasedSnake);
                    Score += 100;
                    if (gameSpeed >= 50)
                    {
                        gameSpeed--;
                    }
                    Obstacles.Add(Obstacle.GenerateObstacle(GeneratePosition()));
                    Obstacle.PlaceObstacle(Obstacles.Last());
                }
                if(Obstacles.Contains(snakeNewHead))
                {
                    isGameOver = true;
                    break;
                }

                Position snakeTail = Snake.GetSnake.Dequeue();
                Console.SetCursorPosition(snakeTail.Col, snakeTail.Row);
                Console.Write(" ");
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
