namespace SnakeGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
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

            Console.WriteLine("Welcome to the snake game, press any key to start playing!");
            Console.ReadKey(false);
            Console.Clear();
            Game game = new Game(25, 40);
            game.GameLoop();
            Console.Clear();
            Console.WriteLine($"Game over! You scored {game.Score} s-s-s-snake points!");
        }
    }
}