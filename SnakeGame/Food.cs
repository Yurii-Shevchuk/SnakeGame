using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    internal class Food :IPlacable
    {
        private Food() { }

        public static Position CreateAndPlace(Position position) 
        {
            Position food = position;
            Console.SetCursorPosition(food.Col, food.Row);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("$");
            Console.ResetColor();
            return food;
        }
    }
}
