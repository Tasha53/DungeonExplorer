using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Starts game
            Game game = new Game();
            game.Start();
            //Ends game
            Console.WriteLine("Game over, press any key to exit...");
            Console.ReadKey();
        }
    }
}
