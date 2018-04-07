using System;
using System.Collections.Generic;
// using System.Linq;
using System.Diagnostics;

class Simulation {

    // number of professions
    public int profAmmount { get; private set; }

    // references to Persons
    public HashSet<Person> Population { get; private set; }
    public HashSet<Person>[] Demographics { get; private set; }

    // market
    public Market market { get; private set; }


    public bool continueSim { get; set; }
    public void run() {
        continueSim = true;
        while(continueSim) {
            turn();
        }
    }

    public void turn() {

    }


    // initialize Persons in array
    void initPopulation(int ammountPerProf) {
        for(int i = 0; i < profAmmount; i++) {
            Demographics[i] = new HashSet<Person>();
        }
        for(int i = 0; i < profAmmount; i++) {
            initProfession(i, ammountPerProf);
        }
    }

    void initProfession(int professionID, int Ammount) {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        Type type = null;
        try {
            // cast the current proffesion
            Profession prof = (Profession)professionID;
            // cast it to type
            type = Type.GetType(prof.ToString());
        } catch (Exception e) {
            Console.WriteLine("Error: Could not cast Profession");
            Console.WriteLine("Log: Happened in {0}-ava Profession", ((Profession)professionID).ToString());
            Console.WriteLine(e);
        }


        for(int j = 0; j < Ammount; j++) {
            try {
                Person temp = (Person)Activator.CreateInstance(type);
                Population.Add(temp);
                Demographics[professionID].Add(temp);
            } catch (Exception e) {
                Console.WriteLine("Error: Could not initialize Person from type");
                Console.WriteLine(e);
            }
        }

        // test
        Console.WriteLine(type.ToString() + "\t" + stopwatch.Elapsed.TotalMilliseconds.ToString());
    }

    public Simulation(int ammountPerProf) {
        // get professions from enum at compile
        profAmmount = Enum.GetNames(typeof(Profession)).Length;
        SimInstance = this;
        
        Population = new HashSet<Person>();
        Demographics = new HashSet<Person>[profAmmount];

        initPopulation(ammountPerProf);
        market = new Market(profAmmount);

    }
    // singleton reference
    public static Simulation SimInstance { get; private set; }



    // extra functionality to specify the ammount of each profession, 
    //instead of same for every profession

    // constructor
    public Simulation(params int[] ammountPerProf) {
        // get professions from enum at compile
        profAmmount = Enum.GetNames(typeof(Profession)).Length;
        // add inputs and initialize array
        Population = new HashSet<Person>();
        Demographics = new HashSet<Person>[profAmmount];
        // use inputs to initilize population
        initPopulation(ammountPerProf);
    }

    // initialize Persons in array
    void initPopulation(int[] ammountPerProf) {
        if(ammountPerProf.Length < profAmmount) {
            Console.WriteLine("Error: not enough inputs");
        } else {
            for(int i = 0; i < profAmmount; i++) {
                initProfession(i, ammountPerProf[i]);
            }
        }
    }
}