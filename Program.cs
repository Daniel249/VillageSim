using System;

namespace EconSim
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Simulation Sim = new Simulation(Enum.GetNames(typeof(Profession)).Length);
        }
    }
}
