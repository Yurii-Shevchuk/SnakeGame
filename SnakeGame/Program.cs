namespace SnakeGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Game game = new Game(25, 40);
            game.GameLoop();
            Console.Clear();
            Console.WriteLine($"Game over! You scored {game.Score} s-s-s-snaky points!");
        }
    }
}