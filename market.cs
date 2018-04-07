using System;
class Market {
    OrderBook[] Orderbooks;

    // NOTE: resourceID is used to place the offer on the respective orderbook

    public void placeOffer(Person Seller, int resourceID, int Ammount, decimal Price) {
        Offer newOffer = new Offer(Seller, Ammount, Price);
        Orderbooks[resourceID].addOffer(newOffer);
    }


    // TODO return enum. completed/partially completed/not completed
    public int searchOffer(Person buyer, int resourceID, int buyAmmount, int maxPrice) {
        OrderBook book = Orderbooks[resourceID];

        while(buyAmmount > 0 && book.HeadNode.Price <= maxPrice && book.HeadNode != null) {
            int offerAmmount = book.HeadNode.AmmountResource;
            int transAmmount;
            Person seller = book.HeadNode.Seller;

            if(offerAmmount < buyAmmount) {
                transAmmount = offerAmmount;
                buyAmmount -= offerAmmount;
                book.deleteLowestOffer();
            } else {
                transAmmount = buyAmmount;
                buyAmmount = 0;
                book.HeadNode.consumeOffer(buyAmmount);
            }

            decimal totalCost = book.HeadNode.Price*transAmmount;
            buyer.Transaction((-1)*totalCost, resourceID, transAmmount);
            seller.Transaction(totalCost, resourceID, transAmmount);
        }
        if(buyAmmount == 0) {
            // completed
            return 0;
        } else if(book.HeadNode.Price > maxPrice) {
            // partially completed
            return 1;
        } else if(book.HeadNode == null) {
            // ran out of book
            return 2;
        } else {
            throw new NotSupportedException();
        }
    }



    // constructor
    public Market(int resourceAmmount) {
        Orderbooks = new OrderBook[resourceAmmount];
        for(int i = 0; i < resourceAmmount; i++) {
            Orderbooks[i] = new OrderBook();
        }
    }
}