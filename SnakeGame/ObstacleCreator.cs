using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    internal class ObstacleCreator :AbstractCreator<Obstacle>
    {
        public override Obstacle CreatePlacable(Position position, string graphics)
        {
            return new Obstacle(position, graphics);
        }
    }
}
