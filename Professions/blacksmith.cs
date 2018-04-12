
class Blacksmith : Person {

    public override Profession Role { 
        get {
            return Profession.Blacksmith;
        }
    }
    public override void work() {
        if(Inventory[(int)Profession.Miner] >= 2 && Inventory[(int)Profession.Lumberjack] >= 2) {
            Inventory[(int)Profession.Miner] -= 2;
            Inventory[(int)Profession.Lumberjack] -= 2;
            Inventory[(int)Profession.Blacksmith]++;
        }
        Inventory[(int)Profession.Farmer]--;
    }

    protected override void considerBuy() {
        Market woodMarket = ((VillageSim)(Simulation.SimInstance)).Markets[(int)Profession.Lumberjack];
        Market oreMarket = ((VillageSim)(Simulation.SimInstance)).Markets[(int)Profession.Miner];
        Market toolMarket = ((VillageSim)(Simulation.SimInstance)).Markets[(int)Profession.Blacksmith];

        decimal productionCosts = 2*(woodMarket.getLastPrice() + oreMarket.getLastPrice());

        if(toolMarket.getLastPrice() > productionCosts) {
            woodMarket.searchOffer(this, (int)Profession.Lumberjack, 2, woodMarket.getLastPrice() + 0.2m);
            oreMarket.searchOffer(this, (int)Profession.Miner, 2, oreMarket.getLastPrice() + 0.2m);
        }
        base.considerBuy();
    }

    // constructor 
    public Blacksmith() {
        Inventory[(int)Profession.Blacksmith] += 2;
    }
}