using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
            Direction = (int)Directions.right;
        }

        public Game(int height, int width, string path)
        {
            Queue<Position> snake;
            _grid = new Grid(height, width);
            _grid.Draw();
            using (StreamReader reader = new StreamReader(path))
            using (JsonReader reader2 = new JsonTextReader(reader))
            {
                reader2.SupportMultipleContent = true;
                JsonSerializer serializer = new JsonSerializer { CheckAdditionalContent = false };                 
                _obstacles = (List<Obstacle>)serializer.Deserialize(reader2, typeof(List<Obstacle>));
                reader2.Read();
                snake = (Queue<Position>)serializer.Deserialize(reader2, typeof(Queue<Position>));
                reader2.Read();
                Score = (int)serializer.Deserialize(reader2, typeof(int));
                reader2.Read();
                Direction = (int)serializer.Deserialize(reader2 , typeof(int));
            }
            foreach (var obstacle in _obstacles)
            {
                obstacle.Place();
            }
            _isGameOver = false;
            _foodCreator = new FoodCreator();
            _obstacleCreator = new ObstacleCreator();  
            _snake = new Snake(snake);
            _snake.Draw();
        }

        public List<Obstacle> Obstacles => _obstacles;

        public AbstractCreator<Food> FoodCreator => _foodCreator;
        public AbstractCreator<Obstacle> ObstacleCreator => _obstacleCreator;
        public Snake Snake => _snake;
        public bool IsGameLost
        {
            get => _isGameOver;
            private set => _isGameOver = value;
        }
        public Grid GameGrid => _grid;

        public int Direction
        {
            get;
            private set;
        }
        public int Score { get; private set; }
        
        private void DrawScore()
        {
            Console.SetCursorPosition(0, GameGrid.Height + 1);
            Console.Write(Score);
        }

        private void HandleExit()
        {
            int choice = -1;
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo userInput = Console.ReadKey(true);
                ConsoleKey key = userInput.Key;
                if (key == ConsoleKey.Escape)
                {
                    Menu exitMenu = new Menu("Do you want to save your progress?", new string[] { "Yes", "No" });
                    choice = exitMenu.Run();
                }
            }
            if(choice == 0)
            {
                SaveGame();
            }
            else if(choice == 1)
            {
                IsGameLost = true;
            }

        }

        private void SaveGame()
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;

            using (StreamWriter writer = new StreamWriter("savegame.txt"))
            using (JsonWriter jsonWriter = new JsonTextWriter(writer))
            {
                serializer.Serialize(jsonWriter, Obstacles);
                serializer.Serialize(jsonWriter, Snake.GetSnake);
                writer.WriteLine();
                serializer.Serialize(jsonWriter, Score);
                writer.WriteLine();
                serializer.Serialize(jsonWriter, Direction);
            }
            IsGameLost = true;
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
            while (!IsGameLost)
            {
                DrawScore();
                Direction = Snake.HandleMovement(Direction);

                Position snakeHead = Snake.GetSnake.Last();
                Position snakeNewHead = Snake.UpdateHead(Direction);

                //-------------------------
                IsGameLost = IsGameOver(snakeNewHead);
                //-------------------------

                Snake.UpdateBody(snakeHead);

                Snake.DrawNewHead(snakeNewHead, Direction);

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
                HandleExit();
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
