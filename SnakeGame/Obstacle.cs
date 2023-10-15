using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    internal class Obstacle
    {
        public Obstacle()
        {

        }
        public static Position GenerateObstacle(Position position)
        {
            Position obstacle = position;
            return obstacle;
        }
        public static void PlaceObstacle(Position obstacle)
        {
            Console.SetCursorPosition(obstacle.Col, obstacle.Row);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("+");
            Console.ResetColor();
        }
    }
}
