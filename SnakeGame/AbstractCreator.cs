using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    internal abstract class AbstractCreator<T> where T : IPlacable
    {
        public abstract T CreatePlacable(Position position, string graphics);
    }
}
