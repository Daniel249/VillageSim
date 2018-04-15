

class Miner : Person {

    public override Profession Role { 
        get {
            return Profession.Miner;
        }
    }

    public override void work() {

        if(Inventory[(int)Profession.Blacksmith] > 0) {
            Inventory[(int)Profession.Miner] += 3;
            // chance of breaking
            int num = rnd.Next(0,8);
                if(num == 5) {
                    Inventory[(int)Profession.Blacksmith]--;
                }
        } else {
            Inventory[(int)Profession.Miner] += 2;
        }
        Inventory[(int)Profession.Farmer]--;
    }

    protected override void considerBuy() {
        decimal orePrice = ((VillageSim)(Simulation.SimInstance)).Markets[(int)Profession.Miner].getLastPrice();
        Market toolMarket = ((VillageSim)(Simulation.SimInstance)).Markets[(int)Profession.Blacksmith];

        toolMarket.searchOffer(this, (int)Profession.Blacksmith, 1, orePrice*4);

        base.considerBuy();
    }

    // constructor 
    public Miner() {

    } 
}