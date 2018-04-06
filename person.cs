  
class Person {

    // profession
    // main product location in inventory
    public int Role { get; private set; }

    // Inventory
    public int[] Inventory { get; private set; }

    // Currency
    public decimal Cash { get; private set; }

    // set inventory
    public void setInventory(int[] _inventory) {
        Inventory = _inventory;
    }

    public void Transaction(decimal cash, int resourceID, int resourceAmmount) {
        Inventory[resourceID] += resourceAmmount;
        Cash += cash;
    }
    // methods

    // main method
    public void turn() {

    }

    // override by inherited classes
    protected virtual void work() {

    }
    
    void considerSell(int resourceID, int ammount, int price) {
        // check inventory and role. think and then sell
    }

    void considerBuy(int resourceID, int ammount, int price) {

    }


    // constructor
    protected Person(Profession prof) {
        Role = (int)prof;
        //Inventory = new decimal[]
    }
}

struct Adress {
    int ID;
    int Role;
}