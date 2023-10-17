using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    internal class ObstacleCreator :AbstractCreator
    {
        public override IPlacable CreatePlacable(Position position, string graphics)
        {
            return new Obstacle(position, graphics);
        }
    }
}
