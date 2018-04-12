using System;
class OrderBook {
    public Offer HeadNode { get; private set; }
    public int OfferAmmount { get; private set; }

    public void deleteLowestOffer() {
        HeadNode = HeadNode.nextOffer;
        OfferAmmount--;
    }

    // place offer in node list
    public void addOffer(Offer newBid) {
        OfferAmmount++;
        // Headnode check
        if(HeadNode == null || newBid.Price < HeadNode.Price) {
            newBid.nextOffer = HeadNode;
            HeadNode = newBid;
        } else {
        // loop check
            // actually previous node to the one being compared
            Offer currentNode = HeadNode;

            while(currentNode != null) {
                if(currentNode.nextOffer == null || currentNode.nextOffer.Price > newBid.Price) {
                    // insert between current and current.next
                    newBid.nextOffer = currentNode.nextOffer;
                    currentNode.nextOffer = newBid;
                    break;
                } else {
                    currentNode = currentNode.nextOffer;
                }
            }
        }
        CutBook();
    }



    static int maxSize = 2000;
    // if OfferAmmount surpasses maxSize, cut size to maxSize/2
    void CutBook() {
        if(OfferAmmount > maxSize) {
            Offer currentOffer = HeadNode;
            for(int i = 0; i < maxSize/2; i++) {
                currentOffer = currentOffer.nextOffer;
            }
            currentOffer.nextOffer = null;
            OfferAmmount = maxSize/2 + 1;
        }
    }


    // constructor
    public OrderBook() {
        HeadNode = null;
        OfferAmmount = 0;
    }
}