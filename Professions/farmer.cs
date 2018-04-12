using System;
using System.Linq;
class Farmer : Person {

    public override Profession Role { 
        get {
            return Profession.Farmer;
        }
    }
    public override void work() {
        if(Inventory[(int)Profession.Lumberjack] >= 2) {
            Inventory[(int)Profession.Lumberjack] -= 2;
            if(Inventory[(int)Profession.Blacksmith] > 0) {
                Inventory[(int)Profession.Farmer] += 3;
                // chance of breaking
                int num = rnd.Next(0,8);
                if(num == 5) {
                    Inventory[(int)Profession.Blacksmith]--;
                }
            } else {
                Inventory[(int)Profession.Farmer] += 2;
            }
        }
    }

    protected override void considerBuy() {
        Market woodMarket = ((VillageSim)(Simulation.SimInstance)).Markets[(int)Profession.Lumberjack];
        Market toolMarket = ((VillageSim)(Simulation.SimInstance)).Markets[(int)Profession.Blacksmith];
        decimal foodPrice = ((VillageSim)(Simulation.SimInstance)).Markets[(int)Profession.Farmer].getLastPrice();
        // buy wood
        if(Inventory[(int)Profession.Lumberjack] < 3) {
            woodMarket.searchOffer(this, (int)Profession.Lumberjack, 2, foodPrice*2);
        }
        // buy tools
        if(Inventory[(int)Profession.Blacksmith] < 1) {
            toolMarket.searchOffer(this, (int)Profession.Blacksmith, 1, foodPrice*8);
        }
    }

    // protected override void considerSell() {}

    // constructor 
    public Farmer() {

    } 
}