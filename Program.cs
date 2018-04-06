using System;
using System.Diagnostics;

namespace EconSim
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //Simulation Sim = new Simulation(100,100,2,2);
            Simulation Sim = new Simulation(100);
            Random rnd = new Random();
            Stopwatch stop = new Stopwatch();
            stop.Start();
            for(int i = 0; i < 10; i++) {
                //stop.Restart();
                int rand = rnd.Next(1,30);
                int randAmmount = rnd.Next(1,10);
                Sim.market.placeOffer(1,1,randAmmount,rand);
            }
            // Offer a = Sim.market.searchOffer(2,1,3,10);
            Console.WriteLine(stop.Elapsed.TotalMilliseconds.ToString());
            Sim.run();
            Console.Read();
        }
    }
}
