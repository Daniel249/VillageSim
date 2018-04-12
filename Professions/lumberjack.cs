

class Lumberjack : Person {

    public override Profession Role { 
        get {
            return Profession.Lumberjack;
        }
    }
    public override void work() {

        if(Inventory[(int)Profession.Blacksmith] > 0) {
            Inventory[(int)Profession.Lumberjack] += 4;
            // chance of breaking
            int num = rnd.Next(0,8);
            if(num == 5) {
                Inventory[(int)Profession.Blacksmith]--;
            }
        } else {
            Inventory[(int)Profession.Lumberjack] += 2;
        }
        Inventory[(int)Profession.Farmer]--;
    }

    protected override void considerBuy() {
        decimal woodPrice = ((VillageSim)(Simulation.SimInstance)).Markets[(int)Profession.Lumberjack].getLastPrice();
        Market toolMarket = ((VillageSim)(Simulation.SimInstance)).Markets[(int)Profession.Blacksmith];

        toolMarket.searchOffer(this, (int)Profession.Blacksmith, 1, woodPrice*15);

        base.considerBuy();
    }

    // constructor 
    public Lumberjack() {

    } 
}