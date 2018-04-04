
class Offer {
    int SellerID;
    int AmmountResource;
    public int Price { get; private set; }



    // reference to next node
    public Offer nextOffer { get; set; }

    // add to list. recursive
    public void addNode(Offer add) {
        if(nextOffer == null || add.Price < nextOffer.Price) {
            add.nextOffer = this.nextOffer;
            nextOffer = this;
        } else {
            nextOffer.addNode(add);
        }
    }


    // constructor
    public Offer(int _sellerID, int _ammountResource, int _price) {
        SellerID = _sellerID;
        AmmountResource= _ammountResource;
        Price = _price;
    }
}