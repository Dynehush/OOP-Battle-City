

using Battle_City.Core;

namespace Battle_City
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Game.StartGame();
            Game.Run();
        }
    }
}
