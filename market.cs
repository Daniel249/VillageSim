
class Market {
    OrderBook[] Orderbooks;

    public void placeOffer(int sellerID, int resourceID, int Ammount, int Price) {
        Offer newOffer = new Offer(sellerID, Ammount, Price);
        Orderbooks[resourceID].addOffer(newOffer);
    }

    // constructor
    public Market(int resourceAmmount) {
        Orderbooks = new OrderBook[resourceAmmount];
        for(int i = 0; i < resourceAmmount; i++) {
            Orderbooks[i] = new OrderBook();
        }
    }
}