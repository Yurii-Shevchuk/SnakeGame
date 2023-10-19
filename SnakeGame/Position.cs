using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    internal struct Position
    {
        private int _row;
        private int _col;

        public int Col => _col;
        public int Row => _row;
        public Position(int row, int col)
        {
            _row = row;
            _col = col;
        }

        public static bool operator ==(Position left, Position right)
        {
            return left._col == right._col && left._row == right._row;
        }

        public static bool operator !=(Position left, Position right)
        {
            return left._col == right._col && left._row == right._row;
        }

    }
}
