using System;

abstract class Person {

    protected static Random rnd = new Random();
    // profession
    // main product location in inventory
    public abstract Profession Role { get; }

    // Inventory
    public int[] Inventory { get; private set; }

    // Currency
    public decimal Cash { get; private set; }

    // close deal
    public void Transaction(decimal cash, int resourceID, int resourceAmmount) {
        Inventory[resourceID] += resourceAmmount;
        Cash += cash;
    }


    // methods

    // main method
    public void turn() {
        work();
        considerSell();
        considerBuy();
        checkBankrupcy();
    }

    // override by inherited classes
    public abstract void work();
    

    // sell logic
    // always sells produced
    protected virtual void considerSell() {

        Market resourceMarket = ((VillageSim)(Simulation.SimInstance)).Markets[(int)Role];
        int searchDelay;
        decimal resourcePrice = resourceMarket.getLastPrice(out searchDelay);
        int randomNum = rnd.Next(-3, 9);
        if(randomNum < -1) {
            randomNum = -1;
        }
        decimal randomPrice = decimal.Divide(randomNum, 20) + resourcePrice - searchDelay*0.5m;
        if(randomPrice < 0.2m) {
            randomPrice = 0.2m;
        }
        resourceMarket.placeOffer(this, Inventory[(int)Role],randomPrice);
    }

    // buy logic
    // implementation for food
    protected virtual void considerBuy() {
        Market foodMarket = ((VillageSim)(Simulation.SimInstance)).Markets[(int)Profession.Farmer];
        if(Inventory[(int)Profession.Farmer] < 2) {
            decimal lastLogPrice = foodMarket.getLastPrice();

            int buyAmmount = (int)(Cash / lastLogPrice);
            if(buyAmmount > 3) {
                buyAmmount = 3;
            }
            // buy max possible ammount or 3 at 0.1 more than market price
            foodMarket.searchOffer(this, (int)Profession.Farmer, buyAmmount, lastLogPrice+0.1m);
        }
    }

    // check for death
    protected void checkBankrupcy() {
        if(Inventory[(int)Profession.Farmer] < 0) {
            ((VillageSim)(Simulation.SimInstance)).DeadPeople.Add(this);
        }
    }
    // set inventory. on respawn only
    public void Respawn(int[] _inventory) {
        Inventory = _inventory;
    }

    // constructor
    protected Person() {
        Inventory = new int[((VillageSim)(Simulation.SimInstance)).profAmmount];
        // gift food and wood to everyone
        int gift = rnd.Next(2, 5);
        if(Role != Profession.Farmer) {
            Inventory[(int)Profession.Farmer] = gift;
        }
        Inventory[(int)Profession.Lumberjack] = gift;
        Inventory[(int)Profession.Miner] = gift;
    }
}