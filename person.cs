
class Person {

    // profession
    // main product location in inventory
    public int Role { get; private set; }

    // inventory
    // 0 for gold
    public decimal[] Inventory { get; private set; }

    // methods

    // main method
    public void turn() {

    }

    // override by inherited classes
    protected virtual void work() {

    }
    
    // place bid
    void placeBid() {
        // check inventory and role. think and then sell
    }

    // constructor
    protected Person(Profession prof) {
        Role = (int)prof;
        //Inventory = new decimal[]
    }
}