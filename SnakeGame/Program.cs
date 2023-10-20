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
            Menu mainMenu = new Menu("Choose a preferable option", new string[] {"New game", "Continue game (if any)"});
            int choice = mainMenu.Run();
            Game game;
            if (choice == 0)
            {
                game = new Game(25, 40);
            }
            else
            {
                try
                {
                    game = new Game(25, 40, "savegame.txt");
                }
                catch
                {
                    Console.WriteLine("No saved games found, press any key to start a new game");
                    Console.ReadKey(true);
                    game = new Game(25, 40);
                }
            }


            game.GameLoop();
            Console.Clear();
            Console.WriteLine($"Game over! You scored {game.Score} s-s-s-snake points!");
        }
    }
}