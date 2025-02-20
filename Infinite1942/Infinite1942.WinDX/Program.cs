namespace Infinite1942
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using var game = new Infinite1942Game();
            game.Run();
        }
    }
}