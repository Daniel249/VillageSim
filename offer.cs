
class Offer {
    public int SellerID { get; private set; }
    public int AmmountResource { get; private set; }
    public decimal Price { get; private set; }


    // reference to next node
    public Offer nextOffer;


    // -1: Price already off
    // 0: ammount to big, keep search
    // 1: Match
    public int checkMatch(int maxAmmount, int maxPrice) {
        // stop search if already off price
        if(maxPrice < Price) {
            return -1;
        }
        if(maxAmmount < AmmountResource) {
            return 0;
        }
        // match both price and ammount
        return 1;
    }


    // constructor
    public Offer(int _sellerID, int _ammountResource, decimal _price) {
        SellerID = _sellerID;
        AmmountResource= _ammountResource;
        Price = _price;
    }
}