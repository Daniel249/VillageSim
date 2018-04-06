using System;
// using System.Linq;
using System.Diagnostics;

class Simulation {

    // number of professions
    public int profAmmount { get; private set; }

    // references to Persons
    public Person[][] Population { get; private set; }

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

        Person[] newDemographic = new Person[Ammount];

        for(int j = 0; j < Ammount; j++) {
            try {
                var temp = Activator.CreateInstance(type);
                newDemographic[j] = (Person)temp;
            } catch (Exception e) {
                Console.WriteLine("Error: Could not initialize Person from type");
                Console.WriteLine(e);
            }
        }
        Population[professionID] = newDemographic;

        // test
        Console.WriteLine(type.ToString() + "\t" + stopwatch.Elapsed.TotalMilliseconds.ToString());
    }

    public Simulation(int ammountPerProf) {
        // get professions from enum at compile
        profAmmount = Enum.GetNames(typeof(Profession)).Length;
        
        Population = new Person[profAmmount][];

        initPopulation(ammountPerProf);
        market = new Market(profAmmount);

        SimInstance = this;
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
        Population = new Person[profAmmount][];
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