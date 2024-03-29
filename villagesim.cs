using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

class VillageSim : Simulation {

    
    // number of professions
    public int profAmmount { get; private set; }

    // references to Persons
    public HashSet<Person> Population { get; private set; }
    public HashSet<Person>[] Demographics { get; private set; }


    // money per person
    decimal MoneySupply;
    decimal currentMoneySupply;

    // market
    public RTMarket[] Markets { get; private set; }

    // TODO: test wih Parallel.ForEach
    // note paralellism overhead

    //  Parallel.ForEach(Population, p =>
    //  {
    //      //Your stuff
    //  });

    // potential to run foreach(demo d in demografics) {foreach Person p in d }

    // TODO individual person turn control

    // public bool nextPerson;
    protected override void turn() {
        // start new log
        for(int i = 0; i < Markets.Length; i++) {
            Markets[i].StartTimeSpan();
        }
        // turn
        foreach(Person p in Population) {
            p.turn();
        }
        // print data
        Interface.printDems();
        // respawn death row people
        // foreach(Person p in DeadPeople) {
        //     lostCash += p.Cash;
        //     RespawnPerson(p);
        // }
        RespawnBankruptPeople();
        // update current DataPanels. BEFORE endTimeSpan, else currents return null
        Interface.updateCurrentData(getCurrentLogs());
        // store current logs
        for(int i = 0; i < Markets.Length; i++) {    
            Markets[i].EndTimeSpan();
        }
        // determine most profitable
        // decimal currentHighest_perCapita = 0m;
        // for(int i = 0; i < Markets.Length; i++) {
        //     // CurrencyVolume / Demographic.Count > currentHighest
        //     if(Markets[i].Logs.LastOrDefault().CurrencyVolume / Demographics[i].Count > currentHighest_perCapita) {
        //         currentHighest_perCapita = Markets[i].Logs.LastOrDefault().CurrencyVolume / Demographics[i].Count;
                
        //         currentProfitable = (Profession)i;
        //     }
        // }
        DeadPeople.Clear();
    }


    // Set up
    protected override void SimSetup() {
        FakeLogSetup(10, 5, 3, 10);
        Interface.initInterface();
        // use given moneysupply and re initialize to total money supply
        initMoneySupply(MoneySupply);
        MoneySupply = MoneySupply*Population.Count;
        currentMoneySupply = MoneySupply;
    }

    // save a first log
    void FakeLogSetup(params int[] defaultPrice) {
        for(int i = 0; i < defaultPrice.Length; i++) {
            // initialize first fake log
            Markets[i].StartTimeSpan();
            TimeSpan fakeLog =  Markets[i].CurrentLog;
            // log fake transaction
            decimal price = (decimal)defaultPrice[i];
            int resourceVolume = 1;
            decimal currencyVolume = price*resourceVolume; 
            
            fakeLog.LogTransaction(price, resourceVolume, currencyVolume);
            // store fake log
            Markets[i].EndTimeSpan();
        }
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

    // init demographics
    void initProfession(int professionID, int Ammount) {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        // Type type = getProfession(professionID);
        Profession prof = (Profession)professionID;
        
        for(int j = 0; j < Ammount; j++) {
            // try {
            //     Person temp = (Person)Activator.CreateInstance(type);
            //     Population.Add(temp);
            //     Demographics[professionID].Add(temp);
            // } catch (Exception e) {
            //     Console.WriteLine("Error: Could not initialize Person from type");
            //     Console.WriteLine(e);
            // }
            Person temp = prof.ToPerson();
            Population.Add(temp);
            Demographics[professionID].Add(temp);
        }
        // test
        Console.WriteLine(prof.ToString() + "\t" + stopwatch.Elapsed.TotalMilliseconds.ToString());
    }

    // helper : cast int to Person type
    Type getProfession(int professionID) {
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
        return type;
    }

    // gift first money to exist
    void initMoneySupply(decimal num) {
        foreach(Person p in Population) {
            p.Transaction(num, 0, 0);
        }
    }



    // person respawn control

    // Persons on death row
    public List<Person> DeadPeople = new List<Person>();
    public Profession currentProfitable = 0;
    void RespawnBankruptPeople() {
        // divide number of bankrupt people in profAmmount ammount of integer parts
        int [] afterDiv = Helper.IntegerDivision(DeadPeople.Count, profAmmount);

        for(int i = 0; i < DeadPeople.Count; i++) {
            // delete person references
            Person person = DeadPeople[i];
            Population.Remove(person);
            Demographics[(int)person.Role].Remove(person);
            // lostCash += person.Cash;
            currentMoneySupply -= person.Cash;
        }
        for(int prof = 0; prof < afterDiv.Length; prof++) {
            for(int i = 0; i < afterDiv[prof]; i++) {
                
                // check money supply
                decimal cash = 0;
                if(MoneySupply - currentMoneySupply > 10) {
                    currentMoneySupply += 10;
                    cash = 10;
                }
                // initialize new person
                // Person temp = (Person)Activator.CreateInstance(type);
                Person temp = ((Profession)prof).ToPerson();
                temp.Transaction(cash, 0, 0);
                // create references
                Demographics[prof].Add(temp);
                Population.Add(temp);
            }
        }

    }

    // interface helper: get each market's current Logs
    TimeSpan[] getCurrentLogs() {
        TimeSpan[] logs = new TimeSpan[profAmmount];

        for(int i = 0; i < profAmmount; i++) {
            logs[i] = Markets[i].CurrentLog;
        }
        return logs;
    }

    // constructor
    public VillageSim(int ammountPerProf, decimal moneysupply) {
        // get professions from enum at compile
        profAmmount = Enum.GetNames(typeof(Profession)).Length;
        SimInstance = this;
        
        Population = new HashSet<Person>();
        Demographics = new HashSet<Person>[profAmmount];
        MoneySupply = moneysupply;

        initPopulation(ammountPerProf);
        Markets = new RTMarket[profAmmount];
        for(int i = 0; i < profAmmount; i++) {
            Markets[i] = new RTMarket(profAmmount);
        }
    }


    // // extra functionality to specify the ammount of each profession, 
    // //instead of same for every profession

    // // constructor
    // public VillageSim(params int[] ammountPerProf) {
    //     // get professions from enum at compile
    //     profAmmount = Enum.GetNames(typeof(Profession)).Length;
    //     // add inputs and initialize array
    //     Population = new HashSet<Person>();
    //     Demographics = new HashSet<Person>[profAmmount];
    //     // use inputs to initilize population
    //     initPopulation(ammountPerProf);
    // }

    // // initialize Persons in array
    // void initPopulation(int[] ammountPerProf) {
    //     if(ammountPerProf.Length < profAmmount) {
    //         Console.WriteLine("Error: not enough inputs");
    //     } else {
    //         for(int i = 0; i < profAmmount; i++) {
    //             initProfession(i, ammountPerProf[i]);
    //         }
    //     }
    // }
 }