using System;
// using System.Linq;
using System.Diagnostics;

partial class Simulation {

    // number of professions
    public int profAmmount { get; private set; }

    // references to Persons
    public Person[] Population { get; private set; }


    // initialize Persons in array
    void initPopulation(int ammountPerProf) {
        int counter = 0;

        for(int i = 0; i < profAmmount; i++) {
            initProfession(i, ammountPerProf, counter);
            counter += ammountPerProf;
        }
    }

    void initProfession(int professionID, int Ammount, int startPoint) {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        Type type = null;
            try {
                // cast the current proffesion
                Profession prof = (Profession)professionID;
                // cast it to type
                type = Type.GetType(prof.ToString());
            } catch(Exception e) {
                Console.WriteLine("Error: Could not cast Profession");
                Console.WriteLine("Log: Happened in {0}-ava Profession", ((Profession)professionID).ToString());
                Console.WriteLine(e);
            }

            for(int j = 0; j < Ammount; j++) {
                try {
                    var temp = Activator.CreateInstance(type);
                    Population[startPoint + j] = (Person)temp;
                } catch (Exception e) {
                    Console.WriteLine("Error: Could not initialize Person from type");
                    Console.WriteLine(e);
                }
            }

            // test
            Console.WriteLine(type.ToString() + "\t" + stopwatch.Elapsed.TotalMilliseconds.ToString());
            stopwatch.Restart();
    }

    public Simulation(int ammountPerProf) {
        // get professions from enum at compile
        profAmmount = Enum.GetNames(typeof(Profession)).Length;
        
        Population = new Person[ammountPerProf*profAmmount];

        initPopulation(ammountPerProf);


    }
}