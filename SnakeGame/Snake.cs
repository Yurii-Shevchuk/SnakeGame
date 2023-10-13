using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    
    internal class Snake
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

        /*public void FeedSnake(Position position)
        {
            GetSnake.Enqueue;
        }*/
        public bool IsObstacle(Position obstacle)
        {
           return  GetSnake.Contains(obstacle);
        }
    }
    

}
