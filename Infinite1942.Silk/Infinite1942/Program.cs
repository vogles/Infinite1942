namespace Infinite1942
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game("Infinite 1942", 800, 600);
            game.RunDX();
        }
    }
}
