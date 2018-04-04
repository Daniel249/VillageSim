
class OrderBook {
    public Offer HeadNode { get; private set; }

    // place offer in node list
    public void addOffer(Offer newBid) {
        if(HeadNode == null || newBid.Price < HeadNode.Price) {
            newBid.nextOffer = HeadNode;
            HeadNode = newBid;
        } else {
            HeadNode.addNode(newBid);
        }
    }

    // constructor
    public OrderBook() {
        HeadNode = null;
    }
}