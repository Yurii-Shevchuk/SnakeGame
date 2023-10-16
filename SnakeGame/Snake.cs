using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    
    internal class Snake : IMovable
    {
        private Queue<Position> _snake = new Queue<Position>();
        public Snake()
        {
            _snake = InitializeSnake(_snake);
        }

        public Queue<Position> GetSnake => _snake;
        private Queue<Position> InitializeSnake(Queue<Position> snake)
        {
            for (int i = 1; i <= 6; i++)
            {
                snake.Enqueue(new Position(1, i));
            }
            return snake;
        }

        public void Draw()
        {
            foreach (var position in GetSnake)
            {
                Console.SetCursorPosition(position.Col, position.Row);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("*");
            }
            Console.ResetColor();
        }

        public Position UpdateHead(int direction)
        {
            Position[] directions = new Position[]
            {
                new Position(0, 1),
                new Position(0, -1),
                new Position(1, 0),
                new Position(-1, 0),
            };
            Position snakeHead = GetSnake.Last();
            Position nextDirection = directions[direction];
            Position snakeNewHead = new Position(snakeHead.Row + nextDirection.Row,
                snakeHead.Col + nextDirection.Col);
            return snakeNewHead;
        }


        public void UpdateBody(Position body)
        {
            Console.SetCursorPosition(body.Col, body.Row);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("*");
            Console.ResetColor();
        }

        public void DrawNewHead(Position newHead, int direction)
        {
            GetSnake.Enqueue(newHead);
            Console.SetCursorPosition(newHead.Col, newHead.Row);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            if (direction == (int)Directions.right) Console.Write(">");
            if (direction == (int)Directions.left) Console.Write("<");
            if (direction == (int)Directions.up) Console.Write("^");
            if (direction == (int)Directions.down) Console.Write("v");
        }

        public void EraseTail()
        {
            Position snakeTail = GetSnake.Dequeue();
            Console.SetCursorPosition(snakeTail.Col, snakeTail.Row);
            Console.Write(" ");
        }


        public int HandleMovement(int direction)
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
    }
    

}
