using System;

namespace RockPaperScissorsGame
{
    class Program 
    {
        static void Main(string[] args)
        {
            Game game = new Game(args);
            game.Run();
            Console.ReadKey();
        }     
    }
}
