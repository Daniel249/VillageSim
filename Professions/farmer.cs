using System;
using System.Linq;
class Farmer : Person {
    static int woodMultiplier = 2;
    static int inventorySize = 3;
    public override Profession Role { 
        get {
            return Profession.Farmer;
        }
    }
    public override void work() {
        Inventory[(int)Profession.Farmer] -= 1;

        if(Inventory[(int)Profession.Farmer] >= inventorySize) {
            return;
        }

        if(Inventory[(int)Profession.Lumberjack] >= woodMultiplier) {
            Inventory[(int)Profession.Lumberjack] -= woodMultiplier;
            if(Inventory[(int)Profession.Blacksmith] > 0) {
                Inventory[(int)Profession.Farmer] += 3;
                // chance of breaking
                int num = rnd.Next(0,4);
                if(num == 2) {
                    Inventory[(int)Profession.Blacksmith]--;
                }
            } else {
                Inventory[(int)Profession.Farmer] += 2;
            }
        // } else {
        //     Inventory[(int)Profession.Farmer] += 1;
        }
    }

    protected override void considerBuy() {
        RTMarket woodMarket = ((VillageSim)(Simulation.SimInstance)).Markets[(int)Profession.Lumberjack];
        RTMarket toolMarket = ((VillageSim)(Simulation.SimInstance)).Markets[(int)Profession.Blacksmith];
        decimal foodPrice = ((VillageSim)(Simulation.SimInstance)).Markets[(int)Profession.Farmer].getLastPrice();
        // buy wood
        if(Inventory[(int)Profession.Lumberjack] < 3) {
            woodMarket.searchOffer(this, (int)Profession.Lumberjack, 2, foodPrice);
        }
        // buy tools
        if(Inventory[(int)Profession.Blacksmith] == 0) {
            toolMarket.searchOffer(this, (int)Profession.Blacksmith, 1, foodPrice*4);
        }
    }

    // protected override void considerSell() {
    //     Market foodMarket = ((VillageSim)(Simulation.SimInstance)).Markets[(int)Profession.Farmer];
    //     decimal woodPrice = ((VillageSim)(Simulation.SimInstance)).Markets[(int)Profession.Lumberjack].getLastPrice();
    //     foodMarket.placeOffer(this, Inventory[(int)Profession.Farmer], woodPrice*2)
    // }

    // protected override void considerSell() {}

    // constructor 
    public Farmer() {

    } 
}