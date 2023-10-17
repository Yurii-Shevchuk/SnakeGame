using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    internal class Obstacle :IPlacable
    {
        private Position _position;
        private string _graphics;
        public Obstacle(Position position, string graphics)
        {
            _position = position;
            _graphics = graphics;
        }

        public Position Coordinates => _position;
        public string Graphics => _graphics;
        public void Place()
        {
            Console.SetCursorPosition(Coordinates.Col, Coordinates.Row);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(Graphics);
            Console.ResetColor();
        }
    }
}
