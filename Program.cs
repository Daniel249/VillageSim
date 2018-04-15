using System;
using System.Diagnostics;

namespace EconSim
{
    class Program
    {
        static void Main(string[] args)
        {
            VillageSim Sim = new VillageSim(800, 10);
            Sim.run();

            Console.ReadKey(true);
        }
    }
}