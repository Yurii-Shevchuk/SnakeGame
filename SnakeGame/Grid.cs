using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace SnakeGame
{
    internal class Grid
    {
        private string[,] _grid;
        private int _width;
        private int _height;
        private Random _random;

        public Grid(int height, int width)
        {
            _grid = GenerateGrid(width, height);
            _width = width;
            _height = height;
            _random = new Random();
        }
        public string GetElementAt(int x, int y)
        {
            return _grid[y, x];
        }
        public int Height => _height;
        public int Width => _width;

        private string[,] GenerateGrid(int width, int height)
        {
            string[,] grid = new string[height, width * 2 - 2];
            for (int i = 0; i < width; i+=2)
            {
                grid[0, i] = "*";
                grid[0, i+1] = " ";
            }
            Console.WriteLine();
            for (int i = 1; i < height - 1; i++)
            {
                grid[i,0] = "*";
                for (int j = 1; j < width; j++)
                {
                    grid[i, j] = " ";
                }
                grid[i, width-1] = "*";
                
            }
            for (int i = 0; i < width; i += 2)
            {
                grid[height-1, i] = "* ";
            }
            return grid;
        }

        public Position GeneratePosition()
        {
            Position position;
            do
            {
                position = new Position(_random.Next(1, Height - 1), _random.Next(1, Width - 1));
            } while (GetElementAt(position.Col, position.Row) != " ");
            return position;
        }
        public void Draw()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    string element = GetElementAt(x, y);
                    SetCursorPosition(x, y);
                    Write(element);
                }
                WriteLine();
            }
        }

    }
}
