using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    internal class Menu
    {
        private string _prompt;
        private string[] _options;
        private int _index;
        public Menu(string prompt, string[] options)
        {
            _prompt = prompt;
            _options = options;
            _index = 0;
        }

        public void DisplayMenu()
        {
            Console.WriteLine(_prompt);
            Console.WriteLine();
            for (int i=0; i<_options.Length; i++)
            {
                if(i ==  _index)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine(_options[i]);
            }
            Console.ResetColor();
        }

        public int Run()
        {
            ConsoleKey key;
            do
            {
                Console.Clear();
                DisplayMenu();
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                key = keyInfo.Key;
                if(key == ConsoleKey.UpArrow)
                {
                    _index--;
                    if( _index < 0 )
                    {
                        _index = _options.Length - 1;
                    }
                }
                else if(key == ConsoleKey.DownArrow)
                {
                    _index++;
                    if( _index > _options.Length - 1 )
                    {
                        _index = 0;
                    }
                }
            } while (key != ConsoleKey.Enter);
            return _index;
        }
    }
}
