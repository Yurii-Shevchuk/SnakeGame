using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    internal abstract class AbstractCreator
    {
        public abstract IPlacable CreatePlacable(Position position, string graphics);
    }
}
