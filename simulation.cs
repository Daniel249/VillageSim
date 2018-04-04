using System;
using System.Linq;
using System.Diagnostics;

class Simulation {

    // number of professions
    public int profAmmount { get; private set; }

    // references to Persons
    public Person[] Population { get; private set; }


    // initialize Persons in array
    void initPopulation(int[] ammountPerProf) {
        if(ammountPerProf.Length < profAmmount) {
            Console.WriteLine("Error: not enough inputs");
        } else {
            int counter = 0;
            // for each profession
            for(int i = 0; i < profAmmount; i++) {
                Type type = null;
                try {
                    // cast the current proffesion
                    Profession prof = (Profession)i;
                    // cast it to type
                    type = Type.GetType(prof.ToString());
                } catch(Exception e) {
                    Console.WriteLine("Error: Could not cast Profession");
                    Console.WriteLine("Log: Happened in {0}-ava Profession",i);
                    Console.WriteLine(e);
                }

                // add to Population
                for(int j = 0; j < ammountPerProf[i]; j++) {
                    try {
                        var temp = Activator.CreateInstance(type);
                        Population[counter] = (Person)temp;
                    } catch (Exception e) {
                        Console.WriteLine("Error: Could not initialize Person from type");
                        Console.WriteLine(e);
                    }
                    counter++;
                }
            }
        }
    }

    void initPopulation(int ammountPerProf) {
        int counter = 0;

        for(int i = 0; i < profAmmount; i++) {
            Type type = null;
            try {
                // cast the current proffesion
                Profession prof = (Profession)i;
                // cast it to type
                type = Type.GetType(prof.ToString());
            } catch(Exception e) {
                Console.WriteLine("Error: Could not cast Profession");
                Console.WriteLine("Log: Happened in {0}-ava Profession",i);
                Console.WriteLine(e);
            }

            for(int j = 0; j < ammountPerProf; j++) {
                try {
                    var temp = Activator.CreateInstance(type);
                    Population[counter] = (Person)temp;
                } catch (Exception e) {
                    Console.WriteLine("Error: Could not initialize Person from type");
                    Console.WriteLine(e);
                }
                counter++;
            }

            // test
            Console.WriteLine(type.ToString() + "\t" + stopwatch.Elapsed.TotalMilliseconds.ToString());
            stopwatch.Restart();

        }
    }


    // constructor
    public Simulation(params int[] ammountPerProf) {
        // get professions from enum at compile
        profAmmount = Enum.GetNames(typeof(Profession)).Length;
        // add inputs and initialize array
        Population = new Person[ammountPerProf.Sum()];
        // use inputs to initilize population
        initPopulation(ammountPerProf);
    }
    // test
    Stopwatch stopwatch;
    public Simulation(int ammountPerProf) {
        // get professions from enum at compile
        profAmmount = Enum.GetNames(typeof(Profession)).Length;
        
        Population = new Person[ammountPerProf*profAmmount];
        stopwatch = new Stopwatch();
        
        // test
        stopwatch.Start();

        initPopulation(ammountPerProf);


    }
}