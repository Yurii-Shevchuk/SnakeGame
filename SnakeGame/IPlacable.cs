﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    internal interface IPlacable
    {
        static Position CreateAndPlace(Position position)
        {
            throw new NotImplementedException();
        }
    }
}
