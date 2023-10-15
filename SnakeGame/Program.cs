namespace SnakeGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.CursorVisible = false;

            Console.WriteLine(@"           /^\/^\
         _|__|  O|
\/     /~     \_/ \
 \____|__________/  \
        \_______      \
                `\     \                 \
                  |     |                  \
                 /      /                    \
                /     /                       \\
              /      /                         \ \
             /     /                            \  \
           /     /             _----_            \   \
          /     /           _-~      ~-_         |   |
         (      (        _-~    _--_    ~-_     _/   |
          \      ~-____-~    _-~    ~-_    ~-_-~    /
            ~-_           _-~          ~-_       _-~
               ~--______-~                ~-___-~");

            Console.WriteLine("Welcome to the snake game, press any key to start playing! Controls are arrow keys ;)\nP.S: Beware of the obstacles");
            Console.ReadKey(false);
            Console.ResetColor();
            Console.Clear();
            Game game = new Game(25, 40);
            game.GameLoop();
            Console.Clear();
            Console.WriteLine($"Game over! You scored {game.Score} s-s-s-snake points!");
        }
    }
}