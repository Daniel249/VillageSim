using System;
class OrderBook {
    public Offer HeadNode;

    // place offer in node list
    public void addOffer(Offer newBid) {
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
    }
    public Offer scanOffer(int maxAmmount, int maxPrice) {
        // Headnode check
        int num = HeadNode.checkMatch(maxAmmount, maxPrice);

        if(num == -1) {
            return null;
        } else if(num == 1) {
            Offer tempOffer = HeadNode;
            HeadNode = HeadNode.nextOffer;
            return tempOffer;
        } else if(num == 0) {
        // loop check
            Offer currentNode = HeadNode;

            while(currentNode.nextOffer != null) {
                int mun = currentNode.nextOffer.checkMatch(maxAmmount, maxPrice);
                if(mun == -1) {
                    return null;
                } else if(mun == 1) {
                    Offer tempOffer = currentNode.nextOffer;
                    currentNode.nextOffer = currentNode.nextOffer.nextOffer;
                    return tempOffer;
                } else if(mun == 0) {
                    currentNode = currentNode.nextOffer;
                } else {
                    Console.WriteLine("Error on Offers scan: Match check on offer returns invalid Enum");
                    return null;
                }
            }
            // if HeadNode.next == null. it wont (while) loop
            return null;

        } else {
            Console.WriteLine("Error on Offers scan: Match check on offer returns invalid Enum");
            return null;
        }
    }



    // constructor
    public OrderBook() {
        HeadNode = null;
    }
}