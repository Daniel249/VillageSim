using System;
class Offer {
    // weakreference to Seller. cannot be set
    private WeakReference<Person> _seller;
    public Person Seller {
        get {
            Person seller;
            // maybe do something if false
            _seller.TryGetTarget(out seller);
            return seller;
        }
        set {
            throw new NotSupportedException();
        }
    }
    public int AmmountResource { get; private set; }
    public decimal Price { get; private set; }


    // reference to next node
    public Offer nextOffer;




    // consume available ResourceAmmount
    public void consumeOffer(int consumed) {
        AmmountResource -= consumed;
    }


    // constructor
    public Offer(Person seller, int _ammountResource, decimal _price) {
        _seller = new WeakReference<Person>(seller);
        AmmountResource= _ammountResource;
        Price = _price;
    }
}