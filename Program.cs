using System;
using System.Diagnostics;

namespace EconSim
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //Console.Clear();
            //Simulation Sim = new Simulation(100,100,2,2);
            VillageSim Sim = new VillageSim(800);
            Random rnd = new Random();
            Stopwatch stop = new Stopwatch();
            stop.Start();
            foreach(Person p in Sim.Population) {
                int rand = rnd.Next(1,30);
                int randAmmount = rnd.Next(1,10);
                Sim.Markets[(int)p.Role].placeOffer(p,randAmmount,rand);
            }
            // Console.WriteLine(GC.GetTotalMemory(true));
            // Offer a = Sim.market.searchOffer(2,1,3,10);
            Console.WriteLine(stop.Elapsed.TotalMilliseconds.ToString());
            //Console.SetCursorPosition(0,0);
            Sim.run();

            Console.ReadKey(true);
        }
    }
}