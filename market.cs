using System;
using System.Collections.Generic;
class Market {
    OrderBook Orderbook;
    public List<TimeSpan> Logs { get; private set; }
    public TimeSpan CurrentLog { get; private set; } 

    // NOTE: resourceID is used to place the offer on the respective orderbook

    public void placeOffer(Person Seller, int Ammount, decimal Price) {
        Offer newOffer = new Offer(Seller, Ammount, Price);
        Orderbook.addOffer(newOffer);
    }


    // TODO return enum. completed/partially completed/not completed
    public int searchOffer(Person buyer, int resourceID, int buyAmmount, int maxPrice) {
        decimal price = Orderbook.HeadNode.Price;

        while(buyAmmount > 0 && price <= maxPrice && Orderbook.HeadNode != null) {
            int offerAmmount = Orderbook.HeadNode.AmmountResource;
            int transAmmount;
            Person seller = Orderbook.HeadNode.Seller;

            if(offerAmmount < buyAmmount) {
                transAmmount = offerAmmount;
                buyAmmount -= offerAmmount;
                Orderbook.deleteLowestOffer();
            } else {
                transAmmount = buyAmmount;
                buyAmmount = 0;
                Orderbook.HeadNode.consumeOffer(buyAmmount);
            }

            decimal totalCost = price*transAmmount;
            buyer.Transaction((-1)*totalCost, resourceID, transAmmount);
            seller.Transaction(totalCost, resourceID, transAmmount);

            CurrentLog.LogTransaction(price, transAmmount);
        }
        if(buyAmmount == 0) {
            // completed
            return 0;
        } else if(price > maxPrice) {
            // partially completed
            return 1;
        } else if(Orderbook.HeadNode == null) {
            // ran out of book
            return 2;
        } else {
            throw new NotSupportedException();
        }
    }

    // place all current logs in their respective logs
    public void EndTimeSpan() {
        Logs.Add(CurrentLog);
        CurrentLog = null;
    }
    public void StartTimeSpan() {
        CurrentLog = new TimeSpan();
    }

    // constructor
    public Market(int resourceAmmount) {
        // initialize orderbook
        Orderbook = new OrderBook();
        // initialize log
        Logs = new List<TimeSpan>();

    }
}