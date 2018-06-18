using System;
using System.Linq;
using System.Collections.Generic;

// In Real Time Market
// offers affect the market state immediately
class RTMarket {

    OrderBook Orderbook;
    public List<TimeSpan> Logs { get; private set; }
    public TimeSpan CurrentLog { get; private set; } 

    // NOTE: resourceID is used to place the offer on the respective orderbook

    public void placeOffer(Person Seller, int Ammount, decimal Price) {
        Offer newOffer = new Offer(Seller, Ammount, Price);
        Orderbook.addOffer(newOffer);
    }


    // TODO return enum. completed/partially completed/not completed
    public int searchOffer(Person buyer, int resourceID, int buyAmmount, decimal maxPrice) {
        if(Orderbook.HeadNode == null) {
            return 2;
        }
        decimal price = Orderbook.HeadNode.Price;

        while(buyAmmount > 0 && Orderbook.HeadNode != null && price <= maxPrice ) {
            // delete all lowest offer with dead seller
            Person seller = Orderbook.HeadNode.Seller;
            if(seller == null) {
                Orderbook.deleteLowestOffer();
                continue;
            }

            int offerAmmount = Orderbook.HeadNode.AmmountResource;
            int transAmmount;

            if(offerAmmount <= buyAmmount) {
                transAmmount = offerAmmount;
                buyAmmount -= offerAmmount;
                Orderbook.deleteLowestOffer();
            } else {
                transAmmount = buyAmmount;
                buyAmmount = 0;
                Orderbook.HeadNode.consumeOffer(transAmmount);
            }

            bool continueSearch = closeDeal(buyer, seller, price, transAmmount, resourceID);
            // if(!continueSearch) {
            //     return 1;
            // }
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

    bool closeDeal(Person buyer, Person seller, decimal price, int ammount, int resourceID) {
        decimal totalCost = price*ammount;
        if(buyer.Cash >= totalCost && seller.Inventory[resourceID] >= ammount) {
            buyer.Transaction((-1)*totalCost, resourceID, ammount);
            seller.Transaction(totalCost, resourceID, (-1)*ammount);

            CurrentLog.LogTransaction(price, ammount, totalCost);
            return true;
        } else {
            return false;
        }
    }

    public decimal getLastPrice() {
        decimal num = CurrentLog.High;
        
        // check logs from last until Close != 0
        int i = Logs.Count;
        while(num == 0) {
            num = Logs[i - 1].High;
            i--;
        }
        return num;
    }

    public decimal getLastPrice(out int how) {
        decimal num = CurrentLog.High;
        how = 0;
        // check logs from last until Close != 0
        int i = Logs.Count;
        while(num == 0) {
            num = Logs[i - 1].High;
            i--;
            how++;
        }
        return num;
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
    public RTMarket(int resourceAmmount) {
        // initialize orderbook
        Orderbook = new OrderBook();
        // initialize log
        Logs = new List<TimeSpan>();

    }
}