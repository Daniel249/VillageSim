
class Market {
    OrderBook[] Orderbooks;

    // NOTE: resourceID is used to place the offer on the respective orderbook
    //      as well as finding the seller after a succesfull searchOffer
    //      given that Units can only sell resources from their profession
    public void placeOffer(int sellerID, int resourceID, int Ammount, decimal Price) {
        Offer newOffer = new Offer(sellerID, Ammount, Price);
        Orderbooks[resourceID].addOffer(newOffer);
    }

    public Offer searchOffer(int buyerID, int resourceID, int maxAmmount, int maxPrice) {
        Offer foundOffer = Orderbooks[resourceID].scanOffer(maxAmmount, maxPrice);
        closeDeal(foundOffer, buyerID, resourceID);

        return foundOffer;
    }
    void closeDeal(Offer closeOffer, int buyerID, int resourceID) {
        int sellerID = closeOffer.SellerID;
        int Ammount = closeOffer.AmmountResource;
        decimal Price = closeOffer.Price;
        decimal totalCost = Ammount*Price;

        Simulation.SimInstance.Population[resourceID][sellerID]
            .Transaction(totalCost, resourceID, (-1)*Ammount);

        Simulation.SimInstance.Population[resourceID][sellerID]
            .Transaction(totalCost, resourceID, (-1)*Ammount);
    }
    // constructor
    public Market(int resourceAmmount) {
        Orderbooks = new OrderBook[resourceAmmount];
        for(int i = 0; i < resourceAmmount; i++) {
            Orderbooks[i] = new OrderBook();
        }
    }
}