﻿using System;
using System.Diagnostics;

namespace EconSim
{
    class Program
    {
        static void Main(string[] args)
        {
            VillageSim Sim = new VillageSim(2000, 100);
            Sim.run();

            Console.ReadKey(true);
        }
    }
}