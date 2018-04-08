using System;
using System.Collections.Generic;
class Market {
    OrderBook[] Orderbooks;
    public List<TimeSpan>[] Logs { get; private set; }
    public TimeSpan[] CurrentLogs { get; private set; } 

    // NOTE: resourceID is used to place the offer on the respective orderbook

    public void placeOffer(Person Seller, int resourceID, int Ammount, decimal Price) {
        Offer newOffer = new Offer(Seller, Ammount, Price);
        Orderbooks[resourceID].addOffer(newOffer);
    }


    // TODO return enum. completed/partially completed/not completed
    public int searchOffer(Person buyer, int resourceID, int buyAmmount, int maxPrice) {
        OrderBook book = Orderbooks[resourceID];
        decimal price = book.HeadNode.Price;

        while(buyAmmount > 0 && price <= maxPrice && book.HeadNode != null) {
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

            decimal totalCost = price*transAmmount;
            buyer.Transaction((-1)*totalCost, resourceID, transAmmount);
            seller.Transaction(totalCost, resourceID, transAmmount);

            CurrentLogs[resourceID].LogTransaction(price, transAmmount);
        }
        if(buyAmmount == 0) {
            // completed
            return 0;
        } else if(price > maxPrice) {
            // partially completed
            return 1;
        } else if(book.HeadNode == null) {
            // ran out of book
            return 2;
        } else {
            throw new NotSupportedException();
        }
    }

    // place all current logs in their respective logs
    public void EndTimeSpan() {
        for(int i = 0; i < CurrentLogs.Length; i++) {
            Logs[i].Add(CurrentLogs[i]);
            CurrentLogs[i] = null;
        }
    }
    public void StartTimeSpan() {
        for(int i = 0; i < CurrentLogs.Length; i++) {
            CurrentLogs[i] = new TimeSpan();
        }
    }

    // constructor
    public Market(int resourceAmmount) {
        Orderbooks = new OrderBook[resourceAmmount];
        for(int i = 0; i < resourceAmmount; i++) {
            Orderbooks[i] = new OrderBook();
        }
        // initialize array of currents
        CurrentLogs = new TimeSpan[resourceAmmount];
        // initialize array of lists 
        Logs = new List<TimeSpan>[resourceAmmount];
        // initialize lists
        for(int i = 0; i < resourceAmmount; i++) {
            Logs[i] = new List<TimeSpan>();
        }
    }
}