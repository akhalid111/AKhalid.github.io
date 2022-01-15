using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChukNorris.Client
{
    class Program
    {
        static async Task Main(string[] args)

        {
            Console.WriteLine(Environment.NewLine + "Hello,This program displays jokes !");
            Console.WriteLine(Environment.NewLine + "Enter j  to display a random joke.");
            Console.WriteLine("Enter n  to display next joke.");
            Console.WriteLine("Enter p  to display previous joke.");
            Console.WriteLine("Enter exit to leave.");

            var jokesManager = new JokesManager();

            while (true)
            {
                Console.Write(Environment.NewLine + "Enter option : ");
                var keyValue = Console.ReadLine();

                if (string.Compare(keyValue.ToLowerInvariant(), "exit") == 0)
                    break;

                var joke = await jokesManager.DisplayJokes(keyValue);

                Console.WriteLine(Environment.NewLine + $"{joke} ");
            }
        }
    }
}
