using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    internal class Obstacle :IPlacable
    {
        private Obstacle()
        {

        }
        public static Position CreateAndPlace(Position position)
        {
            Position obstacle = position;
            Console.SetCursorPosition(obstacle.Col, obstacle.Row);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("+");
            Console.ResetColor();
            return obstacle;
        }
    }
}
