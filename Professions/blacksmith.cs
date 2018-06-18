
class Blacksmith : Person {

    public override Profession Role { 
        get {
            return Profession.Blacksmith;
        }
    }
    public override void work() {
        Inventory[(int)Profession.Farmer]--;

        if(Inventory[(int)Profession.Blacksmith] >= 6) {
            return;
        }

        if(Inventory[(int)Profession.Miner] >= 1 && Inventory[(int)Profession.Lumberjack] >= 1) {
            Inventory[(int)Profession.Miner] -= 1;
            Inventory[(int)Profession.Lumberjack] -= 1;
            Inventory[(int)Profession.Blacksmith]++;
        }
    }

    protected override void considerBuy() {
        RTMarket woodMarket = ((VillageSim)(Simulation.SimInstance)).Markets[(int)Profession.Lumberjack];
        RTMarket oreMarket = ((VillageSim)(Simulation.SimInstance)).Markets[(int)Profession.Miner];
        RTMarket toolMarket = ((VillageSim)(Simulation.SimInstance)).Markets[(int)Profession.Blacksmith];

        decimal productionCosts = 1*(woodMarket.getLastPrice() + oreMarket.getLastPrice());

        //if(toolMarket.getLastPrice() > productionCosts) {
            woodMarket.searchOffer(this, (int)Profession.Lumberjack, 2, woodMarket.getLastPrice() + 0.2m);
            oreMarket.searchOffer(this, (int)Profession.Miner, 2, oreMarket.getLastPrice() + 0.2m);
        //}
        base.considerBuy();
    }

    // constructor 
    public Blacksmith() {
        Inventory[(int)Profession.Blacksmith] += 2;
    }
}