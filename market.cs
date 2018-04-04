
class Market {
    OrderBook Orderbook;

    public void placeOffer(int ID, int Ammount, int Price) {
        Offer newOffer = new Offer(ID, Ammount, Price);
        Orderbook.addOffer(newOffer);
    }

    // constructor
    public Market() {
        Orderbook = new OrderBook();
    }
}