

class Lumberjack : Person {

    public override Profession Role { 
        get {
            return Profession.Lumberjack;
        }
    }
    public override void work() {
        Inventory[(int)Profession.Farmer]--;

        if(Inventory[(int)Profession.Lumberjack] >= 6) {
            return;
        }

        if(Inventory[(int)Profession.Blacksmith] > 0) {
            Inventory[(int)Profession.Lumberjack] += 3;
            // chance of breaking
            int num = rnd.Next(0,4);
            if(num == 2) {
                Inventory[(int)Profession.Blacksmith]--;
            }
        } else {
            Inventory[(int)Profession.Lumberjack] += 2;
        }
    }

    protected override void considerBuy() {
        decimal woodPrice = ((VillageSim)(Simulation.SimInstance)).Markets[(int)Profession.Lumberjack].getLastPrice();
        Market toolMarket = ((VillageSim)(Simulation.SimInstance)).Markets[(int)Profession.Blacksmith];

        base.considerBuy();
        
        if(Inventory[(int)Profession.Blacksmith] == 0) {
            toolMarket.searchOffer(this, (int)Profession.Blacksmith, 1, woodPrice*4);
        }

    }

    // constructor 
    public Lumberjack() {

    } 
}